using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace ISOA
{

    /// <summary>
    /// ISOA客户端基类
    /// </summary>
    public abstract class ISOACBase
    {
        /// <summary>
        /// 判断Tunnel标志
        /// </summary>
        /// <param name="urls">标准的URL格式</param>
        /// <returns>单通道标志</returns>
        protected virtual bool IsSingleTunnel(string urls)
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
        protected virtual void ValidateUrl(string urls)
        {
            if (string.IsNullOrWhiteSpace(urls))
            {
                throw new ISOAException(AttributeHelper.GetMemberText(FailedCode.EC_PARAM) + ": ISOA URL 不能为空！", (int)FailedCode.EC_PARAM, ISOAExceptionLevel.ERROR);
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
                        throw new ISOAException(AttributeHelper.GetMemberText(FailedCode.EC_URL), (int)FailedCode.EC_URL, ISOAExceptionLevel.ERROR, e);
                    }

                    isHostMatch = Regex.IsMatch(uri.Host, @"((25[0-5])|(2[0-4]\d)|(1\d\d)|([1-9]\d)|\d)(\.((25[0-5])|(2[0-4]\d)|(1\d\d)|([1-9]\d)|\d)){3}"); //IP正则
                    if (!isHostMatch)
                    {
                        isHostMatch = Regex.IsMatch(uri.Host, @"[a-zA-Z0-9][-a-zA-Z0-9]{0,62}(\.[a-zA-Z0-9][-a-zA-Z0-9]{0,62})+\.?"); //域名正则
                    }
                    if (!isHostMatch)
                    {
                        throw new ISOAException(AttributeHelper.GetMemberText(FailedCode.EC_HOST), (int)FailedCode.EC_HOST, ISOAExceptionLevel.ERROR);
                    }
                    else if (uri.Port < 0 || uri.Port > 65535)
                    {
                        throw new ISOAException(AttributeHelper.GetMemberText(FailedCode.EC_PORT), (int)FailedCode.EC_PORT, ISOAExceptionLevel.ERROR);
                    }
                }
            }

        }

        /// <summary>
        /// 解析URL序列
        /// </summary>
        /// <returns></returns>
        protected virtual UrlModel[] ParseUrl(string urls)
        {
            List<UrlModel> result = new List<UrlModel>();
            if (string.IsNullOrWhiteSpace(urls))
            {
                throw new ISOAException(AttributeHelper.GetMemberText(FailedCode.EC_PARAM) + ": ISOA URL 不能为空！", (int)FailedCode.EC_PARAM, ISOAExceptionLevel.ERROR);
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
                        throw new ISOAException(AttributeHelper.GetMemberText(FailedCode.EC_URL), (int)FailedCode.EC_URL, ISOAExceptionLevel.ERROR, e);
                    }

                    hostMatch = Regex.Match(uri.Host, @"((25[0-5])|(2[0-4]\d)|(1\d\d)|([1-9]\d)|\d)(\.((25[0-5])|(2[0-4]\d)|(1\d\d)|([1-9]\d)|\d)){3}"); //IP正则
                    if (!hostMatch.Success)
                    {
                        hostMatch = Regex.Match(uri.Host, @"[a-zA-Z0-9][-a-zA-Z0-9]{0,62}(\.[a-zA-Z0-9][-a-zA-Z0-9]{0,62})+\.?"); //域名正则
                    }
                    if (!hostMatch.Success)
                    {
                        throw new ISOAException(AttributeHelper.GetMemberText(FailedCode.EC_HOST), (int)FailedCode.EC_HOST, ISOAExceptionLevel.ERROR);
                    }
                    else if (uri.Port >= 0 && uri.Port <= 65535)
                    {
                        result.Add(new UrlModel() { IP = uri.Host, Port = uri.Port });
                    }
                    else
                    {
                        throw new ISOAException(AttributeHelper.GetMemberText(FailedCode.EC_PORT), (int)FailedCode.EC_PORT, ISOAExceptionLevel.ERROR);
                    }
                }
            }

            return result.ToArray();
        }

        /// <summary>
        /// 验证服务名称，须满足三段式命名，每一段由大写字母和下划线组成，段与段之间用'.'分隔，第一段总长不超过10，第二段总长不超过20，第三段总长不超过98
        /// </summary>
        /// <param name="serviceName">服务名称</param>
        /// <returns></returns>
        protected virtual bool ValidateServiceName(string serviceName)
        {
            try
            {
                return Regex.IsMatch(serviceName, @"^[A-Z_]{1,10}\.[A-Z_]{1,20}\.[A-Z_]{1,98}$", RegexOptions.None);
            }
            catch (Exception e)
            {
                throw new ISOAException(AttributeHelper.GetMemberText(FailedCode.EC_PARAM) + ": 服务名不合法！", (int)FailedCode.EC_PARAM, ISOAExceptionLevel.ERROR, e);
            }
        }

        /// <summary>
        /// 请求上下文转成字节流
        /// 
        /// </summary>
        /// <param name="serviceName">服务名称</param>
        /// <param name="requestData">请求数据</param>
        /// <param name="headPack">头包数据</param>
        /// <param name="requestPack">请求包数据</param>
        /// <returns/>
        protected virtual byte[] PutRequestContextToBuffer(string serviceName, byte[] requestData,ref HeadPack headPack,RequestPack requestPack)
        {
            List<byte> list = new List<byte>();
            requestPack.RequestData = requestData;
            requestPack.RequestSize = requestData.Length;
            requestPack.ServiceName = serviceName.PadRight(128);
            if (String.IsNullOrWhiteSpace(requestPack.UserName))
            {
                requestPack.UserName = " ".PadRight(64);
            }
            else
            {
                requestPack.UserName = requestPack.UserName.Trim().PadRight(64);
            }
            byte[] requestBytes = this.PutRequestPackToBuffer(requestPack);
            if (String.IsNullOrWhiteSpace(headPack.Mac))
            {
                headPack.Mac = " ".PadRight(130);
            }
            else
            {
                headPack.Mac = headPack.Mac.Trim().PadRight(130);
            }
            if (requestData.Length >= (int)ushort.MaxValue)
            {
                headPack.Flag = (int)HeadFlag.BPACK_ZIP;
            }
            headPack.OSize = requestData.Length + 128 + 4 + 64;
            if ((headPack.Flag & (int)HeadFlag.BPACK_ZIP) == (int)HeadFlag.BPACK_ZIP)
            {
                requestBytes = DataHelper.Compress(requestBytes);
                headPack.Size = requestBytes.Length;
            }
            else
            {
                headPack.Size = headPack.OSize;
            }
            byte[] headBytes = this.PutHeadPackToBuffer(headPack);
            list.AddRange(headBytes);
            list.AddRange(requestBytes);

            return list.ToArray();
        }


        /// <summary>
        /// RequestPack转成字节流
        /// 
        /// </summary>
        /// <param name="requestPack"/>
        /// <returns/>
        protected virtual byte[] PutRequestPackToBuffer(RequestPack requestPack)
        {
            List<byte> list = new List<byte>();
            list.AddRange(DataHelper.GetBytesFromString(requestPack.UserName));
            list.AddRange(DataHelper.GetBytesFromString(requestPack.ServiceName));
            int num = IPAddress.HostToNetworkOrder(requestPack.RequestSize);
            list.AddRange(BitConverter.GetBytes(num));
            list.AddRange(requestPack.RequestData);

            return list.ToArray();
        }

        /// <summary>
        /// HeadPack转成字节流
        /// 
        /// </summary>
        /// <param name="head"/>
        /// <returns/>
        protected virtual byte[] PutHeadPackToBuffer(HeadPack head)
        {
            List<byte> list = new List<byte>();
            int num = IPAddress.HostToNetworkOrder(head.Flag);
            list.AddRange(BitConverter.GetBytes(num));
            num = IPAddress.HostToNetworkOrder(head.Size);
            list.AddRange(BitConverter.GetBytes(num));
            num = IPAddress.HostToNetworkOrder(head.OSize);
            list.AddRange(BitConverter.GetBytes(num));
            list.AddRange(DataHelper.GetBytesFromString(head.Mac));

            return list.ToArray();
        }

        /// <summary>
        /// 读取并判断包头信息
        /// 
        /// </summary>
        /// <param name="head"/><param name="stream"/>
        protected virtual void ReadToHeadPack(ref HeadPack head, NetworkStream stream)
        {
            byte[] bytes = new byte[142];
            int num = 0;
            try
            {
                num = this.Read(stream, bytes, 0, bytes.Length);
            }
            catch (Exception e)
            {
                throw new ISOAException(AttributeHelper.GetMemberText(FailedCode.EC_RECV), (int)FailedCode.EC_RECV, ISOAExceptionLevel.ERROR, e);
            }
            if (num != 142)
            {
                throw new ISOAException(string.Format("包头数据不完整,理应大小为:{0},实际大小为:{1}", (object)142, (object)num), ISOAExceptionLevel.FATAL);
            }
            try
            {
                this.PutBufferToHeadPack(ref head, bytes);
            }
            catch(Exception e)
            {
                throw new ISOAException(AttributeHelper.GetMemberText(FailedCode.EC_UNPACK), (int)FailedCode.EC_UNPACK, ISOAExceptionLevel.ERROR, e);
            }
            if ((head.Flag & (int)HeadFlag.BPACK_FAILED) != (int)HeadFlag.BPACK_FAILED)
            {
                return;
            }
            bytes = new byte[head.Size];
            try
            {
                this.Read(stream, bytes, 0, bytes.Length);
            }
            catch (Exception e)
            {
                throw new ISOAException(AttributeHelper.GetMemberText(FailedCode.EC_RECV), (int)FailedCode.EC_RECV, ISOAExceptionLevel.ERROR, e);
            }
            throw new ISOAException("ISOA运行服务时错误:" + DataHelper.GetStringFromBytes(bytes), ISOAExceptionLevel.FATAL);
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
        protected virtual int Read(NetworkStream stream, byte[] data, int offset, int Length)
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
        /// 字节流转成HeadPack;
        /// 
        /// </summary>
        /// <param name="head"/>
        /// <param name="dataBuffer"/>
        protected virtual void PutBufferToHeadPack(ref HeadPack head, byte[] dataBuffer)
        {
            int num = BitConverter.ToInt32(dataBuffer, 0);
            head.Flag = IPAddress.NetworkToHostOrder(num);
            int position = 4;
            num = BitConverter.ToInt32(dataBuffer, position);
            head.Size = IPAddress.NetworkToHostOrder(num);
            position += 4;
            num = BitConverter.ToInt32(dataBuffer, position);
            head.OSize = IPAddress.NetworkToHostOrder(num);
            position += 4;
            head.Mac = BitConverter.ToString(dataBuffer, position, 130);
        }

        /// <summary>
        /// 读取响应数据包信息
        /// 
        /// </summary>
        protected virtual byte[] ReadToReactPack(ref HeadPack head, NetworkStream stream)
        {
            int Length = head.Size;
            byte[] bytes = new byte[Length];
            int num = 0;
            try
            {
                num = this.Read(stream, bytes, 0, Length);
            }
            catch (Exception e)
            {
                throw new ISOAException(AttributeHelper.GetMemberText(FailedCode.EC_RECV), (int)FailedCode.EC_RECV, ISOAExceptionLevel.ERROR, e);
            }
            if (num != head.Size)
            {
                throw new ISOAException(string.Format("业务数据不完整,大小应为:{0},实际大小为:{1}", (object)head.Size, (object)num), ISOAExceptionLevel.ERROR);
            }
            if (head.Flag != 0)
            {
                return bytes; //?
            }
            byte[] dataBuffer = (head.Flag & (int)HeadFlag.BPACK_ZIP) != (int)HeadFlag.BPACK_ZIP ? bytes : DataHelper.Decompress(bytes, head.OSize);
            ReactPack reactPack = new ReactPack();
            try
            {
                this.PutBufferToReactPack(ref reactPack, dataBuffer);
            }
            catch(Exception e)
            {
                throw new ISOAException(AttributeHelper.GetMemberText(FailedCode.EC_UNPACK), (int)FailedCode.EC_UNPACK, ISOAExceptionLevel.ERROR, e);
            }

            return reactPack.ReactData;
        }

        /// <summary>
        /// 字节流转成ReactPack;
        /// 
        /// </summary>
        /// <param name="reactPack"/>
        /// <param name="dataBuffer"/>
        protected virtual void PutBufferToReactPack(ref ReactPack reactPack, byte[] dataBuffer)
        {
            reactPack.ServiceName = DataHelper.GetStringFromBytes(dataBuffer, 0, 128);
            int position = 128;
            reactPack.ServiceCost = DataHelper.GetStringFromBytes(dataBuffer, position, 20);
            position += 20;
            int num = BitConverter.ToInt32(dataBuffer, position);
            reactPack.ReactSize = IPAddress.NetworkToHostOrder(num);
            position += 4;
            reactPack.ReactData = new byte[reactPack.ReactSize];
            int index = 0;
            while (position < dataBuffer.Length)
            {
                reactPack.ReactData[index] = dataBuffer[position];
                ++position;
                ++index;
            }
        }

        /// <summary>
        /// 存根请求上下文转成buffer,直连时使用
        /// </summary>
        /// <param name="headPack">头包数据</param>
        /// <param name="stubPack">存根数据</param>
        /// <returns/>
        protected virtual byte[] PutStubRequestContextToBuffer(HeadPack headPack, StubPack stubPack)
        {
            List<byte> list = new List<byte>();
            byte[] buffer = null;
            headPack.Size = 276;
            headPack.OSize = headPack.Size;
            if (String.IsNullOrWhiteSpace(headPack.Mac))
            {
                headPack.Mac = " ".PadRight(130);
            }
            else
            {
                headPack.Mac = headPack.Mac.Trim().PadRight(130);
            }
            buffer = this.PutHeadPackToBuffer(headPack);
            list.AddRange(buffer);
            buffer = this.PutStubPackToBuffer(stubPack);
            list.AddRange(buffer);

            return list.ToArray();
        }


        /// <summary>
        /// StubPack(存根信息)转成字节流,直连时使用
        /// 
        /// </summary>
        /// <param name="stubPack">存根数据</param>
        /// <returns/>
        protected virtual byte[] PutStubPackToBuffer(StubPack stubPack)
        {
            List<byte> list = new List<byte>();
            byte[] buffer = null;
            buffer = DataHelper.GetBytesFromStringWithoutEncoding(stubPack.ServiceName);
            list.AddRange(buffer);
            buffer = DataHelper.GetBytesFromStringWithoutEncoding(stubPack.TunnelName);
            list.AddRange(buffer);
            buffer = DataHelper.GetBytesFromStringWithoutEncoding(stubPack.Remsgid);
            list.AddRange(buffer);

            return list.ToArray();
        }

        /// <summary>
        /// 节流转成StubPack(存根信息),直连时使用
        /// 
        /// </summary>
        /// <param name="stubPack">存根数据</param>
        /// <param name="dataBuffer">数据Buffer</param>
        protected virtual void PutBufferToStubPack(ref StubPack stubPack, byte[] dataBuffer)
        {
            stubPack.ServiceName = DataHelper.GetStringFromBytes(dataBuffer, 0, 128);
            int position = 0 + 128;
            stubPack.IP = DataHelper.GetStringFromBytes(dataBuffer, position, 40);
            stubPack.IP = stubPack.IP.TrimEnd(" ".ToCharArray());
            position += 40;
            stubPack.Port = DataHelper.GetStringFromBytes(dataBuffer, position, 128);
            stubPack.Port = stubPack.Port.TrimEnd(" ".ToCharArray());
            position += 128;
            stubPack.TunnelName = DataHelper.GetStringFromBytes(dataBuffer, position, 128);
            position += 128;
            stubPack.Remsgid = DataHelper.GetStringFromBytes(dataBuffer, position, 20);
        }
    }
}
