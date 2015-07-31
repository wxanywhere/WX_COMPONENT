using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace WX.Windows.Diagram.Designer.CommandExtension
{
    public class DesignItemCommandBinding:CommandBinding
    {
        public event DesignItemExecutedRoutedEventHandler DesignItemExecuted;
        public DesignItemCommandBinding(ICommand command, DesignItemExecutedRoutedEventHandler executed)
        {
            DesignItemExecuted=executed;
        }
    }
}
