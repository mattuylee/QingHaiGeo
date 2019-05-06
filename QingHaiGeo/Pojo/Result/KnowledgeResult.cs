
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QingHaiGeo{
    /// <summary>
    /// 包含地质科普列表的请求结果
    /// </summary>
    public class KnowledgeResult : BaseResult {

        /// <summary>
        /// 包含地质科普列表的请求结果
        /// </summary>
        public KnowledgeResult() {
        }

        /// <summary>
        /// 请求的地质科普列表
        /// </summary>
        public Knowledge[] knowledges;

    }
}