
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QingHaiGeo{
    /// <summary>
    /// 包含用户点赞的请求结果
    /// </summary>
    public class LikeResult : BaseResult {

        /// <summary>
        /// 包含用户点赞的请求结果
        /// </summary>
        public LikeResult() {
        }

        /// <summary>
        /// 用户点赞列表
        /// </summary>
        public Like[] likes;

    }
}