using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISOA
{
    /// <summary>
    /// MemberTextAttribute
    /// </summary>
    public class MemberTextAttribute:Attribute
    {
        private string _Text = String.Empty;

        /// <summary>
        /// MemberTextAttribute
        /// </summary>
        /// <param name="text"></param>
        public MemberTextAttribute(string text)
        {
            _Text = text;
        }

        /// <summary>
        /// Text
        /// </summary>
        public string Text
        {
            get
            {
                return _Text;
            }
        }
    }
}
