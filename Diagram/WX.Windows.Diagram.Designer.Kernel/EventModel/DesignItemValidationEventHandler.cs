using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WX.Windows.Diagram.Designer
{
    public delegate void DesignItemValidationEventHandler(object sender, DesignItemValidationEventArgs e);

    public class DesignItemValidationEventArgs : EventArgs
    {
        private DesignItemInfo[] _designItemInfos;

        public DesignItemInfo[] DesignItemInfos
        {
            get
            {
                return this._designItemInfos;
            }
        }

        public DesignItemValidationEventArgs(DesignItemInfo[] designItemInfos)
            : base()
        {
            this._designItemInfos = designItemInfos;
        }
    }
}
