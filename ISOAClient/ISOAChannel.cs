using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.IO;
using System.Reflection;
using VO;
using System.Xml.Linq;
using VO;

namespace ISOAClient
{
    public static class ISOAChannel
    {
        /// <summary>
        /// 初始化头包和错误包
        /// </summary>
        static ISOAChannel()
        {
            ORMExpand.Common.SysHeader = new VO_SYS_HEAD(0, string.Empty);
            ORMExpand.Common.SysError = new VO_SYS_ERROR();
            ORMExpand.Common.EntityAssemblyName = typeof(VO_SYS_ERROR).Assembly.FullName; //"VO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"; //Assembly.GetExecutingAssembly().FullName;
        }

        /// <summary>
        /// 服务对象字典
        /// </summary>
        private static Dictionary<string, AServiceAdapter> protocolObject = new Dictionary<string, AServiceAdapter>();

        /// <summary>
        /// 调用后台服务(由配置文件指定数据交换格式)
        /// </summary>
        /// <typeparam name="T">DataSet 或者 VODictionary</typeparam>
        /// <param name="serviceName">服务名格式：系统名.模块名.服务名</param>
        /// <param name="inputValues">服务参数</param>
        /// <returns></returns>
        public static T CallService<T>(string serviceName, params object[] inputValues)
        {
            string[] Name = serviceName.Split('.');
            if (Name.Length != 3 || string.IsNullOrWhiteSpace(Name[2]))
            {
                string errorMsg = string.Format("服务名：{0}不符合三段式命名规格：系统名.模块名.服务名.", Name);
                throw new Exception(errorMsg);
            }

            //根据服务名查找协议
            Protocol protocol = ServiceAdapterConfig.GetProtocol(serviceName);
            if (protocol == null)
            {
                string errorMsg = string.Format("服务：{0}未配置调用协议", Name);
                throw new Exception(errorMsg);
            }

            //实例化协议对象           
            if (protocolObject.ContainsKey(protocol.Key) == false)
            {
                Assembly assembly = Assembly.Load(ORMExpand.Common.ORMAssemblyName);
                Type type = assembly.GetType(protocol.ClassName);
                AServiceAdapter Adapter = (AServiceAdapter)Activator.CreateInstance(type);
#if DEBUG
                Adapter.InPutParamDebugSet = new AServiceAdapter.InPutParam(InPutParam);
                Adapter.OutPutParamDebugSet = new AServiceAdapter.OutPutParam(OutPutParam);
#endif
                protocolObject.Add(protocol.Key, Adapter);
            }

            return protocolObject[protocol.Key].CallService<T>(serviceName, serviceName, inputValues); //临时处理，第一个参数原为GNID(功能Id)
        }
#if DEBUG

        /// <summary>
        /// 输入参数调试设置
        /// </summary>
        /// <param name="inPutStr"></param>
        public static void InPutParam(dynamic inPutStr)
        {
        }

        /// <summary>
        /// 输出参数调试设置
        /// </summary>
        /// <param name="outPutStr"></param>
        public static void OutPutParam(dynamic outPutStr)
        {
        }
#endif
    }
}

