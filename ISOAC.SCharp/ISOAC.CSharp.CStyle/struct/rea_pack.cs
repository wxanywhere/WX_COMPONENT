using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ydtf.isoa
{
    /// <summary>
    /// Define the rea_pack structure 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct rea_pack
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        internal string service;

        /// <summary>
        /// 服务耗时，以微妙记
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        internal string service_cost;

        /// <summary>
        /// 响应数据长度
        /// </summary>
        internal int react_size;

        /// <summary>
        /// 响应数据
        /// </summary>
        internal byte[] react_data;
    }
}
