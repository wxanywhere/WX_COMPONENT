using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;

namespace ISOA
{
    /// <summary>
    /// ISOACTcpMultiPointProxyProvider
    /// </summary>
    public class ISOACTcpMultiPointProxyProvider : ISOACBase, IISOACProvider
    {

        /// <summary>
        /// HeadPack
        /// </summary>
        public HeadPack? HeadPack { get; set; }

        /// <summary>
        /// RequestPack
        /// </summary>
        public RequestPack? RequestPack { get; set; }

        /// <summary>
        /// ConnectionTypeFlag
        /// </summary>
        public int ConnectionTypeFlag { get; set; }

        /// <summary>
        /// ExtraDataInfo
        /// </summary>
        public ExtraDataInfo ExtraDataInfo { get; set; }

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="urls"></param>
        /// <param name="flag"></param>
        /// <param name="dataInfo"></param>
        /// <returns></returns>
        public HandleProviderBase Init(string urls, int flag, ExtraDataInfo dataInfo)
        {

            throw new NotImplementedException();
        }

        /// <summary>
        /// Call
        /// </summary>
        /// <param name="handleProvider"></param>
        /// <param name="serviceName"></param>
        /// <param name="requestData"></param>
        /// <returns></returns>
        public byte[] Call(HandleProviderBase handleProvider, string serviceName, byte[] requestData)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Destroy
        /// </summary>
        /// <param name="handleProvider"></param>
        public void Destroy(HandleProviderBase handleProvider)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// GetError
        /// </summary>
        /// <param name="failedCode"></param>
        /// <returns></returns>
        public string GetError(int failedCode)
        {
            throw new NotImplementedException();
        }
    }
}
