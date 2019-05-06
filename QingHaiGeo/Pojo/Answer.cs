
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QingHaiGeo{
    /// <summary>
    /// 问答-回答
    /// </summary>
    public class Answer {

        /// <summary>
        /// 问答-回答
        /// </summary>
        public Answer() {
        }

        /// <summary>
        /// 数据库文档ID。客户端请求时字段名为code，类型为字符串
        /// </summary>
        public ObjectId _id;

        /// <summary>
        /// 问题ID
        /// </summary>
        public string questionId;

        /// <summary>
        /// 回答
        /// </summary>
        public string answer;

        /// <summary>
        /// 回答者ID
        /// </summary>
        public string userId;

        /// <summary>
        /// 回答者
        /// </summary>
        public BaseUser user;

        /// <summary>
        /// 点赞总数
        /// </summary>
        public int likeCount;

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime time;


    }
}