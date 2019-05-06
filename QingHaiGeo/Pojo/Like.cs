
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace QingHaiGeo
{

    /// <summary>
    /// 用户点赞主体
    /// </summary>
    public class Like
    {
        /// <summary>
        /// 被点赞对象的编号
        /// </summary>
        public string code;

        /// <summary>
        /// 被点赞对象的类型
        /// </summary>
        public TargetType type;

        /// <summary>
        /// 被点赞对象的标题
        /// </summary>
        public string title;

        /// <summary>
        /// 被点赞对象的简介
        /// </summary>
        public string description;

        /// <summary>
        /// 点赞时间
        /// </summary>
        public DateTime time;
    }
}