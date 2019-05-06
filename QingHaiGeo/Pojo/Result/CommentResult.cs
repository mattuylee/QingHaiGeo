
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QingHaiGeo{
    /// <summary>
    /// 包含评论列表的请求结果
    /// </summary>
    public class CommentResult : BaseResult {

        /// <summary>
        /// 包含评论列表的请求结果
        /// </summary>
        public CommentResult() {
        }

        /// <summary>
        /// 请求的评论列表
        /// </summary>
        public Comment[] comments;

    }
}