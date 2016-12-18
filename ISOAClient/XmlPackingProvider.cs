using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ClientHelper;
using DataModelBase;

namespace ISOAClient
{
    public class XmlPackingProvider:IPackingProvider
    {
        /// <summary>
        /// 组织输入参数(将来应该使用XmlTextWriter)
        /// 
        /// </summary>
        /// <param name="serviceName">服务名称</param>
        /// <param name="inParams">输入参数</param>
        /// <returns>
        /// 转换后的输入参数
        /// </returns>
        public string OrganizeInput(string serviceName,params object[] inParams)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            sb.Append("<app id=\"\">");
            sb.Append("<svc id=\"" + serviceName + "\" act=\"request\" SVC_GUID=\"`\">");
            if (inParams != null)
            {
                foreach (var param in inParams)
                {
                    if (param is VOBase)
                    {
                        sb.Append("<param>");
                        this.GetXmlStr(sb, param as VOBase);
                        sb.Append("</param>");
                    }
                    else if (param is IEnumerable<VOBase>)
                    {
                        sb.Append("<param>");

                        foreach (var entity in param as IEnumerable<VOBase>)
                        {
                            this.GetXmlStr(sb, entity);
                        }
                        sb.Append("</param>");
                    }
                }
            }
            sb.Append("</svc>");
            sb.Append("</app>");
            return sb.ToString();
        }

        /// <summary>
        /// 组织输出数据集
        /// 
        /// </summary>
        /// <param name="serviceName">服务名称</param
        /// ><param name="outputParm">输入参数</param>
        /// <returns>
        /// 数据集
        /// </returns>
        public Tuple<T1, T2> OrganizeOutput<T1, T2>(byte[] reactBytes)
            where T1 : VOBase, new()
            where T2 : VOBase, new()
        {
            var hashSet = new HashSet<string>();
            var sysHead = (T1)null;
            var sysError = (T2)null;
            using (Stream input = new MemoryStream(reactBytes))
            {
                using (XmlTextReader textReader = new XmlTextReader(input))
                {
                    while (textReader.Read())
                    {
                        if (textReader.NodeType == XmlNodeType.Element)
                        {
                            switch (textReader.LocalName)
                            {
                                case "app":
                                case "svc":
                                case "param":
                                    continue;
                                default:
                                    if (!hashSet.Contains(textReader.LocalName))
                                    {
                                        hashSet.Add(textReader.LocalName);
                                        if (typeof(T1).Name == ConfigHelper.ENTITY_PROFIX + textReader.LocalName)
                                        {
                                            sysHead = new T1();
                                            this.PackToVO(textReader, sysHead);
                                        }
                                        else if (typeof(T2).Name == ConfigHelper.ENTITY_PROFIX + textReader.LocalName)
                                        {
                                            sysError = new T2();
                                            this.PackToVO(textReader, sysError);
                                        }

                                    }
                                    else
                                    {

                                    }
                                    continue;
                            }
                        }
                    }
                }
            }

            return new Tuple<T1, T2>(sysHead, sysError);

        }

