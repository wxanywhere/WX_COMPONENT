using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ydtf.isoa
{
    /// <summary>
    /// AttributeHelper
    /// </summary>
    internal static class attribute_helper
    {
        /// <summary>
        /// GetMemberText
        /// </summary>
        /// <param name="enm"></param>
        /// <returns></returns>
        internal static string GetMemberText(Enum enm)
        {
            if (enm != null)
            {
                MemberInfo[] mi = enm.GetType().GetMember(enm.ToString());
                if (mi != null && mi.Length > 0)
                {
                    member_textAttribute attribute = Attribute.GetCustomAttribute(mi[0], typeof(member_textAttribute)) as member_textAttribute;
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
