using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WX.Windows.Diagram.Designer
{
    public abstract class ModelBase : INotifyPropertyChanged
    {
        public ModelBase()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.VerifyPropertyName(propertyName);
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                string msg = "Invalid property name: " + propertyName;

                if (this.ThrowOnInvalidPropertyName)
                    throw new Exception(msg);
                else
                    Debug.Fail(msg);
            }
        }
        protected virtual bool ThrowOnInvalidPropertyName { get; private set; }

        private DateTime _CreateDate;
        [Category("基本信息")]
        [DisplayName("创建日期")]
        [Description("创建日期")]
        public DateTime CreateDate
        {
            get { return _CreateDate; }
            set
            {
                if (_CreateDate != value)
                {
                    _CreateDate = value;
                    this.OnPropertyChanged("CreateDate");
                }
            }
        }

        private string _CreateUerName;
        [Category("基本信息")]
        [DisplayName("创建人")]
        [Description("创建人")]
        public string CreateUerName
        {
            get { return _CreateUerName; }
            set
            {
                if (_CreateUerName != value)
                {
                    _CreateUerName = value;
                    this.OnPropertyChanged("CreateUerName");
                }
            }
        }

        private DateTime _ModifyDate;
        [Category("基本信息")]
        [DisplayName("修改日期")]
        [Description("修改日期")]
        public DateTime ModifyDate
        {
            get { return _ModifyDate; }
            set
            {
                if (_ModifyDate != value)
                {
                    _ModifyDate = value;
                    this.OnPropertyChanged("ModifyDate");
                }
            }
        }

        private string _ModifyUserName;
        [Category("基本信息")]
        [DisplayName("修改人")]
        [Description("修改人")]
        public string ModifyUserName
        {
            get { return _ModifyUserName; }
            set
            {
                if (_ModifyUserName != value)
                {
                    _ModifyUserName = value;
                    this.OnPropertyChanged("ModifyUserName");
                }
            }
        }
    }
}
