
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QingHaiGeo{
    /// <summary>
    /// 用户信息，注册用户或修改用户信息时的请求体（弃用）
    /// </summary>
    public class UserInfo : BaseUser {

        /// <summary>
        /// 用户信息，注册用户或修改用户信息时的请求体（弃用）
        /// </summary>
        public UserInfo() {
        }

        /// <summary>
        /// 密码
        /// </summary>
        public string password;

    }
}