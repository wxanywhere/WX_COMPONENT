using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ISOA
{
    /// <summary>
    /// Define the stub_pack structure
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct StubPack
    {
        /// <summary>
        /// 服务名称  
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string ServiceName;

        /// <summary>
        /// IP地址
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string IP;

        /// <summary>
        /// 端口号 
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string Port;

        /// <summary>
        /// 服务通道名
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string TunnelName;

        /// <summary>
        /// 消息类型 
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string Remsgid;

        /// <summary>
        /// 平均耗时，以秒记
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string AverageCost;

        /// <summary>
        /// 服务最长等待时间
        /// </summary>
        public int Iosec;
    }
}
