using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace ydtf.isoa
{
    /// <summary>
    /// ISOA客户端编程接口
    /// </summary>
    internal interface iisoac
    {
        /// <summary>
        /// 初始化服务访问句柄资源
        /// </summary>
        /// <param name="url">ISOA的url字符串格式为：isoa://ip:port 或isoa://domain:port</param>
        /// <param name="attr">条件信息</param>
        /// <returns>成功，返回服务访问句柄；失败，返回null</returns>
        t_isoac init(string url, t_isoac_attr attr);

        /// <summary>
        /// 建立连接
        /// </summary>
        /// <param name="handle">服务访问句柄</param>
        /// <returns>成功，返回0；失败，返回值大于或等于1</returns>
        int connect(t_isoac handle);

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="handle">服务访问句柄</param>
        /// <param name="svc_name">服务名</param>
        /// <param name="req_data">请求数据</param>
        /// <returns>成功，返回0；失败，返回值大于或等于1</returns>
        int send(t_isoac handle, string svc_name, byte[] req_data);

        /// <summary>
        /// 接受响应数据
        /// </summary>
        /// <param name="handle">服务访问句柄</param>
        /// <returns>成功，返回字节流数据；失败，返回null</returns>
        byte[] recv(t_isoac handle);

        /// <summary>
        /// 销毁句柄资源
        /// </summary>
        /// <param name="handle">服务访问句柄</param>
        void destroy(t_isoac handle);

        /// <summary>
        /// 服务轮询
        /// </summary>
        /// <param name="handle">服务访问句柄</param>
        /// <param name="svc_name">服务名></param>
        /// <param name="req_data">请求数据</param>
        /// <returns>成功，返回0；失败，返回值大于或等于1</returns>
        int poll(t_isoac handle, string svc_name, byte[] req_data);

        /// <summary>
        /// 服务调用
        /// </summary>
        /// <param name="handle">服务访问句柄</param>
        /// <param name="svc_name">服务名</param>
        /// <param name="req_data">请求数据</param>
        /// <returns>响应数据</returns>
        byte[] call(t_isoac handle, string svc_name, byte[] req_data);

    }

}
