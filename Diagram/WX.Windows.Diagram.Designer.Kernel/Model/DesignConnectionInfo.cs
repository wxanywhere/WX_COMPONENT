using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace WX.Windows.Diagram.Designer
{
    public class DesignConnectionInfo : ConnectionShapeInfo
    {
        public DesignConnectionInfo()
        {
            this.ShapeType = ShapeType.Connection;
            this.DesignInfo = new DesignInfo(); //需要设置默认值
        }

        public DesignConnectionInfo(ConnectionShapeInfo connectionShapeInfo):this()
        {
            this.ShapeCategory = ShapeCategory.Connection;
            this.SerialNo = connectionShapeInfo.SerialNo;
            this.GroupID = connectionShapeInfo.GroupID;
            this.IsInGroup = connectionShapeInfo.IsInGroup;
            this.StyleKey = connectionShapeInfo.StyleKey;
            this.Style = connectionShapeInfo.Style;
            this.ToolTip = connectionShapeInfo.ToolTip;
            this.IsChecked = connectionShapeInfo.IsChecked;
            this.IsEnabled = connectionShapeInfo.IsEnabled;
            this.SourceSvgBuffer = connectionShapeInfo.SourceSvgBuffer;
            this.SinkSvgBuffer = connectionShapeInfo.SinkSvgBuffer;
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

        private Guid _SourceID;
        public Guid SourceID
        {
            get { return _SourceID; }
            set
            {
                if (_SourceID != value)
                {
                    _SourceID = value;
                    this.OnPropertyChanged("SourceID");
                }
            }
        }

        private ConnectorOrientation _SourceOrientation;
        public ConnectorOrientation SourceOrientation
        {
            get { return _SourceOrientation; }
            set
            {
                if (_SourceOrientation != value)
                {
                    _SourceOrientation = value;
                    this.OnPropertyChanged("SourceOrientation");
                }
            }
        }

        private Guid _SinkID;
        public Guid SinkID
        {
            get { return _SinkID; }
            set
            {
                if (_SinkID != value)
                {
                    _SinkID = value;
                    this.OnPropertyChanged("SinkID");
                }
            }
        }

        private ConnectorOrientation _SinkOrientation;
        public ConnectorOrientation SinkOrientation
        {
            get { return _SinkOrientation; }
            set
            {
                if (_SinkOrientation != value)
                {
                    _SinkOrientation = value;
                    this.OnPropertyChanged("SinkOrientation");
                }
            }
        }

        private PathGeometry _PathGeometry;
        public PathGeometry PathGeometry
        {
            get { return _PathGeometry; }
            set
            {
                if (_PathGeometry != value)
                {
                    _PathGeometry = value;
                    this.OnPropertyChanged("PathGeometry");
                }
            }
        }

        public DesignInfo DesignInfo { get; set; }

    }
}
