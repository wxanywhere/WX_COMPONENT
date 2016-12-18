using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISOA;
using DataModelBase;

namespace ISOAClient
{
    public class JsonPackingProvider : IPackingProvider
    {
        private string _OperationCodeFieldName = String.Empty;
        public JsonPackingProvider(string operationCodeFieldName)
        {
            _OperationCodeFieldName = operationCodeFieldName;
        }
        public string OrganizeInput(string serviceName,params object[] inParams)
        {
            var jarr=new JArray();
            var jobjTem = (JObject)null;
            var jarrTem = (JArray)null;
            if (inParams != null)
            {
                foreach (var param in inParams)
                {
                    if (param is VOBase)
                    {
                        jobjTem = new JObject();
                        this.GetJsonContent(jobjTem, param as VOBase);
                        jarr.Add(jobjTem);
                    }
                    else if (param is IEnumerable<VOBase>)
                    {
                        jarrTem = new JArray();
                        foreach (var entity in param as IEnumerable<VOBase>)
                        {
                            jobjTem = new JObject();
                            this.GetJsonContent(jobjTem, entity);
                            jarrTem.Add(jobjTem);
                        }
                        jarr.Add(jarrTem);
                    }
                }
            }

            return jarr.ToString();
        }

        public Tuple<T1, T2> OrganizeOutput<T1, T2>(byte[] reactBytes)
            where T1 : VOBase, new()
            where T2 : VOBase, new()
        {
            var sysHead = (T1)null;
            var sysError = (T2)null;
            using (var reader = new JsonTextReader(new StringReader(GlobalHelper.DataEncoding.GetString(reactBytes))))
            {
                reader.Read();
                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        sysHead = new T1();
                        this.PackToVO(reader, sysHead);
                    }
                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        sysError = new T2();
                        this.PackToVO(reader, sysError);
                    }
                }
            }

            return new Tuple<T1, T2>(sysHead, sysError);
        }

        public Tuple<T1, T2, T3[]> OrganizeOutput<T1, T2, T3>(byte[] reactBytes)
            where T1 : VOBase, new()
            where T2 : VOBase, new()
            where T3 : VOBase, new()
        {
            var sysHead = (T1)null;
            var sysError = (T2)null;
            var entityA = (T3)null;
            var entitiesA = new List<T3>();
            using (var reader = new JsonTextReader(new StringReader(GlobalHelper.DataEncoding.GetString(reactBytes))))
            {
                reader.Read();
                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        sysHead = new T1();
                        this.PackToVO(reader, sysHead);
                    }
                    if (sysHead == null || sysHead.GetFieldInfo(_OperationCodeFieldName) == null)
                    {
                      throw new Exception("解包系统头失败！");
                    }
                    if (Convert.ToInt32(sysHead.GetFieldInfo(_OperationCodeFieldName).Value) == 0)
                    {
                      if (reader.TokenType == JsonToken.StartArray)
                      {
                        reader.Read();
                        while (reader.TokenType == JsonToken.StartObject)
                        {
                          entityA = new T3();
                          this.PackToVO(reader, entityA);
                          entitiesA.Add(entityA);
                        }
                      }
                    }
                    else
                    {
                      if (reader.TokenType == JsonToken.StartObject)
                      {
                        sysError = new T2();
                        this.PackToVO(reader, sysError);
                        if (sysError == null)
                        {
                          throw new Exception("解包错误失败！");
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

            var sysHead = (T1)null;
            var sysError = (T2)null;
            var entityA = (T3)null;
            var entityB = (T4)null;
            var entitiesA = new List<T3>();
            var entitiesB = new List<T4>();
            using (var reader = new JsonTextReader(new StringReader(GlobalHelper.DataEncoding.GetString(reactBytes))))
            {
                reader.Read();
                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        sysHead = new T1();
                        this.PackToVO(reader, sysHead);
                    }
                    if (sysHead == null || sysHead.GetFieldInfo(_OperationCodeFieldName) == null)
                    {
                      throw new Exception("解包系统头失败！");
                    }
                    if (Convert.ToInt32(sysHead.GetFieldInfo(_OperationCodeFieldName).Value) == 0)
                    {
                      if (reader.TokenType == JsonToken.StartArray)
                      {
                        reader.Read();
                        while (reader.TokenType == JsonToken.StartObject)
                        {
                          entityA = new T3();
                          this.PackToVO(reader, entityA);
                          entitiesA.Add(entityA);
                        }
                        reader.Read();
                      }
                      if (reader.TokenType == JsonToken.StartArray)
                      {
                        reader.Read();
                        while (reader.TokenType == JsonToken.StartObject)
                        {
                          entityB = new T4();
                          this.PackToVO(reader, entityB);
                          entitiesB.Add(entityB);
                        }
                      }
                    }
                    else
                    {
                      if (reader.TokenType == JsonToken.StartObject)
                      {
                        sysError = new T2();
                        this.PackToVO(reader, sysError);
                        if (sysError == null)
                        {
                          throw new Exception("解包错误失败！");
                        }
                      }
                    }
                }
            }

            return new Tuple<T1, T2, T3[],T4[]>(sysHead, sysError, entitiesA.ToArray(),entitiesB.ToArray());
        }

        public Tuple<T1, T2, T3[], T4[], T5[]> OrganizeOutput<T1, T2, T3, T4, T5>(byte[] reactBytes)
            where T1 : VOBase, new()
            where T2 : VOBase, new()
            where T3 : VOBase, new()
            where T4 : VOBase, new()
            where T5 : VOBase, new()
        {

            var sysHead = (T1)null;
            var sysError = (T2)null;
            var entityA = (T3)null;
            var entityB = (T4)null;
            var entityC = (T5)null;
            var entitiesA = new List<T3>();
            var entitiesB = new List<T4>();
            var entitiesC = new List<T5>();
            using (var reader = new JsonTextReader(new StringReader(GlobalHelper.DataEncoding.GetString(reactBytes))))
            {
                reader.Read();
                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        sysHead = new T1();
                        this.PackToVO(reader, sysHead);
                    }
                    if (sysHead == null || sysHead.GetFieldInfo(_OperationCodeFieldName) == null)
                    {
                      throw new Exception("解包系统头失败！");
                    }
                    if (Convert.ToInt32(sysHead.GetFieldInfo(_OperationCodeFieldName).Value) == 0)
                    {
                      if (reader.TokenType == JsonToken.StartArray)
                      {
                        reader.Read();
                        while (reader.TokenType == JsonToken.StartObject)
                        {
                          entityA = new T3();
                          this.PackToVO(reader, entityA);
                          entitiesA.Add(entityA);
                        }
                        reader.Read();
                      }
                      if (reader.TokenType == JsonToken.StartArray)
                      {
                        reader.Read();
                        while (reader.TokenType == JsonToken.StartObject)
                        {
                          entityB = new T4();
                          this.PackToVO(reader, entityB);
                          entitiesB.Add(entityB);
                        }
                        reader.Read();
                      }
                      if (reader.TokenType == JsonToken.StartArray)
                      {
                        reader.Read();
                        while (reader.TokenType == JsonToken.StartObject)
                        {
                          entityC = new T5();
                          this.PackToVO(reader, entityC);
                          entitiesC.Add(entityC);
                        }
                      }
                    }
                    else
                    {
                      if (reader.TokenType == JsonToken.StartObject)
                      {
                        sysError = new T2();
                        this.PackToVO(reader, sysError);
                        if (sysError == null)
                        {
                          throw new Exception("解包错误失败！");
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
            using (var reader = new JsonTextReader(new StringReader(GlobalHelper.DataEncoding.GetString(reactBytes))))
            {
                reader.Read();
                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        sysHead = new T1();
                        this.PackToVO(reader, sysHead);
                    }
                    if (sysHead == null || sysHead.GetFieldInfo(_OperationCodeFieldName) == null)
                    {
                      throw new Exception("解包系统头失败！");
                    }
                    if (Convert.ToInt32(sysHead.GetFieldInfo(_OperationCodeFieldName).Value) == 0)
                    {
                      if (reader.TokenType == JsonToken.StartArray)
                      {
                        reader.Read();
                        while (reader.TokenType == JsonToken.StartObject)
                        {
                          entityA = new T3();
                          this.PackToVO(reader, entityA);
                          entitiesA.Add(entityA);
                        }
                        reader.Read();
                      }
                      if (reader.TokenType == JsonToken.StartArray)
                      {
                        reader.Read();
                        while (reader.TokenType == JsonToken.StartObject)
                        {
                          entityB = new T4();
                          this.PackToVO(reader, entityB);
                          entitiesB.Add(entityB);
                        }
                        reader.Read();
                      }
                      if (reader.TokenType == JsonToken.StartArray)
                      {
                        reader.Read();
                        while (reader.TokenType == JsonToken.StartObject)
                        {
                          entityC = new T5();
                          this.PackToVO(reader, entityC);
                          entitiesC.Add(entityC);
                        }
                        reader.Read();
                      }
                      if (reader.TokenType == JsonToken.StartArray)
                      {
                        reader.Read();
                        while (reader.TokenType == JsonToken.StartObject)
                        {
                          entityD = new T6();
                          this.PackToVO(reader, entityD);
                          entitiesD.Add(entityD);
                        }
                      }
                    }
                    else
                    {
                      if (reader.TokenType == JsonToken.StartObject)
                      {
                        sysError = new T2();
                        this.PackToVO(reader, sysError);
                        if (sysError == null)
                        {
                          throw new Exception("解包错误失败！");
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
            using (var reader = new JsonTextReader(new StringReader(GlobalHelper.DataEncoding.GetString(reactBytes))))
            {
                reader.Read();
                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        sysHead = new T1();
                        this.PackToVO(reader, sysHead);
                    }
                    if (sysHead == null || sysHead.GetFieldInfo(_OperationCodeFieldName) == null)
                    {
                      throw new Exception("解包系统头失败！");
                    }
                    if (Convert.ToInt32(sysHead.GetFieldInfo(_OperationCodeFieldName).Value) == 0)
                    {
                      if (reader.TokenType == JsonToken.StartArray)
                      {
                        reader.Read();
                        while (reader.TokenType == JsonToken.StartObject)
                        {
                          entityA = new T3();
                          this.PackToVO(reader, entityA);
                          entitiesA.Add(entityA);
                        }
                        reader.Read();
                      }
                      if (reader.TokenType == JsonToken.StartArray)
                      {
                        reader.Read();
                        while (reader.TokenType == JsonToken.StartObject)
                        {
                          entityB = new T4();
                          this.PackToVO(reader, entityB);
                          entitiesB.Add(entityB);
                        }
                        reader.Read();
                      }
                      if (reader.TokenType == JsonToken.StartArray)
                      {
                        reader.Read();
                        while (reader.TokenType == JsonToken.StartObject)
                        {
                          entityC = new T5();
                          this.PackToVO(reader, entityC);
                          entitiesC.Add(entityC);
                        }
                        reader.Read();
                      }
                      if (reader.TokenType == JsonToken.StartArray)
                      {
                        reader.Read();
                        while (reader.TokenType == JsonToken.StartObject)
                        {
                          entityD = new T6();
                          this.PackToVO(reader, entityD);
                          entitiesD.Add(entityD);
                        }
                        reader.Read();
                      }
                      if (reader.TokenType == JsonToken.StartArray)
                      {
                        reader.Read();
                        while (reader.TokenType == JsonToken.StartObject)
                        {
                          entityE = new T7();
                          this.PackToVO(reader, entityE);
                          entitiesE.Add(entityE);
                        }
                      }
                    }
                    else
                    {
                      if (reader.TokenType == JsonToken.StartObject)
                      {
                        sysError = new T2();
                        this.PackToVO(reader, sysError);
                        if (sysError == null)
                        {
                          throw new Exception("解包错误失败！");
                        }
                      }
                    }
                }
            }

            return new Tuple<T1, T2, T3[], T4[], T5[], T6[], T7[]>(sysHead, sysError, entitiesA.ToArray(), entitiesB.ToArray(), entitiesC.ToArray(), entitiesD.ToArray(),entitiesE.ToArray());
        }

        private void GetJsonContent(JObject jobj, VOBase entity)
        {
            foreach(var field in entity.VOFieldInfos)
            {
                if (field.DataType == typeof(Decimal[]))
                {
                    var valueArray=(Decimal[])field.Value;
                    for (int i = 0; i < field.MaxLength / 8; i++) //特殊处理，将来应该避免
                    {
                        jobj.Add(String.Format("{0}[{1}]", field.Name, i), ((int)valueArray[i]).ToString());
                    }
                }
                else
                {
                    jobj.Add(field.Name, ConvertToJsonSpecialStr(field.ValueText));
                }
            }
        }

        private void PackToVO(JsonTextReader reader, VOBase entity)
        {
            while (reader.Read() && reader.TokenType == JsonToken.PropertyName)
            {
                var field = entity.GetFieldInfo(reader.Value.ToString());
                reader.Read();
                var s = ConvertJsonSpecialStr(reader.Value.ToString());
                switch (Type.GetTypeCode(field.DataType))
                {
                    case TypeCode.Decimal:
                        field.Value = Decimal.Parse(s);
                        break;
                    case TypeCode.String:
                        field.Value = s; //"`".Equals(s) ? string.Empty : s
                        break;
                }
            }
            reader.Read();
        }

        /// <summary>
        /// 转换Json的特殊字符
        /// 
        /// </summary>
        /// <param name="str">需要转换的字符串</param>
        /// <returns>
        /// 转换后的字符串
        /// </returns>
        private string ConvertJsonSpecialStr(string value)
        {
            return value.Replace("&amp;", "&").Replace("&quot;", "\"").Replace("&lt;", "<").Replace("&gt;", ">").Replace("NULL", "");
        }

        /// <summary>
        /// 转换成Json的特殊字符
        /// 
        /// </summary>
        /// <param name="value">需要转换的字符串</param>
        /// <returns>
        /// 转换后的字符串
        /// </returns>
        private string ConvertToJsonSpecialStr(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return ""; //"`"
            else
                return value.Replace("&", "&amp;").Replace("\"", "&quot;").Replace("<", "&lt;").Replace(">", "&gt;");
        }
    }
}
