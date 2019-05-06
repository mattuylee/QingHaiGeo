
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QingHaiGeo
{

    /// <summary>
    /// 用户点赞列表
    /// </summary>
    public class UserLikes
    {

        /// <summary>
        /// 用户点赞列表
        /// </summary>
        public UserLikes()
        {
        }

        /// <summary>
        /// 数据库文档ID
        /// </summary>
        public ObjectId _id;

        /// <summary>
        /// 用户ID（非用户名）
        /// </summary>
        public string userId;

        /// <summary>
        /// 点赞列表
        /// </summary>
        public Like[] likes;
    }
}