        /// <summary>
        /// 组织输出数据集
        /// 
        /// </summary>
        /// <param name="serviceName">服务名称</param
        /// ><param name="outputParm">输入参数</param>
        /// <returns>
        /// 数据集
        /// </returns>
        public Tuple<T1, T2, T3[]> OrganizeOutput<T1, T2, T3>(byte[] reactBytes)
            where T1 : VOBase, new()
            where T2 : VOBase, new()
            where T3 : VOBase, new()
        {
            var hashSet = new HashSet<string>();
            var sysHead = (T1)null;
            var sysError = (T2)null;
            var entityA = (T3)null;
            var entitiesA = new List<T3>();
            using (Stream input = new MemoryStream(reactBytes))
            {
                using (XmlTextReader textReader = new XmlTextReader(input))
                {
                    while (textReader.Read())
                    {
                        if (textReader.NodeType == XmlNodeType.Element)
                        {
                            switch (textReader.LocalName)
                            {
                                case "app":
                                case "svc":
                                case "param":
                                    continue;
                                default:
                                    if (!hashSet.Contains(textReader.LocalName))
                                    {
                                        hashSet.Add(textReader.LocalName);
                                        if (typeof(T1).Name == ConfigHelper.ENTITY_PROFIX + textReader.LocalName)
                                        {
                                            sysHead = new T1();
                                            this.PackToVO(textReader, sysHead);
                                        }
                                        else if (typeof(T2).Name == ConfigHelper.ENTITY_PROFIX + textReader.LocalName)
                                        {
                                            sysError = new T2();
                                            this.PackToVO(textReader, sysError);
                                        }
                                        else if (typeof(T3).Name == ConfigHelper.ENTITY_PROFIX + textReader.LocalName)
                                        {
                                            entityA = new T3();
                                            this.PackToVO(textReader, entityA);
                                            entitiesA.Add(entityA);
                                        }

                                    }
                                    else
                                    {
                                        if (typeof(T3).Name == ConfigHelper.ENTITY_PROFIX + textReader.LocalName)
                                        {
                                            entityA = new T3();
                                            this.PackToVO(textReader, entityA);
                                            entitiesA.Add(entityA);
                                        }
                                    }
                                    continue;
                            }
                        }
                    }
                }
            }

            return new Tuple<T1, T2, T3[]>(sysHead, sysError, entitiesA.ToArray());

        }


        public Tuple<T1, T2, T3[], T4[]> OrganizeOutput<T1, T2, T3, T4>(byte[] reactBytes)
            where T1 : VOBase, new()
            where T2 : VOBase, new()
            where T3 : VOBase, new()
            where T4 : VOBase, new()
        {
            var hashSet = new HashSet<string>();
            var sysHead = (T1)null;
            var sysError = (T2)null;
            var entityA = (T3)null;
            var entityB = (T4)null;
            var entitiesA = new List<T3>();
            var entitiesB = new List<T4>();
            using (Stream input = new MemoryStream(reactBytes))
            {
                using (XmlTextReader textReader = new XmlTextReader(input))
                {
                    while (textReader.Read())
                    {
                        if (textReader.NodeType == XmlNodeType.Element)
                        {
                            switch (textReader.LocalName)
                            {
                                case "app":
                                case "svc":
                                case "param":
                                    continue;
                                default:
                                    if (!hashSet.Contains(textReader.LocalName))
                                    {
                                        hashSet.Add(textReader.LocalName);
                                        if (typeof(T1).Name == ConfigHelper.ENTITY_PROFIX + textReader.LocalName)
                                        {
                                            sysHead = new T1();
                                            this.PackToVO(textReader, sysHead);
                                        }
                                        else if (typeof(T2).Name == ConfigHelper.ENTITY_PROFIX + textReader.LocalName)
                                        {
                                            sysError = new T2();
                                            this.PackToVO(textReader, sysError);
                                        }
                                        else if (typeof(T3).Name == ConfigHelper.ENTITY_PROFIX + textReader.LocalName)
                                        {
                                            entityA = new T3();
                                            this.PackToVO(textReader, entityA);
                                            entitiesA.Add(entityA);
                                        }
                                        else if (typeof(T4).Name == ConfigHelper.ENTITY_PROFIX + textReader.LocalName)
                                        {
                                            entityB = new T4();
                                            this.PackToVO(textReader, entityB);
                                            entitiesB.Add(entityB);
                                        }
                                    }
                                    else
                                    {
                                        if (typeof(T3).Name == ConfigHelper.ENTITY_PROFIX + textReader.LocalName)
                                        {
                                            entityA = new T3();
                                            this.PackToVO(textReader, entityA);
                                            entitiesA.Add(entityA);
                                        }
                                        else if (typeof(T4).Name == ConfigHelper.ENTITY_PROFIX + textReader.LocalName)
                                        {
                                            entityB = new T4();
                                            this.PackToVO(textReader, entityB);
                                            entitiesB.Add(entityB);
                                        }

                                    }
                                    continue;
                            }
                        }
                    }
                }
            }

            return new Tuple<T1, T2, T3[],T4[]>(sysHead, sysError, entitiesA.ToArray(),entitiesB.ToArray());
        }

