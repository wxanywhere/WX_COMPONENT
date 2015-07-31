using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WX.Windows.Diagram.Designer
{
    public class RubberbandAdorner : Adorner
    {
        private  static readonly Guid DefaultGuid=new Guid();
        private Point? startPoint, endPoint;
        private Rectangle rubberband;
        private DesignCanvas designCanvas;
        private VisualCollection visuals;
        private Canvas adornerCanvas;

        protected override int VisualChildrenCount
        {
            get
            {
                return this.visuals.Count;
            }
        }

        public RubberbandAdorner(DesignCanvas designCanvas, Point? dragStartPoint)
            : base(designCanvas)
        {
            this.designCanvas = designCanvas;
            this.startPoint = dragStartPoint;

            this.adornerCanvas = new Canvas();
            this.adornerCanvas.Background = Brushes.Transparent;
            this.visuals = new VisualCollection(this);
            this.visuals.Add(this.adornerCanvas);

            this.rubberband = new Rectangle();
            this.rubberband.Stroke = Brushes.Navy;
            this.rubberband.StrokeThickness = 1;
            this.rubberband.StrokeDashArray = new DoubleCollection(new double[] { 2 });                        
            
            this.adornerCanvas.Children.Add(this.rubberband);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (!this.IsMouseCaptured)
                {
                    this.CaptureMouse();
                }

                this.endPoint = e.GetPosition(this);
                this.UpdateRubberband();
                this.UpdateSelection();
                e.Handled = true;
            }
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            if (this.IsMouseCaptured)
            {
                this.ReleaseMouseCapture();
            }

            AdornerLayer adornerLayer = this.Parent as AdornerLayer;
            if (adornerLayer != null)
            {
                adornerLayer.Remove(this);
            }
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            this.adornerCanvas.Arrange(new Rect(arrangeBounds));
            return arrangeBounds;
        }

        protected override Visual GetVisualChild(int index)
        {
            return this.visuals[index];
        }

        private void UpdateRubberband()
        {
            double left = Math.Min(this.startPoint.Value.X, this.endPoint.Value.X);
            double top = Math.Min(this.startPoint.Value.Y, this.endPoint.Value.Y);

            double width = Math.Abs(this.startPoint.Value.X - this.endPoint.Value.X);
            double height = Math.Abs(this.startPoint.Value.Y - this.endPoint.Value.Y);

            this.rubberband.Width = width;
            this.rubberband.Height = height;
            Canvas.SetLeft(this.rubberband, left);
            Canvas.SetTop(this.rubberband, top);
        }

        private void UpdateSelection()
        {
            var rubberBand = new Rect(this.startPoint.Value, this.endPoint.Value);
            var designItem=(DesignItem)null;

            designCanvas.SelectionService.ClearSelection();
            foreach (Control item in this.designCanvas.Children)
            {
                Rect itemRect = VisualTreeHelper.GetDescendantBounds(item);
                Rect itemBounds = item.TransformToAncestor(designCanvas).TransformBounds(itemRect);
                if (rubberBand.Contains(itemBounds))
                {
                    if (item is DesignItem)
                    {
                        designItem = item as DesignItem;
                        if (designItem.ParentID == DefaultGuid)
                        {
                            designCanvas.SelectionService.AddToSelection(designItem);
                        }
                    }
                    else
                    {
                        designCanvas.SelectionService.AddToSelection(item as Connection);
                    }
                }
            }
        }
    }
}
