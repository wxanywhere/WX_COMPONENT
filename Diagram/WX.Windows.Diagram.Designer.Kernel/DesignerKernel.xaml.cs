using System;
using System.Windows;
using System.Windows.Controls;

namespace WX.Windows.Diagram.Designer
{
    public partial class DesignerKernel :UserControl
    {
        private DesignCanvas canvas = null;
        public DesignerKernel()
        {
            InitializeComponent();
            canvas = this.DesignCanvas;
            canvas.DesignCanvasMouseClick += (o, e) => { this.RaiseDesignCanvasMouseClick(e.canvasInfo); };
            canvas.DesignCanvasMouseDoubleClick += (o, e) => { this.RaiseDesignCanvasMouseDoubleClick(e.canvasInfo); };
            canvas.DesignCanvasMouseRightClick += (o, e) => { this.RaiseDesignCanvasMouseRightClick(e.canvasInfo); };
            canvas.DesignItemMouseDoubleClick += (o, e) => { this.RaiseDesignItemMouseDoubleClick(e.DesignItemInfo); };
            canvas.DesignItemMouseRightClick += (o, e) => { this.RaiseDesignItemMouseRightClick(e.DesignItemInfo); };
            canvas.DesignItemLinkButtonClick += (o, e) => { this.RaiseDesignItemLinkButtonClick(e.DesignItemInfo); };
            canvas.DesignItemLinkButtonDoubleClick += (o, e) => { this.RaiseDesignItemLinkButtonDoubleClick(e.DesignItemInfo); };
            canvas.DesignConnectionMouseDoubleClick += (o, e) => { this.RaiseDesignConnectionMouseDoubleClick(e.DesignConnection); };
            canvas.DesignConnectionMouseRightClick += (o, e) => { this.RaiseDesignConnectionMouseRightClick(e.DesignConnection); };
            canvas.DesignConnectionMouseRightClick += (o, e) => { this.RaiseDesignConnectionMouseRightClick(e.DesignConnection); };


            canvas.DesignItemSelected += (o, e) =>{this.RaiseDesignItemSelected(e.DesignItemInfo);};
            canvas.DesignConnectionSelected+=(o,e)=>{this.RaiseDesignConnectionSelected(e.DesignConnection);};
            canvas.DesignItemMultiSelected += (o, e) =>{this.RaiseDesignItemMultiSelected(e.DesignItemInfos);};
            canvas.DesignShapeMultiSelected += (o, e) =>{this.RaiseDesignShapeMultiSelected(e.DesignItemInfos,e.DesignConnectionInfos);};

            canvas.PreAddDesignItem += (o, e) => { this.RaisePreAddDesignItem(e.DesignItemInfos); };
            canvas.AddingDesignItem += (o, e) => { this.RaiseAddingDesignItem(e.DesignItemInfos); };
            canvas.AddedDesignItem += (o, e) => { this.RaiseAddedDesignItem(e.DesignItemInfo); };
            canvas.PreAddDesignConnection += (o, e) => { this.RaisePreAddDesignConnection(e.SourceItemInfo,e.SinkItemInfo); };
            canvas.AddingDesignConnection += (o, e) => { this.RaiseAddingDesignConnection(e.SourceItemInfo,e.SinkItemInfo); };
            canvas.AddedDesignConnection += (o, e) => { this.RaiseAddedDesignConnection(e.DesignConnection); };
            canvas.AddingDesignShapes += (o, e) => { this.RaiseAddingDesignShapes(e.DesignItemInfos,e.DesignConnectionInfos); };
            canvas.AddedDesignShapes += (o, e) => { this.RaiseAddedDesignShapes(e.DesignItemInfos,e.DesignConnectionInfos); };
            canvas.DeletingDesignShapes += (o, e) => { this.RaiseDeletingDesignShapes(e.DesignItemInfos,e.DesignConnectionInfos); };
            canvas.DeletedDesignShapes += (o, e) => { this.RaiseDeletedDesignShapes(e.DesignItemInfos,e.DesignConnectionInfos); };
            canvas.ModifyingDesignShapes += (o, e) => { this.RaiseModifyingDesignShapes(e.DesignItemInfos,e.DesignConnectionInfos); };
            canvas.ModifiedDesignShapes += (o, e) => { this.RaiseModifiedDesignShapes(e.DesignItemInfos,e.DesignConnectionInfos); };

            canvas.DesignItemCollectionChanged += (o, e) => { this.RaiseDesignItemCollectionChanged(e.DesignItemInfos); };
            canvas.DesignWorkspaceSaving += (o, e) => { this.RaiseDesignWorkspaceSaving(e.CanvasInfo,e.DesignItemInfos,e.DesignConnectionInfos); };

        
        }

        public ShapeCategory ShapeCategory { get; set; }

        public CanvasInfo CanvasInfo
        {
            get
            {
                return this.canvas.CanvasInfo;
            }
        }

    }
}
