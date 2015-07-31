using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace WX.Windows.Diagram.Designer
{
  public abstract class ShapeInfo:ModelBase
  {
      private int _SerialNo;
      public int SerialNo
      {
          get { return _SerialNo; }
          set
          {
              if (_SerialNo != value)
              {
                  _SerialNo = value;
                  this.OnPropertyChanged("SerialNo");
              }
          }
      }

      private bool _GroupID;
      public bool GroupID
      {
          get { return _GroupID; }
          set
          {
              if (_GroupID != value)
              {
                  _GroupID = value;
                  this.OnPropertyChanged("GroupID");
              }
          }
      }

      private bool _IsInGroup;
      public bool IsInGroup
      {
          get { return _IsInGroup; }
          set
          {
              if (_IsInGroup != value)
              {
                  _IsInGroup = value;
                  this.OnPropertyChanged("IsInGroup");
              }
          }
      }

      private ShapeType _ShapeType;
      [Category("基本信息")]
      [DisplayName("图元类型")]
      [Description("图元类型")]
      public ShapeType ShapeType
      {
          get { return _ShapeType; }
          set
          {
              if (_ShapeType != value)
              {
                  _ShapeType = value;
                  this.OnPropertyChanged("ShapeType");
              }
          }
      }

      private ShapeCategory _ShapeCategory;
      [Category("基本信息")]
      [DisplayName("图元类别")]
      [Description("图元类别")]
      public ShapeCategory ShapeCategory
      {
          get { return _ShapeCategory; }
          set
          {
              if (_ShapeCategory != value)
              {
                  _ShapeCategory = value;
                  this.OnPropertyChanged("ShapeCategory");
              }
          }
      }

      private string _StyleKey;
      public string StyleKey
      {
          get { return _StyleKey; }
          set
          {
              if (_StyleKey != value)
              {
                  _StyleKey = value;
                  this.OnPropertyChanged("StyleKey");
              }
          }
      }

      private Style _Style;
      public Style Style
      {
          get { return _Style; }
          set
          {
              if (_Style != value)
              {
                  _Style = value;
                  this.OnPropertyChanged("Style");
              }
          }
      }

      private string _ToolTip;
      [Category("基本信息")]
      [DisplayName("图元类型")]
      [Description("图元类型")]
      public string ToolTip
      {
          get { return _ToolTip; }
          set
          {
              if (_ToolTip != value)
              {
                  _ToolTip = value;
                  this.OnPropertyChanged("ToolTip");
              }
          }
      }

      private bool _IsChecked;
      [Category("基本信息")]
      [DisplayName("是否选用")]
      [Description("是否选用")]
      public bool IsChecked
      {
          get { return _IsChecked; }
          set
          {
              if (_IsChecked != value)
              {
                  _IsChecked = value;
                  this.OnPropertyChanged("IsChecked");
              }
          }
      }

      private bool _IsEnabled;
      [Category("基本信息")]
      [DisplayName("是否启用")]
      [Description("是否启用")]
      public bool IsEnabled
      {
          get { return _IsEnabled; }
          set
          {
              if (_IsEnabled != value)
              {
                  _IsEnabled = value;
                  this.OnPropertyChanged("IsEnabled");
              }
          }
      }

  }
}
