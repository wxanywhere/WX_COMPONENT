using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ISOA
{
    /// <summary>
    /// Define the rea_pack structure 
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ReactPack
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string ServiceName;

        /// <summary>
        /// 服务耗时，以微妙记
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
        public string ServiceCost;

        /// <summary>
        /// 响应数据长度
        /// </summary>
        public int ReactSize;

        /// <summary>
        /// 响应数据
        /// </summary>
        public byte[] ReactData;
    }
}
