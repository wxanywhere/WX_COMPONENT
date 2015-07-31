using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WX.Windows.Diagram.Designer
{
    public partial class AssignWorkspace : Window
    {
        private AssignWorkspaceViewModel viewModel;
        public AssignWorkspace()
        {
            InitializeComponent();
            this.viewModel = new AssignWorkspaceViewModel();
            this.DataContext = this.viewModel;
            
        }
        public CanvasInfo SelectedCanvasInfo
        {
            get { return viewModel.SelectedCanvasInfo; }
        }

        public AssignWorkspaceViewModel ViewModel
        {
            get
            {
                return viewModel;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SelectedCanvasInfo != null)
            {
                this.Close();
            }
        }

    }
}
