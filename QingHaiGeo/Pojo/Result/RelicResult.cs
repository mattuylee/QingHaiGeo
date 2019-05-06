
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QingHaiGeo{
    /// <summary>
    /// 请求遗迹时的返回结构
    /// </summary>
    public class RelicResult : BaseResult {

        /// <summary>
        /// 请求遗迹时的返回结构
        /// </summary>
        public RelicResult() {
        }

        /// <summary>
        /// 请求的遗迹列表
        /// </summary>
        public Relic[] relics;

    }
}