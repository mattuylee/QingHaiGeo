
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QingHaiGeo {
    /// <summary>
    /// 包含消息列表的请求结果
    /// </summary>
    public class MessageResult : BaseResult {

        /// <summary>
        /// 包含消息列表的请求结果
        /// </summary>
        public MessageResult() {
        }

        /// <summary>
        /// 消息列表
        /// </summary>
        public Message[] messages;

    }
}