        public Tuple<T1, T2, T3[], T4[], T5[]> OrganizeOutput<T1, T2, T3, T4,T5>(byte[] reactBytes)
            where T1 : VOBase, new()
            where T2 : VOBase, new()
            where T3 : VOBase, new()
            where T4 : VOBase, new()
            where T5 : VOBase, new()
        {
            var hashSet = new HashSet<string>();
            var sysHead = (T1)null;
            var sysError = (T2)null;
            var entityA = (T3)null;
            var entityB = (T4)null;
            var entityC = (T5)null;
            var entitiesA = new List<T3>();
            var entitiesB = new List<T4>();
            var entitiesC = new List<T5>();
            using (Stream input = new MemoryStream(reactBytes))
            {
                using (XmlTextReader textReader = new XmlTextReader(input))
                {
                    while (textReader.Read())
                    {
                        if (textReader.NodeType == XmlNodeType.Element)
                        {
                            switch (textReader.LocalName)
                            {
                                case "app":
                                case "svc":
                                case "param":
                                    continue;
                                default:
                                    if (!hashSet.Contains(textReader.LocalName))
                                    {
                                        hashSet.Add(textReader.LocalName);
                                        if (typeof(T1).Name == ConfigHelper.ENTITY_PROFIX + textReader.LocalName)
                                        {
                                            sysHead = new T1();
                                            this.PackToVO(textReader, sysHead);
                                        }
                                        else if (typeof(T2).Name == ConfigHelper.ENTITY_PROFIX + textReader.LocalName)
                                        {
                                            sysError = new T2();
                                            this.PackToVO(textReader, sysError);
                                        }
                                        else if (typeof(T3).Name == ConfigHelper.ENTITY_PROFIX + textReader.LocalName)
                                        {
                                            entityA = new T3();
                                            this.PackToVO(textReader, entityA);
                                            entitiesA.Add(entityA);
                                        }
                                        else if (typeof(T4).Name == ConfigHelper.ENTITY_PROFIX + textReader.LocalName)
                                        {
                                            entityB = new T4();
                                            this.PackToVO(textReader, entityB);
                                            entitiesB.Add(entityB);
                                        }
                                        else if (typeof(T5).Name == ConfigHelper.ENTITY_PROFIX + textReader.LocalName)
                                        {
                                            entityC = new T5();
                                            this.PackToVO(textReader, entityC);
                                            entitiesC.Add(entityC);
                                        }
                                    }
                                    else
                                    {
                                        if (typeof(T3).Name == ConfigHelper.ENTITY_PROFIX + textReader.LocalName)
                                        {
                                            entityA = new T3();
                                            this.PackToVO(textReader, entityA);
                                            entitiesA.Add(entityA);
                                        }
                                        else if (typeof(T4).Name == ConfigHelper.ENTITY_PROFIX + textReader.LocalName)
                                        {
                                            entityB = new T4();
                                            this.PackToVO(textReader, entityB);
                                            entitiesB.Add(entityB);
                                        }
                                        else if (typeof(T5).Name == ConfigHelper.ENTITY_PROFIX + textReader.LocalName)
                                        {
                                            entityC = new T5();
                                            this.PackToVO(textReader, entityC);
                                            entitiesC.Add(entityC);
                                        }

                                    }
                                    continue;
                            }
                        }
                    }
                }
            }

            return new Tuple<T1, T2, T3[], T4[], T5[]>(sysHead, sysError, entitiesA.ToArray(), entitiesB.ToArray(),entitiesC.ToArray());
        }

