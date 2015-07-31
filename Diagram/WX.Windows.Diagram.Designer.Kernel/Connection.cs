using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using WX.Windows.Diagram.Designer.Kernel;

namespace WX.Windows.Diagram.Designer
{
    public class Connection : Shape, ISelectable, INotifyPropertyChanged
    {
        private DesignCanvas canvas = null;
        private Adorner connectionAdorner;

        #region Properties

        public Guid ID { get; set; }

        // source connector
        private Connector source;
        public Connector Source
        {
            get
            {
                return source;
            }
            set
            {
                if (source != value)
                {
                    if (source != null)
                    {
                        source.PropertyChanged -= new PropertyChangedEventHandler(OnConnectorPositionChanged);
                        source.Connections.Remove(this);
                    }

                    source = value;

                    if (source != null)
                    {
                        source.Connections.Add(this);
                        source.PropertyChanged += new PropertyChangedEventHandler(OnConnectorPositionChanged);
                    }

                    UpdatePathGeometry();
                }
            }
        }

        // sink connector
        private Connector sink;
        public Connector Sink
        {
            get { return sink; }
            set
            {
                if (sink != value)
                {
                    if (sink != null)
                    {
                        sink.PropertyChanged -= new PropertyChangedEventHandler(OnConnectorPositionChanged);
                        sink.Connections.Remove(this);
                    }

                    sink = value;

                    if (sink != null)
                    {
                        sink.Connections.Add(this);
                        sink.PropertyChanged += new PropertyChangedEventHandler(OnConnectorPositionChanged);
                    }
                    UpdatePathGeometry();
                }
            }
        }

        /*
        // connection path geometry
        private PathGeometry pathGeometry;
        public PathGeometry PathGeometry
        {
            get { return pathGeometry; }
            set
            {
                if (pathGeometry != value)
                {
                    pathGeometry = value;
                    UpdateAnchorPosition();
                    OnPropertyChanged("PathGeometry");
                }
            }
        }
        */

        // between source connector position and the beginning 
        // of the path geometry we leave some space for visual reasons; 
        // so the anchor position source really marks the beginning 
        // of the path geometry on the source side
        private Point anchorPositionSource;
        public Point AnchorPositionSource
        {
            get { return anchorPositionSource; }
            set
            {
                if (anchorPositionSource != value)
                {
                    anchorPositionSource = value;
                    OnPropertyChanged("AnchorPositionSource");
                }
            }
        }

        // slope of the path at the anchor position
        // needed for the rotation angle of the arrow
        private double anchorAngleSource = 0;
        public double AnchorAngleSource
        {
            get { return anchorAngleSource; }
            set
            {
                if (anchorAngleSource != value)
                {
                    anchorAngleSource = value;
                    OnPropertyChanged("AnchorAngleSource");
                }
            }
        }

        // analogue to source side
        private Point anchorPositionSink;
        public Point AnchorPositionSink
        {
            get { return anchorPositionSink; }
            set
            {
                if (anchorPositionSink != value)
                {
                    anchorPositionSink = value;
                    OnPropertyChanged("AnchorPositionSink");
                }
            }
        }
        // analogue to source side
        private double anchorAngleSink = 0;
        public double AnchorAngleSink
        {
            get { return anchorAngleSink; }
            set
            {
                if (anchorAngleSink != value)
                {
                    anchorAngleSink = value;
                    OnPropertyChanged("AnchorAngleSink");
                }
            }
        }

        private ArrowSymbol sourceArrowSymbol = ArrowSymbol.None;
        public ArrowSymbol SourceArrowSymbol
        {
            get { return sourceArrowSymbol; }
            set
            {
                if (sourceArrowSymbol != value)
                {
                    sourceArrowSymbol = value;
                    OnPropertyChanged("SourceArrowSymbol");
                }
            }
        }

        public ArrowSymbol sinkArrowSymbol = ArrowSymbol.Arrow;
        public ArrowSymbol SinkArrowSymbol
        {
            get { return sinkArrowSymbol; }
            set
            {
                if (sinkArrowSymbol != value)
                {
                    sinkArrowSymbol = value;
                    OnPropertyChanged("SinkArrowSymbol");
                }
            }
        }

        // specifies a point at half path length
        private Point labelPosition;
        public Point LabelPosition
        {
            get { return labelPosition; }
            set
            {
                if (labelPosition != value)
                {
                    labelPosition = value;
                    OnPropertyChanged("LabelPosition");
                }
            }
        }

        // pattern of dashes and gaps that is used to outline the connection path
        private DoubleCollection strokeDashArray;
        public DoubleCollection StrokeDashArray
        {
            get
            {
                return strokeDashArray;
            }
            set
            {
                if (strokeDashArray != value)
                {
                    strokeDashArray = value;
                    OnPropertyChanged("StrokeDashArray");
                }
            }
        }

