using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WX.Windows.Diagram.Designer
{
    public delegate void SingleDesignItemActionEventHandler(object sender, SingleDesignItemActionEventArgs e);

    public class SingleDesignItemActionEventArgs
    {
        private DesignItemInfo _designItemInfo;

        public DesignItemInfo DesignItemInfo
        {
            get
            {
                return this._designItemInfo;
            }
        }

        public SingleDesignItemActionEventArgs(DesignItemInfo designItemInfo)
            : base()
        {
            this._designItemInfo = designItemInfo;
        }
    }
}
