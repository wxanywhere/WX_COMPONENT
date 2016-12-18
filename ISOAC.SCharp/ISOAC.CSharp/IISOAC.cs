using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace ISOA
{
    /// <summary>
    /// ISOA客户端编程接口
    /// </summary>
    public interface IISOAC
    {
        /// <summary>
        /// 初始化服务调用资源
        /// </summary>
        /// <param name="urls">ISOA Url字符串，多个Url以英文字符的逗号隔开，格式为：ISOA://ip:port,ISOA://domain:port</param>
        /// <param name="flag">连接类型</param>
        /// <param name="DataInfo">其他数据信息</param>
        /// <returns>处理句柄提供器</returns>
        HandleProviderBase Init(string urls, int flag, ExtraDataInfo DataInfo);

        /// <summary>
        /// 调用ISOA服务
        /// </summary>
        /// <param name="handleProvider">句柄提供器</param>
        /// <param name="serviceName">服务名称</param>
        /// <param name="requestData">请求数据</param>
        /// <returns>响应数据</returns>
        byte[] Call(HandleProviderBase handleProvider, string serviceName, byte[] requestData);

        /// <summary>
        /// 销毁句柄提供器资源
        /// </summary>
        /// <param name="handleProvider">句柄提供器</param>
        void Destroy(HandleProviderBase handleProvider);

        /// <summary>
        /// 获取错误信息
        /// </summary>
        /// <param name="failedCode">错误代码</param>
        /// <returns>错误信息</returns>
        string GetError(int failedCode);
    }

}
