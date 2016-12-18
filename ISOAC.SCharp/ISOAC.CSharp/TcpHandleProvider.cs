using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace ISOA
{
    /// <summary>
    /// TCP协议下句柄提供器
    /// </summary>
    public class TcpHandleProvider : HandleProviderBase
    {
        /// <summary>
        /// NetworkStream
        /// </summary>
        public NetworkStream NetworkStream { get; set; }

        /// <summary>
        /// TcpClient
        /// </summary>
        public TcpClient TcpClient { get; set; }
    }
}
