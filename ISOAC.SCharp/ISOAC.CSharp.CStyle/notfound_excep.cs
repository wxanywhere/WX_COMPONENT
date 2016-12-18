using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ydtf.isoa
{
  /// <summary>
  /// 服务查询没有结果集异常
  /// 
  /// </summary>
  public class notfound_excep : excep
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
    public notfound_excep()
    {
      this.ExceptionCode = 1403;
    }

    /// <summary>
    /// 构造函数
    /// 
    /// </summary>
    /// <param name="message">异常消息</param>
    public notfound_excep(string message)
    {
    }

    /// <summary>
    /// 构造函数
    /// 
    /// </summary>
    /// <param name="message">异常消息</param><param name="exceptionLevel">异常级别</param>
    public notfound_excep(string message, excep_level exceptionLevel)
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
    public notfound_excep(string message, excep_level exceptionLevel, Exception innerException)
    {
    }
  }
}
