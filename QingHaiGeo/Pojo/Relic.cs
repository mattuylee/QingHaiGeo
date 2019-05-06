
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QingHaiGeo
{
    /// <summary>
    /// 地质遗迹
    /// </summary>
    public class Relic : IRelicMediaResource {

        /// <summary>
        /// 地质遗迹
        /// </summary>
        public Relic() {
        }

        /// <summary>
        /// MongoDB ID
        /// </summary>
        public ObjectId _id { get; set; }

        /// <summary>
        /// 遗迹编号
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 遗迹名称
        /// </summary>
        public string name;

        /// <summary>
        /// 遗迹位置
        /// </summary>
        public Location location;

        /// <summary>
        /// 关于遗迹的介绍
        /// </summary>
        public string description;

        /// <summary>
        /// 遗迹类型代码，返回给前端时忽略
        /// </summary>
        public string relicTypeCode;

        /// <summary>
        /// 遗迹类型，数据库中总是为null或不存在于数据库，请求遗迹时根据类型代码再从数据库中获取
        /// </summary>
        public RelicType relicType;

        /// <summary>
        /// 遗迹图片URL
        /// </summary>
        public string[] pictures { get; set; }

        /// <summary>
        /// 介绍音频URL
        /// </summary>
        public string music { get; set; }

        /// <summary>
        /// 遗迹视频信息
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