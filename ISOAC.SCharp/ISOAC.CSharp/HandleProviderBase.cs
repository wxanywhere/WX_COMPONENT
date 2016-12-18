using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISOA
{
    /// <summary>
    /// 句柄提供器基类
    /// </summary>
    public abstract class HandleProviderBase
    {
        /// <summary>
        /// 取得HandleProvide子类实例
        /// </summary>
        /// <typeparam name="T">具体的HandleProvide子类类型</typeparam>
        /// <returns>具体的HandleProvide子类类型</returns>
        public virtual T RetrieveProvider<T>() where T : HandleProviderBase,new()
        {
            return this as T;
        }
    }
}
