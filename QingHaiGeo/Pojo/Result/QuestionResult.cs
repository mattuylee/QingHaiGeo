
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QingHaiGeo{
    /// <summary>
    /// 包含问题列表的请求结果
    /// </summary>
    public class QuestionResult : BaseResult {

        /// <summary>
        /// 包含问题列表的请求结果
        /// </summary>
        public QuestionResult() {
        }

        /// <summary>
        /// 请求的问题列表
        /// </summary>
        public Question[] questions;

    }
}