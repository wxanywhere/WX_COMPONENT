using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WX.Windows.Diagram.Designer
{
    public class AssignWorkspaceViewModel : ViewModelBase
    {
        private CanvasInfo[] _CanvasInfos;
        public CanvasInfo[] CanvasInfos
        {
            get { return _CanvasInfos; }
            set
            {
                if (_CanvasInfos != value)
                {
                    _CanvasInfos = value;
                    this.OnPropertyChanged("CanvasInfos");
                }

            }
        }

        private CanvasInfo _SelectedCanvasInfo;
        public CanvasInfo SelectedCanvasInfo
        {
            get { return _SelectedCanvasInfo; }
            set
            {
                if (_SelectedCanvasInfo != value)
                {
                    _SelectedCanvasInfo = value;
                    this.OnPropertyChanged("SelectedCanvasInfo");
                }
            }
        }
    }
}
