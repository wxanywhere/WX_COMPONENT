using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WX.Windows.Diagram.Designer
{
    public class ConnectionShapeInfoUnit : ShapeInfoUnit
    {
        public ConnectionShapeInfoUnit(ShapeInfo shapeInfo)
            : base(shapeInfo)
        {
           
        }

        private object _SourceSvgDrawing;
        public object SourceSvgDrawing
        {
            get { return _SourceSvgDrawing; }
            set
            {
                if (_SourceSvgDrawing != value)
                {
                    _SourceSvgDrawing = value;
                    this.OnPropertyChanged("SourceSvgDrawing");
                }
            }
        }

        private object _SinkSvgDrawing;
        public object SinkSvgDrawing
        {
            get { return _SinkSvgDrawing; }
            set
            {
                if (_SinkSvgDrawing != value)
                {
                    _SinkSvgDrawing = value;
                    this.OnPropertyChanged("SinkSvgDrawing");
                }
            }
        }
    }
}
