using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISOA
{
    /// <summary>
    /// ISOACBuiltInProvider
    /// </summary>
    public enum ISOACBuiltInProvider
    {
        /// <summary>
        /// 单点直连
        /// </summary>
        TcpSinglePointDirectProvider,

        /// <summary>
        /// 单点中介连接
        /// </summary>
        TcpSinglePointProxyProvider,
    }
}
