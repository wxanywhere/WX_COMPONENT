using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ISOA
{
    /// <summary>
    /// Define the req_rea_pack structure
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RequestReactPack
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string ServiceName;

        /// <summary>
        /// 必须完整复制StubPack.TunnelName
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string TunnelName;

        /// <summary>
        /// 必须完整复制StubPack.Remsgid
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string Remsgid;
    }
}
