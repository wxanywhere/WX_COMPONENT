using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpVectors.Runtime;
using SharpVectors.Converters;
using System.IO;
using System.Xml;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WX.Windows.Diagram.Designer.Kernel
{
    /// <summary>
    /// SVG帮助类
    /// </summary>
    public sealed class SvgHelper
    {
        /// <summary>
        /// 创建SvgDrawingCanvas(Svg画布)
        /// </summary>
        /// <param name="svgReader"></param>
        /// <returns>SvgDrawingCanvas(Svg画布)</returns>
        private static SvgDrawingCanvas CreateSvgDrawingCanvas(FileSvgReader svgReader)
        {
            SvgDrawingCanvas svgCanvas = new SvgDrawingCanvas();
            svgCanvas.RenderDiagrams(svgReader.Drawing);
            return svgCanvas;
        }

        /// <summary>
        /// 创建SVG图像
        /// </summary>
        /// <param name="svgStream">SVG文件流</param>
        /// <returns>SVG图像</returns>
        public static SvgImage CreateSvgImage(Stream svgStream)
        {
            var svgImage = new SvgImage();
            var drawingImage = (DrawingImage)null;
            var svgReader = new FileSvgReader(null);

            svgReader.Read(svgStream);
            drawingImage = new DrawingImage(svgReader.Drawing);
            svgImage.Source=drawingImage;
            svgImage.Stretch = Stretch.Fill;

            return svgImage;
        }

        /// <summary>
        /// 创建SvgDrawingCanvas(Svg画布)
        /// </summary>
        /// <param name="svgFileName">svg文件路径</param>
        /// <returns>SvgDrawingCanvas(Svg画布)</returns>
        public static SvgDrawingCanvas CreateSvgDrawingCanvas(string svgFileName)
        {
            FileSvgReader svgReader = new FileSvgReader(null);
            svgReader.Read(svgFileName);
            return CreateSvgDrawingCanvas(svgReader);
        }

        /// <summary>
        /// 创建SvgDrawingCanvas(Svg画布)
        /// </summary>
        /// <param name="svgUri">svg文件的Uri路径</param>
        /// <returns>SvgDrawingCanvas(Svg画布)</returns>
        public static SvgDrawingCanvas CreateSvgDrawingCanvas(Uri svgUri)
        {
            FileSvgReader svgReader = new FileSvgReader(null);
            svgReader.Read(svgUri);
            return CreateSvgDrawingCanvas(svgReader);
        }


        /// <summary>
        /// 创建SvgDrawingCanvas(Svg画布)
        /// </summary>
        /// <param name="svgStream">svg文件流</param>
        /// <returns>SvgDrawingCanvas(Svg画布)</returns>
        public static SvgDrawingCanvas CreateSvgDrawingCanvas(Stream svgStream)
        {
            FileSvgReader svgReader = new FileSvgReader(null);
            svgReader.Read(svgStream);
            return CreateSvgDrawingCanvas(svgReader);
        }

        /// <summary>
        /// 创建SvgDrawingCanvas(Svg画布)
        /// </summary>
        /// <param name="svgTextReader">svg文件的TextReader</param>
        /// <returns>SvgDrawingCanvas(Svg画布)</returns>
        public static SvgDrawingCanvas CreateSvgDrawingCanvas(TextReader svgTextReader)
        {
            FileSvgReader svgReader = new FileSvgReader(null);
            svgReader.Read(svgTextReader);
            return CreateSvgDrawingCanvas(svgReader);
        }

        /// <summary>
        /// 创建SvgDrawingCanvas(Svg画布)
        /// </summary>
        /// <param name="svgTextReader">svg文件的XmlReader</param>
        /// <returns>SvgDrawingCanvas(Svg画布)</returns>
        public static SvgDrawingCanvas CreateSvgDrawingCanvas(XmlReader svgXmlReader)
        {
            FileSvgReader svgReader = new FileSvgReader(null);
            svgReader.Read(svgXmlReader);
            return CreateSvgDrawingCanvas(svgReader);
        }

        /// <summary>
        /// 创建ViewBox
        /// </summary>
        /// <returns></returns>
        private static Viewbox CreateViewBox()
        {
            Viewbox viewBox = new Viewbox();
            //viewBox.Stretch = Stretch.Fill;
            return viewBox;
        }

        /// <summary>
        /// 创建含有SvgDrawingCanvas(Svg画布)的ViewBox
        /// </summary>
        /// <param name="svgFileName">svg文件路径</param>
        /// <returns>含有SvgDrawingCanvas(Svg画布)的ViewBox</returns>
        public static Viewbox CreateSvgViewBox(string svgFileName)
        {
            var viewBox = CreateViewBox();
            viewBox.Child = CreateSvgDrawingCanvas(svgFileName);
            return viewBox;
        }

        /// <summary>
        /// 创建含有SvgDrawingCanvas(Svg画布)的ViewBox
        /// </summary>
        /// <param name="svgUri">svg文件的Uri路径</param>
        /// <returns>含有SvgDrawingCanvas(Svg画布)的ViewBox</returns>
        public static Viewbox CreateSvgViewBox(Uri svgUri)
        {
            var viewBox = CreateViewBox();
            viewBox.Child = CreateSvgDrawingCanvas(svgUri);
            return viewBox;
        }

        /// <summary>
        /// 创建含有SvgDrawingCanvas(Svg画布)的ViewBox
        /// </summary>
        /// <param name="svgStream">svg文件流</param>
        /// <returns>含有SvgDrawingCanvas(Svg画布)的ViewBox</returns>
        public static Viewbox CreateSvgViewBox(Stream svgStream)
        {
            var viewBox = CreateViewBox();
            viewBox.Child = CreateSvgDrawingCanvas(svgStream);
            return viewBox;
        }

        /// <summary>
        /// 创建含有SvgDrawingCanvas(Svg画布)的ViewBox
        /// </summary>
        /// <param name="svgTextReader">svg文件的TextReader</param>
        /// <returns>含有SvgDrawingCanvas(Svg画布)的ViewBox</returns>
        public static Viewbox CreateSvgViewBox(TextReader svgTextReader)
        {
            var viewBox = CreateViewBox();
            viewBox.Child = CreateSvgDrawingCanvas(svgTextReader);
            return viewBox;
        }

        /// <summary>
        /// 创建含有SvgDrawingCanvas(Svg画布)的ViewBox
        /// </summary>
        /// <param name="svgTextReader">svg文件的XmlReader</param>
        /// <returns>含有SvgDrawingCanvas(Svg画布)的ViewBox</returns>
        public static Viewbox CreateSvgViewBox(XmlReader svgXmlReader)
        {
            var viewBox = CreateViewBox();
            viewBox.Child = CreateSvgDrawingCanvas(svgXmlReader);
            return viewBox;
        }
    }

    ///// <summary>
    ///// SVG帮助类
    ///// </summary>
    //public sealed class SvgHelper
    //{
    //    /// <summary>
    //    /// 创建SvgCanvas(Svg画布)
    //    /// </summary>
    //    /// <param name="svgReader"></param>
    //    /// <returns>SvgCanvas(Svg画布)</returns>
    //    private static SvgCanvas CreateSvgDrawingCanvas(SvgReader svgReader)
    //    {
    //        SvgCanvas svgCanvas = new SvgCanvas();
    //        svgCanvas.RenderDiagrams(svgReader.Drawing);
    //        return svgCanvas;
    //    }

    //    /// <summary>
    //    /// 创建SvgCanvas(Svg画布)
    //    /// </summary>
    //    /// <param name="svgFileName">svg文件路径</param>
    //    /// <returns>SvgCanvas(Svg画布)</returns>
    //    public static SvgCanvas CreateSvgDrawingCanvas(string svgFileName)
    //    {
    //        SvgReader svgReader = new SvgReader();
    //        svgReader.Read(svgFileName);
    //        return CreateSvgDrawingCanvas(svgReader);
    //    }

    //    /// <summary>
    //    /// 创建SvgCanvas(Svg画布)
    //    /// </summary>
    //    /// <param name="svgUri">svg文件的Uri路径</param>
    //    /// <returns>SvgCanvas(Svg画布)</returns>
    //    public static SvgCanvas CreateSvgDrawingCanvas(Uri svgUri)
    //    {
    //        SvgReader svgReader = new SvgReader();
    //        svgReader.Read(svgUri);
    //        return CreateSvgDrawingCanvas(svgReader);
    //    }


    //    /// <summary>
    //    /// 创建SvgCanvas(Svg画布)
    //    /// </summary>
    //    /// <param name="svgStream">svg文件流</param>
    //    /// <returns>SvgCanvas(Svg画布)</returns>
    //    public static SvgCanvas CreateSvgDrawingCanvas(Stream svgStream)
    //    {
    //        SvgReader svgReader = new SvgReader();
    //        svgReader.Read(svgStream);
    //        return CreateSvgDrawingCanvas(svgReader);
    //    }

    //    /// <summary>
    //    /// 创建SvgCanvas(Svg画布)
    //    /// </summary>
    //    /// <param name="svgTextReader">svg文件的TextReader</param>
    //    /// <returns>SvgCanvas(Svg画布)</returns>
    //    public static SvgCanvas CreateSvgDrawingCanvas(TextReader svgTextReader)
    //    {
    //        SvgReader svgReader = new SvgReader();
    //        svgReader.Read(svgTextReader);
    //        return CreateSvgDrawingCanvas(svgReader);
    //    }

    //    /// <summary>
    //    /// 创建SvgCanvas(Svg画布)
    //    /// </summary>
    //    /// <param name="svgTextReader">svg文件的XmlReader</param>
    //    /// <returns>SvgCanvas(Svg画布)</returns>
    //    public static SvgCanvas CreateSvgDrawingCanvas(XmlReader svgXmlReader)
    //    {
    //        SvgReader svgReader = new SvgReader();
    //        svgReader.Read(svgXmlReader);
    //        return CreateSvgDrawingCanvas(svgReader);
    //    }

    //    /// <summary>
    //    /// 创建ViewBox
    //    /// </summary>
    //    /// <returns></returns>
    //    private static Viewbox CreateViewBox()
    //    {
    //        Viewbox viewBox = new Viewbox();
    //        viewBox.Stretch = Stretch.Fill;
    //        return viewBox;
    //    }

    //    /// <summary>
    //    /// 创建含有SvgCanvas(Svg画布)的ViewBox
    //    /// </summary>
    //    /// <param name="svgFileName">svg文件路径</param>
    //    /// <returns>含有SvgCanvas(Svg画布)的ViewBox</returns>
    //    public static Viewbox CreateSvgViewBox(string svgFileName)
    //    {
    //        var viewBox = CreateViewBox();
    //        viewBox.Child = CreateSvgDrawingCanvas(svgFileName);
    //        return viewBox;
    //    }

    //    /// <summary>
    //    /// 创建含有SvgCanvas(Svg画布)的ViewBox
    //    /// </summary>
    //    /// <param name="svgUri">svg文件的Uri路径</param>
    //    /// <returns>含有SvgCanvas(Svg画布)的ViewBox</returns>
    //    public static Viewbox CreateSvgViewBox(Uri svgUri)
    //    {
    //        var viewBox = CreateViewBox();
    //        viewBox.Child = CreateSvgDrawingCanvas(svgUri);
    //        return viewBox;
    //    }

    //    /// <summary>
    //    /// 创建含有SvgCanvas(Svg画布)的ViewBox
    //    /// </summary>
    //    /// <param name="svgStream">svg文件流</param>
    //    /// <returns>含有SvgCanvas(Svg画布)的ViewBox</returns>
    //    public static Viewbox CreateSvgViewBox(Stream svgStream)
    //    {
    //        var viewBox = CreateViewBox();
    //        viewBox.Child = CreateSvgDrawingCanvas(svgStream);
    //        return viewBox;
    //    }

    //    /// <summary>
    //    /// 创建含有SvgCanvas(Svg画布)的ViewBox
    //    /// </summary>
    //    /// <param name="svgTextReader">svg文件的TextReader</param>
    //    /// <returns>含有SvgCanvas(Svg画布)的ViewBox</returns>
    //    public static Viewbox CreateSvgViewBox(TextReader svgTextReader)
    //    {
    //        var viewBox = CreateViewBox();
    //        viewBox.Child = CreateSvgDrawingCanvas(svgTextReader);
    //        return viewBox;
    //    }

    //    /// <summary>
    //    /// 创建含有SvgCanvas(Svg画布)的ViewBox
    //    /// </summary>
    //    /// <param name="svgTextReader">svg文件的XmlReader</param>
    //    /// <returns>含有SvgCanvas(Svg画布)的ViewBox</returns>
    //    public static Viewbox CreateSvgViewBox(XmlReader svgXmlReader)
    //    {
    //        var viewBox = CreateViewBox();
    //        viewBox.Child = CreateSvgDrawingCanvas(svgXmlReader);
    //        return viewBox;
    //    }
    //}

    ///// <summary>
    ///// SvgReader
    ///// </summary>
    //public class SvgReader : FileSvgReader
    //{
    //    public SvgReader()
    //        : base(null)
    //    {
    //    }
    //}
}
