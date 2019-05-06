
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QingHaiGeo{
    public class Message {

        public Message() {
        }

        /// <summary>
        /// 消息ID
        /// </summary>
        public ObjectId _id;

        /// <summary>
        /// 消息内容。由于涉及到昵称等易变数据，应在用户请求数据时生成(?)
        /// </summary>
        public string title;

        /// <summary>
        /// 对象ID（引发消息的对象）
        /// </summary>
        public string targetCode;

        /// <summary>
        /// 对象类型（即引发消息的对象的类型）
        /// </summary>
        public TargetType targetType;

        /// <summary>
        /// 消息类型
        /// </summary>
        public MessageType messageType;

        /// <summary>
        /// 用户ID
        /// </summary>
        public string userId;

        /// <summary>
        /// 消息触发者（如点赞者等）
        /// 
        /// 在数据库中存储为用户ID，提供给用户时为用户名(?)
        /// </summary>
        public string sender;

        /// <summary>
        /// 是否为未读消息
        /// </summary>
        public bool unread;

        /// <summary>
        /// 消息产生时间
        /// </summary>
        public DateTime time;



    }
}