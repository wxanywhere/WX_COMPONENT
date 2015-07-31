using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WX.Windows.Diagram.Designer
{
    public class ConnectionShapeInfo : ShapeInfo
    {
        public byte[] SourceSvgBuffer { get; set; }

        public byte[] SinkSvgBuffer { get; set; }
    }
}