        public Tuple<T1, T2, T3[], T4[], T5[], T6[]> OrganizeOutput<T1, T2, T3, T4, T5, T6>(byte[] reactBytes)
            where T1 : VOBase, new()
            where T2 : VOBase, new()
            where T3 : VOBase, new()
            where T4 : VOBase, new()
            where T5 : VOBase, new()
            where T6 : VOBase, new()
        {
            var hashSet = new HashSet<string>();
            var sysHead = (T1)null;
            var sysError = (T2)null;
            var entityA = (T3)null;
            var entityB = (T4)null;
            var entityC = (T5)null;
            var entityD = (T6)null;
            var entitiesA = new List<T3>();
            var entitiesB = new List<T4>();
            var entitiesC = new List<T5>();
            var entitiesD = new List<T6>();
            using (Stream input = new MemoryStream(reactBytes))
            {
                using (XmlTextReader textReader = new XmlTextReader(input))
                {
                    while (textReader.Read())
                    {
                        if (textReader.NodeType == XmlNodeType.Element)
                        {
                            switch (textReader.LocalName)
                            {
                                case "app":
                                case "svc":
                                case "param":
                                    continue;
                                default:
                                    if (!hashSet.Contains(textReader.LocalName))
                                    {
                                        hashSet.Add(textReader.LocalName);
                                        if (typeof(T1).Name == ConfigHelper.ENTITY_PROFIX + textReader.LocalName)
                                        {
                                            sysHead = new T1();
                                            this.PackToVO(textReader, sysHead);
                                        }
                                        else if (typeof(T2).Name == ConfigHelper.ENTITY_PROFIX + textReader.LocalName)
                                        {
                                            sysError = new T2();
                                            this.PackToVO(textReader, sysError);
                                        }
                                        else if (typeof(T3).Name == ConfigHelper.ENTITY_PROFIX + textReader.LocalName)
                                        {
                                            entityA = new T3();
                                            this.PackToVO(textReader, entityA);
                                            entitiesA.Add(entityA);
                                        }
                                        else if (typeof(T4).Name == ConfigHelper.ENTITY_PROFIX + textReader.LocalName)
                                        {
                                            entityB = new T4();
                                            this.PackToVO(textReader, entityB);
                                            entitiesB.Add(entityB);
                                        }
                                        else if (typeof(T5).Name == ConfigHelper.ENTITY_PROFIX + textReader.LocalName)
                                        {
                                            entityC = new T5();
                                            this.PackToVO(textReader, entityC);
                                            entitiesC.Add(entityC);
                                        }
                                        else if (typeof(T6).Name == ConfigHelper.ENTITY_PROFIX + textReader.LocalName)
                                        {
                                            entityD = new T6();
                                            this.PackToVO(textReader, entityD);
                                            entitiesD.Add(entityD);
                                        }
                                    }
                                    else
                                    {
                                        if (typeof(T3).Name == ConfigHelper.ENTITY_PROFIX + textReader.LocalName)
                                        {
                                            entityA = new T3();
                                            this.PackToVO(textReader, entityA);
                                            entitiesA.Add(entityA);
                                        }
                                        else if (typeof(T4).Name == ConfigHelper.ENTITY_PROFIX + textReader.LocalName)
                                        {
                                            entityB = new T4();
                                            this.PackToVO(textReader, entityB);
                                            entitiesB.Add(entityB);
                                        }
                                        else if (typeof(T5).Name == ConfigHelper.ENTITY_PROFIX + textReader.LocalName)
                                        {
                                            entityC = new T5();
                                            this.PackToVO(textReader, entityC);
                                            entitiesC.Add(entityC);
                                        }
                                        else if (typeof(T6).Name == ConfigHelper.ENTITY_PROFIX + textReader.LocalName)
                                        {
                                            entityD = new T6();
                                            this.PackToVO(textReader, entityD);
                                            entitiesD.Add(entityD);
                                        }

                                    }
                                    continue;
                            }
                        }
                    }
                }
            }

            return new Tuple<T1, T2, T3[], T4[], T5[], T6[]>(sysHead, sysError, entitiesA.ToArray(), entitiesB.ToArray(), entitiesC.ToArray(),entitiesD.ToArray());
        }

