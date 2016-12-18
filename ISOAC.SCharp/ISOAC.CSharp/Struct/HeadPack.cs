using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ISOA
{
    /// <summary>
    /// Define the ISOA_HEAD structure
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct HeadPack
    {
        /// <summary>
        /// 标志位:4字节
        /// 
        /// </summary>
        public int Flag;

        /// <summary>
        /// 数据长度:4字节[数据加密或者压缩之后的实际长度]
        /// 
        /// </summary>
        internal int Size;

        /// <summary>
        /// 数据原始长度：4字节
        /// 
        /// </summary>
        internal int OSize;

        /// <summary>
        /// 数据校检：130字节
        /// 
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 130)]
        public string Mac;
    }
}