        #endregion

        public Connection(Connector source, Connector sink,ConnectionShapeInfo connectionShapeInfo=null)
        {
            var connectionShapInfo = connectionShapeInfo;
            var designConnectionInfo = (DesignConnectionInfo)null;
            if (connectionShapeInfo != null)
            {
                designConnectionInfo=new DesignConnectionInfo(connectionShapInfo);
            }
            else
            {
                var shapInfo = new ConnectionShapeInfo();
                designConnectionInfo = new DesignConnectionInfo(shapInfo);
            }
            var currentDate = DateTime.Now;
            designConnectionInfo.CreateDate = currentDate;
            designConnectionInfo.ModifyDate = currentDate;
            designConnectionInfo.SourceID = source.ParentDesignItem.ID;
            designConnectionInfo.SourceOrientation = source.Orientation;
            designConnectionInfo.SinkID = sink.ParentDesignItem.ID;
            designConnectionInfo.SinkOrientation = sink.Orientation;
            this.ShapeInfoUnit = new ConnectionShapeInfoUnit(designConnectionInfo);
            if (designConnectionInfo.SourceSvgBuffer != null)
            {
                using (var stream = new MemoryStream(designConnectionInfo.SourceSvgBuffer))
                {
                    (this.ShapeInfoUnit as ConnectionShapeInfoUnit).SourceSvgDrawing = SvgHelper.CreateSvgViewBox(stream);
                }
            }
            if (designConnectionInfo.SinkSvgBuffer != null)
            {
                using (var stream = new MemoryStream(designConnectionInfo.SinkSvgBuffer))
                {
                    (this.ShapeInfoUnit as ConnectionShapeInfoUnit).SinkSvgDrawing = SvgHelper.CreateSvgViewBox(stream);
                }
            }
            this.SetBinding(Connection.TextProperty, BindingHelper.CreateBinding(designConnectionInfo, "Text"));
            this.SetBinding(DesignCanvas.ZIndexProperty, BindingHelper.CreateBinding(designConnectionInfo, "ZIndex"));
            designConnectionInfo.PropertyChanged += (obj, e) =>
            {
                if (e.PropertyName.Equals("PathGeometry"))
                {
                    UpdateAnchorPosition();
                }
            };

            this.ID = Guid.NewGuid();
            this.Source = source;
            this.Sink = sink;
            //base.Unloaded += new RoutedEventHandler(Connection_Unloaded); //Resolve the issue of swithing tabItem
            base.SelectChanged += (a, b) =>
            {
                if (this.IsSelected) ShowAdorner();
                else HideAdorner();
            };
        }


        protected override void OnMouseDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            // usual selection business
            canvas = VisualTreeHelper.GetParent(this) as DesignCanvas;
            if (canvas != null)
            {
                if ((Keyboard.Modifiers & (ModifierKeys.Shift | ModifierKeys.Control)) != ModifierKeys.None)
                    if (this.IsSelected)
                    {
                        canvas.SelectionService.RemoveFromSelection(this);
                    }
                    else
                    {
                        canvas.SelectionService.AddToSelection(this);
                    }
                else if (!this.IsSelected)
                {
                    canvas.SelectionService.SelectItem(this);
                }
                var designItemInfo = this.ShapeInfoUnit.ShapeInfo as DesignConnectionInfo;
                if (e.ChangedButton == MouseButton.Left && e.LeftButton == MouseButtonState.Pressed)
                {
                    if (e.ClickCount == 2)
                    {
                        canvas.RaiseDesignConnectionMouseDoubleClick(designItemInfo);
                    }
                }
                else if (e.ChangedButton == MouseButton.Right && e.RightButton == MouseButtonState.Pressed)
                {
                    if (e.ClickCount == 1)
                    {
                        canvas.RaiseDesignConnectionMouseRightClick(designItemInfo);
                    }
                }
                Focus();
            }
            e.Handled = false;
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            if (canvas.SelectionService.IsSelectionChanged)
            {
                canvas.SelectionService.RaiseDesignShapeSelectAction();
            }
        }

        void OnConnectorPositionChanged(object sender, PropertyChangedEventArgs e)
        {
            // whenever the 'Position' property of the source or sink Connector 
            // changes we must update the connection path geometry
            if (e.PropertyName.Equals("Position"))
            {
                UpdatePathGeometry();
            }
        }

