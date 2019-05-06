
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QingHaiGeo{
    /// <summary>
    /// 包含问题回答列表的请求结果
    /// </summary>
    public class AnswerResult : BaseResult {

        /// <summary>
        /// 包含问题回答列表的请求结果
        /// </summary>
        public AnswerResult() {
        }

        /// <summary>
        /// 请求的问题列表
        /// </summary>
        public Answer[] answers;

    }
}