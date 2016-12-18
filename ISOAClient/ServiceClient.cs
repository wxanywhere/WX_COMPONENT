using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using ISOA;
using DataModelBase;
using ClientHelper;

namespace ISOAClient
{
    public class ServiceClient
    {
        private static PackingProviderType _ProviderType = PackingProviderType.ValuePackingProvider;
        private string _OperationCodeFieldName = String.Empty;

        public ServiceClient()
        {
            InitializeEncoding();

            _OperationCodeFieldName = ConfigHelper.OPERATION_CODE_FIELD_NAME;

            if (ConfigHelper.PACKING_TYPE.ToLower().StartsWith("xml"))
            {
                _ProviderType = PackingProviderType.XmlPackingProvider;
            }
            else if (ConfigHelper.PACKING_TYPE.ToLower().StartsWith("json"))
            {
                _ProviderType = PackingProviderType.JsonPackingProvider;
            }
            else if (ConfigHelper.PACKING_TYPE.ToLower().StartsWith("value"))
            {
                _ProviderType = PackingProviderType.ValuePackingProvider;
            }

        }

        private static void InitializeEncoding()
        {
            GlobalHelper.DataEncoding = Encoding.GetEncoding(ConfigHelper.DATA_ENCODING);
            GlobalHelper.HeaderEncoding = Encoding.GetEncoding(ConfigHelper.HEADER_ENCODING);
            GlobalHelper.ErrorEncoding = Encoding.GetEncoding(ConfigHelper.ERROR_ENCODING);
        }

        public ServiceClient(PackingProviderType ProviderType,string operationCodeFieldName="")
        {
            if (ProviderType == PackingProviderType.ValuePackingProvider && String.IsNullOrWhiteSpace(operationCodeFieldName))
            {
                throw new Exception("流方式打包时须给出操作代码字段名！");
            }
            _ProviderType = ProviderType;
            _OperationCodeFieldName = operationCodeFieldName;
            InitializeEncoding();
        }

