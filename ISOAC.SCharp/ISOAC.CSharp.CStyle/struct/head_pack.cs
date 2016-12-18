using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ydtf.isoa
{
    /// <summary>
    /// Define the ISOA_HEAD structure
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct head_pack
    {
        /// <summary>
        /// 标志位:4字节
        /// </summary>
        internal int flag;

        /// <summary>
        /// 数据长度:4字节[数据加密或者压缩之后的实际长度]
        /// </summary>
        internal int size;

        /// <summary>
        /// 数据原始长度：4字节
        /// </summary>
        internal int osize;

        /// <summary>
        /// 数据校检：130字节
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 130)]
        internal string mac;
    }
}
