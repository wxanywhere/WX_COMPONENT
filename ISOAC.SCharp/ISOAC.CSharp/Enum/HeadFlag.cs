using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISOA
{
    /// <summary>
    /// 
    /// </summary>
    public enum HeadFlag
    {
        /// <summary>
        /// BPACK_FAILED
        /// </summary>
        BPACK_FAILED = 0x0000000001,

        /// <summary>
        /// BPACK_CATCH_ERR
        /// </summary>
        BPACK_CATCH_ERR = BPACK_FAILED << 1,

        /// <summary>
        /// BPACK_ZIP
        /// </summary>
        BPACK_ZIP = BPACK_FAILED << 2,

        /// <summary>
        /// BPACK_CRYPTO
        /// </summary>
        BPACK_CRYPTO = BPACK_FAILED << 3,

        /// <summary>
        /// BPACK_MAC
        /// </summary>
        BPACK_MAC = BPACK_FAILED << 4,

        /// <summary>
        /// BPACK_NEXTREQ
        /// </summary>
        BPACK_NEXTREQ = BPACK_FAILED << 5,

        /// <summary>
        /// BPACK_NEXT
        /// </summary>
        BPACK_NEXT = BPACK_FAILED << 6,

        /// <summary>
        /// BPACK_SPORT
        /// </summary>
        BPACK_SPORT = BPACK_FAILED << 7,

        /// <summary>
        /// BPACK_DETECTOR
        /// </summary>
        BPACK_DETECTOR = BPACK_FAILED << 8,

        /// <summary>
        /// BPACK_ASYNC
        /// </summary>
        BPACK_ASYNC = BPACK_FAILED << 9,

        /// <summary>
        /// BPACK_SYNC
        /// </summary>
        BPACK_SYNC = BPACK_FAILED << 10,

        /// <summary>
        /// BPACK_REACT
        /// </summary>
        BPACK_REACT = BPACK_FAILED << 11,

        /// <summary>
        /// BPACK_BUSY
        /// </summary>
        BPACK_BUSY = BPACK_FAILED << 12,

        /// <summary>
        /// BPACK_MAXBIT
        /// </summary>
        BPACK_MAXBIT = BPACK_FAILED << 20,
    }
}
