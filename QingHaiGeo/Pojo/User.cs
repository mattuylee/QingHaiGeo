
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QingHaiGeo {
    /// <summary>
    /// 用户主体
    /// </summary>
    public class User : BaseUser {

        /// <summary>
        /// 用户主体
        /// </summary>
        public User() {
        }

        /// <summary>
        /// 用户ID。传回客户端时需要做转换处理。转换为id: string
        /// </summary>
        public string id;

        /// <summary>
        /// 密码
        /// </summary>
        public string password;

        /// <summary>
        /// 电话号码
        /// </summary>
        public string phone;

        /// <summary>
        /// 邮箱
        /// </summary>
        public string email;

        /// <summary>
        /// 地址
        /// </summary>
        public string adress;

        /// <summary>
        /// 头像URL
        /// </summary>
        public string avatar;

        /// <summary>
        /// 姓名
        /// </summary>
        public string realName;

        /// <summary>
        /// 身份证号
        /// </summary>
        public string idCardNumber;

        /// <summary>
        /// 微信ID
        /// </summary>
        public string wechat;

        /// <summary>
        /// 用户积分
        /// </summary>
        public int creditCount;

        /// <summary>
        /// 是否是专业用户
        /// </summary>
        public bool isProfessional;

        /// <summary>
        /// 是否管理员
        /// </summary>
        public bool isAdmin;
        //是否超级管理员
        public bool isSuperAdmin;

        /// <summary>
        /// 是否被冻结
        /// </summary>
        public bool isFreezed;
    }
}