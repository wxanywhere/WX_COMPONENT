using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISOA;
using DataModelBase;

namespace ISOAClient
{
    public class PackingAdapter:ICallService
    {
        private IPackingProvider _Provider = null;

        public PackingAdapter(IPackingProvider provider)
        {
            _Provider = provider;
        }

        public Tuple<T1, T2> CallService<T1, T2>(string urls, string serviceName, params object[] inParams)
            where T1 : VOBase, new()
            where T2 : VOBase, new()
        {
            try
            {
                var tcpStr = _Provider.OrganizeInput(serviceName, inParams);
                var requestData = DataHelper.GetBytesFromString(tcpStr, GlobalHelper.DataEncoding);
                var client = new ISOAC();
                var reactBytes = client.Call(urls, serviceName, requestData);
                var result = _Provider.OrganizeOutput<T1, T2>(reactBytes);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Tuple<T1, T2, T3[]> CallService<T1, T2, T3>(string urls, string serviceName, params object[] inParams)
            where T1:VOBase,new ()
            where T2 : VOBase, new()
            where T3 : VOBase, new()
        {
            try
            {
                var tcpStr =_Provider.OrganizeInput(serviceName,inParams);
                var requestData = DataHelper.GetBytesFromString(tcpStr,GlobalHelper.DataEncoding);
                var client = new ISOAC();
                var reactBytes = client.Call(urls, serviceName, requestData);
                var result = _Provider.OrganizeOutput<T1, T2, T3>(reactBytes);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Tuple<T1, T2, T3[], T4[]> CallService<T1, T2, T3, T4>(string urls, string serviceName, params object[] inParams)
            where T1:VOBase,new ()
            where T2 : VOBase, new()
            where T3 : VOBase, new()
            where T4 : VOBase, new()
        {
            try
            {
                var tcpStr = _Provider.OrganizeInput(serviceName,inParams);
                var requestData = DataHelper.GetBytesFromString(tcpStr, GlobalHelper.DataEncoding);
                var client = new ISOAC();
                var reactBytes = client.Call(urls, serviceName, requestData);
                var result = _Provider.OrganizeOutput<T1, T2, T3,T4>(reactBytes);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Tuple<T1, T2, T3[], T4[], T5[]> CallService<T1, T2, T3, T4, T5>(string urls, string serviceName, params object[] inParams)
            where T1 : VOBase, new()
            where T2 : VOBase, new()
            where T3 : VOBase, new()
            where T4 : VOBase, new()
            where T5 : VOBase, new()
        {
            try
            {
                var tcpStr = _Provider.OrganizeInput(serviceName, inParams);
                var requestData = DataHelper.GetBytesFromString(tcpStr, GlobalHelper.DataEncoding);
                var client = new ISOAC();
                var reactBytes = client.Call(urls, serviceName, requestData);
                var result = _Provider.OrganizeOutput<T1, T2, T3, T4, T5>(reactBytes);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Tuple<T1, T2, T3[], T4[], T5[], T6[]> CallService<T1, T2, T3, T4, T5, T6>(string urls, string serviceName, params object[] inParams)
            where T1 : VOBase, new()
            where T2 : VOBase, new()
            where T3 : VOBase, new()
            where T4 : VOBase, new()
            where T5 : VOBase, new()
            where T6 : VOBase, new()
        {
            try
            {
                var tcpStr = _Provider.OrganizeInput(serviceName, inParams);
                var requestData = DataHelper.GetBytesFromString(tcpStr, GlobalHelper.DataEncoding);
                var client = new ISOAC();
                var reactBytes = client.Call(urls, serviceName, requestData);
                var result = _Provider.OrganizeOutput<T1, T2, T3, T4, T5, T6>(reactBytes);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Tuple<T1, T2, T3[], T4[], T5[], T6[], T7[]> CallService<T1, T2, T3, T4, T5, T6, T7>(string urls, string serviceName, params object[] inParams)
            where T1 : VOBase, new()
            where T2 : VOBase, new()
            where T3 : VOBase, new()
            where T4 : VOBase, new()
            where T5 : VOBase, new()
            where T6 : VOBase, new()
            where T7 : VOBase, new()
        {
            try
            {
                var tcpStr = _Provider.OrganizeInput(serviceName, inParams);
                var requestData = DataHelper.GetBytesFromString(tcpStr, GlobalHelper.DataEncoding);
                var client = new ISOAC();
                var reactBytes = client.Call(urls, serviceName, requestData);
                var result = _Provider.OrganizeOutput<T1, T2, T3, T4, T5, T6, T7>(reactBytes);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
