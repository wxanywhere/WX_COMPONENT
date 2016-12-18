using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace ydtf.isoa
{
    /// <summary>
    /// isoac_helper
    /// </summary>
    internal static class isoac_helper
    {
        /// <summary>
        /// 判断Tunnel标志
        /// </summary>
        /// <param name="urls">标准的URL格式</param>
        /// <returns>单通道标志</returns>
        internal static bool IsSingleTunnel(string urls)
        {
            bool retFlag = true;
            if (!String.IsNullOrWhiteSpace(urls) && urls.Contains(','))
            {
                retFlag = false;
            }

            return retFlag;
        }

        /// <summary>
        ///  验证URL序列
        /// </summary>
        /// <param name="urls">URL序列</param>
        internal static void ValidateUrl(string urls)
        {
            if (string.IsNullOrWhiteSpace(urls))
            {
                throw new excep(attribute_helper.GetMemberText(ec_errno.EC_PARAM) + ": ISOA URL 不能为空！", (int)ec_errno.EC_PARAM, excep_level.ERROR);
            }
            else
            {
                var strArray = Regex.Split(urls.Trim(), ",", RegexOptions.Singleline);
                Uri uri = null;
                bool isHostMatch = false;
                foreach (var str in strArray)
                {
                    try
                    {
                        uri = new Uri(str);
                    }
                    catch (Exception e)
                    {
                        throw new excep(attribute_helper.GetMemberText(ec_errno.EC_URL), (int)ec_errno.EC_URL, excep_level.ERROR, e);
                    }

                    isHostMatch = Regex.IsMatch(uri.Host, @"((25[0-5])|(2[0-4]\d)|(1\d\d)|([1-9]\d)|\d)(\.((25[0-5])|(2[0-4]\d)|(1\d\d)|([1-9]\d)|\d)){3}"); //IP正则
                    if (!isHostMatch)
                    {
                        isHostMatch = Regex.IsMatch(uri.Host, @"[a-zA-Z0-9][-a-zA-Z0-9]{0,62}(\.[a-zA-Z0-9][-a-zA-Z0-9]{0,62})+\.?"); //域名正则
                    }
                    if (!isHostMatch)
                    {
                        throw new excep(attribute_helper.GetMemberText(ec_errno.EC_HOST), (int)ec_errno.EC_HOST, excep_level.ERROR);
                    }
                    else if (uri.Port < 0 || uri.Port > 65535)
                    {
                        throw new excep(attribute_helper.GetMemberText(ec_errno.EC_PORT), (int)ec_errno.EC_PORT, excep_level.ERROR);
                    }
                }
            }

        }

        /// <summary>
        /// 解析URL序列
        /// </summary>
        /// <returns></returns>
        internal static url_model[] ParseUrl(string urls)
        {
            List<url_model> result = new List<url_model>();
            if (string.IsNullOrWhiteSpace(urls))
            {
                throw new excep(attribute_helper.GetMemberText(ec_errno.EC_PARAM) + ": ISOA URL 不能为空！", (int)ec_errno.EC_PARAM, excep_level.ERROR);
            }
            else
            {
                var strArray = Regex.Split(urls.Trim(), ",", RegexOptions.Singleline);
                Uri uri = null;
                Match hostMatch = null;
                foreach (var str in strArray)
                {
                    try
                    {
                        uri = new Uri(str);
                    }
                    catch (Exception e)
                    {
                        throw new excep(attribute_helper.GetMemberText(ec_errno.EC_URL), (int)ec_errno.EC_URL, excep_level.ERROR, e);
                    }

                    hostMatch = Regex.Match(uri.Host, @"((25[0-5])|(2[0-4]\d)|(1\d\d)|([1-9]\d)|\d)(\.((25[0-5])|(2[0-4]\d)|(1\d\d)|([1-9]\d)|\d)){3}"); //IP正则
                    if (!hostMatch.Success)
                    {
                        hostMatch = Regex.Match(uri.Host, @"[a-zA-Z0-9][-a-zA-Z0-9]{0,62}(\.[a-zA-Z0-9][-a-zA-Z0-9]{0,62})+\.?"); //域名正则
                    }
                    if (!hostMatch.Success)
                    {
                        throw new excep(attribute_helper.GetMemberText(ec_errno.EC_HOST), (int)ec_errno.EC_HOST, excep_level.ERROR);
                    }
                    else if (uri.Port >= 0 && uri.Port <= 65535)
                    {
                        result.Add(new url_model() { ip = uri.Host, port = uri.Port });
                    }
                    else
                    {
                        throw new excep(attribute_helper.GetMemberText(ec_errno.EC_PORT), (int)ec_errno.EC_PORT, excep_level.ERROR);
                    }
                }
            }

            return result.ToArray();
        }

        /// <summary>
        /// 验证服务名称，须满足三段式命名，每一段由大写字母和下划线组成，段与段之间用'.'分隔，第一段总长不超过10，第二段总长不超过20，第三段总长不超过98
        /// </summary>
        /// <param name="svc_name">服务名称</param>
        /// <returns></returns>
        internal static bool ValidateServiceName(string svc_name)
        {
            try
            {
                return Regex.IsMatch(svc_name, @"^[A-Z_]{1,10}\.[A-Z_]{1,20}\.[A-Z_]{1,98}$", RegexOptions.None);
            }
            catch (Exception e)
            {
                throw new excep(attribute_helper.GetMemberText(ec_errno.EC_PARAM)+": 服务名不合法！", (int)ec_errno.EC_PARAM, excep_level.ERROR, e);
            }

        }

        /// <summary>
        /// 请求上下文转成字节流
        /// 
        /// </summary>
        /// <param name="svc_name">服务名称</param>
        /// <param name="req_data">请求数据</param>
        /// <param name="headPack">头包数据</param>
        /// <param name="req">请求包数据</param>
        /// <returns/>
        internal static byte[] PutRequestContextToBuffer(string svc_name, byte[] req_data, head_pack headPack, req_pack req)
        {
            List<byte> list = new List<byte>();
            req.req_data = req_data;
            req.req_size = req_data.Length;
            req.service = svc_name.PadRight(128);
            if (String.IsNullOrWhiteSpace(req.user))
            {
                req.user = " ".PadRight(64);
            }
            else
            {
                req.user = req.user.Trim().PadRight(64);
            }
            byte[] requestBytes = isoac_helper.PutRequestPackToBuffer(req);
            if (String.IsNullOrWhiteSpace(headPack.mac))
            {
                headPack.mac = " ".PadRight(130);
            }
            else
            {
                headPack.mac = headPack.mac.Trim().PadRight(130);
            }
            if (req_data.Length >= (int)ushort.MaxValue)
            {
                headPack.flag = (int)head_flag.BPACK_ZIP;
            }
            headPack.osize = req_data.Length + 128 + 4 + 64;
            if ((headPack.flag & (int)head_flag.BPACK_ZIP) == (int)head_flag.BPACK_ZIP)
            {
                requestBytes = data_helper.Compress(requestBytes);
                headPack.size = requestBytes.Length;
            }
            else
            {
                headPack.size = headPack.osize;
            }
            byte[] headBytes = isoac_helper.PutHeadPackToBuffer(headPack);
            list.AddRange(headBytes);
            list.AddRange(requestBytes);

            return list.ToArray();
        }


        /// <summary>
        /// req_pack转成字节流
        /// 
        /// </summary>
        /// <param name="req"/>
        /// <returns/>
        internal static byte[] PutRequestPackToBuffer(req_pack req)
        {
            List<byte> list = new List<byte>();
            list.AddRange(data_helper.GetBytesFromString(req.user));
            list.AddRange(data_helper.GetBytesFromString(req.service));
            int num = IPAddress.HostToNetworkOrder(req.req_size);
            list.AddRange(BitConverter.GetBytes(num));
            list.AddRange(req.req_data);

            return list.ToArray();
        }

        /// <summary>
        /// head_pack转成字节流
        /// 
        /// </summary>
        /// <param name="head"/>
        /// <returns/>
        internal static byte[] PutHeadPackToBuffer(head_pack head)
        {
            List<byte> list = new List<byte>();
            int num = IPAddress.HostToNetworkOrder(head.flag);
            list.AddRange(BitConverter.GetBytes(num));
            num = IPAddress.HostToNetworkOrder(head.size);
            list.AddRange(BitConverter.GetBytes(num));
            num = IPAddress.HostToNetworkOrder(head.osize);
            list.AddRange(BitConverter.GetBytes(num));
            list.AddRange(data_helper.GetBytesFromString(head.mac));

            return list.ToArray();
        }

        /// <summary>
        /// 读取并判断包头信息
        /// 
        /// </summary>
        /// <param name="head"/><param name="stream"/>
        internal static void ReadToHeadPack(ref head_pack head, NetworkStream stream)
        {
            byte[] bytes = new byte[142];
            int num = 0;
            try
            {
                num = isoac_helper.Read(stream, bytes, 0, bytes.Length);
            }
            catch (Exception e)
            {
                throw new excep(attribute_helper.GetMemberText(ec_errno.EC_RECV), (int)ec_errno.EC_RECV, excep_level.ERROR, e);
            }
            if (num != 142)
            {
                throw new excep(string.Format("包头数据不完整,理应大小为:{0},实际大小为:{1}", (object)142, (object)num), excep_level.FATAL);
            }
            try
            {
                isoac_helper.PutBufferToHeadPack(ref head, bytes);
            }
            catch (Exception e)
            {
                throw new excep(attribute_helper.GetMemberText(ec_errno.EC_UNPACK), (int)ec_errno.EC_UNPACK, excep_level.ERROR, e);
            }
            if ((head.flag & (int)head_flag.BPACK_FAILED) != (int)head_flag.BPACK_FAILED)
            {
                return;
            }
            bytes = new byte[head.size];
            try
            {
                isoac_helper.Read(stream, bytes, 0, bytes.Length);
            }
            catch (Exception e)
            {
                throw new excep(attribute_helper.GetMemberText(ec_errno.EC_RECV), (int)ec_errno.EC_RECV, excep_level.ERROR, e);
            }
            throw new excep("ISOA运行服务时错误:" + data_helper.GetStringFromBytes(bytes), excep_level.FATAL);
        }

        /// <summary>
        /// 从networkStream读Length字节的数据;
        ///             为防止网络状况问题和数据报大小缓冲限制;
        ///             要读取多次,直至读出指定数目的字节.
        /// 
        /// </summary>
        /// <param name="stream">NetworkStrean对象</param>
        /// <param name="data">字节数组缓冲</param>
        /// <param name="offset">偏移,从offset字节处装入读入的数据</param>
        /// <param name="Length">需要读出的字节总数</param>
        /// <returns/>
        internal static int Read(NetworkStream stream, byte[] data, int offset, int Length)
        {
            int count = Length;
            int numA = 0;
            int numB = 0;
            while (count != 0)
            {
                numB = stream.Read(data, offset + numA, count);
                if (numB < 0)
                {
                    return -1;
                }
                count -= numB;
                numA += numB;
            }
            return numA;
        }

        /// <summary>
        /// 字节流转成head_pack;
        /// 
        /// </summary>
        /// <param name="head"/>
        /// <param name="buffer"/>
        internal static void PutBufferToHeadPack(ref head_pack head, byte[] buffer)
        {
            int num = BitConverter.ToInt32(buffer, 0);
            head.flag = IPAddress.NetworkToHostOrder(num);
            int position = 4;
            num = BitConverter.ToInt32(buffer, position);
            head.size = IPAddress.NetworkToHostOrder(num);
            position += 4;
            num = BitConverter.ToInt32(buffer, position);
            head.osize = IPAddress.NetworkToHostOrder(num);
            position += 4;
            head.mac = BitConverter.ToString(buffer, position, 130);
        }

        /// <summary>
        /// 读取响应数据包信息
        /// 
        /// </summary>
        internal static byte[] ReadToReactPack(ref head_pack head, NetworkStream stream)
        {
            int Length = head.size;
            byte[] bytes = new byte[Length];
            int num = 0;
            try
            {
                num = isoac_helper.Read(stream, bytes, 0, Length);
            }
            catch (Exception e)
            {
                throw new excep(attribute_helper.GetMemberText(ec_errno.EC_RECV), (int)ec_errno.EC_RECV, excep_level.ERROR, e);
            }
            if (num != head.size)
            {
                throw new excep(string.Format("业务数据不完整,大小应为:{0},实际大小为:{1}", (object)head.size, (object)num), excep_level.FATAL);
            }
            if (head.flag != 0)
            {
                return bytes; //?
            }
            byte[] buffer = (head.flag & (int)head_flag.BPACK_ZIP) != (int)head_flag.BPACK_ZIP ? bytes : data_helper.Decompress(bytes, head.osize);
            rea_pack rea = new rea_pack();
            try
            {
                isoac_helper.PutBufferToReactPack(ref rea, buffer);
            }
            catch (Exception e)
            {
                throw new excep(attribute_helper.GetMemberText(ec_errno.EC_UNPACK), (int)ec_errno.EC_UNPACK, excep_level.ERROR, e);
            }

            return rea.react_data;
        }

        /// <summary>
        /// 字节流转成rea_pack;
        /// 
        /// </summary>
        /// <param name="rea"/>
        /// <param name="buffer"/>
        internal static void PutBufferToReactPack(ref rea_pack rea, byte[] buffer)
        {
            rea.service = data_helper.GetStringFromBytes(buffer, 0, 128);
            int position = 128;
            rea.service_cost = data_helper.GetStringFromBytes(buffer, position, 20);
            position += 20;
            int num = BitConverter.ToInt32(buffer, position);
            rea.react_size = IPAddress.NetworkToHostOrder(num);
            position += 4;
            rea.react_data = new byte[rea.react_size];
            int index = 0;
            while (position < buffer.Length)
            {
                rea.react_data[index] = buffer[position];
                ++position;
                ++index;
            }
        }

        /// <summary>
        /// 存根请求上下文转成buffer,直连时使用
        /// </summary>
        /// <param name="headPack">头包数据</param>
        /// <param name="stub">存根数据</param>
        /// <returns/>
        internal static byte[] PutStubRequestContextToBuffer(head_pack headPack, stub_pack stub)
        {
            List<byte> list = new List<byte>();
            byte[] buffer = null;
            headPack.size = 276;
            headPack.osize = headPack.size;
            if (String.IsNullOrWhiteSpace(headPack.mac))
            {
                headPack.mac = " ".PadRight(130);
            }
            else
            {
                headPack.mac = headPack.mac.Trim().PadRight(130);
            }
            buffer = isoac_helper.PutHeadPackToBuffer(headPack);
            list.AddRange(buffer);
            buffer = isoac_helper.PutStubPackToBuffer(stub);
            list.AddRange(buffer);

            return list.ToArray();
        }


        /// <summary>
        /// stub_pack(存根信息)转成字节流,直连时使用
        /// 
        /// </summary>
        /// <param name="stub">存根数据</param>
        /// <returns/>
        internal static byte[] PutStubPackToBuffer(stub_pack stub)
        {
            List<byte> list = new List<byte>();
            byte[] buffer = null;
            buffer = data_helper.GetBytesFromStringWithoutEncoding(stub.service);
            list.AddRange(buffer);
            buffer = data_helper.GetBytesFromStringWithoutEncoding(stub.tunnel);
            list.AddRange(buffer);
            buffer = data_helper.GetBytesFromStringWithoutEncoding(stub.remsgid);
            list.AddRange(buffer);

            return list.ToArray();
        }

        /// <summary>
        /// 节流转成stub_pack(存根信息),直连时使用
        /// 
        /// </summary>
        /// <param name="stub">存根数据</param>
        /// <param name="buffer">数据Buffer</param>
        internal static void PutBufferToStubPack(ref stub_pack stub, byte[] buffer)
        {
            stub.service = data_helper.GetStringFromBytes(buffer, 0, 128);
            int position = 0 + 128;
            stub.ip = data_helper.GetStringFromBytes(buffer, position, 40);
            stub.ip = stub.ip.TrimEnd(" ".ToCharArray());
            position += 40;
            stub.port = data_helper.GetStringFromBytes(buffer, position, 128);
            stub.port = stub.port.TrimEnd(" ".ToCharArray());
            position += 128;
            stub.tunnel = data_helper.GetStringFromBytes(buffer, position, 128);
            position += 128;
            stub.remsgid = data_helper.GetStringFromBytes(buffer, position, 20);
        }
    }
}