        public Tuple<T1, T2, T3[], T4[], T5[], T6[], T7[]> OrganizeOutput<T1, T2, T3, T4, T5, T6, T7>(byte[] reactBytes)
            where T1 : VOBase, new()
            where T2 : VOBase, new()
            where T3 : VOBase, new()
            where T4 : VOBase, new()
            where T5 : VOBase, new()
            where T6 : VOBase, new()
            where T7 : VOBase, new()
        {
            var hashSet = new HashSet<string>();
            var sysHead = (T1)null;
            var sysError = (T2)null;
            var entityA = (T3)null;
            var entityB = (T4)null;
            var entityC = (T5)null;
            var entityD = (T6)null;
            var entityE = (T7)null;
            var entitiesA = new List<T3>();
            var entitiesB = new List<T4>();
            var entitiesC = new List<T5>();
            var entitiesD = new List<T6>();
            var entitiesE = new List<T7>();
            using (Stream input = new MemoryStream(reactBytes))
            {
                using (XmlTextReader textReader = new XmlTextReader(input))
                {
                    while (textReader.Read())
                    {
                        if (textReader.NodeType == XmlNodeType.Element)
                        {
                            switch (textReader.LocalName)
                            {
                                case "app":
                                case "svc":
                                case "param":
                                    continue;
                                default:
                                    if (!hashSet.Contains(textReader.LocalName))
                                    {
                                        hashSet.Add(textReader.LocalName);
                                        if (typeof(T1).Name == ConfigHelper.ENTITY_PROFIX + textReader.LocalName)
                                        {
                                            sysHead = new T1();
                                            this.PackToVO(textReader, sysHead);
                                        }
                                        else if (typeof(T2).Name == ConfigHelper.ENTITY_PROFIX + textReader.LocalName)
                                        {
                                            sysError = new T2();
                                            this.PackToVO(textReader, sysError);
                                        }
                                        else if (typeof(T3).Name == ConfigHelper.ENTITY_PROFIX + textReader.LocalName)
                                        {
                                            entityA = new T3();
                                            this.PackToVO(textReader, entityA);
                                            entitiesA.Add(entityA);
                                        }
                                        else if (typeof(T4).Name == ConfigHelper.ENTITY_PROFIX + textReader.LocalName)
                                        {
                                            entityB = new T4();
                                            this.PackToVO(textReader, entityB);
                                            entitiesB.Add(entityB);
                                        }
                                        else if (typeof(T5).Name == ConfigHelper.ENTITY_PROFIX + textReader.LocalName)
                                        {
                                            entityC = new T5();
                                            this.PackToVO(textReader, entityC);
                                            entitiesC.Add(entityC);
                                        }
                                        else if (typeof(T6).Name == ConfigHelper.ENTITY_PROFIX + textReader.LocalName)
                                        {
                                            entityD = new T6();
                                            this.PackToVO(textReader, entityD);
                                            entitiesD.Add(entityD);
                                        }
                                        else if (typeof(T7).Name == ConfigHelper.ENTITY_PROFIX + textReader.LocalName)
                                        {
                                            entityE = new T7();
                                            this.PackToVO(textReader, entityE);
                                            entitiesE.Add(entityE);
                                        }
                                    }
                                    else
                                    {
                                        if (typeof(T3).Name == ConfigHelper.ENTITY_PROFIX + textReader.LocalName)
                                        {
                                            entityA = new T3();
                                            this.PackToVO(textReader, entityA);
                                            entitiesA.Add(entityA);
                                        }
                                        else if (typeof(T4).Name == ConfigHelper.ENTITY_PROFIX + textReader.LocalName)
                                        {
                                            entityB = new T4();
                                            this.PackToVO(textReader, entityB);
                                            entitiesB.Add(entityB);
                                        }
                                        else if (typeof(T5).Name == ConfigHelper.ENTITY_PROFIX + textReader.LocalName)
                                        {
                                            entityC = new T5();
                                            this.PackToVO(textReader, entityC);
                                            entitiesC.Add(entityC);
                                        }
                                        else if (typeof(T6).Name == ConfigHelper.ENTITY_PROFIX + textReader.LocalName)
                                        {
                                            entityD = new T6();
                                            this.PackToVO(textReader, entityD);
                                            entitiesD.Add(entityD);
                                        }
                                        else if (typeof(T7).Name == ConfigHelper.ENTITY_PROFIX + textReader.LocalName)
                                        {
                                            entityE = new T7();
                                            this.PackToVO(textReader, entityE);
                                            entitiesE.Add(entityE);
                                        }
                                    }
                                    continue;
                            }
                        }
                    }
                }
            }

            return new Tuple<T1, T2, T3[], T4[], T5[], T6[], T7[]>(sysHead, sysError, entitiesA.ToArray(), entitiesB.ToArray(), entitiesC.ToArray(), entitiesD.ToArray(),entitiesE.ToArray());
        }

