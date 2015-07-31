using SharpVectors.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WX.Windows.Diagram.Designer.Kernel;

namespace WX.Windows.Diagram.Designer
{
  public class ShapeInfoProvider
  {
      public static readonly ShapeInfoProvider INS = new ShapeInfoProvider();
      private ShapeInfoProvider() { }

      private void AddTemplateContent(FrameworkElementFactory root, DependencyObject obj)
      {
          var element = new FrameworkElementFactory(obj.GetType());
          root.AppendChild(element);
          for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
          {
              DependencyObject child = VisualTreeHelper.GetChild(obj, i);
              if (child != null)
              {
                  AddTemplateContent(element, child);
              }
          }
      }

      private List<ShapeInfo> GetSVGShapeInfosX()
      {
          Style style = new Style(typeof(Shape));
          var svgBuffer = File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Resources\Svg\Butterfly.svg"));
          var template = new ControlTemplate(typeof(Shape));
          var elemFactoryRoot = new FrameworkElementFactory(typeof(Border));
          using (var stream = new MemoryStream(svgBuffer))
          {
              var svgImage = SvgHelper.CreateSvgImage(stream);
              AddTemplateContent(elemFactoryRoot, svgImage);
          }
          template.VisualTree = elemFactoryRoot;
          style.Setters.Add(new Setter(Shape.TemplateProperty, template));
          Application.Current.Resources.Add("SVGShapeStyle0011", style);

          return new List<ShapeInfo>()
          {
              new ItemShapeInfo(){ShapeType=ShapeType.DesignItem,ShapeCategory=ShapeCategory.Business, StyleKey="SVGShapeStyle0011",Style=Application.Current.Resources["SVGShapeStyle0011"] as Style,ToolTip="SVG",IsChecked=true,IsEnabled=true,SvgBuffer=null}
          };
      }

      private List<ShapeInfoUnit> GetSVGShapeInfoUnits()
      {
          var shapeInfoUnites = new List<ShapeInfoUnit>();
          var shapeInfoA = new ItemShapeInfo();
          shapeInfoA.ShapeType = ShapeType.DesignItem;
          shapeInfoA.ShapeCategory = ShapeCategory.Business;
          shapeInfoA.StyleKey = "SVGBusinessShapeStyle";
          shapeInfoA.Style = Application.Current.Resources["SVGBusinessShapeStyle"] as Style;
          shapeInfoA.ToolTip = "SVG";
          shapeInfoA.IsChecked = true;
          shapeInfoA.IsEnabled = true;
          shapeInfoA.SvgBuffer = File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,@"Resources\Svg\Butterfly.svg"));
          var shapeInfoUnit = new ItemShapeInfoUnit(shapeInfoA);
          using (var stream = new MemoryStream(shapeInfoA.SvgBuffer))
          {
              shapeInfoUnit.SvgDrawing = SvgHelper.CreateSvgViewBox(stream);
          }
          shapeInfoUnites.Add(shapeInfoUnit);

