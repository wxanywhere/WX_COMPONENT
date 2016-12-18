using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;

namespace ISOA
{
    /// <summary>
    /// TCP协议下通过服务代理调用服务
    /// </summary>
    public class ISOACTcpSinglePointProxyProvider : ISOACBase, IISOACProvider
    {
        /// <summary>
        /// 头包
        /// </summary>
        public HeadPack? HeadPack { get; set; }

        /// <summary>
        /// 请求包
        /// </summary>
        public RequestPack? RequestPack { get; set; }

        /// <summary>
        /// 连接类型
        /// </summary>
        public int ConnectionTypeFlag { get; set; }

        /// <summary>
        /// 其他数据信息
        /// </summary>
        public ExtraDataInfo ExtraDataInfo { get; set; }

        /// <summary>
        /// 初始化服务调用资源
        /// </summary>
        /// <param name="urls">ISOA Url字符串，多个Url以英文字符的逗号隔开，格式为：ISOA://ip:port,ISOA://domain:port</param>
        /// <param name="connectionTypeflag">连接类型</param>
        /// <param name="dataInfo">附加数据</param>
        /// <returns>处理句柄提供器</returns>
        public HandleProviderBase Init(string urls, int connectionTypeflag, ExtraDataInfo dataInfo)
        {
            UrlModel[] urlModels = null;
            try
            {
                urlModels = this.ParseUrl(urls);
                if (urlModels == null || !(urlModels.Length > 0))
                {
                    throw new ISOAException(AttributeHelper.GetMemberText(FailedCode.EC_URL), (int)FailedCode.EC_URL, ISOAExceptionLevel.ERROR);
                }
            }
            catch (Exception e)
            {
                throw new ISOAException(AttributeHelper.GetMemberText(FailedCode.EC_URL), (int)FailedCode.EC_URL, ISOAExceptionLevel.ERROR,e);
            }
            TcpHandleProvider handle = new TcpHandleProvider();
            try
            {
                handle.TcpClient = new TcpClient(urlModels[0].IP, urlModels[0].Port);
            }
            catch(Exception e)
            {
                throw new ISOAException(AttributeHelper.GetMemberText(FailedCode.EC_CONNECT), (int)FailedCode.EC_CONNECT,ISOAExceptionLevel.ERROR,e);
            }
            try
            {
                handle.NetworkStream = handle.TcpClient.GetStream();
            }
            catch (Exception e)
            {
                throw new ISOAException(AttributeHelper.GetMemberText(FailedCode.EC_SOCK), (int)FailedCode.EC_SOCK, ISOAExceptionLevel.ERROR, e);
            }

            return handle;
        }

        /// <summary>
        /// 调用ISOA服务，使用服务端代理
        /// </summary>
        /// <param name="handleProvider">句柄提供器</param>
        /// <param name="serviceName">服务名称</param>
        /// <param name="requestData">请求数据</param>
        /// <returns>响应数据</returns>
        public byte[] Call(HandleProviderBase handleProvider, string serviceName, byte[] requestData)
        {
            var provider = handleProvider.RetrieveProvider<TcpHandleProvider>();
            if (provider == null || provider.TcpClient== null || provider.NetworkStream == null)
            {
                throw new ISOAException("provider处理有误！", ISOAExceptionLevel.ALARM);
            }
            try
            {
                HeadPack headPack = this.HeadPack.HasValue ? this.HeadPack.Value : new HeadPack();
                RequestPack requestPack = this.RequestPack.HasValue ? this.RequestPack.Value : new RequestPack();
                byte[] buffer = null;
                try
                {
                    buffer = this.PutRequestContextToBuffer(serviceName, requestData, ref headPack, requestPack);
                }
                catch(Exception e)
                {
                    throw new ISOAException(AttributeHelper.GetMemberText(FailedCode.EC_PACK), (int)FailedCode.EC_PACK, ISOAExceptionLevel.ERROR, e);
                }
                try
                {
                    provider.NetworkStream.Write(buffer, 0, buffer.Length);
                }
                catch(Exception e)
                {
                    throw new ISOAException(AttributeHelper.GetMemberText(FailedCode.EC_SEND), (int)FailedCode.EC_SEND, ISOAExceptionLevel.ERROR, e);
                }
                try
                {
                    this.ReadToHeadPack(ref headPack, provider.NetworkStream);
                }
                catch(Exception e)
                {
                    throw new ISOAException(AttributeHelper.GetMemberText(FailedCode.EC_PEER), (int)FailedCode.EC_PEER, ISOAExceptionLevel.ERROR, e);
                }
                try
                {
                    return this.ReadToReactPack(ref headPack, provider.NetworkStream);
                }
                catch(Exception e)
                {
                    throw new ISOAException(AttributeHelper.GetMemberText(FailedCode.EC_RECV), (int)FailedCode.EC_RECV, ISOAExceptionLevel.ERROR, e);
                }
            }
            catch(ISOAException e)
            {
                throw new ISOAException("服务调用失败",ISOAExceptionLevel.ERROR,e);
            }
            finally
            {
                provider.NetworkStream.Close();
                provider.NetworkStream = null;
                provider.TcpClient.Close();
                provider.TcpClient = null; 
            }
        }

        /// <summary>
        /// 销毁句柄提供器资源
        /// </summary>
        /// <param name="handleProvider">句柄提供器</param>
        public void Destroy(HandleProviderBase handleProvider)
        {
            var provider = handleProvider.RetrieveProvider<TcpHandleProvider>();
            try
            {
                if (provider != null)
                {
                    if (provider.NetworkStream != null)
                    {
                        provider.NetworkStream.Close();
                    }
                    if (provider.TcpClient != null)
                    {
                        provider.NetworkStream.Close();
                    }
                }
            }
            catch (Exception e)
            {
                throw new ISOAException("销毁资源失败！", ISOAExceptionLevel.ERROR, e);
            }
        }

        /// <summary>
        /// 获取错误信息
        /// </summary>
        /// <param name="failedCode">错误代码</param>
        /// <returns>错误信息</returns>
        public string GetError(int failedCode)
        {
            return AttributeHelper.GetMemberText((FailedCode)failedCode);
        }
    }
}
