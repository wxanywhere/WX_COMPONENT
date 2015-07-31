using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WX.Windows.Diagram.Designer
{
    public partial class DesignCanvas:IDesignCanvas
    {
        public void NewDesign()
        {
            var canvasInfo = new CanvasInfo();
            var currentDate = DateTime.Now;
            canvasInfo.CreateDate = currentDate;
            canvasInfo.ModifyDate = currentDate;
            canvasInfo.WorkspaceID = Guid.NewGuid();
            canvasInfo.Name = "新的设计";
            this.CanvasInfo = canvasInfo;
        }

        public void LoadDesignInfo(CanvasInfo canvasInfo, DesignItemInfo[] designItemInfos, DesignConnectionInfo[] designConnectionInfos)
        {
            var originalDesignItemInfos = new List<DesignItemInfo>();
            var originalDesignConnectionInfos = new List<DesignConnectionInfo>();
            var designItemInfo = (DesignItemInfo)null;
            var designConnectionInfo = (DesignConnectionInfo)null;
            foreach (var item in designItemInfos)
            {
                designItemInfo = new DesignItemInfo();
                designItemInfo.ShapeType = ShapeType.DesignItem;
                designItemInfo.ShapeCategory = item.ShapeCategory;
                designItemInfo.SerialNo = item.SerialNo;
                designItemInfo.GroupID = item.GroupID;
                designItemInfo.IsInGroup = item.IsInGroup;
                designItemInfo.StyleKey = item.StyleKey;
                designItemInfo.Style = item.Style;
                designItemInfo.ToolTip = item.ToolTip;
                designItemInfo.IsChecked = item.IsChecked;
                designItemInfo.IsEnabled = item.IsEnabled;
                designItemInfo.Text = item.Text;
                designItemInfo.ID = item.ID;
                designItemInfo.ZIndex = item.ZIndex;
                designItemInfo.DesignInfo.Backgroud = item.DesignInfo.Backgroud;
                designItemInfo.DesignInfo.Foreground = item.DesignInfo.Foreground;
                designItemInfo.DesignInfo.FontSize = item.DesignInfo.FontSize;
                designItemInfo.DesignInfo.Bold = item.DesignInfo.Bold;
                designItemInfo.DesignInfo.Italic = item.DesignInfo.Italic;
                designItemInfo.DesignInfo.HorizontalAlignment = item.DesignInfo.HorizontalAlignment;
                designItemInfo.DesignInfo.VerticalAlignment = item.DesignInfo.VerticalAlignment;
                designItemInfo.ParentID = item.ParentID;
                designItemInfo.X = item.X;
                designItemInfo.Y = item.Y;
                designItemInfo.Width = item.Width;
                designItemInfo.Height = item.Height;
                originalDesignItemInfos.Add(designItemInfo);
            }
            foreach (var item in designConnectionInfos)
            {
                designConnectionInfo = new DesignConnectionInfo();
                designConnectionInfo.ShapeType = ShapeType.DesignItem;
                designConnectionInfo.ShapeCategory = item.ShapeCategory;
                designConnectionInfo.SerialNo = item.SerialNo;
                designConnectionInfo.GroupID = item.GroupID;
                designConnectionInfo.IsInGroup = item.IsInGroup;
                designConnectionInfo.StyleKey = item.StyleKey;
                designConnectionInfo.Style = item.Style;
                designConnectionInfo.ToolTip = item.ToolTip;
                designConnectionInfo.IsChecked = item.IsChecked;
                designConnectionInfo.IsEnabled = item.IsEnabled;
                designConnectionInfo.Text = item.Text;
                designConnectionInfo.ID = item.ID;
                designConnectionInfo.ZIndex = item.ZIndex;
                designConnectionInfo.DesignInfo.Backgroud = item.DesignInfo.Backgroud;
                designConnectionInfo.DesignInfo.Foreground = item.DesignInfo.Foreground;
                designConnectionInfo.DesignInfo.FontSize = item.DesignInfo.FontSize;
                designConnectionInfo.DesignInfo.Bold = item.DesignInfo.Bold;
                designConnectionInfo.DesignInfo.Italic = item.DesignInfo.Italic;
                designConnectionInfo.DesignInfo.HorizontalAlignment = item.DesignInfo.HorizontalAlignment;
                designConnectionInfo.DesignInfo.VerticalAlignment = item.DesignInfo.VerticalAlignment;
                designConnectionInfo.SourceID = item.SourceID;
                designConnectionInfo.SourceOrientation = item.SourceOrientation;
                designConnectionInfo.SinkID = item.SinkID;
                designConnectionInfo.SinkOrientation = item.SinkOrientation;
                designConnectionInfo.PathGeometry = item.PathGeometry;
                originalDesignConnectionInfos.Add(designConnectionInfo);
            }
            this.OriginalDesignItemInfos = this.OriginalDesignItemInfos;
            this.OriginalDesignConnectionInfos = this.OriginalDesignConnectionInfos;
            this.CurrentDesignItemInfos.AddRange(this.OriginalDesignItemInfos);
            this.RaiseDesignItemCollectionChanged(this.CurrentDesignItemInfos.ToArray());
        }

        public void LoadConnection(ConnectionShapeInfo[] shapeInfos)
        {
            ConnectionShapeInfos = shapeInfos;
        }

        public void LoadOptionConnection(ShapeInfo[] shapeInfos)
        {
            throw new NotImplementedException();
        }

        public void LoadTipToolboxShapes(ShapeInfo[] shapeInfos)
        {
            throw new NotImplementedException();
        }

        public Tuple<CanvasInfo,DesignItemInfo[], DesignConnectionInfo[]> RetrieveDesignInfos()
        {
            throw new NotImplementedException();
        }

        public DesignItemInfo[] RetrieveDesignItemInfos()
        {
            return this.CurrentDesignItemInfos.Where(a=>!a.IsGroup).ToArray();
        }
        public DesignConnectionInfo[] RetrieveDesignConnectionInfos()
        {
            throw new NotImplementedException();
        }

        public void SelectDesignItem(DesignItemInfo designItemInfo)
        {
            var designItem = this.Children.OfType<DesignItem>().First(a => a.ID == designItemInfo.ID);
            var scrollViewer = this.Parent as ScrollViewer;
            var itemLeft = DesignCanvas.GetLeft(designItem);
            var itemTop = DesignCanvas.GetTop(designItem);
            var itemCenterLeft = itemLeft + designItem.ActualWidth / 2.0;
            var itemCenterTop = itemTop + designItem.ActualHeight / 2.0;
            if (!(itemCenterLeft > scrollViewer.HorizontalOffset && itemCenterLeft < scrollViewer.ViewportWidth + scrollViewer.HorizontalOffset &&
                itemCenterTop > scrollViewer.VerticalOffset && itemCenterTop < scrollViewer.ViewportHeight + scrollViewer.VerticalOffset))
            {
                var itemRight = this.ActualWidth - itemLeft - designItem.ActualWidth;
                var itemBottom = this.ActualHeight - itemTop - designItem.ActualHeight;
                var itemCenterMinMarginWidth = Math.Min(itemLeft, itemRight) + designItem.ActualWidth / 2.0;
                var itemCenterMinMarginHeight = Math.Min(itemTop, itemBottom) + designItem.ActualHeight / 2.0;
                var viewCenterWith = scrollViewer.ViewportWidth / 2.0;
                var viewCenterHeight = scrollViewer.ViewportHeight / 2.0;
                var x = 0.0;
                var y = 0.0;
                var width = 0.0;
                var height = 0.0;
                if (itemCenterMinMarginWidth > viewCenterWith)
                {
                    x = itemCenterLeft - viewCenterWith;
                    width = viewCenterWith * 2.0;
                }
                else
                {
                    x = itemCenterLeft - itemCenterMinMarginWidth;
                    width = itemCenterMinMarginWidth * 2.0;
                }
                if (itemCenterMinMarginHeight > viewCenterHeight)
                {
                    y = itemCenterTop - viewCenterHeight;
                    height = viewCenterHeight * 2.0;
                }
                else
                {
                    y = itemCenterTop - itemCenterMinMarginHeight;
                    height = itemCenterMinMarginHeight * 2;
                }
                this.BringIntoView(new Rect(x, y, width, height));
            }
            else
            {
                designItem.BringIntoView();
            }
            this.selectionService.SelectItem(designItem);
        }

        public event CanvasActionEventHandler DesignCanvasMouseClick;
        public virtual void RaiseDesignCanvasMouseClick(CanvasInfo canvasInfo)
        {
            if (DesignCanvasMouseClick != null)
            {
                DesignCanvasMouseClick(this, new CanvasActionEventArgs(canvasInfo));
            }
        }

        public event CanvasActionEventHandler DesignCanvasMouseDoubleClick;
        public virtual void RaiseDesignCanvasMouseDoubleClick(CanvasInfo canvasInfo)
        {
            if (DesignCanvasMouseDoubleClick != null)
            {
                DesignCanvasMouseDoubleClick(this, new CanvasActionEventArgs(canvasInfo));
            }
        }

        public event CanvasActionEventHandler DesignCanvasMouseRightClick;
        public virtual void RaiseDesignCanvasMouseRightClick(CanvasInfo canvasInfo)
        {
            if (DesignCanvasMouseRightClick != null)
            {
                DesignCanvasMouseRightClick(this, new CanvasActionEventArgs(canvasInfo));
            }
        }


        public event SingleDesignItemActionEventHandler DesignItemMouseDoubleClick;
        public virtual void RaiseDesignItemMouseDoubleClick(DesignItemInfo designItemInfo)
        {
            if (DesignItemMouseDoubleClick != null)
            {
                DesignItemMouseDoubleClick(this, new SingleDesignItemActionEventArgs(designItemInfo));
            }
        }

        public event SingleDesignItemActionEventHandler DesignItemMouseRightClick;
        public virtual void RaiseDesignItemMouseRightClick(DesignItemInfo designItemInfo)
        {
            if (DesignItemMouseRightClick != null)
            {
                DesignItemMouseRightClick(this, new SingleDesignItemActionEventArgs(designItemInfo));
            }
        }

        public event SingleDesignItemActionEventHandler DesignItemLinkButtonClick;
        public virtual void RaiseDesignItemLinkButtonClick(DesignItemInfo designItemInfo)
        {
            if (DesignItemLinkButtonClick != null)
            {
                DesignItemLinkButtonClick(this, new SingleDesignItemActionEventArgs(designItemInfo));
            }
        }

        public event SingleDesignItemActionEventHandler DesignItemLinkButtonDoubleClick;
        public virtual void RaiseDesignItemLinkButtonDoubleClick(DesignItemInfo designItemInfo)
        {
            if (DesignItemLinkButtonDoubleClick != null)
            {
                DesignItemLinkButtonDoubleClick(this, new SingleDesignItemActionEventArgs(designItemInfo));
            }
        }


        public event SingleDesignConnectionActionEventHandler DesignConnectionMouseDoubleClick;
        public virtual void RaiseDesignConnectionMouseDoubleClick(DesignConnectionInfo designConnectionInfo)
        {
            if (DesignConnectionMouseDoubleClick != null)
            {
                DesignConnectionMouseDoubleClick(this, new SingleDesignConnectionActionEventArgs(designConnectionInfo));
            }
        }

        public event SingleDesignConnectionActionEventHandler DesignConnectionMouseRightClick;
        public virtual void RaiseDesignConnectionMouseRightClick(DesignConnectionInfo designConnectionInfo)
        {
            if (DesignConnectionMouseRightClick != null)
            {
                DesignConnectionMouseRightClick(this, new SingleDesignConnectionActionEventArgs(designConnectionInfo));
            }
        }

        public event SingleDesignItemActionEventHandler DesignItemSelected;
        public virtual void RaiseDesignItemSelected(DesignItemInfo designItemInfo)
        {
            if (DesignItemSelected != null)
            {
                DesignItemSelected(this, new SingleDesignItemActionEventArgs(designItemInfo));
            }
        }

        public event SingleDesignConnectionActionEventHandler DesignConnectionSelected;
        public virtual void RaiseDesignConnectionSelected(DesignConnectionInfo designConnectionInfo)
        {
            if (DesignConnectionSelected != null)
            {
                DesignConnectionSelected(this, new SingleDesignConnectionActionEventArgs(designConnectionInfo));
            }
        }

        public event DesignItemValidationEventHandler PreAddDesignItem;
        public virtual void RaisePreAddDesignItem(DesignItemInfo[] designItemInfos)
        {
            if (PreAddDesignItem != null)
            {
                PreAddDesignItem(this, new DesignItemValidationEventArgs(designItemInfos));
            }
        }

        public event DesignItemValidationEventHandler AddingDesignItem;
        public virtual void RaiseAddingDesignItem(DesignItemInfo[] designItemInfos)
        {
            if (AddingDesignItem != null)
            {
                AddingDesignItem(this, new DesignItemValidationEventArgs(designItemInfos));
            }
        }

        public event SingleDesignItemActionEventHandler AddedDesignItem;
        public virtual void RaiseAddedDesignItem(DesignItemInfo designItemInfo)
        {
            if (AddedDesignItem != null)
            {
                AddedDesignItem(this, new SingleDesignItemActionEventArgs(designItemInfo));
            }
        }

        public event DesignConnectionValidationEventHandler PreAddDesignConnection;
        public virtual void RaisePreAddDesignConnection(DesignItemInfo sourceItemInfo, DesignItemInfo sinkItemInfo)
        {
            if (PreAddDesignConnection != null)
            {
                PreAddDesignConnection(this, new DesignConnectionValidationEventArgs(sourceItemInfo, sinkItemInfo));
            }
        }

        public event DesignConnectionValidationEventHandler AddingDesignConnection;
        public virtual void RaiseAddingDesignConnection(DesignItemInfo sourceItemInfo, DesignItemInfo sinkItemInfo)
        {
            if (AddingDesignConnection != null)
            {
                AddingDesignConnection(this, new DesignConnectionValidationEventArgs(sourceItemInfo, sinkItemInfo));
            }
        }

        public event SingleDesignConnectionActionEventHandler AddedDesignConnection;
        public virtual void RaiseAddedDesignConnection(DesignConnectionInfo designConnectionInfo)
        {
            if (AddedDesignConnection != null)
            {
                AddedDesignConnection(this, new SingleDesignConnectionActionEventArgs(designConnectionInfo));
            }
        }

        //-----------------------------------

        public event MultiDesignItemActionEventHandler DesignItemMultiSelected;
        public virtual void RaiseDesignItemMultiSelected(DesignItemInfo[] designItemInfos)
        {
            if (DesignItemMultiSelected != null)
            {
                DesignItemMultiSelected(this, new MultiDesignItemActionEventArgs(designItemInfos));
            }
        }

        public event MultiDesignShapeActionEventHandler DesignShapeMultiSelected;
        public virtual void RaiseDesignShapeMultiSelected(DesignItemInfo[] designItemInfos, DesignConnectionInfo[] designConnectionInfos)
        {
            if (DesignShapeMultiSelected != null)
            {
                DesignShapeMultiSelected(this, new MultiDesignShapeActionEventArgs(designItemInfos, designConnectionInfos));
            }
        }

        public event MultiDesignShapeActionEventHandler AddingDesignShapes;
        public virtual void RaiseAddingDesignShapes(DesignItemInfo[] designItemInfos, DesignConnectionInfo[] designConnectionInfos)
        {
            if (AddingDesignShapes != null)
            {
                AddingDesignShapes(this, new MultiDesignShapeActionEventArgs(designItemInfos, designConnectionInfos));
            }
        }

        public event MultiDesignShapeActionEventHandler AddedDesignShapes;
        public virtual void RaiseAddedDesignShapes(DesignItemInfo[] designItemInfos, DesignConnectionInfo[] designConnectionInfos)
        {
            if (AddedDesignShapes != null)
            {
                AddedDesignShapes(this, new MultiDesignShapeActionEventArgs(designItemInfos, designConnectionInfos));
            }
        }

        public event MultiDesignShapeActionEventHandler DeletingDesignShapes;
        public virtual void RaiseDeletingDesignShapes(DesignItemInfo[] designItemInfos, DesignConnectionInfo[] designConnectionInfos)
        {
            if (DeletingDesignShapes != null)
            {
                DeletingDesignShapes(this, new MultiDesignShapeActionEventArgs(designItemInfos, designConnectionInfos));
            }
        }

        public event MultiDesignShapeActionEventHandler DeletedDesignShapes;
        public virtual void RaiseDeletedDesignShapes(DesignItemInfo[] designItemInfos, DesignConnectionInfo[] designConnectionInfos)
        {
            if (DeletedDesignShapes != null)
            {
                DeletedDesignShapes(this, new MultiDesignShapeActionEventArgs(designItemInfos, designConnectionInfos));
            }
        }

        public event MultiDesignShapeActionEventHandler ModifyingDesignShapes;
        public virtual void RaiseModifyingDesignShapes(DesignItemInfo[] designItemInfos, DesignConnectionInfo[] designConnectionInfos)
        {
            if (ModifyingDesignShapes != null)
            {
                ModifyingDesignShapes(this, new MultiDesignShapeActionEventArgs(designItemInfos, designConnectionInfos));
            }
        }

        public event MultiDesignShapeActionEventHandler ModifiedDesignShapes;
        public virtual void RaiseModifiedDesignShapes (DesignItemInfo[] designItemInfos, DesignConnectionInfo[] designConnectionInfos)
        {
            if (ModifiedDesignShapes != null)
            {
                ModifiedDesignShapes(this, new MultiDesignShapeActionEventArgs(designItemInfos, designConnectionInfos));
            }
        }

        public event DesignItemCollectionChangedEventHandler DesignItemCollectionChanged;
        public virtual void RaiseDesignItemCollectionChanged(DesignItemInfo[] designItemInfos)
        {
            if (DesignItemCollectionChanged != null)
            {
                DesignItemCollectionChanged(this, new DesignItemCollectionChangedEventArgs(designItemInfos));
            }
        }

        public event WorkspaceActionEventHandler DesignWorkspaceSaving;
        public virtual void RaiseDesignWorkspaceSaving(CanvasInfo canvasInfo, DesignItemInfo[] designItemInfos, DesignConnectionInfo[] designConnectionInfos)
        {
            if (DesignWorkspaceSaving != null)
            {
                DesignWorkspaceSaving(this, new WorkspaceActionEventArgs(canvasInfo,designItemInfos, designConnectionInfos));
            }
        }
    }
}
