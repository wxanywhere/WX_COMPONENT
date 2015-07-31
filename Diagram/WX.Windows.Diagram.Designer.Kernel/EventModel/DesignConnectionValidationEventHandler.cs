using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WX.Windows.Diagram.Designer
{
    public delegate void DesignConnectionValidationEventHandler(object sender, DesignConnectionValidationEventArgs e);

    public class DesignConnectionValidationEventArgs
    {
        private DesignItemInfo _sourceItemInfo;
        private DesignItemInfo _sinkItemInfo;

        public DesignItemInfo SourceItemInfo
        {
            get
            {
                return this._sourceItemInfo;
            }
        }

        public DesignItemInfo SinkItemInfo
        {
            get
            {
                return this._sinkItemInfo;
            }
        }

        public DesignConnectionValidationEventArgs(DesignItemInfo sourceItemInfo, DesignItemInfo sinkItemInfo)
            : base()
        {
            this._sourceItemInfo = sourceItemInfo;
            this._sinkItemInfo = sinkItemInfo;
        }
    }
}
