using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;

namespace WX.Windows.Diagram.Designer
{
    public class Shape : ContentControl
    {
        public Shape()
        {
        }
        public string Text
        {
            get { return (string)this.GetValue(Shape.TextProperty); }
            set { this.SetValue(Shape.TextProperty, value); }
        }
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(Shape), new FrameworkPropertyMetadata(string.Empty));

        /// <summary>
        /// 获取或设置是否选中
        /// 
        /// </summary>
        public bool IsSelected
        {
            get { return (bool)this.GetValue(Shape.IsSelectedProperty); }
            set { this.SetValue(Shape.IsSelectedProperty, value); }
        }
        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register("IsSelected", typeof(bool), typeof(Shape), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(Shape.IsSelectedCallback)));

        private static void IsSelectedCallback(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            Shape shape = obj as Shape;
            if (shape == null) return;
            shape.RaiseEvent(new RoutedPropertyChangedEventArgs<bool>((bool)args.OldValue, (bool)args.NewValue, Shape.SelectChangedEvent));
        }

        /// <summary>
        /// 获取或设置是否只读
        /// 
        /// </summary>
        public bool IsReadOnly
        {
            get { return (bool)this.GetValue(Shape.IsReadOnlyProperty); }
            set { this.SetValue(Shape.IsReadOnlyProperty, value); }
        }
        public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(Shape), new FrameworkPropertyMetadata(false));

        /// <summary>
        /// 获取或设置数据项对象
        /// 
        /// </summary>
        public ShapeInfoUnit ShapeInfoUnit
        {
            get { return (ShapeInfoUnit)this.GetValue(Shape.ShapeInfoUnitProperty); }
            set { this.SetValue(Shape.ShapeInfoUnitProperty, value); }
        }
        public static readonly DependencyProperty ShapeInfoUnitProperty = DependencyProperty.Register("ShapeInfoUnit", typeof(object), typeof(Shape), new FrameworkPropertyMetadata(null));


        public event RoutedPropertyChangedEventHandler<bool> SelectChanged
        {
          add{this.AddHandler(Shape.SelectChangedEvent, value);}
          remove{this.RemoveHandler(Shape.SelectChangedEvent, value);}
        }
        public static readonly RoutedEvent SelectChangedEvent=EventManager.RegisterRoutedEvent("SelectChanged", RoutingStrategy.Direct, typeof (RoutedPropertyChangedEventHandler<bool>), typeof (Shape));
    }
}
