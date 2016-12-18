using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ydtf.isoa
{
    /// <summary>
    /// 服务访问条件
    /// </summary>
    public class t_isoac_attr
    {
        /// <summary>
        /// 连接标志
        /// </summary>
        public int falg { get; set; }

        /// <summary>
        /// mac算法 0为不计算 1-MD5 2.SHA0
        /// </summary>
        public string mac { get; set; }

        /// <summary>
        /// 为不压缩其他为压缩阀值，超过则要进行压缩
        /// </summary>
        public byte zip_size { get; set; } //?

        /// <summary>
        /// 通讯方式 bCOMM_AUTO 自动 bCOMM_SYNC 同步 bCOMM_ASYNC 异步
        /// </summary>
        public int comm_mode { get; set; }

        /// <summary>
        /// 连接超时时间，缺省20秒
        /// </summary>
        public int connect_timeout { get; set; }

        /// <summary>
        /// 尝试连接次数，缺省2次
        /// </summary>
        public int try_connect_times { get; set; }

        /// <summary>
        /// 接收超时时间, 缺省20秒，取到stub后加上iosec计算超时时间
        /// </summary>
        public int recv_timeout { get; set; }

        /// <summary>
        /// 发送超时时间，缺省20秒
        /// </summary>
        public int send_timeout { get; set; }

    }
}
