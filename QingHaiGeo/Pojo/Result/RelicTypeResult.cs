
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QingHaiGeo{
    /// <summary>
    /// 请求遗迹类型的返回结构
    /// </summary>
    public class RelicTypeResult : BaseResult {

        /// <summary>
        /// 请求遗迹类型的返回结构
        /// </summary>
        public RelicTypeResult() {
        }

        /// <summary>
        /// 请求的遗迹类型
        /// </summary>
        public RelicType relicType;

    }
}