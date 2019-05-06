
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QingHaiGeo{
    /// <summary>
    /// 定义基本用户信息
    /// </summary>
    public abstract class BaseUser {

        /// <summary>
        /// 定义基本用户信息
        /// </summary>
        public BaseUser() {
        }

        /// <summary>
        /// 用户名，可用于登录。长度应做限制，约2到12个字符
        /// </summary>
        public string userName;

        /// <summary>
        /// 昵称
        /// </summary>
        public string nickName;

    }
}