        public Tuple<T1, T2> CallService<T1, T2>(string serviceName, params object[] inParams)
            where T1 : VOBase, new()
            where T2 : VOBase, new()
        {
            var adapter = (PackingAdapter)null;
            var result = (Tuple<T1, T2>)null;
            try
            {
                switch (_ProviderType)
                {
                    case PackingProviderType.XmlPackingProvider:
                        adapter = new PackingAdapter(new XmlPackingProvider());
                        result = adapter.CallService<T1, T2>(ConfigHelper.URLS, serviceName, inParams);
                        break;
                    case PackingProviderType.JsonPackingProvider:
                        adapter = new PackingAdapter(new JsonPackingProvider(_OperationCodeFieldName));
                        result = adapter.CallService<T1, T2>(ConfigHelper.URLS, serviceName, inParams);
                        break;
                    case PackingProviderType.ValuePackingProvider:
                        adapter = new PackingAdapter(new ValuePackingProvider(_OperationCodeFieldName));
                        result = adapter.CallService<T1, T2>(ConfigHelper.URLS, serviceName, inParams);
                        break;
                    default:
                        break;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Tuple<T1,T2,T3[]> CallService<T1,T2,T3>(string serviceName, params object[] inParams) 
            where T1 : VOBase, new()
            where T2 : VOBase, new()
            where T3 : VOBase, new()
        {
            var adapter = (PackingAdapter)null;
            var result = (Tuple<T1, T2, T3[]>)null;
            try
            {
                switch (_ProviderType)
                {
                    case PackingProviderType.XmlPackingProvider:
                        adapter = new PackingAdapter(new XmlPackingProvider());
                        result = adapter.CallService<T1, T2, T3>(ConfigHelper.URLS, serviceName, inParams);
                        break;
                    case PackingProviderType.JsonPackingProvider:
                        adapter = new PackingAdapter(new JsonPackingProvider(_OperationCodeFieldName));
                        result = adapter.CallService<T1, T2, T3>(ConfigHelper.URLS, serviceName, inParams);
                        break;
                    case PackingProviderType.ValuePackingProvider:
                        adapter = new PackingAdapter(new ValuePackingProvider(_OperationCodeFieldName));
                        result = adapter.CallService<T1, T2, T3>(ConfigHelper.URLS, serviceName, inParams);
                        break;
                    default:
                        break;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Tuple<T1, T2, T3[], T4[]> CallService<T1, T2, T3, T4>(string serviceName, params object[] inParams)
            where T1 : VOBase, new()
            where T2 : VOBase, new()
            where T3 : VOBase, new()
            where T4 : VOBase, new()
        {
            var adapter = (PackingAdapter)null;
            var result = (Tuple<T1, T2, T3[],T4[]>)null;
            try
            {
                switch (_ProviderType)
                {
                    case PackingProviderType.XmlPackingProvider:
                        adapter = new PackingAdapter(new XmlPackingProvider());
                        result = adapter.CallService<T1, T2, T3, T4>(ConfigHelper.URLS, serviceName, inParams);
                        break;
                    case PackingProviderType.JsonPackingProvider:
                        adapter = new PackingAdapter(new JsonPackingProvider(_OperationCodeFieldName));
                        result = adapter.CallService<T1, T2, T3, T4>(ConfigHelper.URLS, serviceName, inParams);
                        break;
                    case PackingProviderType.ValuePackingProvider:
                        adapter = new PackingAdapter(new ValuePackingProvider(_OperationCodeFieldName));
                        result = adapter.CallService<T1, T2, T3, T4>(ConfigHelper.URLS, serviceName, inParams);
                        break;
                    default:
                        break;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Tuple<T1, T2, T3[], T4[], T5[]> CallService<T1, T2, T3, T4, T5>(string serviceName, params object[] inParams)
            where T1 : VOBase, new()
            where T2 : VOBase, new()
            where T3 : VOBase, new()
            where T4 : VOBase, new()
            where T5 : VOBase, new()
        {
            var adapter = (PackingAdapter)null;
            var result = (Tuple<T1, T2, T3[], T4[], T5[]>)null;
            try
            {
                switch (_ProviderType)
                {
                    case PackingProviderType.XmlPackingProvider:
                        adapter = new PackingAdapter(new XmlPackingProvider());
                        result = adapter.CallService<T1, T2, T3, T4, T5>(ConfigHelper.URLS, serviceName, inParams);
                        break;
                    case PackingProviderType.JsonPackingProvider:
                        adapter = new PackingAdapter(new JsonPackingProvider(_OperationCodeFieldName));
                        result = adapter.CallService<T1, T2, T3, T4, T5>(ConfigHelper.URLS, serviceName, inParams);
                        break;
                    case PackingProviderType.ValuePackingProvider:
                        adapter = new PackingAdapter(new ValuePackingProvider(_OperationCodeFieldName));
                        result = adapter.CallService<T1, T2, T3, T4, T5>(ConfigHelper.URLS, serviceName, inParams);
                        break;
                    default:
                        break;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Tuple<T1, T2, T3[], T4[], T5[], T6[]> CallService<T1, T2, T3, T4, T5, T6>(string serviceName, params object[] inParams)
            where T1 : VOBase, new()
            where T2 : VOBase, new()
            where T3 : VOBase, new()
            where T4 : VOBase, new()
            where T5 : VOBase, new()
            where T6 : VOBase, new()
        {
            var adapter = (PackingAdapter)null;
            var result = (Tuple<T1, T2, T3[], T4[], T5[], T6[]>)null;
            try
            {
                switch (_ProviderType)
                {
                    case PackingProviderType.XmlPackingProvider:
                        adapter = new PackingAdapter(new XmlPackingProvider());
                        result = adapter.CallService<T1, T2, T3, T4, T5, T6>(ConfigHelper.URLS, serviceName, inParams);
                        break;
                    case PackingProviderType.JsonPackingProvider:
                        adapter = new PackingAdapter(new JsonPackingProvider(_OperationCodeFieldName));
                        result = adapter.CallService<T1, T2, T3, T4, T5, T6>(ConfigHelper.URLS, serviceName, inParams);
                        break;
                    case PackingProviderType.ValuePackingProvider:
                        adapter = new PackingAdapter(new ValuePackingProvider(_OperationCodeFieldName));
                        result = adapter.CallService<T1, T2, T3, T4, T5, T6>(ConfigHelper.URLS, serviceName, inParams);
                        break;
                    default:
                        break;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Tuple<T1, T2, T3[], T4[], T5[], T6[], T7[]> CallService<T1, T2, T3, T4, T5, T6, T7>(string serviceName, params object[] inParams)
            where T1 : VOBase, new()
            where T2 : VOBase, new()
            where T3 : VOBase, new()
            where T4 : VOBase, new()
            where T5 : VOBase, new()
            where T6 : VOBase, new()
            where T7 : VOBase, new()
        {
            var adapter = (PackingAdapter)null;
            var result = (Tuple<T1, T2, T3[], T4[], T5[], T6[], T7[]>)null;
            try
            {
                switch (_ProviderType)
                {
                    case PackingProviderType.XmlPackingProvider:
                        adapter = new PackingAdapter(new XmlPackingProvider());
                        result = adapter.CallService<T1, T2, T3, T4, T5, T6, T7>(ConfigHelper.URLS, serviceName, inParams);
                        break;
                    case PackingProviderType.JsonPackingProvider:
                        adapter = new PackingAdapter(new JsonPackingProvider(_OperationCodeFieldName));
                        result = adapter.CallService<T1, T2, T3, T4, T5, T6, T7>(ConfigHelper.URLS, serviceName, inParams);
                        break;
                    case PackingProviderType.ValuePackingProvider:
                        adapter = new PackingAdapter(new ValuePackingProvider(_OperationCodeFieldName));
                        result = adapter.CallService<T1, T2, T3, T4, T5, T6, T7>(ConfigHelper.URLS, serviceName, inParams);
                        break;
                    default:
                        break;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
