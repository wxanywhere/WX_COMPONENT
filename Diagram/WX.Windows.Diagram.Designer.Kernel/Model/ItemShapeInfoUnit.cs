using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WX.Windows.Diagram.Designer
{
    public class ItemShapeInfoUnit : ShapeInfoUnit
    {
        public ItemShapeInfoUnit(ShapeInfo shapeInfo)
            : base(shapeInfo)
        {
           
        }

        private object _SvgDrawing;
        public object SvgDrawing
        {
            get { return _SvgDrawing; }
            set
            {
                if (_SvgDrawing != value)
                {
                    _SvgDrawing = value;
                    this.OnPropertyChanged("SvgDrawing");
                }
            }
        }
    }
}
