using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace WX.Windows.Diagram.Designer
{
    public class Connector : Control, INotifyPropertyChanged
    {
        // drag start point, relative to the DesignCanvas
        private Point? dragStartPoint = null;

        public ConnectorOrientation Orientation { get; set; }

        // center position of this Connector relative to the DesignCanvas
        private Point position;
        public Point Position
        {
            get { return position; }
            set
            {
                if (position != value)
                {
                    position = value;
                    OnPropertyChanged("Position");
                }
            }
        }

        // the DesignItem this Connector belongs to;
        // retrieved from DataContext, which is set in the
        // DesignItem template
        private DesignItem parentDesignItem;
        public DesignItem ParentDesignItem
        {
            get
            {
                if (parentDesignItem == null)
                    parentDesignItem = this.DataContext as DesignItem;

                return parentDesignItem;
            }
        }

        // keep track of connections that link to this connector
        private List<Connection> connections;
        public List<Connection> Connections
        {
            get
            {
                if (connections == null)
                    connections = new List<Connection>();
                return connections;
            }
        }

        public bool IsConnectorMouseOver
        {
            get { return (bool)GetValue(IsConnectorMouseOverProperty); }
            set { SetValue(IsConnectorMouseOverProperty, value); }
        }
        public static readonly DependencyProperty IsConnectorMouseOverProperty =
            DependencyProperty.Register("IsConnectorMouseOver", typeof(bool), typeof(Connector), new PropertyMetadata(false));

        public Connector()
        {
            // fired when layout changes
            base.LayoutUpdated += new EventHandler(Connector_LayoutUpdated);            
        }

        // when the layout changes we update the position property
        void Connector_LayoutUpdated(object sender, EventArgs e)
        {
            DesignCanvas canvas = GetDesignCanvas(this);
            if (canvas != null)
            {
                //get centre position of this Connector relative to the DesignCanvas
                this.Position = this.TransformToAncestor(canvas).Transform(new Point(this.Width / 2, this.Height / 2));
            }
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DesignCanvas canvas = GetDesignCanvas(this);
            if (canvas != null)
            {
                // position relative to DesignCanvas
                this.dragStartPoint = new Point?(e.GetPosition(canvas));
                e.Handled = true;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            // if mouse button is not pressed we have no drag operation, ...
            if (e.LeftButton != MouseButtonState.Pressed)
                this.dragStartPoint = null;

            // but if mouse button is pressed and start point value is set we do have one
            if (this.dragStartPoint.HasValue)
            {
                // create connection adorner 
                DesignCanvas canvas = GetDesignCanvas(this);
                if (canvas != null)
                {
                    AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(canvas);
                    if (adornerLayer != null)
                    {
                        ConnectorAdorner adorner = new ConnectorAdorner(canvas, this);
                        if (adorner != null)
                        {
                            adornerLayer.Add(adorner);
                            e.Handled = true;
                        }
                    }
                }
            }
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            base.OnMouseEnter(e);
            IsConnectorMouseOver = true;
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            IsConnectorMouseOver = false;
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            IsConnectorMouseOver = false;
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            IsConnectorMouseOver = false;
        }

        internal ConnectorInfo GetInfo()
        {
            ConnectorInfo info = new ConnectorInfo();
            info.DesignItemLeft = DesignCanvas.GetLeft(this.ParentDesignItem);
            info.DesignItemTop = DesignCanvas.GetTop(this.ParentDesignItem);
            info.DesignItemSize = new Size(this.ParentDesignItem.ActualWidth, this.ParentDesignItem.ActualHeight);
            info.Orientation = this.Orientation;
            info.Position = this.Position;
            return info;
        }

        // iterate through visual tree to get parent DesignCanvas
        private DesignCanvas GetDesignCanvas(DependencyObject element)
        {
            while (element != null && !(element is DesignCanvas))
                element = VisualTreeHelper.GetParent(element);

            return element as DesignCanvas;
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

    // provides compact info about a connector; used for the 
    // routing algorithm, instead of hand over a full fledged Connector
    internal struct ConnectorInfo
    {
        public double DesignItemLeft { get; set; }
        public double DesignItemTop { get; set; }
        public Size DesignItemSize { get; set; }
        public Point Position { get; set; }
        public ConnectorOrientation Orientation { get; set; }
    }

    public enum ConnectorOrientation
    {
        None,
        Left,
        Top,
        Right,
        Bottom
    }
}
