using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace ISOA
{
    internal class ISOACAdapter
    {
        private IISOACProvider _Provider=null;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="provider"></param>
        public ISOACAdapter(IISOACProvider provider)
        {
            _Provider=provider;
        }

        /// <summary>
        /// 调用自定义实现
        /// </summary>
        /// <param name="urls"></param>
        /// <param name="serviceName"></param>
        /// <param name="requestData"></param>
        /// <returns></returns>
        public byte[] Call(string urls, string serviceName, byte[] requestData)
        {
            HandleProviderBase handleProvider = null;
            try
            {
                handleProvider = _Provider.Init(urls, _Provider.ConnectionTypeFlag, _Provider.ExtraDataInfo);
                return _Provider.Call(handleProvider, serviceName, requestData);
            }
            catch (Exception e)
            {
                throw new ISOAException("服务调用失败", ISOAExceptionLevel.FATAL,e);
            }
            finally
            {
                _Provider.Destroy(handleProvider);
            }
        }

    }
}
