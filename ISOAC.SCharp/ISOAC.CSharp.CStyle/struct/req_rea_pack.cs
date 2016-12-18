using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ydtf.isoa
{
    /// <summary>
    /// Define the req_rea_pack structure
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct req_rea_pack
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        internal string service;

        /// <summary>
        /// 必须完整复制stub_pack.tunnel
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        internal string tunnel;

        /// <summary>
        /// 必须完整复制stub_pack.remsgid
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        internal string remsgid;
    }
}
