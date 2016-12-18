using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace ydtf.isoa
{
    /// <summary>
    /// 服务访问句柄
    /// </summary>
    public class t_isoac
    {
        /// <summary>
        /// NetworkStream数据交换句柄
        /// </summary>
        public NetworkStream net_stream { get; internal set; }

        /// <summary>
        /// TcpClient连接句柄
        /// </summary>
        public TcpClient tcp_client { get; internal set; }
    }
}
