using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace WX.Windows.Diagram.Designer
{
    public class CanvasInfo:ModelBase
    {
        private Guid _WorkspaceID;
        public Guid WorkspaceID
        {
            get { return _WorkspaceID; }
            set
            {
                if (_WorkspaceID != value)
                {
                    _WorkspaceID = value;
                    this.OnPropertyChanged("WorkspaceID");
                }
            }
        }

       
        private string _Name;
        [Category("基本信息")]
        [DisplayName("名称")]
        [Description("名称")]
        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    this.OnPropertyChanged("Name");
                }
            }
        }

        private string _Type;
        [Category("基本信息")]
        [DisplayName("名称")]
        [Description("名称")]
        public string Type
        {
            get { return _Type; }
            set
            {
                if (_Type != value)
                {
                    _Type = value;
                    this.OnPropertyChanged("Type");
                }
            }
        }
    }
}
