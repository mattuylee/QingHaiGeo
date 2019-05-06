
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QingHaiGeo{
    /// <summary>
    /// 包含随手拍列表的请求结果
    /// </summary>
    public class MomentResult : BaseResult {

        /// <summary>
        /// 包含随手拍列表的请求结果
        /// </summary>
        public MomentResult() {
        }

        /// <summary>
        /// 请求的随手拍列表
        /// </summary>
        public Moment[] monments;

    }
}