        /// <summary>
        /// 解包成实体
        /// 
        /// </summary>
        /// <param name="textReader">xml读取器</param>
        /// <param name="entityInfo">实体信息</param>
        private void PackToVO(XmlTextReader textReader, VOBase entity)
        {
            while (textReader.MoveToNextAttribute())
            {
                var field = entity.GetFieldInfo(textReader.LocalName);
                var s = ConvertXMLSpecialStr(textReader.Value);
                switch (Type.GetTypeCode(field.DataType))
                {
                    case TypeCode.Decimal:
                        field.Value = Decimal.Parse(s);
                        continue;
                    case TypeCode.String:
                        field.Value = "`".Equals(s) ? string.Empty : s;
                        continue;
                }
            }
        }

        /// <summary>
        /// 实体对象转换成Http字符串
        /// 
        /// </summary>
        /// <param name="sb">需要拼接的输入参数</param><param name="entity">实体接口</param>
        private void GetXmlStr(StringBuilder sb, VOBase entity)
        {
            sb.Append("<" + entity.GetType().Name.Replace(ConfigHelper.ENTITY_PROFIX, "") + " ");
            foreach(var field in entity.VOFieldInfos)
            {
                switch (Type.GetTypeCode(field.DataType))
                {
                    case TypeCode.Decimal:
                        if (field.Value == null)
                        {
                            sb.Append(field.Name + "=\"0\" ");
                        }
                        else
                        {
                            sb.Append(field.Name + "=\"" + field.Value.ToString() + "\" ");
                        }
                        continue;
                    case TypeCode.String:
                        if (field.Value == null || string.IsNullOrWhiteSpace(field.Value.ToString()))
                        {
                            sb.Append(field.Name + "=\"`\" ");
                        }
                        else
                        {
                            sb.Append(field.Name + "=\"" + ConvertToXMLSpecialStr(field.Value.ToString()) + "\" ");
                        }
                        continue;
                }
            }
            sb.Append("/>");
        }

        /// <summary>
        /// 转换XML的特殊字符
        /// 
        /// </summary>
        /// <param name="str">需要转换的字符串</param>
        /// <returns>
        /// 转换后的字符串
        /// </returns>
        private string ConvertXMLSpecialStr(string value)
        {
            return value.Replace("&amp;", "&").Replace("&quot;", "\"").Replace("&lt;", "<").Replace("&gt;", ">").Replace("NULL", "");
        }

        /// <summary>
        /// 转换成XML的特殊字符
        /// 
        /// </summary>
        /// <param name="value">需要转换的字符串</param>
        /// <returns>
        /// 转换后的字符串
        /// </returns>
        private string ConvertToXMLSpecialStr(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return "`";
            else
                return value.Replace("&", "&amp;").Replace("\"", "&quot;").Replace("<", "&lt;").Replace(">", "&gt;");
        }
    }
}
