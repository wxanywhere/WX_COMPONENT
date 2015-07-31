using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;

namespace WX.Windows.Diagram.Designer
{
    public class ConnectorAdorner : Adorner
    {
        private PathGeometry pathGeometry;
        private DesignCanvas designCanvas;
        private Connector sourceConnector;
        private Pen drawingPen;

        private DesignItem hitDesignItem;
        private DesignItem HitDesignItem
        {
            get { return hitDesignItem; }
            set
            {
                if (hitDesignItem != value)
                {
                    if (hitDesignItem != null)
                        hitDesignItem.IsDragConnectionOver = false;

                    hitDesignItem = value;

                    if (hitDesignItem != null)
                        hitDesignItem.IsDragConnectionOver = true;
                }
            }
        }

        private Connector hitConnector;
        private Connector HitConnector
        {
            get { return hitConnector; }
            set
            {
                if (hitConnector != value)
                {
                    if (hitConnector != null)
                        hitConnector.IsConnectorMouseOver = false;

                    hitConnector = value;

                    if (hitConnector != null)
                        hitConnector.IsConnectorMouseOver = true;
                }
            }
        }

        public ConnectorAdorner(DesignCanvas canvas, Connector sourceConnector)
            : base(canvas)
        {
            this.designCanvas = canvas;
            this.sourceConnector = sourceConnector;
            drawingPen = new Pen(Brushes.LightSlateGray, 1);
            drawingPen.LineJoin = PenLineJoin.Round;
            this.Cursor = Cursors.Cross;
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            //if (e.LeftButton == MouseButtonState.Pressed && hitConnector.IsConnectorMouseOver)
            //{

            //}
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            if (HitConnector != null)
            {
                this.hitConnector.IsConnectorMouseOver = false;
                Connector sourceConnector = this.sourceConnector;
                Connector sinkConnector = this.HitConnector;
                var sourceItemInfo = (this.sourceConnector.ParentDesignItem.Content as Shape).ShapeInfoUnit.ShapeInfo as DesignItemInfo;
                var sinkItemInfo = (this.hitConnector.ParentDesignItem.Content as Shape).ShapeInfoUnit.ShapeInfo as DesignItemInfo;
                this.designCanvas.RaiseAddingDesignConnection(sourceItemInfo, sinkItemInfo);
                var defaultConnectionShapeInfo = (ConnectionShapeInfo)null;
                if (this.designCanvas.ConnectionShapeInfos!=null && this.designCanvas.ConnectionShapeInfos.Length > 0)
                {
                    defaultConnectionShapeInfo = this.designCanvas.ConnectionShapeInfos[0];
                }
                Connection newConnection = new Connection(sourceConnector, sinkConnector, defaultConnectionShapeInfo);
                this.designCanvas.RaiseAddedDesignConnection(newConnection.ShapeInfoUnit.ShapeInfo as DesignConnectionInfo);
                Canvas.SetZIndex(newConnection, designCanvas.Children.Count);

                this.designCanvas.Children.Add(newConnection);
                
            }
            if (HitDesignItem != null)
            {
                this.HitDesignItem.IsDragConnectionOver = false;
            }

            if (this.IsMouseCaptured) this.ReleaseMouseCapture();

            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this.designCanvas);
            if (adornerLayer != null)
            {
                adornerLayer.Remove(this);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (!this.IsMouseCaptured) this.CaptureMouse();
                HitTesting(e.GetPosition(this));
                this.pathGeometry = GetPathGeometry(e.GetPosition(this));
                this.InvalidateVisual();
                if (this.hitConnector != null && hitConnector.IsConnectorMouseOver)
                {
                    var sourceItemInfo = (this.sourceConnector.ParentDesignItem.Content as Shape).ShapeInfoUnit.ShapeInfo as DesignItemInfo;
                    var sinkItemInfo = (this.hitConnector.ParentDesignItem.Content as Shape).ShapeInfoUnit.ShapeInfo as DesignItemInfo;
                    this.designCanvas.RaisePreAddDesignConnection(sourceItemInfo,sinkItemInfo);
                }
            }
            else
            {
                if (this.IsMouseCaptured) this.ReleaseMouseCapture();
            }
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            dc.DrawGeometry(null, drawingPen, this.pathGeometry);

            // without a background the OnMouseMove event would not be fired
            // Alternative: implement a Canvas as a child of this adorner, like
            // the ConnectionAdorner does.
            dc.DrawRectangle(Brushes.Transparent, null, new Rect(RenderSize));
        }

        private PathGeometry GetPathGeometry(Point position)
        {
            PathGeometry geometry = new PathGeometry();

            ConnectorOrientation targetOrientation;
            if (HitConnector != null)
                targetOrientation = HitConnector.Orientation;
            else
                targetOrientation = ConnectorOrientation.None;

            List<Point> pathPoints = PathFinder.GetConnectionLine(sourceConnector.GetInfo(), position, targetOrientation);

            if (pathPoints.Count > 0)
            {
                PathFigure figure = new PathFigure();
                figure.StartPoint = pathPoints[0];
                pathPoints.Remove(pathPoints[0]);
                figure.Segments.Add(new PolyLineSegment(pathPoints, true));
                geometry.Figures.Add(figure);
            }

            return geometry;
        }

        private void HitTesting(Point hitPoint)
        {
            bool hitConnectorFlag = false;

            DependencyObject hitObject = designCanvas.InputHitTest(hitPoint) as DependencyObject;
            while (hitObject != null &&
                   hitObject != sourceConnector.ParentDesignItem &&
                   hitObject.GetType() != typeof(DesignCanvas))
            {
                if (hitObject is Connector)
                {
                    HitConnector = hitObject as Connector;
                    hitConnectorFlag = true;
                }

                if (hitObject is DesignItem)
                {
                    HitDesignItem = hitObject as DesignItem;
                    if (!hitConnectorFlag)
                        HitConnector = null;
                    return;
                }
                hitObject = VisualTreeHelper.GetParent(hitObject);
            }

            HitConnector = null;
            HitDesignItem = null;
        }
    }
}
