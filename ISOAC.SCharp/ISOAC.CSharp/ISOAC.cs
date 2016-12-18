using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace ISOA
{
    /// <summary>
    /// ISOA客户端调用接口
    /// </summary>
    public class ISOAC:ISOACBase
    {
        private HeadPack? _headPack=new Nullable<HeadPack>();
        private RequestPack? _RequestPack=new Nullable<RequestPack>();

        /// <summary>
        /// 默认构造
        /// </summary>
        public ISOAC():base()
        {

        }

        /// <summary>
        /// 使用自定义头包
        /// </summary>
        /// <param name="headPack"></param>
        public ISOAC(HeadPack headPack)
        {
            _headPack=new Nullable<HeadPack>(headPack);
        }

        /// <summary>
        /// 使用自定义请求包
        /// </summary>
        /// <param name="requestPack"></param>
        public ISOAC(RequestPack requestPack)
        {
            _RequestPack=new Nullable<RequestPack>(requestPack);
        }

        /// <summary>
        /// 使用自定义头包及请求包
        /// </summary>
        /// <param name="headPack">投包</param>
        /// <param name="requestPack">请求包</param>
        public ISOAC(HeadPack headPack,RequestPack requestPack)
        {
             _headPack=new Nullable<HeadPack>(headPack);
            _RequestPack=new Nullable<RequestPack>(requestPack);
        }

        /// <summary>
        /// 验证基本参数
        /// </summary>
        /// <param name="urls"></param>
        /// <param name="serviceName"></param>
        /// <param name="requestData"></param>
        private void ValidateBasicParams(string urls, string serviceName, byte[] requestData)
        {
            ValidateUrl(urls);
            ValidateServiceName(serviceName);
            if (requestData == null || !(requestData.Length > 0))
            {
                throw new ISOAException(AttributeHelper.GetMemberText(FailedCode.EC_PARAM) + ":请求数据不能为空！", (int)FailedCode.EC_PARAM, ISOAExceptionLevel.ALARM);
            }
        }

        /// <summary>
        /// 默认使用TCP协议，单点代理方式访问ISOA服务
        /// </summary>
        /// <param name="urls">ISOA Url,格式：ISOA://ip:port,ISOA://doman:port</param>
        /// <param name="serviceName">服务名称</param>
        /// <param name="requestData">请求数据</param>
        /// <returns>响应数据</returns>
        public byte[] Call(string urls, string serviceName, byte[] requestData)
        {
            try
            {
                ValidateBasicParams(urls, serviceName, requestData);
                IISOACProvider provider;
                provider = new ISOACTcpSinglePointProxyProvider();
                if (_headPack.HasValue)
                {
                    provider.HeadPack = _headPack;
                }
                if(_RequestPack.HasValue)
                {
                    provider.RequestPack = _RequestPack;
                }
                var adapter = new ISOACAdapter(provider);

                return adapter.Call(urls, serviceName, requestData);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 使用内置Provider调用服务，由枚举条件选用具体的Provider
        /// </summary>
        /// <param name="urls">ISOA Url,格式：ISOA://ip:port,ISOA://doman:port</param>
        /// <param name="serviceName">服务名称</param>
        /// <param name="requestData">请求数据</param>
        /// <param name="builtInProvider">内置的ISOA访问提供器</param>
        /// <returns>响应数据</returns>
        public byte[] Call(string urls, string serviceName, byte[] requestData, ISOACBuiltInProvider builtInProvider)
        {
            try
            {
                ValidateBasicParams(urls, serviceName, requestData);
                IISOACProvider provider = null;
                ISOACAdapter adapter;
                switch (builtInProvider)
                {
                    case ISOACBuiltInProvider.TcpSinglePointProxyProvider:
                        provider = new ISOACTcpSinglePointProxyProvider();
                        if (_headPack.HasValue)
                        {
                            provider.HeadPack = _headPack;
                        }
                        if (_RequestPack.HasValue)
                        {
                            provider.RequestPack = _RequestPack;
                        }
                        adapter = new ISOACAdapter(provider);

                        return adapter.Call(urls, serviceName, requestData);

                    case ISOACBuiltInProvider.TcpSinglePointDirectProvider:
                        provider = new ISOACTcpSinglePointDirectProvider();
                        if (_headPack.HasValue)
                        {
                            provider.HeadPack = _headPack;
                        }
                        if (_RequestPack.HasValue)
                        {
                            provider.RequestPack = _RequestPack;
                        }
                        adapter = new ISOACAdapter(provider);

                        return adapter.Call(urls, serviceName, requestData);

                    default:
                        break;

                }

                return null;
            }
            catch (ISOAException e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 使用内置或自定义的Provider调用服务，直接使用Provider实例参数
        /// </summary>
        /// <param name="urls">ISOA Url,格式：ISOA://ip:port,ISOA://doman:port</param>
        /// <param name="serviceName">服务名称</param>
        /// <param name="requestData">请求数据</param>
        /// <param name="provider">现有或用户定制的Provider</param>
        /// <returns>响应数据</returns>
        public byte[] Call(string urls, string serviceName, byte[] requestData,IISOACProvider provider)
        {
            if (_headPack.HasValue)
            {
                provider.HeadPack = _headPack;
            }
            if (_RequestPack.HasValue)
            {
                provider.RequestPack = _RequestPack;
            }

            try
            {
                ValidateBasicParams(urls, serviceName, requestData);
                var adapter = new ISOACAdapter(provider);
                return adapter.Call(urls, serviceName, requestData);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

    }
}
