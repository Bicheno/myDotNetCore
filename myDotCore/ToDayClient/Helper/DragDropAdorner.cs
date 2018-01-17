using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace ToDayClient.Helper
{
    /// <summary>
    /// 拖放过程中的装饰器
    /// </summary>
    public class DragDropAdorner : Adorner
    {
        public DragDropAdorner(UIElement parent)
            : base(parent)
        {
            IsHitTestVisible = false; // Seems Adorner is hit test visible?
            mDraggedElement = parent as FrameworkElement;
        }

        /// <summary>
        /// 记录鼠标相于当前mDraggedElement的起此偏移量
        /// 用于渲染预览图时保持与鼠标的相移位置
        /// </summary>
        public Point Pox { get; set; }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (mDraggedElement != null)
            {
                Win32.POINT screenPos = new Win32.POINT();
                if (Win32.GetCursorPos(ref screenPos))
                {
                    Point pos = PointFromScreen(new Point(screenPos.X, screenPos.Y));
                    //Point pos = Pox;
                    Rect rect = new Rect(pos.X - Pox.X, pos.Y - Pox.Y, mDraggedElement.ActualWidth, mDraggedElement.ActualHeight);
                    drawingContext.PushOpacity(0.7);

                    Brush highlight = mDraggedElement.TryFindResource(SystemColors.ControlBrushKey) as Brush;
                    if (highlight != null)
                    {
                        drawingContext.DrawRectangle(highlight, new Pen(Brushes.Transparent, 0), rect);
                    }
                        
                    drawingContext.DrawRectangle(new VisualBrush(mDraggedElement), new Pen(Brushes.Transparent, 0), rect);
                    drawingContext.Pop();
                }
            }
        }

        FrameworkElement mDraggedElement = null;
    }

    public static class Win32
    {
        public struct POINT { public Int32 X; public Int32 Y; }

        // During drag-and-drop operations, the position of the mouse cannot be 
        // reliably determined through GetPosition. This is because control of 
        // the mouse (possibly including capture) is held by the originating 
        // element of the drag until the drop is completed, with much of the 
        // behavior controlled by underlying Win32 calls. As a workaround, you 
        // might need to use Win32 externals such as GetCursorPos.
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(ref POINT point);
    }
}
