using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace WX.Windows.Diagram.Designer
{
    public class DesignItemInfo : ItemShapeInfo
    {
        public DesignItemInfo()
        {
            this.ShapeType = ShapeType.DesignItem;
            this.DesignInfo = new DesignInfo(); //需要设置默认值
        }

        public DesignItemInfo(ItemShapeInfo itemShapeInfo):this()
        {
            this.ShapeCategory = itemShapeInfo.ShapeCategory;
            this.SerialNo = itemShapeInfo.SerialNo;
            this.GroupID = itemShapeInfo.GroupID;
            this.IsInGroup = itemShapeInfo.IsInGroup;
            this.StyleKey = itemShapeInfo.StyleKey;
            this.Style = itemShapeInfo.Style;
            this.ToolTip = itemShapeInfo.ToolTip;
            this.IsChecked = itemShapeInfo.IsChecked;
            this.IsEnabled = itemShapeInfo.IsEnabled;
            this.SvgBuffer = itemShapeInfo.SvgBuffer;
        }

        private string _Text;
        [Category("基本信息")]
        [DisplayName("名称")]
        [Description("名称")]
        public string Text
        {
            get { return _Text; }
            set
            {
                if (_Text != value)
                {
                    _Text = value;
                    this.OnPropertyChanged("Text");
                }
            }
        }

        private Guid _ID;
        public Guid ID
        {
            get { return _ID; }
            set
            {
                if (_ID != value)
                {
                    _ID = value;
                    this.OnPropertyChanged("ID");
                }
            }
        }

        private Decimal _ZIndex;
        public Decimal ZIndex
        {
            get { return _ZIndex; }
            set
            {
                if (_ZIndex != value)
                {
                    _ZIndex = value;
                    this.OnPropertyChanged("ZIndex");
                }
            }
        }

        private Guid _LinkWorkspaceID;
        public Guid LinkWorkspaceID
        {
            get { return _LinkWorkspaceID; }
            set
            {
                if (_LinkWorkspaceID != value)
                {
                    _LinkWorkspaceID = value;
                    this.OnPropertyChanged("LinkWorkspaceID");
                    if (value != new Guid())
                    {
                        LinkButtonVisibility = Visibility.Visible;
                    }
                    else
                    {
                        LinkButtonVisibility = Visibility.Hidden;
                    }
                }
            }
        }

        private string _LinkWorkspaceName;
        public string LinkWorkspaceName
        {
            get { return _LinkWorkspaceName; }
            set
            {
                if (_LinkWorkspaceName != value)
                {
                    _LinkWorkspaceName = value;
                    this.OnPropertyChanged("LinkWorkspaceName");
                }
            }
        }

        private Visibility _LinkButtonVisibility=Visibility.Hidden;
        public Visibility LinkButtonVisibility
        {
            get { return _LinkButtonVisibility; }
            set
            {
                if (_LinkButtonVisibility != value)
                {
                    _LinkButtonVisibility = value;
                    this.OnPropertyChanged("LinkButtonVisibility");
                }
            }
        }

        private Guid _ParentID;
        public Guid ParentID
        {
            get { return _ParentID; }
            set
            {
                if (_ParentID != value)
                {
                    _ParentID = value;
                    this.OnPropertyChanged("ParentID");
                }
            }
        }

        private Decimal _X;
        public Decimal X
        {
            get { return _X; }
            set
            {
                if (_X != value)
                {
                    _X = value;
                    this.OnPropertyChanged("X");
                }
            }
        }

        private Decimal _Y;
        public Decimal Y
        {
            get { return _Y; }
            set
            {
                if (_Y != value)
                {
                    _Y = value;
                    this.OnPropertyChanged("Y");
                }
            }
        }

        private Decimal _Width;
        public Decimal Width
        {
            get { return _Width; }
            set
            {
                if (_Width != value)
                {
                    _Width = value;
                    this.OnPropertyChanged("Width");
                }
            }
        }

        private Decimal _Height;
        public Decimal Height
        {
            get { return _Height; }
            set
            {
                if (_Height != value)
                {
                    _Height = value;
                    this.OnPropertyChanged("Height");
                }
            }
        }

        public bool IsGroup { get; set; } 

        public DesignInfo DesignInfo { get; set; }


    }
}
