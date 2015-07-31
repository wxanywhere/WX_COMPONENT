using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WX.Windows.Diagram.Designer
{
    public delegate void MultiDesignItemActionEventHandler(object sender, MultiDesignItemActionEventArgs e);

    public class MultiDesignItemActionEventArgs : EventArgs
    {
        private DesignItemInfo[] _designItemInfos;

        public DesignItemInfo[] DesignItemInfos
        {
            get
            {
                return this._designItemInfos;
            }
        }

        public MultiDesignItemActionEventArgs(DesignItemInfo[] designItemInfos)
            : base()
        {
            this._designItemInfos = designItemInfos;
        }
    }
}
