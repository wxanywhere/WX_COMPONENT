using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISOA
{
    /// <summary>
    /// IISOACProvider
    /// </summary>
    public interface IISOACProvider :IISOAC
    {
        /// <summary>
        /// 头包
        /// </summary>
        HeadPack? HeadPack { set; get; }

        /// <summary>
        /// 请求包
        /// </summary>
        RequestPack? RequestPack { set; get; }

        /// <summary>
        /// 连接类型(?)
        /// </summary>
        int ConnectionTypeFlag { set; get; }

        /// <summary>
        /// 其他数据信息
        /// </summary>
        ExtraDataInfo ExtraDataInfo { set; get; }
    }
}
