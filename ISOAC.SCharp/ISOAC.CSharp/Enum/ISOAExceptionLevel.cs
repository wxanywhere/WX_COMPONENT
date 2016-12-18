using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISOA
{
    /// <summary>
    /// ISOA服务异常级别
    /// 
    /// </summary>
    public enum ISOAExceptionLevel
    {
        /// <summary>
        /// INFO
        /// </summary>
        INFO,

        /// <summary>
        /// ALARM
        /// </summary>
        ALARM,

        /// <summary>
        /// ERROR
        /// </summary>
        ERROR,

        /// <summary>
        /// FATAL
        /// </summary>
        FATAL,
    }
}
