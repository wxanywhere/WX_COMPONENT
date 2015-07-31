using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace WX.Windows.Diagram.Designer
{
    public partial class DesignerMain
    {
        public static RoutedCommand TabItemWorkspaceClose = new RoutedCommand();
        public static RoutedCommand OpenWorkspaceItem = new RoutedCommand();

        private void TabItemWorkspaceClose_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var currentItem = this.workTab.SelectedItem as TabItem;
            this.workTab.Items.Remove(currentItem);
            this.DesignerKernels.Remove(this.DesignerKernels.First(a => a.CanvasInfo.WorkspaceID.ToString() == currentItem.Uid));
            if (this.workTab.Items.Count > 0)
            {
                (this.workTab.Items[this.workTab.Items.Count - 1] as TabItem).IsSelected = true;
            }
            else
            {
                this.ZoomBoxPresenter.Content = null;
                this.ShapePresenter.Content = null;
                this.ShapeGroupPresenter.Content = null;
            }
        }

        private void OpenWorkspaceItem_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var item = e.OriginalSource as MenuItem;
            if (item == null) return;
            var shapeCategory = ShapeCategory.FlowChart;
            if (e.Parameter != null)
            {
                var flag = Enum.TryParse<ShapeCategory>(e.Parameter.ToString(), true, out shapeCategory);
            }
            var existItem = this.workTab.Items.Cast<TabItem>().FirstOrDefault(a => a.Header == item.Header);
            if (existItem == null)
            {
                var tabItem = new TabItem();
                tabItem.Header = item.Header;
                var designerKernel = new DesignerKernel();
                designerKernel.NewDesign(); //临时
                tabItem.Uid = designerKernel.CanvasInfo.WorkspaceID.ToString();
                this.DesignerKernels.Add(designerKernel);
                designerKernel.DesignCanvasMouseClick += designerKernel_DesignCanvasMouseClick;
                designerKernel.DesignCanvasMouseDoubleClick += designerKernel_DesignCanvasMouseDoubleClick;
                designerKernel.DesignCanvasMouseRightClick += designerKernel_DesignCanvasMouseRightClick;

                designerKernel.DesignItemSelected += designerKernel_DesignItemSelected;
                designerKernel.DesignItemMouseDoubleClick += designerKernel_DesignItemMouseDoubleClick;
                designerKernel.DesignItemMouseRightClick += designerKernel_DesignItemMouseRightClick;
                designerKernel.DesignItemLinkButtonDoubleClick += designerKernel_DesignItemLinkButtonDoubleClick;
                designerKernel.PreAddDesignItem += designerKernel_PreAddDesignItem;
                designerKernel.AddingDesignItem += designerKernel_AddingDesignItem;
                designerKernel.AddedDesignItem += designerKernel_AddedDesignItem;

                designerKernel.DesignConnectionSelected += designerKernel_DesignConnectionSelected;
                designerKernel.DesignConnectionMouseDoubleClick += designerKernel_DesignConnectionMouseDoubleClick;
                designerKernel.DesignConnectionMouseRightClick += designerKernel_DesignConnectionMouseRightClick;
                designerKernel.PreAddDesignConnection += designerKernel_PreAddDesignConnection;
                designerKernel.AddingDesignConnection += designerKernel_AddingDesignConnection;
                designerKernel.AddedDesignConnection += designerKernel_AddedDesignConnection;

                designerKernel.DeletingDesignShapes += designerKernel_DeletingDesignShapes;
                designerKernel.DeletedDesignShapes += designerKernel_DeletedDesignShapes;

                designerKernel.DesignItemCollectionChanged += designerKernel_DesignItemCollectionChanged;
                designerKernel.DesignWorkspaceSaving += designerKernel_DesignWorkspaceSaving;

                designerKernel.ShapeCategory = shapeCategory;
                designerKernel.LoadConnection(ShapeInfoProvider.INS.GetConnectionShapeInfo().Where(a => a.ShapeCategory == designerKernel.ShapeCategory).ToArray());
                tabItem.Content = designerKernel;
                this.workTab.Items.Add(tabItem);
                (this.workTab.Items[this.workTab.Items.Count - 1] as TabItem).IsSelected = true;
            }
            else
            {
                existItem.IsSelected = true;
            }
        }

        void designerKernel_DesignItemCollectionChanged(object sender, DesignItemCollectionChangedEventArgs e)
        {
            viewModel.CurrentShapeInfoUnits = ToShapeInfoUnites(e.DesignItemInfos);
        }

        void designerKernel_DesignWorkspaceSaving(object sender, WorkspaceActionEventArgs e)
        {
            throw new NotImplementedException();
        }

        void designerKernel_DesignCanvasMouseRightClick(object sender, CanvasActionEventArgs e)
        {
            //throw new NotImplementedException();
        }

        void designerKernel_DeletedDesignShapes(object sender, MultiDesignShapeActionEventArgs e)
        {
            //throw new NotImplementedException();
        }

        void designerKernel_DeletingDesignShapes(object sender, MultiDesignShapeActionEventArgs e)
        {
            //throw new NotImplementedException();
        }

        void designerKernel_AddedDesignItem(object sender, SingleDesignItemActionEventArgs e)
        {
            //throw new NotImplementedException();
        }

        void designerKernel_AddingDesignItem(object sender, DesignItemValidationEventArgs e)
        {
            //throw new NotImplementedException();
        }

        void designerKernel_PreAddDesignItem(object sender, DesignItemValidationEventArgs e)
        {
            //throw new NotImplementedException();
        }

        void designerKernel_AddedDesignConnection(object sender, SingleDesignConnectionActionEventArgs e)
        {
            //throw new NotImplementedException();
        }

        void designerKernel_AddingDesignConnection(object sender, DesignConnectionValidationEventArgs e)
        {
            //throw new NotImplementedException();
        }

        void designerKernel_PreAddDesignConnection(object sender, DesignConnectionValidationEventArgs e)
        {
            //throw new NotImplementedException();
        }

        void designerKernel_DesignCanvasMouseDoubleClick(object sender, CanvasActionEventArgs e)
        {
            var designCanvasProperty = new DesignCanvasProperty();
            designCanvasProperty.Owner = Application.Current.MainWindow;
            designCanvasProperty.ShowDialog();
        }

        void designerKernel_DesignCanvasMouseClick(object sender, CanvasActionEventArgs e)
        {
            viewModel.CurrentModelInfo = e.canvasInfo;
        }

        void designerKernel_DesignItemLinkButtonDoubleClick(object sender, SingleDesignItemActionEventArgs e)
        {
            var tabItem=this.workTab.Items.Cast<TabItem>().FirstOrDefault(a => a.Uid == e.DesignItemInfo.LinkWorkspaceID.ToString());
            if (tabItem == null) return;
            tabItem.IsSelected = true;
        }

        void designerKernel_DesignConnectionMouseRightClick(object sender, SingleDesignConnectionActionEventArgs e)
        {
            //throw new NotImplementedException();
        }

        void designerKernel_DesignItemMouseRightClick(object sender, SingleDesignItemActionEventArgs e)
        {
            //throw new NotImplementedException();
        }

        void designerKernel_DesignConnectionMouseDoubleClick(object sender, SingleDesignConnectionActionEventArgs e)
        {
            var designConnectionProperty = new DesignConnectionProperty();
            designConnectionProperty.Owner = Application.Current.MainWindow;
            designConnectionProperty.ShowDialog();
        }

        void designerKernel_DesignConnectionSelected(object sender, SingleDesignConnectionActionEventArgs e)
        {
            viewModel.CurrentModelInfo = e.DesignConnection;
        }

        void designerKernel_DesignItemSelected(object sender, SingleDesignItemActionEventArgs e)
        {
            viewModel.CurrentModelInfo = e.DesignItemInfo;
        }

        private void designerKernel_DesignItemMouseDoubleClick(object sender, SingleDesignItemActionEventArgs e)
        {
            var designItemProperty = new DesignItemProperty();
            designItemProperty.Owner =  Application.Current.MainWindow;
            designItemProperty.ShowDialog();
        }

    }
}
