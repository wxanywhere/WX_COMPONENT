using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WX.Windows.Diagram.Designer
{
    public enum ShapeCategory : byte
    {
        Connection=0,
        Business = 1,
        Application = 2,
        DataModel = 3,
        Technology = 4,
        Network = 5,
        FlowChart = 6
    }
}
