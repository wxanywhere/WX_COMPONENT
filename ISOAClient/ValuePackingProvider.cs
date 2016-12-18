using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISOA;
using DataModelBase;

namespace ISOAClient
{
    public class ValuePackingProvider : IPackingProvider
    {
        private static readonly int NUMLEN = 6;
        private string _OperationCodeFieldName = String.Empty;

        public ValuePackingProvider(string operationCodeFieldName)
        {
            _OperationCodeFieldName = operationCodeFieldName;
        }

        public string OrganizeInput(string serviceName,params object[] inParams)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var param in inParams)
            {
                if (param is VOBase)
                {
                    GetValueStr(sb, param as VOBase);
                }
                else if (param is IEnumerable<VOBase>)
                {
                    GetValueStr(sb, param as IEnumerable<VOBase>);
                }
            }

            return sb.ToString();
        }

        public Tuple<T[]> OrganizeOutput<T>(byte[] reactBytes)
            where T : VOBase, new()
        {
            var beginIndex = 0;

            int entityCount = (int)this.GetValue(reactBytes, beginIndex, NUMLEN, typeof(int), GlobalHelper.DataEncoding);
            beginIndex += NUMLEN;
            var entitiesA = PackToVO<T>(reactBytes, ref beginIndex, entityCount, GlobalHelper.DataEncoding);

            return new Tuple<T[]>(entitiesA);
        }

        public Tuple<T1, T2> OrganizeOutput<T1, T2>(byte[] reactBytes)
            where T1 : VOBase, new()
            where T2 : VOBase, new()
        {
            var beginIndex = 0;
            var sysHead = PackToVO<T1>(reactBytes, ref beginIndex, GlobalHelper.HeaderEncoding);
            var sysError = (T2)null;
            if (sysHead == null || sysHead.GetFieldInfo(_OperationCodeFieldName) == null)
            {
                throw new Exception("解包系统头失败！");
            }
            else
            {
                beginIndex = sysHead.FieldsSumLength;
                sysError = this.PackToVO<T2>(reactBytes, ref beginIndex, GlobalHelper.ErrorEncoding);
                if (sysError == null)
                {
                    throw new Exception("解包错误失败！");
                }
            }

            return new Tuple<T1, T2>(sysHead, sysError);
        }

        public Tuple<T1, T2, T3[]> OrganizeOutput<T1, T2, T3>(byte[] reactBytes)
            where T1 : VOBase, new()
            where T2 : VOBase, new()
            where T3 : VOBase, new()
        {
            var beginIndex = 0;
            var sysHead = PackToVO<T1>(reactBytes, ref beginIndex, GlobalHelper.HeaderEncoding);
            var sysError = (T2)null;
            var entitiesA = (T3[])null;
            if (sysHead == null || sysHead.GetFieldInfo(_OperationCodeFieldName) == null)
            {
                throw new Exception("解包系统头失败！");
            }
            if (Convert.ToInt32(sysHead.GetFieldInfo(_OperationCodeFieldName).Value) == 0)
            {
                int entityCount = (int)this.GetValue(reactBytes, beginIndex, NUMLEN, typeof(int), GlobalHelper.DataEncoding);
                beginIndex += NUMLEN;
                entitiesA = PackToVO<T3>(reactBytes, ref beginIndex, entityCount, GlobalHelper.DataEncoding);
            }
            else
            {
                beginIndex = sysHead.FieldsSumLength;
                sysError = this.PackToVO<T2>(reactBytes, ref beginIndex, GlobalHelper.ErrorEncoding);
                if (sysError == null)
                {
                    throw new Exception("解包错误失败！");
                }
            }

            return new Tuple<T1, T2, T3[]>(sysHead, sysError, entitiesA);
        }

        public Tuple<T1, T2, T3[], T4[]> OrganizeOutput<T1, T2, T3, T4>(byte[] reactBytes)
            where T1 : VOBase, new()
            where T2 : VOBase, new()
            where T3 : VOBase, new()
            where T4 : VOBase, new()
        {
            var beginIndex = 0;
            var sysHead = PackToVO<T1>(reactBytes, ref beginIndex, GlobalHelper.HeaderEncoding);
            var sysError = (T2)null;
            var entitiesA = (T3[])null;
            var entitiesB=(T4[])null;
            if (sysHead == null || sysHead.GetFieldInfo(_OperationCodeFieldName) == null)
            {
                throw new Exception("解包系统头失败！");
            }
            if (Convert.ToInt32(sysHead.GetFieldInfo(_OperationCodeFieldName).Value) == 0)
            {
                int entityCount = (int)this.GetValue(reactBytes, beginIndex, NUMLEN, typeof(int), GlobalHelper.DataEncoding);
                beginIndex += NUMLEN;
                entitiesA = PackToVO<T3>(reactBytes, ref beginIndex, entityCount, GlobalHelper.DataEncoding);

                entityCount = (int)this.GetValue(reactBytes, beginIndex, NUMLEN, typeof(int), GlobalHelper.DataEncoding);
                beginIndex += NUMLEN;
                entitiesB = PackToVO<T4>(reactBytes, ref beginIndex, entityCount, GlobalHelper.DataEncoding);
            }
            else
            {
                beginIndex = reactBytes.Length - sysHead.FieldsSumLength;
                sysError = this.PackToVO<T2>(reactBytes, ref beginIndex, GlobalHelper.ErrorEncoding);
                if (sysError == null)
                {
                    throw new Exception("解包错误失败！");
                }
            }

            return new Tuple<T1, T2, T3[], T4[]>(sysHead, sysError, entitiesA, entitiesB);
        }

        public Tuple<T1, T2, T3[], T4[],T5[]> OrganizeOutput<T1, T2, T3, T4,T5>(byte[] reactBytes)
            where T1 : VOBase, new()
            where T2 : VOBase, new()
            where T3 : VOBase, new()
            where T4 : VOBase, new()
            where T5 : VOBase, new()
        {
            var beginIndex = 0;
            var sysHead = PackToVO<T1>(reactBytes, ref beginIndex, GlobalHelper.HeaderEncoding);
            var sysError = (T2)null;
            var entitiesA = (T3[])null;
            var entitiesB = (T4[])null;
            var entitiesC = (T5[])null;
            if (sysHead == null || sysHead.GetFieldInfo(_OperationCodeFieldName) == null)
            {
                throw new Exception("解包系统头失败！");
            }
            if (Convert.ToInt32(sysHead.GetFieldInfo(_OperationCodeFieldName).Value) == 0)
            {
                int entityCount = (int)this.GetValue(reactBytes, beginIndex, NUMLEN, typeof(int), GlobalHelper.DataEncoding);
                beginIndex += NUMLEN;
                entitiesA = PackToVO<T3>(reactBytes, ref beginIndex, entityCount, GlobalHelper.DataEncoding);

                entityCount = (int)this.GetValue(reactBytes, beginIndex, NUMLEN, typeof(int), GlobalHelper.DataEncoding);
                beginIndex += NUMLEN;
                entitiesB = PackToVO<T4>(reactBytes, ref beginIndex, entityCount, GlobalHelper.DataEncoding);

                entityCount = (int)this.GetValue(reactBytes, beginIndex, NUMLEN, typeof(int), GlobalHelper.DataEncoding);
                beginIndex += NUMLEN;
                entitiesC = PackToVO<T5>(reactBytes, ref beginIndex, entityCount, GlobalHelper.DataEncoding);
            }
            else
            {
                beginIndex = reactBytes.Length - sysHead.FieldsSumLength;
                sysError = this.PackToVO<T2>(reactBytes, ref beginIndex, GlobalHelper.ErrorEncoding);
                if (sysError == null)
                {
                    throw new Exception("解包错误失败！");
                }
            }

            return new Tuple<T1, T2, T3[], T4[], T5[]>(sysHead, sysError, entitiesA, entitiesB,entitiesC);
        }

        public Tuple<T1, T2, T3[], T4[], T5[],T6[]> OrganizeOutput<T1, T2, T3, T4, T5,T6>(byte[] reactBytes)
            where T1 : VOBase, new()
            where T2 : VOBase, new()
            where T3 : VOBase, new()
            where T4 : VOBase, new()
            where T5 : VOBase, new()
            where T6 : VOBase, new()
        {
            var beginIndex = 0;
            var sysHead = PackToVO<T1>(reactBytes, ref beginIndex, GlobalHelper.HeaderEncoding);
            var sysError = (T2)null;
            var entitiesA = (T3[])null;
            var entitiesB = (T4[])null;
            var entitiesC = (T5[])null;
            var entitiesD = (T6[])null;
            if (sysHead == null || sysHead.GetFieldInfo(_OperationCodeFieldName) == null)
            {
                throw new Exception("解包系统头失败！");
            }
            if (Convert.ToInt32(sysHead.GetFieldInfo(_OperationCodeFieldName).Value) == 0)
            {
                int entityCount = (int)this.GetValue(reactBytes, beginIndex, NUMLEN, typeof(int), GlobalHelper.DataEncoding);
                beginIndex += NUMLEN;
                entitiesA = PackToVO<T3>(reactBytes, ref beginIndex, entityCount, GlobalHelper.DataEncoding);

                entityCount = (int)this.GetValue(reactBytes, beginIndex, NUMLEN, typeof(int), GlobalHelper.DataEncoding);
                beginIndex += NUMLEN;
                entitiesB = PackToVO<T4>(reactBytes, ref beginIndex, entityCount, GlobalHelper.DataEncoding);

                entityCount = (int)this.GetValue(reactBytes, beginIndex, NUMLEN, typeof(int), GlobalHelper.DataEncoding);
                beginIndex += NUMLEN;
                entitiesC = PackToVO<T5>(reactBytes, ref beginIndex, entityCount, GlobalHelper.DataEncoding);

                entityCount = (int)this.GetValue(reactBytes, beginIndex, NUMLEN, typeof(int), GlobalHelper.DataEncoding);
                beginIndex += NUMLEN;
                entitiesD = PackToVO<T6>(reactBytes, ref beginIndex, entityCount, GlobalHelper.DataEncoding);
            }
            else
            {
                beginIndex = reactBytes.Length - sysHead.FieldsSumLength;
                sysError = this.PackToVO<T2>(reactBytes, ref beginIndex, GlobalHelper.ErrorEncoding);
                if (sysError == null)
                {
                    throw new Exception("解包错误失败！");
                }
            }

            return new Tuple<T1, T2, T3[], T4[], T5[], T6[]>(sysHead, sysError, entitiesA, entitiesB, entitiesC, entitiesD);
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
            var beginIndex = 0;
            var sysHead = PackToVO<T1>(reactBytes, ref beginIndex, GlobalHelper.HeaderEncoding);
            var sysError = (T2)null;
            var entitiesA = (T3[])null;
            var entitiesB = (T4[])null;
            var entitiesC = (T5[])null;
            var entitiesD = (T6[])null;
            var entitiesE = (T7[])null;
            if (sysHead == null || sysHead.GetFieldInfo(_OperationCodeFieldName) == null)
            {
                throw new Exception("解包系统头失败！");
            }
            if (Convert.ToInt32(sysHead.GetFieldInfo(_OperationCodeFieldName).Value) == 0)
            {
                int entityCount = (int)this.GetValue(reactBytes, beginIndex, NUMLEN, typeof(int), GlobalHelper.DataEncoding);
                beginIndex += NUMLEN;
                entitiesA = PackToVO<T3>(reactBytes, ref beginIndex, entityCount, GlobalHelper.DataEncoding);

                entityCount = (int)this.GetValue(reactBytes, beginIndex, NUMLEN, typeof(int), GlobalHelper.DataEncoding);
                beginIndex += NUMLEN;
                entitiesB = PackToVO<T4>(reactBytes, ref beginIndex, entityCount, GlobalHelper.DataEncoding);

                entityCount = (int)this.GetValue(reactBytes, beginIndex, NUMLEN, typeof(int), GlobalHelper.DataEncoding);
                beginIndex += NUMLEN;
                entitiesC = PackToVO<T5>(reactBytes, ref beginIndex, entityCount, GlobalHelper.DataEncoding);

                entityCount = (int)this.GetValue(reactBytes, beginIndex, NUMLEN, typeof(int), GlobalHelper.DataEncoding);
                beginIndex += NUMLEN;
                entitiesD = PackToVO<T6>(reactBytes, ref beginIndex, entityCount, GlobalHelper.DataEncoding);

                entityCount = (int)this.GetValue(reactBytes, beginIndex, NUMLEN, typeof(int), GlobalHelper.DataEncoding);
                beginIndex += NUMLEN;
                entitiesE = PackToVO<T7>(reactBytes, ref beginIndex, entityCount, GlobalHelper.DataEncoding);
            }
            else
            {
                beginIndex = reactBytes.Length - sysHead.FieldsSumLength;
                sysError = this.PackToVO<T2>(reactBytes, ref beginIndex, GlobalHelper.ErrorEncoding);
                if (sysError == null)
                {
                    throw new Exception("解包错误失败！");
                }
            }

            return new Tuple<T1, T2, T3[], T4[], T5[], T6[], T7[]>(sysHead, sysError, entitiesA, entitiesB, entitiesC, entitiesD, entitiesE.ToArray());
        }

        /// <summary>
        /// 把实体打包成Tcp字符串
        /// 
        /// </summary>
        /// <param name="sb">需要拼接的输入参数</param><param name="entity">实体</param>
        private void GetValueStrX(StringBuilder sb, VOBase entity)
        {
            this.GetValueStrX(sb, entity, "`");
        }

        /// <summary>
        /// 把实体打包成Tcp字符串
        /// 
        /// </summary>
        /// <param name="sb">需要拼接的输入参数</param>
        /// <param name="entity">实体</param>
        /// <param name="fillEmptyStr">当值为空时填充的字符</param>
        private void GetValueStrX(StringBuilder sb, VOBase entity, string fillEmptyStr)
        {
            VOFieldInfo[] fields = entity.VOFieldInfos;
            foreach (var field in fields)
            {

                if(Type.GetTypeCode(field.DataType) == TypeCode.Decimal)
                {
                    if(field.Value==null)
                    {
                        sb.Append("0".PadRight(field.MaxLength));
                    }
                    else
                    {
                        sb.Append(field.ValueText.PadRight(field.MaxLength));
                    }
                }
                else if (field.DataType == typeof(Decimal[])) //特殊处理，将来应该避免
                {
                    if(field.Value==null)
                    {
                        for (int i = 0; i < field.MaxLength / 8; i++)
                        {
                            sb.Append("0".PadRight(8));
                        }
                    }
                    else
                    {
                        foreach (var element in (decimal[])field.Value)
                        {
                            sb.Append(element.ToString().PadRight(8));
                        }
                    }
                }
                else if (Type.GetTypeCode(field.DataType) == TypeCode.String)
                {
                    if (string.IsNullOrWhiteSpace(field.ValueText))
                    {
                        sb.Append(fillEmptyStr.PadRight(field.MaxLength));
                    }
                    else
                    {
                        //sb.Append(field.ValueText.PadRight(field.MaxLength)); 对特殊编码字符不能直接操作
                        int byteCount = GlobalHelper.DataEncoding.GetByteCount(field.ValueText);
                        sb.Append(field.ValueText.PadRight(field.MaxLength - (byteCount - field.ValueText.Length)));
                    }
                }
                
            }
        }

        private void GetValueStr(StringBuilder sb, VOBase entity)
        {
            if (!entity.IsHeadPart)
            {
                sb.Append("1".PadLeft(NUMLEN));
            }
            this.GetValueStrX(sb, entity);
        }

        private void GetValueStr(StringBuilder sb, IEnumerable<VOBase> entities)
        {
            var length = entities.Count();
            sb.Append(length.ToString().PadLeft(NUMLEN));
            foreach (var entity in entities)
            {
                this.GetValueStrX(sb, entity);
            }
        }

        private T PackToVO<T>(byte[] reactBytes, ref int beginIndex, Encoding encoding) where T : VOBase, new()
        {
            var vo = new T();
            foreach (var field in vo.VOFieldInfos)
            {
                field.Value = this.GetValue(reactBytes, beginIndex, field.MaxLength, field.DataType, encoding);
                beginIndex += field.MaxLength;
            }

            return vo;
        }

        private T[] PackToVO<T>(byte[] reactBytes, ref int beginIndex, int entityCount, Encoding encoding) where T : VOBase, new()
        {
            var result = new List<T>();
            var vo = default(T);
            for (int i = 0; i < entityCount; i++)
            {
                vo = new T();
                foreach (var field in vo.VOFieldInfos)
                {
                    field.Value = this.GetValue(reactBytes, beginIndex, field.MaxLength, field.DataType, encoding);
                    beginIndex += field.MaxLength;
                }
                result.Add(vo);
            }

            return result.ToArray();
        }

        /// <summary>
        /// 通过字节数组转换值
        /// </summary>
        /// <param name="reactBytes">输出字节</param>
        /// <param name="conditionByte">条件</param>
        /// <param name="beginIndex">开始遍历输出字节的索引</param>
        /// <param name="maxLength">最大长度</param>
        /// <param name="encoding">字符编码格式</param>
        /// <returns>
        /// 转换后的值
        /// </returns>
        private string GetValue(byte[] reactBytes, byte conditionByte, int beginIndex, int maxLength, Encoding encoding)
        {
            int index = beginIndex;
            int count = maxLength;
            for (int i = 0; i < maxLength; ++i)
            {
                if ((int)reactBytes[index] == (int)conditionByte)
                {
                    count = i;
                    break;
                }
                else
                {
                    ++index;
                }
            }

            return encoding.GetString(reactBytes, beginIndex, count);
        }

        /// <summary>
        /// 通过字节数组转换字符串值
        /// 描述：此方法用在旧的ISOA 版本， 新版本中字符串末尾值填充 0 ,旧版本中字符串末尾值填充 32.
        /// 
        /// </summary>
        /// <param name="reactBytes">输出字节</param>
        /// <param name="conditionByte">条件</param>
        /// <param name="beginIndex">开始遍历输出字节的索引</param>
        /// <param name="maxLength">最大长度</param>
        /// <param name="encoding">字符编码格式</param>
        /// <returns>
        /// 转换后的值
        /// </returns>
        private string GetStringValue(byte[] reactBytes, byte conditionByte, int beginIndex, int maxLength, Encoding encoding)
        {
            int count = 0;
            for (int i = maxLength + beginIndex - 1; i >= beginIndex; --i)
            {
                if ((int)reactBytes[i] != (int)conditionByte)
                {
                    count = i - beginIndex + 1;
                    break;
                }
            }
            if (count > 0)
            {
                string str = encoding.GetString(reactBytes, beginIndex, count);
                if (str != "`")
                {
                    return str;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// 通过字节数组转换值
        /// </summary>
        /// <param name="reactBytes">输出字节</param>
        /// <param name="beginIndex">开始遍历输出字节的索引</param>
        /// <param name="maxLength">最大长度</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="encoding">字符编码格式</param>
        /// <returns>
        /// 转换后的值
        /// </returns>
        private object GetValue(byte[] reactBytes, int beginIndex, int maxLength, Type dataType, Encoding encoding)
        {
            switch (Type.GetTypeCode(dataType))
            {
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                    return (object)int.Parse(this.GetValue(reactBytes, (byte)32, beginIndex, maxLength, encoding));
                case TypeCode.Decimal:
                    return (object)Decimal.Parse(this.GetValue(reactBytes, (byte)32, beginIndex, maxLength, encoding));
                case TypeCode.String:
                    return (object)this.GetStringValue(reactBytes, (byte)32, beginIndex, maxLength, encoding);
                default:
                    return (object)null;
            }
        }
    }
}
