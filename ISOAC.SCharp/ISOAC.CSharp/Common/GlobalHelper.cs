using System;
using System.Text;

namespace ISOA
{
    /// <summary>
    /// GlobalHelper
    /// </summary>
    public static class GlobalHelper
    {
        /// <summary>
        /// 编码均使用UTF8
        /// </summary>
        public static Encoding _DataEncoding = Encoding.UTF8;

        /// <summary>
        /// 头部编码
        /// </summary>
        public static Encoding _HeaderEncoding = Encoding.GetEncoding("gb2312");

        /// <summary>
        /// 错误编码
        /// </summary>
        public static Encoding _ErrorEncoding = Encoding.GetEncoding("gb2312");

        /// <summary>
        /// 数据编码
        /// </summary>
        public static Encoding DataEncoding
        {
            get
            {
                return _DataEncoding;
            }
            set
            {
                _DataEncoding = value;
            }
        }

        /// <summary>
        /// 头部编码
        /// </summary>
        public static Encoding HeaderEncoding
        {
            get
            {
                return _HeaderEncoding;
            }
            set
            {
                _HeaderEncoding = value;
            }
        }

        /// <summary>
        /// 错误编码
        /// </summary>
        public static Encoding ErrorEncoding
        {
            get
            {
                return _ErrorEncoding;
            }
            set
            {
                _ErrorEncoding = value;
            }
        }
    }
}
