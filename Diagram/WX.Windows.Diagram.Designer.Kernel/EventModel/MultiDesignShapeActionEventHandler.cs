using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WX.Windows.Diagram.Designer
{
    public delegate void MultiDesignShapeActionEventHandler(object sender, MultiDesignShapeActionEventArgs e);

    public class MultiDesignShapeActionEventArgs : EventArgs
    {
        private DesignItemInfo[] _designItemInfos;
        private DesignConnectionInfo[] _designConnectionInfos;

        public DesignItemInfo[] DesignItemInfos
        {
            get
            {
                return this._designItemInfos;
            }
        }

        public DesignConnectionInfo[] DesignConnectionInfos
        {
            get
            {
                return this._designConnectionInfos;
            }
        }

        public MultiDesignShapeActionEventArgs(DesignItemInfo[] designItemInfos, DesignConnectionInfo[] designConnectionInfos)
            : base()
        {
            this._designItemInfos = designItemInfos;
            this._designConnectionInfos = designConnectionInfos;
        }
    }
}
