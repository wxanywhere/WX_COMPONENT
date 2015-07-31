
using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Xceed.Wpf.Toolkit.PropertyGrid;
using System.Collections.ObjectModel;
using System.IO;
using WX.Windows.Diagram.Designer.Kernel;

namespace WX.Windows.Diagram.Designer
{
    public partial class DesignerMain : UserControl
    {
        private DesignerMainViewModel viewModel;
        public DesignerMain()
        {
            InitializeComponent();
            this.viewModel = new DesignerMainViewModel();
            this.DataContext = this.viewModel;
            
            this.CommandBindings.Add(new CommandBinding(DesignerMain.TabItemWorkspaceClose, TabItemWorkspaceClose_Executed));
            this.CommandBindings.Add(new CommandBinding(DesignerMain.OpenWorkspaceItem, OpenWorkspaceItem_Executed));
            this.CommandBindings.Add(new CommandBinding(DesignerKernel.AssignDesignShape, AssignDesignShape_Executed));
            this.workTab.SelectionChanged += new SelectionChangedEventHandler(workTab_SelectionChanged);
        }

        private void AssignDesignShape_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var assignWorkspace = new AssignWorkspace();
            assignWorkspace.Owner = Application.Current.MainWindow;
            assignWorkspace.ViewModel.CanvasInfos = this.DesignerKernels
                .Where(a=>a.CanvasInfo.WorkspaceID!=this.CurrentDesignerKernel.CanvasInfo.WorkspaceID)
                .Select(a => a.CanvasInfo).ToArray();
            assignWorkspace.Closed += (obj, ex) =>
            {
                if (assignWorkspace.SelectedCanvasInfo!=null && e.OriginalSource is DesignItem)
                {
                    var designItemInfo=((e.OriginalSource as DesignItem).Content as Shape).ShapeInfoUnit.ShapeInfo as DesignItemInfo;
                    designItemInfo.LinkWorkspaceID = assignWorkspace.SelectedCanvasInfo.WorkspaceID;
                    designItemInfo.LinkWorkspaceName = assignWorkspace.SelectedCanvasInfo.Name;
                }
            };
            assignWorkspace.ShowDialog();
        }


        private List<DesignerKernel> _DesignerKernels;
        public List<DesignerKernel> DesignerKernels
        {
            get
            {
                if (_DesignerKernels == null)
                {
                    _DesignerKernels = new List<DesignerKernel>();
                }
                return _DesignerKernels;
            }
        }

        public DesignerKernel CurrentDesignerKernel { get; set; }

        public List<ShapeInfoUnit> ShapeInfoUnites
        {
            get
            {
                return ShapeInfoProvider.INS.GetShapeInfoUnites();
            }
        }

        private ShapeInfoUnit[] ToShapeInfoUnites(DesignItemInfo[] designItemInfos)
        {
            return designItemInfos.Select(a =>
            {
                var shapeInfoUnit = new ItemShapeInfoUnit(a);
                if (a.SvgBuffer != null)
                {
                    using (var stream = new MemoryStream(a.SvgBuffer))
                    {
                        var svgDrawing = SvgHelper.CreateSvgViewBox(stream);
                        if (svgDrawing != null)
                        {
                            shapeInfoUnit.SvgDrawing = svgDrawing;
                        }
                    }
                }
                return shapeInfoUnit;
            }
            ).ToArray();
        }

        private void workTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.SelectedShapeInfoUnit = null;
            viewModel.CurrentShapeInfoUnits = null;
            var currentItem = this.workTab.SelectedItem as TabItem;
            if (currentItem == null) return;
            var control = currentItem.Content as DependencyObject;
            if (control == null) return;
            var designerKernel = currentItem.Content as DesignerKernel;
            if (designerKernel == null) designerKernel = VisualTreeHelperX.FindVisualChild<DesignerKernel>(control) as DesignerKernel;
            if (designerKernel == null) return;
            this.CurrentDesignerKernel = designerKernel;
            var zoomBoxContainer = designerKernel.FindName("ZoomBoxContainer") as ContentControl;
            if (zoomBoxContainer == null) return;
            this.ZoomBoxPresenter.Content = zoomBoxContainer.Content;
            var shapeContainer = designerKernel.FindName("ShapeContainer") as ContentControl;
            if (shapeContainer == null) return;
            var toolbox = shapeContainer.Content as Toolbox;
            if (toolbox == null) return;
            toolbox.ItemsSource = this.ShapeInfoUnites.Where(a => a.ShapeInfo.ShapeCategory == designerKernel.ShapeCategory);
            this.ShapePresenter.Content = shapeContainer.Content;
            var shapeGroupContainer = designerKernel.FindName("ShapeGroupContainer") as Expander;
            if (shapeGroupContainer == null) return;
            this.ShapeGroupPresenter.Content = shapeGroupContainer.Content;
            viewModel.CurrentShapeInfoUnits = ToShapeInfoUnites(designerKernel.RetrieveDesignItemInfos());
        }

        private void DesignItemInfos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (viewModel.SelectedShapeInfoUnit!=null)
            {
                this.CurrentDesignerKernel.SelectDesignItem(viewModel.SelectedShapeInfoUnit.ShapeInfo as DesignItemInfo);
            }
           
        }
    }
}
