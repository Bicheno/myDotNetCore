using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ToDayClient.Helper
{
    public static class Utils
    {
        public static T FindVisualParent<T>(DependencyObject obj) where T : class
        {
            while (obj != null)
            {
                if (obj is T)
                    return obj as T;

                obj = VisualTreeHelper.GetParent(obj);
            }

            return null;
        }

        /// <summary>
        /// 获取控件同的显示控件集
        /// </summary>
        /// <typeparam name="T">要获取的元素类型</typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static List<T> FindVisualParentCollection<T>(object parent) where T : UIElement
        {
            List<T> visualCollection = new List<T>();
            FindVisualParentCollection(parent as DependencyObject, visualCollection);
            return visualCollection;
        }

        private static void FindVisualParentCollection<T>(DependencyObject child, List<T> visualCollection) where T : UIElement
        {
            var parent = VisualTreeHelper.GetParent(child);
            if (parent != null)
            {
                if (parent is T)
                {
                    visualCollection.Add(parent as T);
                }

                FindVisualParentCollection(parent, visualCollection);
            }
        }

        /// <summary>
        /// 获取控件同的显示控件集
        /// </summary>
        /// <typeparam name="T">要获取的元素类型</typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static List<T> GetVisualChildCollection<T>(object parent) where T : UIElement
        {
            List<T> visualCollection = new List<T>();
            GetVisualChildCollection(parent as DependencyObject, visualCollection);
            return visualCollection;
        }

        private static void GetVisualChildCollection<T>(DependencyObject parent, List<T> visualCollection) where T : UIElement
        {
            int count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child is T)
                {
                    visualCollection.Add(child as T);
                }

                if (child != null)
                {
                    GetVisualChildCollection(child, visualCollection);
                }
            }
        }
    }
}
