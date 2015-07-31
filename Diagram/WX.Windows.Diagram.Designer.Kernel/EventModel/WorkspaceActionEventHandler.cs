using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WX.Windows.Diagram.Designer
{
    public delegate void WorkspaceActionEventHandler(object sender, WorkspaceActionEventArgs e);

    public class WorkspaceActionEventArgs : EventArgs
    {
        private CanvasInfo _CanvasInfo;
        private DesignItemInfo[] _designItemInfos;
        private DesignConnectionInfo[] _designConnectionInfos;

        public CanvasInfo CanvasInfo
        {
            get
            {
                return this._CanvasInfo;
            }
        }

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

        public WorkspaceActionEventArgs(CanvasInfo canvasInfo,DesignItemInfo[] designItemInfos, DesignConnectionInfo[] designConnectionInfos)
            : base()
        {
            this._CanvasInfo = canvasInfo;
            this._designItemInfos = designItemInfos;
            this._designConnectionInfos = designConnectionInfos;
        }
    }
}
