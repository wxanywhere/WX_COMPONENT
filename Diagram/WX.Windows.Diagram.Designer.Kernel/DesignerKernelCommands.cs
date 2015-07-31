using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace WX.Windows.Diagram.Designer
{
    /// <summary>
    /// 工具项及菜单项命令
    /// </summary>
    public partial class DesignerKernel
    {
        /// <summary>
        /// 导出设计命令
        /// </summary>
        public static RoutedCommand ExportDesignWorkspace = new RoutedCommand();

        /// <summary>
        /// 重命名图元命令
        /// </summary>
        public static RoutedCommand RenameDesignShape = new RoutedCommand();

        /// <summary>
        /// 图元分配命令
        /// </summary>
        public static RoutedCommand AssignDesignShape = new RoutedCommand();

        /// <summary>
        /// 打开设计特性命令
        /// </summary>
        public static RoutedCommand OpenDesignFeature = new RoutedCommand();

        /// <summary>
        /// 打开设计特性向导命令
        /// </summary>
        public static RoutedCommand OpenDesignFeatureGuide = new RoutedCommand();

        /// <summary>
        /// 打开设计属性命令
        /// </summary>
        public static RoutedCommand OpenDesignProperty = new RoutedCommand();
    }
}
