using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ydtf.isoa
{
    /// <summary>
    /// MemberTextAttribute
    /// </summary>
    internal class member_textAttribute : Attribute
    {
        private string _Text = String.Empty;

        /// <summary>
        /// MemberTextAttribute
        /// </summary>
        /// <param name="text"></param>
        internal member_textAttribute(string text)
        {
            _Text = text;
        }

        /// <summary>
        /// Text
        /// </summary>
        internal string Text
        {
            get
            {
                return _Text;
            }
        }
    }
}
