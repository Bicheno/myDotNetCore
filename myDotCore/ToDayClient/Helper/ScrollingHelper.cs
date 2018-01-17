using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ToDayClient.Helper
{
    public class ScrollingHelper
    {
        /// <summary>
        /// 解决该元素下的鼠标滚动失灵问题
        /// </summary>
        /// <param name="element"></param>
        public static void UserTheScroll(FrameworkElement element)
        {
            element.PreviewMouseWheel += (sender, e) =>
            {
                var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
                eventArg.RoutedEvent = UIElement.MouseWheelEvent;
                eventArg.Source = sender;
                element.RaiseEvent(eventArg);
            };
        }
    }
}
