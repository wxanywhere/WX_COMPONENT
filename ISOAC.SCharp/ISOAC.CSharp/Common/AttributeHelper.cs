using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ISOA
{
    /// <summary>
    /// AttributeHelper
    /// </summary>
    public static class AttributeHelper
    {
        /// <summary>
        /// GetMemberText
        /// </summary>
        /// <param name="enm"></param>
        /// <returns></returns>
        public static string GetMemberText(Enum enm)
        {
            if (enm != null)
            {
                MemberInfo[] mi = enm.GetType().GetMember(enm.ToString());
                if (mi != null && mi.Length > 0)
                {
                    MemberTextAttribute attribute = Attribute.GetCustomAttribute(mi[0], typeof(MemberTextAttribute)) as MemberTextAttribute;
                    if (attribute != null)
                    {
                        return attribute.Text;
                    }
                }
            }

            return null;
        }
    }
}
