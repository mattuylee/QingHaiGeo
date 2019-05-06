
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QingHaiGeo{
    /// <summary>
    /// 问答-问题
    /// </summary>
    public class Question {

        /// <summary>
        /// 问答-问题
        /// </summary>
        public Question() {
        }

        /// <summary>
        /// 数据库文档ID。客户端请求时字段名为code，类型为字符串
        /// </summary>
        public ObjectId _id;

        /// <summary>
        /// 问题内容
        /// </summary>
        public string question;

        /// <summary>
        /// 提问者ID
        /// </summary>
        public string userId;

        /// <summary>
        /// 提问者
        /// </summary>
        public BaseUser user;

        /// <summary>
        /// 问题的回答总数
        /// </summary>
        public int answerCount;

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