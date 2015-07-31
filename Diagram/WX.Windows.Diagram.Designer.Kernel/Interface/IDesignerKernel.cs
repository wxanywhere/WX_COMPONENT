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
    public interface IDesignerKernel:IDesignCanvas
    {
        /// <summary>
        /// 加载存在的图元到工具箱
        /// </summary>
        /// <param name="shapeInfos"></param>
        void LoadToolBoxExistShapes(ShapeInfo[] shapeInfos);

        /// <summary>
        /// 加载备选的图元到工具箱
        /// </summary>
        /// <param name="shapeInfos"></param>
        void LoadToolBoxCandidateShapes(ShapeInfo[] shapeInfos);

        /// <summary>
        /// 加载图元片段
        /// </summary>
        /// <param name="shapeInfos"></param>
        void LoadToolBoxShapeGroups(ShapeInfo[] shapeInfos);

        //--------------------------

        /// <summary>
        /// 保存工具箱图元更新事件
        /// </summary>
        event MultiShapeActionEventHandler SavedToolboxShapes;
    }
}
