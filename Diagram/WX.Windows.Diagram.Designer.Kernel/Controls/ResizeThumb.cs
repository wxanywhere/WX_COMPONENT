﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace WX.Windows.Diagram.Designer
{
    public class ResizeThumb : Thumb
    {
        public ResizeThumb()
        {
            base.DragDelta += new DragDeltaEventHandler(ResizeThumb_DragDelta);
        }

        void ResizeThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            DesignItem designItem = this.DataContext as DesignItem;
            DesignCanvas canvas = VisualTreeHelper.GetParent(designItem) as DesignCanvas;

            if (designItem != null && canvas != null && designItem.IsSelected)
            {
                double minLeft, minTop, minDeltaHorizontal, minDeltaVertical;
                double dragDeltaVertical, dragDeltaHorizontal, scale;

                IEnumerable<DesignItem> selectedDesignItems = canvas.SelectionService.CurrentSelection.OfType<DesignItem>();

                CalculateDragLimits(selectedDesignItems, out minLeft, out minTop,
                                    out minDeltaHorizontal, out minDeltaVertical);

                foreach (DesignItem item in selectedDesignItems)
                {
                    if (item != null && item.ParentID == Guid.Empty)
                    {
                        switch (base.VerticalAlignment)
                        {
                            case VerticalAlignment.Bottom:
                                dragDeltaVertical = Math.Min(-e.VerticalChange, minDeltaVertical);
                                scale = (item.ActualHeight - dragDeltaVertical) / item.ActualHeight;
                                DragBottom(scale, item, canvas.SelectionService);
                                break;
                            case VerticalAlignment.Top:
                                double top = Canvas.GetTop(item);
                                dragDeltaVertical = Math.Min(Math.Max(-minTop, e.VerticalChange), minDeltaVertical);
                                scale = (item.ActualHeight - dragDeltaVertical) / item.ActualHeight;
                                DragTop(scale, item, canvas.SelectionService);
                                break;
                            default:
                                break;
                        }

                        switch (base.HorizontalAlignment)
                        {
                            case HorizontalAlignment.Left:
                                double left = Canvas.GetLeft(item);
                                dragDeltaHorizontal = Math.Min(Math.Max(-minLeft, e.HorizontalChange), minDeltaHorizontal);
                                scale = (item.ActualWidth - dragDeltaHorizontal) / item.ActualWidth;
                                DragLeft(scale, item, canvas.SelectionService);
                                break;
                            case HorizontalAlignment.Right:
                                dragDeltaHorizontal = Math.Min(-e.HorizontalChange, minDeltaHorizontal);
                                scale = (item.ActualWidth - dragDeltaHorizontal) / item.ActualWidth;
                                DragRight(scale, item, canvas.SelectionService);
                                break;
                            default:
                                break;
                        }
                    }
                }
                e.Handled = true;
            }
        }

        #region Helper methods

        private void DragLeft(double scale, DesignItem item, SelectionService selectionService)
        {
            IEnumerable<DesignItem> groupItems = selectionService.GetGroupMembers(item).Cast<DesignItem>();

            double groupLeft = Canvas.GetLeft(item) + item.Width;
            foreach (DesignItem groupItem in groupItems)
            {
                double groupItemLeft = Canvas.GetLeft(groupItem);
                double delta = (groupLeft - groupItemLeft) * (scale - 1);
                Canvas.SetLeft(groupItem, groupItemLeft - delta);
                groupItem.Width = groupItem.ActualWidth * scale;
            }
        }

        private void DragTop(double scale, DesignItem item, SelectionService selectionService)
        {
            IEnumerable<DesignItem> groupItems = selectionService.GetGroupMembers(item).Cast<DesignItem>();
            double groupBottom = Canvas.GetTop(item) + item.Height;
            foreach (DesignItem groupItem in groupItems)
            {
                double groupItemTop = Canvas.GetTop(groupItem);
                double delta = (groupBottom - groupItemTop) * (scale - 1);
                Canvas.SetTop(groupItem, groupItemTop - delta);
                groupItem.Height = groupItem.ActualHeight * scale;
            }
        }

        private void DragRight(double scale, DesignItem item, SelectionService selectionService)
        {
            IEnumerable<DesignItem> groupItems = selectionService.GetGroupMembers(item).Cast<DesignItem>();

            double groupLeft = Canvas.GetLeft(item);
            foreach (DesignItem groupItem in groupItems)
            {
                double groupItemLeft = Canvas.GetLeft(groupItem);
                double delta = (groupItemLeft - groupLeft) * (scale - 1);

                Canvas.SetLeft(groupItem, groupItemLeft + delta);
                groupItem.Width = groupItem.ActualWidth * scale;
            }
        }

        private void DragBottom(double scale, DesignItem item, SelectionService selectionService)
        {
            IEnumerable<DesignItem> groupItems = selectionService.GetGroupMembers(item).Cast<DesignItem>();
            double groupTop = Canvas.GetTop(item);
            foreach (DesignItem groupItem in groupItems)
            {
                double groupItemTop = Canvas.GetTop(groupItem);
                double delta = (groupItemTop - groupTop) * (scale - 1);

                Canvas.SetTop(groupItem, groupItemTop + delta);
                groupItem.Height = groupItem.ActualHeight * scale;
            }
        }

        private void CalculateDragLimits(IEnumerable<DesignItem> selectedItems, out double minLeft, out double minTop, out double minDeltaHorizontal, out double minDeltaVertical)
        {
            minLeft = double.MaxValue;
            minTop = double.MaxValue;
            minDeltaHorizontal = double.MaxValue;
            minDeltaVertical = double.MaxValue;

            // drag limits are set by these parameters: canvas top, canvas left, minHeight, minWidth
            // calculate min value for each parameter for each item
            foreach (DesignItem item in selectedItems)
            {
                double left = Canvas.GetLeft(item);
                double top = Canvas.GetTop(item);

                minLeft = double.IsNaN(left) ? 0 : Math.Min(left, minLeft);
                minTop = double.IsNaN(top) ? 0 : Math.Min(top, minTop);

                minDeltaVertical = Math.Min(minDeltaVertical, item.ActualHeight - item.MinHeight);
                minDeltaHorizontal = Math.Min(minDeltaHorizontal, item.ActualWidth - item.MinWidth);
            }
        }

        #endregion
    }
}
