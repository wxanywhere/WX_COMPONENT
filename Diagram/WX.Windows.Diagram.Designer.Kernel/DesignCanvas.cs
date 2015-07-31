using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Xml;
using System.Collections.ObjectModel;

namespace WX.Windows.Diagram.Designer
{
    public partial class DesignCanvas : Canvas
    {
        public CanvasInfo CanvasInfo { get; set; }
        public ConnectionShapeInfo[] ConnectionShapeInfos { get; set; }
        public CanvasInfo OriginalCanvasInfo { get; set; }
        public DesignItemInfo[] OriginalDesignItemInfos { get; set; }
        public DesignConnectionInfo[] OriginalDesignConnectionInfos { get; set; }
        private List<DesignItemInfo> _CurrentDesignItemInfos;
        public List<DesignItemInfo> CurrentDesignItemInfos
        {
            get
            {
                if (_CurrentDesignItemInfos == null)
                {
                    _CurrentDesignItemInfos = new List<DesignItemInfo>();
                }
                return _CurrentDesignItemInfos;
            }
        }

        private List<DesignConnectionInfo> _CurrentDesignConnectionInfos;
        public List<DesignConnectionInfo> CurrentDesignConnectionInfos
        {
            get
            {
                if (_CurrentDesignConnectionInfos == null)
                {
                    _CurrentDesignConnectionInfos = new List<DesignConnectionInfo>();
                }
                return _CurrentDesignConnectionInfos;
            }
        }
      

        private Point? rubberbandSelectionStartPoint = null;

        private event SingleDesignItemActionEventHandler _LinkButtonClick;
        public event SingleDesignItemActionEventHandler LinkButtonClick
        {
          add{ this._LinkButtonClick += value;}
          remove{this._LinkButtonClick -= value;}
        }
        internal void OnLinkButtonClick(DesignItemInfo designItemInfo)
        {
          if (this._LinkButtonClick == null) return;
          this._LinkButtonClick(this, new SingleDesignItemActionEventArgs(designItemInfo));
        }

        private event SingleDesignItemActionEventHandler _AddConnection;
        public event SingleDesignItemActionEventHandler AddConnection
        {
          add { this._AddConnection += value; }
          remove { this._AddConnection -= value; }
        }
        internal void OnAddConnection(Connection connection)
        {
          if (this._AddConnection == null) return;
          this._AddConnection(this, new SingleDesignItemActionEventArgs(connection.ShapeInfoUnit.ShapeInfo as DesignItemInfo));
        }

        private SelectionService selectionService;
        internal SelectionService SelectionService
        {
            get
            {
                if (selectionService == null)
                    selectionService = new SelectionService(this);

                return selectionService;
            }
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Source == this)
            {
                // in case that this click is the start of a 
                // drag operation we cache the start point
                this.rubberbandSelectionStartPoint = new Point?(e.GetPosition(this));

                // if you click directly on the canvas all 
                // selected items are 'de-selected'
                //SelectionService.ClearSelection();
                if (e.ChangedButton==MouseButton.Left && e.LeftButton == MouseButtonState.Pressed)
                {
                    if (e.ClickCount == 1)
                    {
                        this.RaiseDesignCanvasMouseClick(this.CanvasInfo);
                    }
                    else if(e.ClickCount == 2)
                    {
                        this.RaiseDesignCanvasMouseDoubleClick(this.CanvasInfo);
                    }
                }
                else if (e.ChangedButton == MouseButton.Right && e.RightButton == MouseButtonState.Pressed)
                {
                    if (e.ClickCount == 1)
                    {
                        this.RaiseDesignCanvasMouseRightClick(this.CanvasInfo);
                    }
                }
                Focus();
                e.Handled = true;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            // if mouse button is not pressed we have no drag operation, ...
            if (e.LeftButton != MouseButtonState.Pressed)
                this.rubberbandSelectionStartPoint = null;

            // ... but if mouse button is pressed and start
            // point value is set we do have one
            if (this.rubberbandSelectionStartPoint.HasValue)
            {
                // create rubberband adorner
                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this);
                if (adornerLayer != null)
                {
                    RubberbandAdorner adorner = new RubberbandAdorner(this, rubberbandSelectionStartPoint);
                    if (adorner != null)
                    {
                        adornerLayer.Add(adorner);
                    }
                }
            }
            e.Handled = true;
        }

