using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISOA
{
  /// <summary>
  /// 服务返回异常基类
  /// 
  /// </summary>
  public class ISOAException : Exception
  {
    /// <summary>
    /// 异常代码
    /// 
    /// </summary>
    private int _exceptionCode;
    private ISOAExceptionLevel _exceptionLevel;
    private string _detailMessage;

    /// <summary>
    /// 异常代码，对应SYS_ERROR.SQLCODE,默认为-1
    /// 
    /// </summary>
    public int ExceptionCode
    {
      get
      {
        return this._exceptionCode;
      }
      protected set
      {
        this._exceptionCode = value;
      }
    }

    /// <summary>
    /// 异常级别，默认值为INFO
    /// 
    /// </summary>
    public ISOAExceptionLevel ExceptionLevel
    {
      get
      {
        return this._exceptionLevel;
      }
      protected set
      {
        this._exceptionLevel = value;
      }
    }

    /// <summary>
    /// 异常详细信息，对应SYS_ERROR.YCXM
    /// 
    /// </summary>
    public string DetailMessage
    {
      get
      {
        return this._detailMessage;
      }
      set
      {
        this._detailMessage = value;
      }
    }

    /// <summary>
    /// 构造函数
    /// 
    /// </summary>
    public ISOAException()
    {
      this._exceptionCode = -1;
      this._detailMessage = string.Empty;
    }

    /// <summary>
    /// 构造函数
    /// 
    /// </summary>
    /// <param name="message">异常消息</param>
    public ISOAException(string message)
    {
      this._exceptionCode = -1;
      this._detailMessage = string.Empty;
    }

    /// <summary>
    /// 构造函数
    /// 
    /// </summary>
    /// <param name="message">异常消息</param>
    /// <param name="errorCode">异常代码</param>
    public ISOAException(string message, int errorCode)
    {
      this._exceptionCode = -1;
      this._detailMessage = string.Empty;
      this._exceptionCode = errorCode;
    }

    /// <summary>
    /// 构造函数
    /// 
    /// </summary>
    /// <param name="message">异常消息</param>
    /// <param name="exceptionLevel">异常级别</param>
    public ISOAException(string message, ISOAExceptionLevel exceptionLevel)
    {
      this._exceptionCode = -1;
      this._detailMessage = string.Empty;
      this._exceptionLevel = exceptionLevel;
    }

    /// <summary>
    /// 构造函数
    /// 
    /// </summary>
    /// <param name="message">异常消息</param>
    /// <param name="exceptionCode">异常代码</param>
    /// <param name="exceptionLevel">异常级别</param>
    public ISOAException(string message, int exceptionCode, ISOAExceptionLevel exceptionLevel)
    {
      this._exceptionCode = -1;
      this._detailMessage = string.Empty;
      this._exceptionCode = exceptionCode;
      this._exceptionLevel = exceptionLevel;
    }

    /// <summary>
    /// 构造函数
    /// 
    /// </summary>
    /// <param name="message">异常消息</param>
    /// <param name="exceptionLevel">异常级别</param>
    /// <param name="innerException">内部异常</param>
    public ISOAException(string message, ISOAExceptionLevel exceptionLevel, Exception innerException)
    {
      this._exceptionCode = -1;
      this._detailMessage = string.Empty;
      this._exceptionLevel = exceptionLevel;
    }

    /// <summary>
    /// 构造函数
    /// 
    /// </summary>
    /// <param name="message">异常消息</param>
    /// <param name="exceptionCode">异常代码</param>
    /// <param name="exceptionLevel">异常级别</param>
    /// <param name="innerException">内部异常</param>
    public ISOAException(string message, int exceptionCode, ISOAExceptionLevel exceptionLevel, Exception innerException)
    {
      this._exceptionCode = -1;
      this._detailMessage = string.Empty;
      this._exceptionCode = exceptionCode;
      this._exceptionLevel = exceptionLevel;
    }

    /// <summary/>
    /// 
    /// <returns/>
    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat("ExceptionCode:{0}{1}", (object) this.ExceptionCode, (object) Environment.NewLine);
      stringBuilder.AppendFormat("ExceptionLevel:{0}{1}", (object) ((object) this.ExceptionLevel).ToString(), (object) Environment.NewLine);
      stringBuilder.AppendFormat("DetailMessage:{0}{1}", (object) this.DetailMessage, (object) Environment.NewLine);
      stringBuilder.Append(base.ToString());
      return ((object) stringBuilder).ToString();
    }
  }
}
