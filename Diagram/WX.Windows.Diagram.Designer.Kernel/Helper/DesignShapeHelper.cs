using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;
using WX.Windows.Diagram.Designer.Kernel;

namespace WX.Windows.Diagram.Designer
{
    public static class DesignShapeHelper
    {
        public static DesignItem CreateDesignItem(DesignItemInfo designItemInfo)
        {
            var currentDate = DateTime.Now;
            designItemInfo.CreateDate = currentDate;
            designItemInfo.ModifyDate = currentDate;
            var shape = new Shape();
            var shapeInfoUnit = new ItemShapeInfoUnit(designItemInfo);
            shape.ShapeInfoUnit = shapeInfoUnit;
            shape.IsHitTestVisible = false;
            shape.Style = Application.Current.Resources[designItemInfo.StyleKey] as Style;
            if (designItemInfo.SvgBuffer != null)
            {
                using (var stream = new MemoryStream(designItemInfo.SvgBuffer))
                {
                    var svgDrawing = SvgHelper.CreateSvgImage(stream); //SvgHelper.CreateSvgViewBox(stream);
                    if (svgDrawing != null)
                    {
                        shapeInfoUnit.SvgDrawing = svgDrawing;
                        shape.SetBinding(Shape.ContentProperty, BindingHelper.CreateBinding(shapeInfoUnit, "SvgDrawing"));
                    }
                }
            }
            shape.SetBinding(Shape.TextProperty, BindingHelper.CreateBinding(designItemInfo, "Text"));
            if (!string.IsNullOrEmpty(designItemInfo.DesignInfo.Backgroud))
            {
                var brushConverter = new BrushConverter();
                shape.Background = (Brush)brushConverter.ConvertFromString(designItemInfo.DesignInfo.Backgroud);
            }
            var designItem = new DesignItem(designItemInfo.ID);
            designItem.ParentID = designItemInfo.ParentID;
            designItem.Content = shape;
            designItem.SetBinding(DesignItem.LinkButtonVisibilityProperty, BindingHelper.CreateBinding(designItemInfo, "LinkButtonVisibility"));
            designItem.SetBinding(DesignItem.WidthProperty, BindingHelper.CreateBinding(designItemInfo,"Width"));
            designItem.SetBinding(DesignItem.HeightProperty, BindingHelper.CreateBinding(designItemInfo, "Height"));
            designItem.SetBinding(DesignCanvas.LeftProperty, BindingHelper.CreateBinding(designItemInfo, "X"));
            designItem.SetBinding(DesignCanvas.TopProperty, BindingHelper.CreateBinding(designItemInfo, "Y"));
            designItem.SetBinding(DesignCanvas.ZIndexProperty, BindingHelper.CreateBinding(designItemInfo, "ZIndex"));
           
            return designItem;
        }

    }
}