        protected override void OnDragEnter(DragEventArgs e)
        {
            base.OnDragEnter(e);
            DragObject dragObject = e.Data.GetData(typeof(DragObject)) as DragObject;
            if (dragObject != null && dragObject.ShapeInfoUnites != null)
            {
                if (dragObject.ShapeInfoUnites.Length == 1)
                {
                    if (dragObject.ShapeInfoUnites[0].ShapeInfo.ShapeType == ShapeType.DesignItem)
                    {
                        var designItemInfos = this.CurrentDesignItemInfos.ToArray();
                        this.RaisePreAddDesignItem(designItemInfos);
                    }
                    else
                    {

                    }
                }
                else
                {
                    //ToDo
                }
            }
        }

        protected override void OnDrop(DragEventArgs e)
        {
            base.OnDrop(e);
            DragObject dragObject = e.Data.GetData(typeof(DragObject)) as DragObject;
            if (dragObject != null && dragObject.ShapeInfoUnites != null)
            {
                if (dragObject.ShapeInfoUnites.Length == 1)
                {
                    if (dragObject.ShapeInfoUnites[0].ShapeInfo.ShapeType == ShapeType.DesignItem)
                    {
                        var designItemInfo = new DesignItemInfo(dragObject.ShapeInfoUnites[0].ShapeInfo as ItemShapeInfo);
                        designItemInfo.ID = Guid.NewGuid();
                        designItemInfo.ZIndex = this.Children.Count;
                        var designItemInfos = this.CurrentDesignItemInfos.Union(new DesignItemInfo[] { designItemInfo }).ToArray();
                        this.RaiseAddingDesignItem(designItemInfos);
                        var designItem = DesignShapeHelper.CreateDesignItem(designItemInfo);
                        Point position = e.GetPosition(this);
                        if (dragObject.DesiredSize.HasValue)
                        {
                            Size desiredSize = dragObject.DesiredSize.Value;
                            designItem.Width = desiredSize.Width;
                            designItem.Height = desiredSize.Height;

                            DesignCanvas.SetLeft(designItem, Math.Max(0, position.X - designItem.Width / 2));
                            DesignCanvas.SetTop(designItem, Math.Max(0, position.Y - designItem.Height / 2));
                        }
                        else
                        {
                            DesignCanvas.SetLeft(designItem, Math.Max(0, position.X));
                            DesignCanvas.SetTop(designItem, Math.Max(0, position.Y));
                        }

                        Canvas.SetZIndex(designItem, this.Children.Count);
                        this.Children.Add(designItem);
                        SetConnectorDecoratorTemplate(designItem);

                        this.SelectionService.SelectItem(designItem);
                        designItem.Focus();
                        this.RaiseAddedDesignItem(designItemInfo);
                        this.CurrentDesignItemInfos.Add(designItemInfo);
                        this.RaiseDesignItemCollectionChanged(this.CurrentDesignItemInfos.Where(a=>!a.IsGroup).ToArray());
                    }
                    else
                    {
                        //ToDo
                    }
                }
                else
                {
                    //ToDo
                }
                if (this.selectionService.IsSelectionChanged)
                {
                    this.selectionService.RaiseDesignShapeSelectAction();
                }
                e.Handled = true;
            }
        }

        protected override Size MeasureOverride(Size constraint)
        {
            Size size = new Size();

            foreach (UIElement element in this.InternalChildren)
            {
                double left = Canvas.GetLeft(element);
                double top = Canvas.GetTop(element);
                left = double.IsNaN(left) ? 0 : left;
                top = double.IsNaN(top) ? 0 : top;

                //measure desired size for each child
                element.Measure(constraint);

                Size desiredSize = element.DesiredSize;
                if (!double.IsNaN(desiredSize.Width) && !double.IsNaN(desiredSize.Height))
                {
                    size.Width = Math.Max(size.Width, left + desiredSize.Width);
                    size.Height = Math.Max(size.Height, top + desiredSize.Height);
                }
            }
            // add margin 
            size.Width += (this.Parent as ScrollViewer).ViewportWidth;
            size.Height += (this.Parent as ScrollViewer).ViewportWidth;
            return size;
        }

        private void SetConnectorDecoratorTemplate(DesignItem item)
        {
            if (item.ApplyTemplate() && item.Content is UIElement)
            {
                ControlTemplate template = DesignItem.GetConnectorDecoratorTemplate(item.Content as UIElement);
                Control decorator = item.Template.FindName("PART_ConnectorDecorator", item) as Control;
                if (decorator != null && template != null)
                    decorator.Template = template;
            }
        }

    }
}
