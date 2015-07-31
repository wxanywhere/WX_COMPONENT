using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Win32;

namespace WX.Windows.Diagram.Designer
{
    /// <summary>
    /// 工具项及菜单项命令
    /// </summary>
    public partial class DesignCanvas 
    {
        /*内置命令
        ApplicationCommands.Save
        ApplicationCommands.Print
        ApplicationCommands.Cut
        ApplicationCommands.Copy
        ApplicationCommands.Paste
        ApplicationCommands.Delete
        */
        /// <summary>
        /// 成组命令
        /// </summary>
        public static RoutedCommand Group = new RoutedCommand();

        /// <summary>
        /// 拆组命令
        /// </summary>
        public static RoutedCommand Ungroup = new RoutedCommand();

        /// <summary>
        /// 上移一层命令
        /// </summary>
        public static RoutedCommand BringForward = new RoutedCommand();

        /// <summary>
        /// 移到最上层命令
        /// </summary>
        public static RoutedCommand BringToFront = new RoutedCommand();

        /// <summary>
        /// 下移一层命令
        /// </summary>
        public static RoutedCommand SendBackward = new RoutedCommand();

        /// <summary>
        /// 移到最下层命令
        /// </summary>
        public static RoutedCommand SendToBack = new RoutedCommand();

        /// <summary>
        /// 居顶对齐命令
        /// </summary>
        public static RoutedCommand AlignTop = new RoutedCommand();

        /// <summary>
        /// 居中垂直对齐命令
        /// </summary>
        public static RoutedCommand AlignVerticalCenters = new RoutedCommand();

        /// <summary>
        /// 居底对齐命令
        /// </summary>
        public static RoutedCommand AlignBottom = new RoutedCommand();

        /// <summary>
        /// 居左对齐命令
        /// </summary>
        public static RoutedCommand AlignLeft = new RoutedCommand();

        /// <summary>
        /// 居中水平对齐命令
        /// </summary>
        public static RoutedCommand AlignHorizontalCenters = new RoutedCommand();

        /// <summary>
        /// 居右对齐命令
        /// </summary>
        public static RoutedCommand AlignRight = new RoutedCommand();

        /// <summary>
        /// 水平等距排列命令
        /// </summary>
        public static RoutedCommand DistributeHorizontal = new RoutedCommand();

        /// <summary>
        /// 垂直等距排列命令
        /// </summary>
        public static RoutedCommand DistributeVertical = new RoutedCommand();

        /// <summary>
        /// 全选命令
        /// </summary>
        public static RoutedCommand SelectAll = new RoutedCommand();
    }
}
