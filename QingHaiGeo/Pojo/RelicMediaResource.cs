using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingHaiGeo
{
    // 遗迹和地质科普的媒体资源项，方便统一管理媒体资源
    public interface IRelicMediaResource
    {
        ObjectId _id { get; set; }
        string code { get; set; }
        string music { get; set; }
        string[] pictures { get; set; }
        VideoInfo[] videos { get; set; }
    }
}