        private void UpdatePathGeometry()
        {
            if (Source != null && Sink != null)
            {
                PathGeometry geometry = new PathGeometry();
                List<Point> linePoints = PathFinder.GetConnectionLine(Source.GetInfo(), Sink.GetInfo(), true);
                if (linePoints.Count > 0)
                {
                    PathFigure figure = new PathFigure();
                    figure.StartPoint = linePoints[0];
                    linePoints.Remove(linePoints[0]);
                    figure.Segments.Add(new PolyLineSegment(linePoints, true));
                    geometry.Figures.Add(figure);

                    (this.ShapeInfoUnit.ShapeInfo as DesignConnectionInfo).PathGeometry = geometry;
                }
            }
        }

        private void UpdateAnchorPosition()
        {
            Point pathStartPoint, pathTangentAtStartPoint;
            Point pathEndPoint, pathTangentAtEndPoint;
            Point pathMidPoint, pathTangentAtMidPoint;

            // the PathGeometry.GetPointAtFractionLength method gets the point and a tangent vector 
            // on PathGeometry at the specified fraction of its length
            var pathGeometry = (this.ShapeInfoUnit.ShapeInfo as DesignConnectionInfo).PathGeometry;
            pathGeometry.GetPointAtFractionLength(0, out pathStartPoint, out pathTangentAtStartPoint);
            pathGeometry.GetPointAtFractionLength(1, out pathEndPoint, out pathTangentAtEndPoint);
            pathGeometry.GetPointAtFractionLength(0.5, out pathMidPoint, out pathTangentAtMidPoint);

            // get angle from tangent vector
            this.AnchorAngleSource = Math.Atan2(-pathTangentAtStartPoint.Y, -pathTangentAtStartPoint.X) * (180 / Math.PI);
            this.AnchorAngleSink = Math.Atan2(pathTangentAtEndPoint.Y, pathTangentAtEndPoint.X) * (180 / Math.PI);

            // add some margin on source and sink side for visual reasons only
            //Related with arrow position
            pathStartPoint.Offset(pathTangentAtStartPoint.X * 5, pathTangentAtStartPoint.Y * 5);
            pathEndPoint.Offset(-pathTangentAtEndPoint.X * 2, -pathTangentAtEndPoint.Y * 2);

            this.AnchorPositionSource = pathStartPoint;
            this.AnchorPositionSink = pathEndPoint;
            this.LabelPosition = pathMidPoint;
        }

        private void ShowAdorner()
        {
            // the ConnectionAdorner is created once for each Connection
            if (this.connectionAdorner == null)
            {
                DesignCanvas canvas = VisualTreeHelper.GetParent(this) as DesignCanvas;

                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this);
                if (adornerLayer != null)
                {
                    this.connectionAdorner = new ConnectionAdorner(canvas, this);
                    adornerLayer.Add(this.connectionAdorner);
                }
            }
            this.connectionAdorner.Visibility = Visibility.Visible;
        }

        internal void HideAdorner()
        {
            if (this.connectionAdorner != null)
                this.connectionAdorner.Visibility = Visibility.Collapsed;
        }

        void Connection_Unloaded(object sender, RoutedEventArgs e)
        {
            // do some housekeeping when Connection is unloaded

            // remove event handler
            this.Source = null;
            this.Sink = null;

            // remove adorner
            if (this.connectionAdorner != null)
            {
                DesignCanvas canvas = VisualTreeHelper.GetParent(this) as DesignCanvas;

                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this);
                if (adornerLayer != null)
                {
                    adornerLayer.Remove(this.connectionAdorner);
                    this.connectionAdorner = null;
                }
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.SourceArrowSymbol = ArrowSymbol.None;
            this.SinkArrowSymbol = ArrowSymbol.Arrow;
            if (this.Template != null)
            {
                if ((this.ShapeInfoUnit as ConnectionShapeInfoUnit).SourceSvgDrawing!=null)
                {
                    var sourceAnchor = this.Template.FindName("PART_SourceAnchorContent", this) as ContentControl;
                    if (sourceAnchor != null)
                    {
                        this.SourceArrowSymbol = ArrowSymbol.None;
                        sourceAnchor.Content = (this.ShapeInfoUnit as ConnectionShapeInfoUnit).SourceSvgDrawing;
                    }
                }
                if ((this.ShapeInfoUnit as ConnectionShapeInfoUnit).SinkSvgDrawing != null)
                {
                    var sinkAnchor = this.Template.FindName("PART_SinkAnchorContent", this) as ContentControl;
                    if (sinkAnchor != null)
                    {
                        this.SinkArrowSymbol = ArrowSymbol.None;
                        sinkAnchor.Content = (this.ShapeInfoUnit as ConnectionShapeInfoUnit).SinkSvgDrawing;
                    }
                }
            }

        }

        #region INotifyPropertyChanged Members

        // we could use DependencyProperties as well to inform others of property changes
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion
    }

    public enum ArrowSymbol
    {
        None,
        Arrow,
        Diamond
    }
}
