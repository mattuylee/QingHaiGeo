using DotnetSpider.Downloader;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using MongoDB.Driver.GridFS;
using System.IO;
using MongoDB.Bson;

namespace QingHaiGeo
{
    public static class WebAPI
    {
        //HTTP Headers
        private static Dictionary<string, string> headers;
        //遗迹类型缓存，避免重复请求远程资源
        private static RelicType[] relicTypesCache;

        static WebAPI()
        {
            downloader = new HttpClientDownloader();
        }
        /// <summary>
        /// 检查网络连接是否可用
        /// </summary>
        /// <returns></returns>
        public static bool IsNetworkAvailable()
        {
            return InternetGetConnectedState(0, 0);
        }
        /// <summary>
        /// 返回数据库是否已连接。若要保证连接可用，请调用<seealso cref="TestDatabase"/>。
        /// </summary>
        public static bool IsDatabaseConnected()
        {
            return isDatabaseConnected;
        }
        /// <summary>
        /// 测试登录服务器是否可访问
        /// </summary>
        /// <returns></returns>
        public static bool TestServer()
        {
            Request request = new Request()
            {
                Method = HttpMethod.Get,
                Url = Config.Server + ":" + Config.Port + "/admin/test"
            };
            try
            {
                Response response = downloader.Download(request);
                return response.StatusCode == HttpStatusCode.OK;
            }
            catch
            {
                return false;
            }
        }

        public static bool TestDatabase()
        {
            if (mongoClient == null)
                ConnectDatabase();
            try
            {
                mongoDatabase.ListCollectionNames();
                return true;
            }
            catch { return false; }
        }
        /// <summary>
        /// 连接到数据库服务器
        /// </summary>
        /// <returns></returns>
        public static bool ConnectDatabase()
        {
            isDatabaseConnected = true;
            string connStr = "mongodb://";
            if (Config.DbUser != string.Empty)
                connStr += Config.DbUser + ":" + Config.DbPassword + "@";
            connStr += Config.DbServer + ":" + Config.DbPort + "/" + DATABASE_NAME + "?connectTimeoutMS=20000";
            try
            {
                mongoClient = new MongoClient(connStr);
                mongoDatabase = mongoClient.GetDatabase(DATABASE_NAME);
                mongoDatabase.ListCollectionNames(); //测试连接是否可用
                gridfsBucket = new GridFSBucket(mongoDatabase);
                return isDatabaseConnected;
            }
            catch
            {
                mongoClient = null;
                return isDatabaseConnected = false;
            }
        }
        
