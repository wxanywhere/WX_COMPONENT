using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WX.Windows.Diagram.Designer
{
    public delegate void DesignItemExecutedRoutedEventHandler(object sender, DesignItemExecutedRoutedEventArgs e);

    public class DesignItemExecutedRoutedEventArgs
    {
        private DesignItemInfo _designItemInfo;

        public DesignItemInfo DesignItemInfo
        {
            get
            {
                return this._designItemInfo;
            }
        }

        public DesignItemExecutedRoutedEventArgs(DesignItemInfo designItemInfo)
            : base()
        {
            this._designItemInfo = designItemInfo;
        }
    }
   
}
