
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QingHaiGeo {
    /// <summary>
    /// 包含用户信息和登录信息的请求结果
    /// </summary>
    public class UserResult : BaseResult {

        /// <summary>
        /// 包含用户信息和登录信息的请求结果
        /// </summary>
        public UserResult() {
        }

        /// <summary>
        /// 用户信息
        /// </summary>
        public User user;

    }
}