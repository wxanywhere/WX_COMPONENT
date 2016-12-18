using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using zlib;

namespace ydtf.isoa
{
    /// <summary>
    /// data_helper
    /// </summary>
    public static class data_helper
    {
        /// <summary>
        /// 转换XML的特殊字符
        /// 
        /// </summary>
        /// <param name="strValue">需要转换的字符串</param>
        /// <returns>
        /// 转换后的字符串
        /// </returns>
        public static string ConvertXMLSpecialChar(string strValue)
        {
            return strValue.Replace("&amp;", "&").Replace("&quot;", "\"").Replace("&lt;", "<").Replace("&gt;", ">").Replace("NULL", "");
        }

        /// <summary>
        /// 转换成XML的特殊字符
        /// 
        /// </summary>
        /// <param name="strValue">需要转换的字符串</param>
        /// <returns>
        /// 转换后的字符串
        /// </returns>
        public static string ConvertToXMLSpecialChar(string strValue)
        {
            if (string.IsNullOrWhiteSpace(strValue))
                return "`";
            else
                return strValue.Replace("&", "&amp;").Replace("\"", "&quot;").Replace("<", "&lt;").Replace(">", "&gt;");
        }

        /// <summary>
        /// 字符串转字节流UTF8编码
        /// 
        /// </summary>
        /// <param name="dataString"/>
        /// <returns/>
        public static byte[] GetBytesFromString(string dataString)
        {
            return global_helper.CurrentEncoding.GetBytes(dataString);
        }

        /// <summary>
        /// 字符串转字节流
        /// 
        /// </summary>
        /// <param name="dataString"/>
        /// <returns/>
        public static byte[] GetBytesFromStringWithoutEncoding(string dataString)
        {
            byte[] bytes = new byte[dataString.Length];
            var chars = dataString.ToCharArray();
            for (int i = 0; i < dataString.Length; ++i) //i++ ?
            {
                bytes[i] = (byte)chars[i];
            }
            return bytes;
        }


        /// <summary>
        /// 字节流转字符串-UTF8编码
        /// 
        /// </summary>
        /// <param name="dataBytes"/>
        /// <returns/>
        public static string GetStringFromBytes(byte[] dataBytes)
        {
            return global_helper.CurrentEncoding.GetString(dataBytes, 0, dataBytes.Length);
        }

        /// <summary>
        /// 字节流转字符串-UTF8编码
        /// 
        /// </summary>
        /// <param name="dataBytes"></param>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <returns/>
        public static string GetStringFromBytes(byte[] dataBytes, int index, int count)
        {
            return global_helper.CurrentEncoding.GetString(dataBytes, index, count).Replace("\0", string.Empty);
        }

        /// <summary>
        /// 压缩数据
        /// 
        /// </summary>
        /// <param name="source"/>
        /// <returns/>
        public static byte[] Compress(byte[] source)
        {
            MemoryStream sourceStream = null;
            MemoryStream resultStream = null;
            ZOutputStream zoutputStream = (ZOutputStream)null;
            try
            {
                sourceStream = new MemoryStream(source);
                resultStream = new MemoryStream();
                zoutputStream = new ZOutputStream((Stream)resultStream, -1);
                data_helper.CopyStream((Stream)sourceStream, (Stream)zoutputStream);
            }
            finally
            {
                if (zoutputStream != null)
                {
                    zoutputStream.Close();
                }
                if (resultStream != null)
                {
                    resultStream.Close();
                }
                if (sourceStream != null)
                {
                    sourceStream.Close();
                }
            }

            return resultStream.ToArray();
        }

        /// <summary>
        /// 拷贝数据流
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[2000];
            int count;
            while ((count = input.Read(buffer, 0, 2000)) > 0)
            {
                output.Write(buffer, 0, count);
            }
            output.Flush();
        }

        /// <summary>
        /// 解压数据
        /// 
        /// </summary>
        /// <param name="source"/>
        /// <param name="length"/>
        /// <returns/>
        public static byte[] Decompress(byte[] source, int length)
        {
            MemoryStream memoryStream = (MemoryStream)null;
            ZOutputStream zoutputStream = (ZOutputStream)null;
            try
            {
                byte[] buffer = new byte[length];
                memoryStream = new MemoryStream(buffer);
                zoutputStream = new ZOutputStream((Stream)memoryStream);
                zoutputStream.Write(source, 0, source.Length);

                return buffer;
            }
            finally
            {
                if (zoutputStream != null)
                {
                    zoutputStream.Close();
                }
                if (memoryStream != null)
                {
                    memoryStream.Close();
                }
            }
        }
    }
}
