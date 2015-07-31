using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WX.Windows.Diagram.Designer
{
    public partial class DesignerKernel:IDesignerKernel
    {
        public void NewDesign()
        {
            this.canvas.NewDesign();
        }

        public void LoadDesignInfo(CanvasInfo canvasInfo, DesignItemInfo[] designItemInfos, DesignConnectionInfo[] designConnectionInfos)
        {
            this.canvas.LoadDesignInfo(canvasInfo, designItemInfos, designConnectionInfos);
        }

        public void LoadConnection(ConnectionShapeInfo[] shapeInfos)
        {
            this.canvas.LoadConnection(shapeInfos);
        }

        public void LoadOptionConnection(ShapeInfo[] shapeInfos)
        {
            throw new NotImplementedException();
        }

        public void LoadTipToolboxShapes(ShapeInfo[] shapeInfos)
        {
            throw new NotImplementedException();
        }

        public Tuple<CanvasInfo, DesignItemInfo[], DesignConnectionInfo[]> RetrieveDesignInfos()
        {
            return this.canvas.RetrieveDesignInfos();
        }

        public DesignItemInfo[] RetrieveDesignItemInfos()
        {
            return this.canvas.RetrieveDesignItemInfos();
        }

        public DesignConnectionInfo[] RetrieveDesignConnectionInfos()
        {
            return this.canvas.RetrieveDesignConnectionInfos();
        }

        public void SelectDesignItem(DesignItemInfo designItemInfo)
        {
            this.canvas.SelectDesignItem(designItemInfo);
        }

        //----------------------------

        public void LoadToolBoxExistShapes(ShapeInfo[] shapeInfos)
        {
            throw new NotImplementedException();
        }

        public void LoadToolBoxCandidateShapes(ShapeInfo[] shapeInfos)
        {
            throw new NotImplementedException();
        }

        public void LoadToolBoxShapeGroups(ShapeInfo[] shapeInfos)
        {
            throw new NotImplementedException();
        }

        //----------------------------

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
        public virtual void RaiseModifiedDesignShapes(DesignItemInfo[] designItemInfos, DesignConnectionInfo[] designConnectionInfos)
        {
            if (ModifiedDesignShapes != null)
            {
                ModifiedDesignShapes(this, new MultiDesignShapeActionEventArgs(designItemInfos, designConnectionInfos));
            }
        }

        //----------------------------

        public event MultiShapeActionEventHandler SavedToolboxShapes;
        public virtual void RaiseSavedToolboxShapes(ISelectable[] shapeInfos)
        {
            if (SavedToolboxShapes != null)
            {
                SavedToolboxShapes(this, new MultiShapeActionEventArgs(shapeInfos));
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
                DesignWorkspaceSaving(this, new WorkspaceActionEventArgs(canvasInfo, designItemInfos, designConnectionInfos));
            }
        }

    }
}
