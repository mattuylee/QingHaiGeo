
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QingHaiGeo{
    /// <summary>
    /// 用户收藏主体
    /// </summary>
    public class Favorite {

        /// <summary>
        /// 用户收藏主体
        /// </summary>
        public Favorite() {
        }

        /// <summary>
        /// 相关收藏项目的编号
        /// </summary>
        public string code;

        /// <summary>
        /// 收藏类型
        /// </summary>
        public TargetType type;

        /// <summary>
        /// 收藏项标题
        /// </summary>
        public string title;

        /// <summary>
        /// 收藏项的简介
        /// </summary>
        public string description;

        /// <summary>
        /// 收藏时间
        /// </summary>
        public DateTime time;



    }
}