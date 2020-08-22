using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingHaiGeo {
    /// <summary>
    /// 地质遗迹
    /// </summary>
    public class CultureVillage: IRelicMediaResource
    {
        /// <summary>
        /// MongoDB ID
        /// </summary>
        public ObjectId _id { get; set; }

        /// <summary>
        /// 文化村编号
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 文化村名称
        /// </summary>
        public string name;
        /// <summary>
        /// 文化村位置
        /// </summary>
        public Location location;
        /// <summary>
        /// 关于文化村的介绍
        /// </summary>
        public string description;
        /// <summary>
        /// 背景音乐
        /// </summary>
        public string music { get; set; }
        /// <summary>
        /// 关于地质科普的图片ID
        /// </summary>
        public string[] pictures { get; set; }

        /// <summary>
        /// 地质科普视频信息
        /// </summary>
        public VideoInfo[] videos { get; set; }

        /// <summary>
        /// 点赞总数
        /// </summary>
        public int likeCount;

        /// <summary>
        /// 是否被冻结
        /// </summary>
        public bool isFreezed;

        /// <summary>
        /// 数据录入者ID，仅提供给管理员
        /// </summary>
        public string recorder;

        /// <summary>
        /// 录入时间，仅提供给管理员
        /// </summary>
        public DateTime recordTime;


    }
}
