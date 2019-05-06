
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QingHaiGeo{
    /// <summary>
    /// 评论
    /// </summary>
    public class Comment {

        /// <summary>
        /// 评论
        /// </summary>
        public Comment() {
        }

        /// <summary>
        /// 评论ID
        /// </summary>
        public ObjectId _id;

        /// <summary>
        /// 评论者。注意，该对象仅包含评论者用户的基本信息（用户名、昵称）
        /// </summary>
        public BaseUser user;

        /// <summary>
        /// 评论者用户ID。
        /// 
        /// 请求添加评论时忽略，由服务器端设置
        /// </summary>
        public string userId;

        /// <summary>
        /// 评论时间
        /// </summary>
        public DateTime time;

        /// <summary>
        /// 评论内容
        /// </summary>
        public string content;

        /// <summary>
        /// 被评论对象的类型
        /// </summary>
        public TargetType targetType;

        /// <summary>
        /// 被评论的对象的ID
        /// </summary>
        public string targetCode;


    }
}