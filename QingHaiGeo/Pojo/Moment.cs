
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QingHaiGeo{
    /// <summary>
    /// 用户发布的动态，可包含文字图片
    /// </summary>
    public class Moment {

        /// <summary>
        /// 用户发布的动态，可包含文字图片
        /// </summary>
        public Moment() {
        }

        /// <summary>
        /// 数据库文档ID。客户端请求时字段名为code，类型为字符串
        /// </summary>
        public ObjectId _id;

        /// <summary>
        /// 发布的文字
        /// </summary>
        public string text;

        /// <summary>
        /// 发布的图片ID，数组
        /// </summary>
        public string[] pictures;

        /// <summary>
        /// 发布者的用户ID
        /// </summary>
        public string userId;

        /// <summary>
        /// 发布者
        /// </summary>
        public BaseUser user;

        /// <summary>
        /// 点赞（喜欢）总数
        /// </summary>
        public int likeCount;

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime time;

    }
}