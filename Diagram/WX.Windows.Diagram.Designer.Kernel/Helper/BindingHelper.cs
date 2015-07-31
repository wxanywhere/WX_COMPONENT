using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;

namespace WX.Windows.Diagram.Designer
{
    public static class BindingHelper
    {
        public static Binding CreateBinding(object dataSource, string path)
        {
            Binding binding = new Binding();
            binding.Source = dataSource;
            binding.Path = new PropertyPath(path);
            binding.Mode = BindingMode.TwoWay;
            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            return binding;
        }
    }
}
