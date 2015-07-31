using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace WX.Windows.Diagram.Designer
{
    /// <summary>
    /// 设计器数据交换及事件接口
    /// </summary>
    public interface IDesignerMain
    {
        /// <summary>
        /// 当前工作区实例
        /// </summary>
        DesignerKernel CurrentDesignWorkspace { get; set; }

        /// <summary>
        /// 设计工作区激活事件
        /// </summary>
        event EventHandler DesigneWorkspaceActivated;

        event EventHandler NavigationTreeItemMouseClick;

        event EventHandler NavigationTreeItemMouseDoubleClick;

        event EventHandler NavigationTreeItemMouseRightClick;
    }
}
