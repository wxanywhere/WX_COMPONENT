using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ydtf.isoa
{
    /// <summary>
    /// Define the req_pack structure
    /// </summary>
    internal struct req_pack
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        internal string user;

        /// <summary>
        /// 服务名
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        internal string service;

        /// <summary>
        /// 请求数据长度
        /// </summary>
        internal int req_size;

        /// <summary>
        /// 请求数据
        /// </summary>
        internal byte[] req_data;
    }
}
