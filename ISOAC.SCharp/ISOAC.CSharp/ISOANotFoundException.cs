using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISOA
{
  /// <summary>
  /// 服务查询没有结果集异常
  /// 
  /// </summary>
  public class ISOANotFoundException : ISOAException
  {
    /// <summary>
    /// 没找到的编号
    /// 
    /// </summary>
    public const int NotFoundSqlCode = 1403;

    /// <summary>
    /// 构造函数
    /// 
    /// </summary>
    public ISOANotFoundException()
    {
      this.ExceptionCode = 1403;
    }

    /// <summary>
    /// 构造函数
    /// 
    /// </summary>
    /// <param name="message">异常消息</param>
    public ISOANotFoundException(string message)
    {
    }

    /// <summary>
    /// 构造函数
    /// 
    /// </summary>
    /// <param name="message">异常消息</param><param name="exceptionLevel">异常级别</param>
    public ISOANotFoundException(string message, ISOAExceptionLevel exceptionLevel)
    {
      this.ExceptionLevel = exceptionLevel;
    }

    /// <summary>
    /// 构造函数
    /// 
    /// </summary>
    /// <param name="message">异常消息</param>
    /// <param name="exceptionLevel">异常级别</param>
    /// <param name="innerException">内部异常</param>
    public ISOANotFoundException(string message, ISOAExceptionLevel exceptionLevel, Exception innerException)
    {
    }
  }
}
