using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WX.Windows.Diagram.Designer
{
    public abstract class ShapeInfoUnit : ModelBase
    {
        public ShapeInfoUnit(ShapeInfo shapeInfo)
        {
            _ShapeInfo = shapeInfo;
        }

        private ShapeInfo _ShapeInfo;
        public ShapeInfo ShapeInfo
        {
            get { return _ShapeInfo; }
        }
    }
}
