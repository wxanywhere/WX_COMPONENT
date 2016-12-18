using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace ydtf.isoa
{
    /// <summary>
    /// 服务调用客户端
    /// </summary>
    public static class isoac
    {

        /// <summary>
        /// 初始化服务访问句柄资源
        /// </summary>
        /// <param name="url">ISOA的url字符串格式为：isoa://ip:port 或isoa://domain:port</param>
        /// <param name="attr">条件信息</param>
        /// <returns>成功，返回服务访问句柄；失败，返回null</returns>
        public static t_isoac init(string url, t_isoac_attr attr)
        {
            isoac_helper.ValidateUrl(url);
            url_model[] urlModels = null;
            try
            {
                urlModels = isoac_helper.ParseUrl(url);
                if (urlModels == null || !(urlModels.Length > 0))
                {
                    throw new excep(attribute_helper.GetMemberText(ec_errno.EC_URL), (int)ec_errno.EC_URL, excep_level.ERROR);
                }
            }
            catch (Exception e)
            {
                throw new excep(attribute_helper.GetMemberText(ec_errno.EC_URL), (int)ec_errno.EC_URL, excep_level.ERROR, e);
            }
            t_isoac handle = new t_isoac();
            try
            {
                handle.tcp_client = new TcpClient(urlModels[0].ip, urlModels[0].port);
            }
            catch (Exception e)
            {
                throw new excep(attribute_helper.GetMemberText(ec_errno.EC_CONNECT), (int)ec_errno.EC_CONNECT, excep_level.ERROR, e);
            }
            try
            {
                handle.net_stream = handle.tcp_client.GetStream();
            }
            catch (Exception e)
            {
                throw new excep(attribute_helper.GetMemberText(ec_errno.EC_SOCK), (int)ec_errno.EC_SOCK, excep_level.ERROR, e);
            }

            return handle;
        }

        /// <summary>
        /// 建立连接
        /// </summary>
        /// <param name="handle">服务访问句柄</param>
        /// <returns>成功，返回0；失败，返回值大于或等于1</returns>
        public static int connect(t_isoac handle)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="handle">服务访问句柄</param>
        /// <param name="svc_name">服务名</param>
        /// <param name="req_data">请求数据</param>
        /// <returns>成功，返回0；失败，返回值大于或等于1</returns>
        public static int send(t_isoac handle, string svc_name, byte[] req_data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 接受响应数据
        /// </summary>
        /// <param name="handle">服务访问句柄</param>
        /// <returns>成功，返回字节流数据；失败，返回null</returns>
        public static byte[] recv(t_isoac handle)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 销毁句柄资源
        /// </summary>
        /// <param name="handle">服务访问句柄</param>
        public static void destroy(t_isoac handle)
        {
            try
            {
                if (handle != null)
                {
                    if (handle.net_stream != null)
                    {
                        handle.net_stream.Close();
                        handle.net_stream = null;
                    }
                    if (handle.tcp_client != null)
                    {
                        handle.net_stream.Close();
                        handle.net_stream = null;
                    }
                }
            }
            catch (Exception e)
            {
                throw new excep("销毁资源失败！", excep_level.ERROR, e);
            }
        }

        /// <summary>
        /// 服务轮询
        /// </summary>
        /// <param name="handle">服务访问句柄</param>
        /// <param name="svc_name">服务名></param>
        /// <param name="req_data">请求数据</param>
        /// <returns>成功，返回0；失败，返回值大于或等于1</returns>
        public static int poll(t_isoac handle, string svc_name, byte[] req_data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 服务调用
        /// </summary>
        /// <param name="handle">服务访问句柄</param>
        /// <param name="svc_name">服务名</param>
        /// <param name="req_data">请求数据</param>
        /// <returns>响应数据</returns>
        public static byte[] call(t_isoac handle, string svc_name, byte[] req_data)
        {
            if (handle == null || handle.tcp_client == null || handle.net_stream == null)
            {
                throw new excep("handle处理有误！", excep_level.ALARM);
            }
            isoac_helper.ValidateServiceName(svc_name);
            if (req_data == null || !(req_data.Length > 0))
            {
                throw new excep(attribute_helper.GetMemberText(ec_errno.EC_PARAM) + ":请求数据不能为空！", (int)ec_errno.EC_PARAM, excep_level.ALARM);
            }
            byte[] buffer = null;
            try
            {
                head_pack headPack = new head_pack();
                req_pack requestPack = new req_pack();
                try
                {
                    buffer = isoac_helper.PutRequestContextToBuffer(svc_name, req_data, headPack, requestPack);
                }
                catch (Exception e)
                {
                    throw new excep(attribute_helper.GetMemberText(ec_errno.EC_PACK), (int)ec_errno.EC_PACK, excep_level.ERROR, e);
                }
                try
                {
                    handle.net_stream.Write(buffer, 0, buffer.Length);
                }
                catch (Exception e)
                {
                    throw new excep(attribute_helper.GetMemberText(ec_errno.EC_SEND), (int)ec_errno.EC_SEND, excep_level.ERROR, e);
                }
                try
                {
                    isoac_helper.ReadToHeadPack(ref headPack, handle.net_stream);
                }
                catch (Exception e)
                {
                    throw new excep(attribute_helper.GetMemberText(ec_errno.EC_PEER), (int)ec_errno.EC_PEER, excep_level.ERROR, e);
                }
                try
                {
                    return isoac_helper.ReadToReactPack(ref headPack, handle.net_stream);
                }
                catch (Exception e)
                {
                    throw new excep(attribute_helper.GetMemberText(ec_errno.EC_RECV), (int)ec_errno.EC_RECV, excep_level.ERROR, e);
                }
            }
            catch (excep e)
            {
                throw new excep("服务调用失败", excep_level.ERROR, e);
            }
            finally
            {
                handle.net_stream.Close();
                handle.tcp_client.Close();
                handle.net_stream = null;
                handle.tcp_client = null;
            }
        }

    }
}