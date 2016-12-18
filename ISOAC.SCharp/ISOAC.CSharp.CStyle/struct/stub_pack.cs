using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ydtf.isoa
{
    /// <summary>
    /// Define the stub_pack structure
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct stub_pack
    {
        /// <summary>
        /// 服务名称  
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        internal string service;

        /// <summary>
        /// ip地址
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        internal string ip;

        /// <summary>
        /// 端口号 
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        internal string port;

        /// <summary>
        /// 服务通道名
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        internal string tunnel;

        /// <summary>
        /// 消息类型 
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        internal string remsgid;

        /// <summary>
        /// 平均耗时，以秒记
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        internal string average_cost;

        /// <summary>
        /// 服务最长等待时间
        /// </summary>
        internal int iosec;
    }
}
