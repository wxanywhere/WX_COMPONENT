using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ISOA
{
    /// <summary>
    /// Define the req_pack structure
    /// </summary>
    public struct RequestPack
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string UserName;

        /// <summary>
        /// 服务名
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        internal string ServiceName;

        /// <summary>
        /// 请求长度
        /// </summary>
        internal int RequestSize;

        /// <summary>
        /// 请求数据
        /// </summary>
        internal byte[] RequestData;
    }
}
