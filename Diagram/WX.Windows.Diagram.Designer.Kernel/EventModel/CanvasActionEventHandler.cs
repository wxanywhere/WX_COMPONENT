using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WX.Windows.Diagram.Designer
{
    public delegate void CanvasActionEventHandler(object sender, CanvasActionEventArgs e);

    public class CanvasActionEventArgs : EventArgs
    {
        private CanvasInfo _canvasInfo;

        public CanvasInfo canvasInfo
        {
            get
            {
                return this._canvasInfo;
            }
        }

        public CanvasActionEventArgs(CanvasInfo canvasInfo)
            : base()
        {
            this._canvasInfo = canvasInfo;
        }
    }
}
