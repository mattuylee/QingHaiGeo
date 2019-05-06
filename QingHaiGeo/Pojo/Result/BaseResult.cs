
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QingHaiGeo{
    /// <summary>
    /// 返回结果的最基本结构，无数据反馈的请求（如删除、创建）的返回结果，同时作为其他结果的父类
    /// </summary>
    public class BaseResult {

        /// <summary>
        /// 返回结果的最基本结构，无数据反馈的请求（如删除、创建）的返回结果，同时作为其他结果的父类
        /// </summary>
        public BaseResult() {
        }

        /// <summary>
        /// 请求错误文本，成功为空
        /// </summary>
        public string error;

    }
}