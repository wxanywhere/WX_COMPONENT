using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WX.Windows.Diagram.Designer
{
    public delegate void MultiShapeActionEventHandler(object sender, MultiShapeActionEventArgs e);

    public class MultiShapeActionEventArgs : EventArgs
    {
        private ISelectable[] _shapeInfos;

        public ISelectable[] ShapeInfos
        {
            get
            {
                return this._shapeInfos;
            }
        }

        public MultiShapeActionEventArgs(ISelectable[] shapeInfos)
            : base()
        {
            this._shapeInfos = shapeInfos;
        }
    }
}
