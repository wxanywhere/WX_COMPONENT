using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace ISOA
{
    /// <summary>
    /// TCP协议下单点直连调用服务
    /// </summary>
    public class ISOACTcpSinglePointDirectProvider : ISOACBase, IISOACProvider
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
        /// <param name="flag">连接类型</param>
        /// <param name="dataInfo">其他数据信息</param>
        /// <returns>处理句柄提供器</returns>
        public HandleProviderBase Init(string urls, int flag, ExtraDataInfo dataInfo)
        {
            UrlModel[] urlModels = this.ParseUrl(urls);
            if (urlModels == null || !(urlModels.Length > 0))
            {
                throw new ISOAException("Url不合法", (int)FailedCode.EC_URL, ISOAExceptionLevel.ERROR);
            }
            TcpHandleProvider handle = new TcpHandleProvider();
            try
            {
                handle.TcpClient = new TcpClient(urlModels[0].IP, urlModels[0].Port);
                handle.NetworkStream = handle.TcpClient.GetStream();

                return handle;
            }
            catch (ISOAException e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 调用ISOA服务,直连(三个步骤)
        /// </summary>
        /// <param name="handleProvider">句柄提供器</param>
        /// <param name="serviceName">服务名称</param>
        /// <param name="requestData">请求数据</param>
        /// <returns>响应数据</returns>
        public byte[] Call(HandleProviderBase handleProvider, string serviceName, byte[] requestData)
        {
            var provider = handleProvider.RetrieveProvider<TcpHandleProvider>();
            if (provider == null || provider.TcpClient == null || provider.NetworkStream == null)
            {
                throw new ISOAException("provider处理有误！", ISOAExceptionLevel.ALARM);
            }
            try
            {
                byte[] buffer = null;
                HeadPack headPack = this.HeadPack.HasValue ? this.HeadPack.Value : new HeadPack();
                RequestPack requestPack = this.RequestPack.HasValue ? this.RequestPack.Value : new RequestPack();
                StubPack stubPack = new StubPack();
                try
                {
                    buffer = this.PutRequestContextToBuffer(serviceName, requestData,ref headPack,requestPack);
                    provider.NetworkStream.Write(buffer, 0, buffer.Length);
                    this.ReadToHeadPack(ref headPack, provider.NetworkStream);
                    buffer = new byte[444]; //存根缓存
                    int num = this.Read(provider.NetworkStream, buffer, 0, buffer.Length);
                    if (num != 444)
                    {
                        throw new ISOAException(string.Format("获取ISOA服务返回的存根数据不完整,大小应为:{0},实际大小为:{1}", (object)444, (object)num), ISOAExceptionLevel.FATAL);
                    }
                    this.PutBufferToStubPack(ref stubPack, buffer);
                }
                catch (ISOAException e)
                {
                    throw new ISOAException("获取存根失败！", ISOAExceptionLevel.ERROR, e);
                }
                finally
                {
                    provider.NetworkStream.Close();
                    provider.TcpClient.Close();
                }
                buffer = this.PutStubRequestContextToBuffer(headPack,stubPack);
                provider.TcpClient = new TcpClient(stubPack.IP, int.Parse(stubPack.Port));
                provider.NetworkStream = provider.TcpClient.GetStream();
                provider.NetworkStream.Write(buffer, 0, buffer.Length);
                this.ReadToHeadPack(ref headPack, provider.NetworkStream);

                return this.ReadToReactPack(ref headPack, provider.NetworkStream);
            }
            catch (ISOAException e)
            {
                throw new ISOAException("服务调用失败", ISOAExceptionLevel.ERROR, e);
            }
            finally
            {
                provider.NetworkStream.Close();
                provider.TcpClient.Close();
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
