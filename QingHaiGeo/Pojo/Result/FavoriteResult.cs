
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QingHaiGeo{
    /// <summary>
    /// 包含用户收藏的请求结果
    /// </summary>
    public class FavoriteResult : BaseResult {

        /// <summary>
        /// 包含用户收藏的请求结果
        /// </summary>
        public FavoriteResult() {
        }

        /// <summary>
        /// 用户收藏列表
        /// </summary>
        public Favorite[] favorites;

    }
}