          return shapeInfoUnites;
      }

      public ConnectionShapeInfo[] GetConnectionShapeInfo()
      {
          var shapeInfos = new List<ConnectionShapeInfo>();
          var shapeInfo = new ConnectionShapeInfo() { ShapeType = ShapeType.Connection, ShapeCategory = ShapeCategory.Business, ToolTip = "业务域", IsChecked = true, IsEnabled = true };
          shapeInfo.SourceSvgBuffer = File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,@"Resources\Svg\Butterfly.svg"));
          shapeInfo.SinkSvgBuffer = File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Resources\Svg\Butterfly.svg"));
          shapeInfos.Add(shapeInfo);

          return shapeInfos.ToArray();
      }

      public List<ShapeInfoUnit> GetShapeInfoUnites()
      {
          var shapeInfoUnites = new List<ShapeInfoUnit>();
          var shapeInfos = new List<ShapeInfo>()
          {
              //处理流程图
              //new ShapeInfo(){ShapeType=ShapeType.Connection,ShapeCategory=ShapeCategory.FlowChart,Style=Application.Current.Resources["ArrowProcessFSStyle"] as Style,ToolTip="箭头过程",IsChecked=true,IsEnabled=true},
              new ItemShapeInfo(){ShapeType=ShapeType.DesignItem,ShapeCategory=ShapeCategory.Business, StyleKey="BusinessDomainStyle",Style=Application.Current.Resources["BusinessDomainStyle"] as Style,ToolTip="业务域",IsChecked=true,IsEnabled=true},
              new ItemShapeInfo(){ShapeType=ShapeType.DesignItem,ShapeCategory=ShapeCategory.Business, StyleKey="BusinessClassifyStyle",Style=Application.Current.Resources["BusinessClassifyStyle"] as Style,ToolTip="业务分类",IsChecked=true,IsEnabled=true},
              new ItemShapeInfo(){ShapeType=ShapeType.DesignItem,ShapeCategory=ShapeCategory.Business, StyleKey="BusinessProcedureStyle",Style=Application.Current.Resources["BusinessProcedureStyle"] as Style,ToolTip="业务流程",IsChecked=true,IsEnabled=true},

              //FlowChart
              new ItemShapeInfo(){ShapeType=ShapeType.DesignItem,ShapeCategory=ShapeCategory.FlowChart, StyleKey="FlowChartProcessStyle",Style=Application.Current.Resources["FlowChartProcessStyle"] as Style,ToolTip="处理",IsChecked=true,IsEnabled=true},
              new ItemShapeInfo(){ShapeType=ShapeType.DesignItem,ShapeCategory=ShapeCategory.FlowChart, StyleKey="FlowChartDecisionStyle",Style=Application.Current.Resources["FlowChartDecisionStyle"] as Style,ToolTip="判断",IsChecked=true,IsEnabled=true},
              new ItemShapeInfo(){ShapeType=ShapeType.DesignItem,ShapeCategory=ShapeCategory.FlowChart, StyleKey="FlowChartDocumentStyle",Style=Application.Current.Resources["FlowChartDocumentStyle"] as Style,ToolTip="文档",IsChecked=true,IsEnabled=true},
              new ItemShapeInfo(){ShapeType=ShapeType.DesignItem,ShapeCategory=ShapeCategory.FlowChart, StyleKey="FlowChartStartStyle",Style=Application.Current.Resources["FlowChartStartStyle"] as Style,ToolTip="开始",IsChecked=true,IsEnabled=true},
              new ItemShapeInfo(){ShapeType=ShapeType.DesignItem,ShapeCategory=ShapeCategory.FlowChart, StyleKey="FlowChartPredefinedStyle",Style=Application.Current.Resources["FlowChartPredefinedStyle"] as Style,ToolTip="预定义",IsChecked=true,IsEnabled=true},
              new ItemShapeInfo(){ShapeType=ShapeType.DesignItem,ShapeCategory=ShapeCategory.FlowChart, StyleKey="FlowChartStoredDataStyle",Style=Application.Current.Resources["FlowChartStoredDataStyle"] as Style,ToolTip="存储",IsChecked=true,IsEnabled=true},
              new ItemShapeInfo(){ShapeType=ShapeType.DesignItem,ShapeCategory=ShapeCategory.FlowChart, StyleKey="FlowChartInternalStorageStyle",Style=Application.Current.Resources["FlowChartInternalStorageStyle"] as Style,ToolTip="内部存储",IsChecked=true,IsEnabled=true},
              new ItemShapeInfo(){ShapeType=ShapeType.DesignItem,ShapeCategory=ShapeCategory.FlowChart, StyleKey="FlowChartSequentialDataStyle",Style=Application.Current.Resources["FlowChartSequentialDataStyle"] as Style,ToolTip="序列数据",IsChecked=true,IsEnabled=true},
              new ItemShapeInfo(){ShapeType=ShapeType.DesignItem,ShapeCategory=ShapeCategory.FlowChart, StyleKey="FlowChartDirectDataStyle",Style=Application.Current.Resources["FlowChartDirectDataStyle"] as Style,ToolTip="定向数据",IsChecked=true,IsEnabled=true},
              new ItemShapeInfo(){ShapeType=ShapeType.DesignItem,ShapeCategory=ShapeCategory.FlowChart, StyleKey="FlowChartManualInputStyle",Style=Application.Current.Resources["FlowChartManualInputStyle"] as Style,ToolTip="手工输入",IsChecked=true,IsEnabled=true},
              new ItemShapeInfo(){ShapeType=ShapeType.DesignItem,ShapeCategory=ShapeCategory.FlowChart, StyleKey="FlowChartCardStyle",Style=Application.Current.Resources["FlowChartCardStyle"] as Style,ToolTip="卡片",IsChecked=true,IsEnabled=true},
              new ItemShapeInfo(){ShapeType=ShapeType.DesignItem,ShapeCategory=ShapeCategory.FlowChart, StyleKey="FlowChartPaperTapeStyle",Style=Application.Current.Resources["FlowChartPaperTapeStyle"] as Style,ToolTip="纸带",IsChecked=true,IsEnabled=true},
              new ItemShapeInfo(){ShapeType=ShapeType.DesignItem,ShapeCategory=ShapeCategory.FlowChart, StyleKey="FlowChartDelayStyle",Style=Application.Current.Resources["FlowChartDelayStyle"] as Style,ToolTip="延迟",IsChecked=true,IsEnabled=true},
              new ItemShapeInfo(){ShapeType=ShapeType.DesignItem,ShapeCategory=ShapeCategory.FlowChart, StyleKey="FlowChartTerminatorStyle",Style=Application.Current.Resources["FlowChartTerminatorStyle"] as Style,ToolTip="终端",IsChecked=true,IsEnabled=true},
              new ItemShapeInfo(){ShapeType=ShapeType.DesignItem,ShapeCategory=ShapeCategory.FlowChart, StyleKey="FlowChartDisplayStyle",Style=Application.Current.Resources["FlowChartDisplayStyle"] as Style,ToolTip="显示",IsChecked=true,IsEnabled=true},
              new ItemShapeInfo(){ShapeType=ShapeType.DesignItem,ShapeCategory=ShapeCategory.FlowChart, StyleKey="FlowChartLoopLimitStyle",Style=Application.Current.Resources["FlowChartLoopLimitStyle"] as Style,ToolTip="无限循环",IsChecked=true,IsEnabled=true},
              new ItemShapeInfo(){ShapeType=ShapeType.DesignItem,ShapeCategory=ShapeCategory.FlowChart, StyleKey="FlowChartPreparationStyle",Style=Application.Current.Resources["FlowChartPreparationStyle"] as Style,ToolTip="准备",IsChecked=true,IsEnabled=true},
              new ItemShapeInfo(){ShapeType=ShapeType.DesignItem,ShapeCategory=ShapeCategory.FlowChart, StyleKey="FlowChartManualOperationStyle",Style=Application.Current.Resources["FlowChartManualOperationStyle"] as Style,ToolTip="手工操作",IsChecked=true,IsEnabled=true},
              new ItemShapeInfo(){ShapeType=ShapeType.DesignItem,ShapeCategory=ShapeCategory.FlowChart, StyleKey="FlowChartOffPageReferenceStyle",Style=Application.Current.Resources["FlowChartOffPageReferenceStyle"] as Style,ToolTip="页",IsChecked=true,IsEnabled=true},
          };
          shapeInfoUnites.AddRange(shapeInfos.Select(a => new ItemShapeInfoUnit(a)).ToList());
          shapeInfoUnites.AddRange(GetSVGShapeInfoUnits());

          return shapeInfoUnites;
      }
  }
}
