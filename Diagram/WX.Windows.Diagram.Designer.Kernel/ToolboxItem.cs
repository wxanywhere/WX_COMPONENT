using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace WX.Windows.Diagram.Designer
{
    // Represents a selectable item in the Toolbox/>.
    public class ToolboxItem : ContentControl
    {
        // caches the start point of the drag operation
        private Point? dragStartPoint = null;

        static ToolboxItem()
        {
            // set the key to reference the style for this control
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(
                typeof(ToolboxItem), new FrameworkPropertyMetadata(typeof(ToolboxItem)));
        }

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseDown(e);
            this.dragStartPoint = new Point?(e.GetPosition(this));
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.LeftButton != MouseButtonState.Pressed)
                this.dragStartPoint = null;

            if (this.dragStartPoint.HasValue)
            {
                DragObject dataObject = new DragObject();
                if (this.Content is ShapeInfoUnit)
                {
                    dataObject.ShapeInfoUnites = new ShapeInfoUnit[]{this.Content as ShapeInfoUnit};
                }
                else if (this.Content is IEnumerable<ShapeInfoUnit>)
                {
                    var shapeInfoUnites = new List<ShapeInfoUnit>(this.Content as IEnumerable<ShapeInfoUnit>).ToArray();
                    dataObject.ShapeInfoUnites = shapeInfoUnites;
                }

                WrapPanel panel = VisualTreeHelper.GetParent(this) as WrapPanel;
                if (panel != null)
                {
                    // desired size for DesignCanvas is the stretched Toolbox item size
                    double scale = 1.3;
                    dataObject.DesiredSize = new Size(panel.ItemWidth * scale, panel.ItemHeight * scale);
                }

                DragDrop.DoDragDrop(this, dataObject, DragDropEffects.All);

                e.Handled = true;
            }
        }
    }

    public class DragObject
    {
        public ShapeInfoUnit[] ShapeInfoUnites { get; set; }

        public Size? DesiredSize { get; set; }

    }
}