        /// <summary>
        /// 获取遗迹类型列表
        /// </summary>
        /// <param name="useCache">启用缓存</param>
        /// <returns></returns>
        public static RelicType[] GetRelicTypes(bool useCache = true)
        {
            if (useCache && relicTypesCache != null)
                return relicTypesCache;

            Request request = new Request()
            {
                Method = HttpMethod.Get,
                Url = Config.Server + ":" + Config.Port + "/relicTypes?size=999999999"
            };
            try
            {
                Response response = downloader.Download(request);
                if (response.StatusCode != HttpStatusCode.OK)
                    return null;
                JObject jObj = (JObject)JsonConvert.DeserializeObject(response.Content.ToString());
                JToken jRelicTypes = jObj.GetValue("relicTypes");
                List<RelicType> relicTypes = new List<RelicType>(jRelicTypes.Count());
                foreach (var jRelicType in jRelicTypes.AsEnumerable())
                {
                    relicTypes.Add(new RelicType()
                    {
                        code = jRelicType["code"].ToString(),
                        category = jRelicType["category"].ToString()
                    });
                }
                return relicTypesCache = relicTypes.ToArray();
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 获取遗迹
        /// </summary>
        /// <param name="code">遗迹ID</param>
        /// <returns></returns>
        //public static Relic GetRelic(string code)
        //{
        //    try
        //    {
        //        var col = mongoDatabase.GetCollection<Relic>("relic");
        //        return col.Find(Builders<Relic>.Filter.Eq("code", code)).First();
        //    }
        //    catch { return null; }

        //}
        /// <summary>
        /// 将新的视频信息添加到对象
        /// </summary>
        /// <param name="code">遗迹或者地质科普ID</param>
        /// <param name="videos">要添加的视频信息</param>
        /// <returns>是否成功</returns>
        public static bool AddVideos<T>(string code, IEnumerable<VideoInfo> videos) where T: IRelicMediaResource
        {
            try
            {
                var col = mongoDatabase.GetCollection<T>(nameof(T).ToLower());
                var update = Builders<T>.Update.AddToSet("videos", videos);
                var res = col.UpdateOne(Builders<T>.Filter.Eq("code", code), update);
                return res.ModifiedCount > 0;
            }
            catch { return false; }
        }
        /// <summary>
        /// 入库普通文件，不分清晰度级别
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="filename"></param>
        /// <returns>文件的文档ID</returns>
        public static string StoreFile(Stream stream, string filename = null)
        {
            if (!isDatabaseConnected)
                return null;
            try
            {
                ObjectId objId = gridfsBucket.UploadFromStream(filename ?? "", stream);
                string id = objId.ToString();
                return id;
            }
            catch { return null; }
        }
        /// <summary>
        /// 将图片和视频等分级资源写入数据库
        /// </summary>
        /// <param name="stream">资源输入流</param>
        /// <param name="id">资源访问ID。文件名为<paramref name="id"/> + 清晰度。如果为null则取用资源的数据库文档ID</param>
        /// <param name="mode">资源的清晰度，用于合成文件名</param>
        /// <returns>
        /// 如果上传失败为null。
        /// 如果<paramref name="id"/>不为null，返回<paramref name="id"/>;
        /// 如果<paramref name="id"/>为null，返回上传文件的数据库文档ID。
        /// </returns>
        public static string StoreResource(Stream stream, string id, SharpnessMode mode)
        {
            try
            {
                // 文件名 = 资源访问ID + 清晰度
                ObjectId objId;
                if (id == null)
                {
                    objId = gridfsBucket.UploadFromStream("", stream);
                    gridfsBucket.Rename(objId, objId.ToString() + mode.ToString());
                    id = objId.ToString();
                }
                else
                    objId = gridfsBucket.UploadFromStream(id + mode.ToString(), stream);
                return id;
            }
            catch(Exception e) { return null; }
        }
        /// <summary>
        /// 将图片分三级入库
        /// </summary>
        /// <param name="picFile">图片文件</param>
        /// <param name="id">访问ID，如果未成功入库任何数据为null（即使部分数据未入库成功也有值）</param>
        /// <param name="noLevel">为true不分级</param>
        /// <returns>是否完全入库成功</returns>
        public static bool StorePicture(FileInfo picFile, out string id, bool noLevel = false)
        {
            id = Guid.NewGuid().ToString("N");
            Stream stream;
            //初级
            if (picFile.Length > 256 * 1024)
            {
                stream = Utility.EncodePicture(picFile.FullName, 10, 480, 0);
                if (stream == null)
                {
                    id = null;
                    return false;
                }
            }
            else
                stream = picFile.OpenRead();
            id = WebAPI.StoreResource(stream, noLevel ? null : id, SharpnessMode.low);
            if (id == null)
                return false;
            stream.Close();
            //中级
            if (picFile.Length > 1024 * 1024)
            {
                stream = Utility.EncodePicture(picFile.FullName, 60, 720, 0);
                if (stream == null)
                    return false;
            }
            else
                stream = picFile.OpenRead();
            if (WebAPI.StoreResource(stream, id, SharpnessMode.mid) == null)
                return false;
            stream.Close();
            //高级
            stream = picFile.OpenRead();
            if (WebAPI.StoreResource(stream, id, SharpnessMode.high) == null)
                return false;
            return true;
        }
        /// <summary>
        /// 将视频分三级入库
        /// </summary>
        /// <param name="videoFile">视频文件</param>
        /// <param name="videoInfo">视频信息。即使只有部分数据入库成功，资源ID也可能有值</param>
        /// <returns>视频文件是否完全入库成功</returns>
        public static bool StoreVideo(FileInfo videoFile, out VideoInfo videoInfo)
        {
            videoInfo = null;
            string tempFileName = Path.GetTempPath() + "\\qhgeo_" + Guid.NewGuid().ToString("N") + ".mp4";
            string id = null;
            try
            {
                if (!Utility.EncodeVideo(videoFile.FullName, tempFileName, SharpnessMode.low))
                    return false;
                Stream istream = new FileStream(tempFileName, FileMode.Open, FileAccess.Read);
                id = WebAPI.StoreResource(istream, Guid.NewGuid().ToString("N"), SharpnessMode.low);
                if (id != null)
                    videoInfo = new VideoInfo() { video = id };
                else
                    return false;
                istream.Close();
                //中级
                if (!Utility.EncodeVideo(videoFile.FullName, tempFileName, SharpnessMode.mid))
                    return false;
                istream = new FileStream(tempFileName, FileMode.Open, FileAccess.Read);
                if (WebAPI.StoreResource(istream, id, SharpnessMode.mid) == null)
                    return false;
                istream.Close();
                //高级
                if (!Utility.EncodeVideo(videoFile.FullName, tempFileName, SharpnessMode.high))
                    return false;
                istream = new FileStream(tempFileName, FileMode.Open, FileAccess.Read);
                if (WebAPI.StoreResource(istream, id, SharpnessMode.high) == null)
                    return false;
                istream.Close();
                //var f = File.OpenWrite(tempFileName);
                //f.Close();
                File.Delete(tempFileName);
            }
            catch
            {
                return false;
            }
            //视频缩略图（如果失败忽略）
            FileInfo posterFile = new FileInfo(
                videoFile.Directory.FullName + "\\" + Path.GetFileNameWithoutExtension(videoFile.Name) + "jpg");
            if (!posterFile.Exists)
            {
                posterFile = new FileInfo(
                    videoFile.Directory.FullName + "\\" + Path.GetFileNameWithoutExtension(videoFile.Name) + "png");
            }
            if (posterFile.Exists)
                StorePicture(posterFile, out videoInfo.poster, true);
            return true;
        }
        /// <summary>
        /// 入库一个遗迹
        /// </summary>
        /// <param name="relic">遗迹</param>
        /// <param name="success">是否成功</param>
        /// <returns>
        /// 如果遗迹code已经存在，返回数据库中的遗迹对象。
        /// 如果入库成功，返回<paramref name="relic"/>，它的_id字段将被赋值。
        /// 如果出错为null
        /// </returns>
        public static Relic StoreRelic(Relic relic, out bool success)
        {
            if (relic == null)
            {
                success = false;
                return null;
            }
            try
            {
                relic.recorder = Config.CurrentUser.id;
                relic.recordTime = DateTime.Now;
                var col = mongoDatabase.GetCollection<Relic>("relic");
                var findResult = col.Find(Builders<Relic>.Filter.Eq("code", relic.code));
                if (findResult.CountDocuments() != 0)
                {
                    success = false;
                    return findResult.First();
                }
                col.InsertOne(relic);
                success = true;
                return relic;
            }
            catch
            {
                success = false;
                return null;
            }
        }
        /// <summary>
        /// 入库一个地质科普
        /// </summary>
        /// <param name="knowledge">地质科普</param>
        /// <param name="success">是否成功</param>
        /// <returns>
        /// 如果地质科普code已经存在，返回数据库中的地质科普对象。
        /// 如果入库成功，返回<paramref name="knowledge"/>，它的_id字段将被赋值。
        /// 如果出错为null
        /// </returns>
        public static Knowledge StoreKnowledge(Knowledge knowledge, out bool success)
        {
            if (knowledge == null)
            {
                success = false;
                return null;
            }
            try
            {
                var col = mongoDatabase.GetCollection<Knowledge>("knowledge");
                var findResult = col.Find(Builders<Knowledge>.Filter.Eq("code", knowledge.code));
                if (findResult.CountDocuments() != 0)
                {
                    success = false;
                    return findResult.First();
                }
                col.InsertOne(knowledge);
                success = true;
                return knowledge;
            }
            catch
            {
                success = false;
                return null;
            }
        }
        /// <summary>
        /// 检查遗迹是否已存在于数据库
        /// </summary>
        /// <param name="code">遗迹ID</param>
        /// <returns></returns>
        public static bool IsRelicExist(string code)
        {
            try
            {
                var col = mongoDatabase.GetCollection<Relic>("relic");
                var findResult = col.Find(Builders<Relic>.Filter.Eq("code", code));
                return findResult.CountDocuments() != 0;
            }
            catch { return false; }
        }
        /// <summary>
        /// 检查地质科普是否已存在于数据库
        /// </summary>
        /// <param name="code">地质科普ID</param>
        /// <returns></returns>
        public static bool IsKnowledgeExist(string code)
        {
            try
            {
                var col = mongoDatabase.GetCollection<Knowledge>("knowledge");
                var findResult = col.Find(Builders<Knowledge>.Filter.Eq("code", code));
                return findResult.CountDocuments() != 0;
            }
            catch { return false; }
        }
        /// <summary>
        /// 从数据库删除文件（不区分清晰的资源）
        /// </summary>
        /// <param name="id">文件的文档ID</param>
        public static void DeleteFile(string id)
        {
            gridfsBucket.Delete(new ObjectId(id));
        }
        /// <summary>
        /// 从数据库删除资源
        /// </summary>
        /// <param name="id">资源访问ID</param>
        /// <param name="mode">清晰度</param>
        public static bool DeleteResource(String id, SharpnessMode mode)
        {
            try
            {
                var filter = Builders<GridFSFileInfo>.Filter.Eq("filename", id + mode.ToString());
                GridFSFileInfo file = gridfsBucket.Find(filter).First();
                gridfsBucket.Delete(file.Id);
                return true;
            }
            catch { return false; }
        }
        //从数据库清除指定遗迹或地质科普的数据
        public static void DeleteRelic(Relic relic) { DeleteObject<Relic>(relic); }
        public static void DeleteKnowledge(Knowledge knowledge) { DeleteObject<Knowledge>(knowledge); }
        public static void DeleteObject<T>(T obj) where T: IRelicMediaResource
        {
            if (obj == null) return;
            if (obj.music != null && obj.music != string.Empty)
                DeleteFile(obj.music);
            List<string> resources = new List<string>();
            if (obj.pictures != null)
                resources.AddRange(obj.pictures);
            if (obj.videos != null)
            {
                foreach (VideoInfo video in obj.videos)
                {
                    resources.Add(video.poster);
                    resources.Add(video.video);
                }
            }
            foreach (string id in resources)
            {
                DeleteResource(id, SharpnessMode.low);
                DeleteResource(id, SharpnessMode.mid);
                DeleteResource(id, SharpnessMode.high);
            }
            if (obj._id == null)
                return;
            try
            {
                var filter = Builders<Relic>.Filter.Eq("_id", obj._id);
                mongoDatabase.GetCollection<Relic>("relic").DeleteOne(filter);
            }
            catch { }
        }

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(int Description, int ReservedValue);

        const string DATABASE_NAME = "qhgeo";
        private static MongoClient mongoClient;
        private static GridFSBucket gridfsBucket;
        private static IMongoDatabase mongoDatabase;
        private static HttpClientDownloader downloader;
        private static bool isDatabaseConnected;
    }
}


