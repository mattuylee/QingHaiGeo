
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QingHaiGeo{
    /// <summary>
    /// 用户收藏列表
    /// </summary>
    public class UserFavorites {

        /// <summary>
        /// 用户收藏列表
        /// </summary>
        public UserFavorites() {
        }

        public ObjectId _id;

        /// <summary>
        /// 用户ID（非用户名）
        /// </summary>
        public string userId;

        /// <summary>
        /// 收藏列表
        /// </summary>
        public Favorite[] favorites;


    }
}