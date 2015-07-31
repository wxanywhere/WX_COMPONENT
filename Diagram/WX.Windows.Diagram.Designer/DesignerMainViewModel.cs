using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace WX.Windows.Diagram.Designer
{
    public class DesignerMainViewModel : ViewModelBase
    {
        //private DesignItemInfo[] _CurrentDesignItemInfos;
        //public DesignItemInfo[] CurrentDesignItemInfos
        //{
        //    get { return _CurrentDesignItemInfos; }
        //    set
        //    {
        //        if (_CurrentDesignItemInfos != value)
        //        {
        //            _CurrentDesignItemInfos = value;
        //            this.OnPropertyChanged("CurrentDesignItemInfos");
        //        }
        //    }
        //}

        //private DesignItemInfo _SelectedDesignItemInfo;
        //public DesignItemInfo SelectedDesignItemInfo
        //{
        //    get { return _SelectedDesignItemInfo; }
        //    set
        //    {
        //        if (_SelectedDesignItemInfo != value)
        //        {
        //            _SelectedDesignItemInfo = value;
        //            this.OnPropertyChanged("SelectedDesignItemInfo");
        //        }
        //    }
        //}


        private ShapeInfoUnit[] _CurrentShapeInfoUnits;
        public ShapeInfoUnit[] CurrentShapeInfoUnits
        {
            get { return _CurrentShapeInfoUnits; }
            set
            {
                if (_CurrentShapeInfoUnits != value)
                {
                    _CurrentShapeInfoUnits = value;
                    this.OnPropertyChanged("CurrentShapeInfoUnits");
                }
            }
        }

        private ShapeInfoUnit _SelectedShapeInfoUnit;
        public ShapeInfoUnit SelectedShapeInfoUnit
        {
            get { return _SelectedShapeInfoUnit; }
            set
            {
                if (_SelectedShapeInfoUnit != value)
                {
                    _SelectedShapeInfoUnit = value;
                    this.OnPropertyChanged("SelectedShapeInfoUnit");
                }
            }
        }

        private ModelBase _CurrentModelInfo;
        public ModelBase CurrentModelInfo
        {
            get { return _CurrentModelInfo; }
            set
            {
                if (_CurrentModelInfo != value)
                {
                    _CurrentModelInfo = value;
                    this.OnPropertyChanged("CurrentModelInfo");
                }
            }
        }

    }
}
