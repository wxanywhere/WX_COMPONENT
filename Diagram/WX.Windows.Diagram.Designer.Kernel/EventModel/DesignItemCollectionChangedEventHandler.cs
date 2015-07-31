using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WX.Windows.Diagram.Designer
{
    public delegate void DesignItemCollectionChangedEventHandler(object sender, DesignItemCollectionChangedEventArgs e);

    public class DesignItemCollectionChangedEventArgs : EventArgs
    {
        private DesignItemInfo[] _designItemInfos;

        public DesignItemInfo[] DesignItemInfos
        {
            get
            {
                return this._designItemInfos;
            }
        }

        public DesignItemCollectionChangedEventArgs(DesignItemInfo[] designItemInfos)
            : base()
        {
            this._designItemInfos = designItemInfos;
        }
    }
}
