using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WX.Windows.Diagram.Designer
{
    public interface IDesignCanvas
    {
        /// <summary>
        /// 创建新的设计
        /// </summary>
        void NewDesign();

        /// <summary>
        /// 加载设计信息
        /// </summary>
        /// <param name="designItemInfos"></param>
        void LoadDesignInfo(CanvasInfo canvasInfo, DesignItemInfo[] designItemInfos, DesignConnectionInfo[] designConnectionInfos);

        /// <summary>
        /// 加载连线(初始化加载画布可使用的所有连线)
        /// </summary>
        /// <param name="shapeInfoUnites"></param>
        void LoadConnection(ConnectionShapeInfo[] shapeInfos);

        /// <summary>
        /// 加载连线选项
        /// </summary>
        /// <param name="shapeInfos"></param>
        void LoadOptionConnection(ShapeInfo[] shapeInfos);

        /// <summary>
        /// 加载提示工具箱的图元）
        /// </summary>
        /// <param name="shapeInfos"></param>
        void LoadTipToolboxShapes(ShapeInfo[] shapeInfos);

        /// <summary>
        /// 获取设计信息
        /// </summary>
        /// <returns></returns>
        Tuple<CanvasInfo,DesignItemInfo[], DesignConnectionInfo[]> RetrieveDesignInfos();

        /// <summary>
        /// 获取设计实体项信息
        /// </summary>
        /// <returns></returns>
        DesignItemInfo[] RetrieveDesignItemInfos();

        /// <summary>
        /// 获取连线信息
        /// </summary>
        /// <returns></returns>
        DesignConnectionInfo[] RetrieveDesignConnectionInfos();

        /// <summary>
        /// 选择指定设计项
        /// </summary>
        /// <param name="designItemInfo"></param>
        void SelectDesignItem(DesignItemInfo designItemInfo);

        //--------------------------

        /// <summary>
        /// 设计画布鼠标单击事件
        /// </summary>
        event CanvasActionEventHandler DesignCanvasMouseClick;

        /// <summary>
        /// 设计画布鼠标双击事件
        /// </summary>
        event CanvasActionEventHandler DesignCanvasMouseDoubleClick;

        /// <summary>
        /// 设计画布鼠标右键单击事件
        /// </summary>
        event CanvasActionEventHandler DesignCanvasMouseRightClick;

        /// <summary>
        /// 设计实体项鼠标双击事件
        /// </summary>
        event SingleDesignItemActionEventHandler DesignItemMouseDoubleClick;

        /// <summary>
        /// 设计实体项鼠标右键单击事件
        /// </summary>
        event SingleDesignItemActionEventHandler DesignItemMouseRightClick;

        /// <summary>
        /// 设计实体项链接按钮鼠标单击事件
        /// </summary>
        event SingleDesignItemActionEventHandler DesignItemLinkButtonClick;

        /// <summary>
        /// 设计实体项链接按钮鼠标双击事件
        /// </summary>
        event SingleDesignItemActionEventHandler DesignItemLinkButtonDoubleClick;

        /// <summary>
        /// 设计连线鼠标双击事件
        /// </summary>
        event SingleDesignConnectionActionEventHandler DesignConnectionMouseDoubleClick;

        /// <summary>
        /// 设计连线鼠标右键单击事件
        /// </summary>
        event SingleDesignConnectionActionEventHandler DesignConnectionMouseRightClick;

        /// <summary>
        /// 设计实体项单选事件
        /// </summary>
        event SingleDesignItemActionEventHandler DesignItemSelected;

        /// <summary>
        /// 设计连线单选事件
        /// </summary>
        event SingleDesignConnectionActionEventHandler DesignConnectionSelected;

        /// <summary>
        /// 预增加设计实体项事件
        /// </summary>
        event DesignItemValidationEventHandler PreAddDesignItem;

        /// <summary>
        /// 增加设计实体项进行事件
        /// </summary>
        event DesignItemValidationEventHandler AddingDesignItem;

        /// <summary>
        /// 增加设计实体项完成事件
        /// </summary>
        event SingleDesignItemActionEventHandler AddedDesignItem;

        /// <summary>
        /// 预增加设计连线事件
        /// </summary>
        event DesignConnectionValidationEventHandler PreAddDesignConnection;

        /// <summary>
        /// 增加设计连线进行事件
        /// </summary>
        event DesignConnectionValidationEventHandler AddingDesignConnection;

        /// <summary>
        /// 增加设计连线完成事件
        /// </summary>
        event SingleDesignConnectionActionEventHandler AddedDesignConnection;

        /// <summary>
        /// 设计实体项多选事件
        /// </summary>
        event MultiDesignItemActionEventHandler DesignItemMultiSelected;

        /// <summary>
        /// 设计图元多选事件(包括实体项及连线)
        /// </summary>
        event MultiDesignShapeActionEventHandler DesignShapeMultiSelected;

        /// <summary>
        /// 增加设计图元进行事件(包括实体项及连线,如成组片段新增的情况)
        /// </summary>
        event MultiDesignShapeActionEventHandler AddingDesignShapes;

        /// <summary>
        /// 增加设计图元完成事件(包括实体项及连线,如成组片段新增的情况)
        /// </summary>
        event MultiDesignShapeActionEventHandler AddedDesignShapes;

        /// <summary>
        /// 删除设计图元进行事件(包括实体项及连线)
        /// </summary>
        event MultiDesignShapeActionEventHandler DeletingDesignShapes;

        /// <summary>
        /// 删除设计图元完成事件(包括实体项及连线)
        /// </summary>
        event MultiDesignShapeActionEventHandler DeletedDesignShapes;

        /// <summary>
        /// 修改设计图元进行事件(包括实体项及连线，如修改某个通用的属性)
        /// </summary>
        event MultiDesignShapeActionEventHandler ModifyingDesignShapes;

        /// <summary>
        /// 修改设计图元完成事件(包括实体项及连线，如修改某个通用的属性)
        /// </summary>
        event MultiDesignShapeActionEventHandler ModifiedDesignShapes;

        /// <summary>
        /// 设计实体项集合改变事件
        /// </summary>
        event DesignItemCollectionChangedEventHandler DesignItemCollectionChanged;

        /// <summary>
        /// 设计工作区保存进行事件(由快捷菜单命令触发)
        /// </summary>
        event WorkspaceActionEventHandler DesignWorkspaceSaving;
    }
}
