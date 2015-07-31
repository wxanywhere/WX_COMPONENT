using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading;

namespace WX.Windows.Diagram.Designer
{
    internal class SelectionService
    {
        private DesignCanvas designCanvas;

        private List<ISelectable> currentSelection;
        internal List<ISelectable> CurrentSelection
        {
            get
            {
                if (currentSelection == null)
                {
                    currentSelection = new List<ISelectable>();
                }
                return currentSelection;
            }
        }

        public bool IsSelectionChanged { get; set; }

        public void RaiseDesignShapeSelectAction()
        {
            if (this.designCanvas != null)
            {
                var selectedShapes = this.designCanvas.SelectionService.CurrentSelection;
                if (selectedShapes.Count == 1)
                {
                    if (selectedShapes[0] is Connection)
                    {
                        var designConnectionInfo = designCanvas.CurrentDesignConnectionInfos.FirstOrDefault(a => (selectedShapes[0] as Connection).ID == a.ID);
                        if (designConnectionInfo != null)
                        {
                            this.designCanvas.RaiseDesignConnectionSelected(designConnectionInfo);
                        }
                    }
                    else if (selectedShapes[0] is DesignItem)
                    {
                        var designItemInfo = designCanvas.CurrentDesignItemInfos.FirstOrDefault(a => (selectedShapes[0] as DesignItem).ID == a.ID);
                        if (designItemInfo != null)
                        {
                            this.designCanvas.RaiseDesignItemSelected(designItemInfo);
                        }
                    }
                }
                else if (selectedShapes.Count > 1)
                {
                    var designItemInfos = designCanvas.CurrentDesignItemInfos.Where(a => selectedShapes.Any(b => (b is DesignItem) && (b as DesignItem).ID == a.ID)).ToArray();
                    var designConnectionInfos = designCanvas.CurrentDesignConnectionInfos.Where(a => selectedShapes.Any(b => (b is Connection) && (b as Connection).ID == a.ID)).ToArray();
                    if (designConnectionInfos.Length==0)
                    {
                        this.designCanvas.RaiseDesignItemMultiSelected(designItemInfos);
                    }
                    else
                    {
                        this.designCanvas.RaiseDesignShapeMultiSelected(designItemInfos, designConnectionInfos);
                    }
                }
            }
            IsSelectionChanged = false;
        }

        public SelectionService(DesignCanvas canvas)
        {
            this.designCanvas = canvas;
        }

        internal void SelectItem(ISelectable item)
        {
            this.ClearSelection();
            this.AddToSelection(item);
        }

        internal void AddToSelection(ISelectable item)
        {
            if (item is IGroupable)
            {
                List<IGroupable> groupItems = GetGroupMembers(item as IGroupable);

                foreach (ISelectable groupItem in groupItems)
                {
                    groupItem.IsSelected = true;
                    CurrentSelection.Add(groupItem);
                }
            }
            else
            {
                item.IsSelected = true;
                CurrentSelection.Add(item);
            }
            IsSelectionChanged = true;
        }

        internal void RemoveFromSelection(ISelectable item)
        {
            if (item is IGroupable)
            {
                List<IGroupable> groupItems = GetGroupMembers(item as IGroupable);

                foreach (ISelectable groupItem in groupItems)
                {
                    groupItem.IsSelected = false;
                    CurrentSelection.Remove(groupItem);
                }
            }
            else
            {
                item.IsSelected = false;
                CurrentSelection.Remove(item);
            }
            IsSelectionChanged = true;
        }

        internal void ClearSelection()
        {
            CurrentSelection.ForEach(item => item.IsSelected = false);
            CurrentSelection.Clear();
        }

        internal void SelectAll()
        {
            ClearSelection();
            CurrentSelection.AddRange(designCanvas.Children.OfType<ISelectable>());
            CurrentSelection.ForEach(item => item.IsSelected = true);
            IsSelectionChanged = true;
        }

        internal void DragSelect()
        {
            ClearSelection();
            var shapes=designCanvas.Children.OfType<ISelectable>().Where(a=>a.IsSelected).ToArray();
            CurrentSelection.AddRange(shapes);
            IsSelectionChanged = true;
        }

        internal List<IGroupable> GetGroupMembers(IGroupable item)
        {
            IEnumerable<IGroupable> list = designCanvas.Children.OfType<IGroupable>();
            IGroupable rootItem = GetRoot(list, item);
            return GetGroupMembers(list, rootItem);
        }

        internal IGroupable GetGroupRoot(IGroupable item)
        {
            IEnumerable<IGroupable> list = designCanvas.Children.OfType<IGroupable>();
            return GetRoot(list, item);
        }

        private IGroupable GetRoot(IEnumerable<IGroupable> list, IGroupable node)
        {
            if (node == null || node.ParentID == Guid.Empty)
            {
                return node;
            }
            else
            {
                foreach (IGroupable item in list)
                {
                    if (item.ID == node.ParentID)
                    {
                        return GetRoot(list, item);
                    }
                }
                return null;
            }
        }

        private List<IGroupable> GetGroupMembers(IEnumerable<IGroupable> list, IGroupable parent)
        {
            List<IGroupable> groupMembers = new List<IGroupable>();
            groupMembers.Add(parent);

            var children = list.Where(node => node.ParentID == parent.ID);

            foreach (IGroupable child in children)
            {
                groupMembers.AddRange(GetGroupMembers(list, child));
            }

            return groupMembers;
        }
    }
}
