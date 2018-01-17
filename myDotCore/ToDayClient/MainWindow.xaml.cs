using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToDayClient.Helper;

namespace ToDayClient
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        AdornerLayer mAdornerLayer = null;  //拖放预览呈现层
        bool hadAddEvent = false;

        public MainWindow()
        {
            InitializeComponent();
            DONE_ListView.Height = 200;
            ScrollingHelper.UserTheScroll(mGrid);
        }

        private void View_MouseUp(object sender, MouseButtonEventArgs e)
        {
            resElement = new List<FrameworkElement>();
            FindChildByType(TODO_ListView, typeof(TextBox));
            if (resElement == null) return;

            //鼠标点击位置
            var mouse = Mouse.GetPosition(TODO_ListView);

            //画TODO_ListView的范围，用来判断鼠标是否点击TODO_ListView外
            Rect rect = new Rect(0, 0, TODO_ListView.ActualWidth, TODO_ListView.ActualHeight);

            var has_mouse = rect.Contains(mouse);
            if (!has_mouse)
            {
                //当鼠标点击TODO_ListView外部时，将所有文本框都设为不可用
                foreach (var item in resElement)
                {
                    if (!item.ForceCursor)
                        item.IsEnabled = false;
                }
            }
        }

        private void TODO_DockPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                var dockpanel = sender as DockPanel;
                if (dockpanel != null && dockpanel.Tag != null)
                {
                    //获取鼠标当前移入的DockPanel下的删除按钮
                    var cur_btn = VisualTreeHelper.GetChild(dockpanel, 1) as FrameworkElement;
                    if (cur_btn is Button)
                    {
                        cur_btn.Visibility = Visibility.Visible;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void TODO_DockPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                var dockpanel = sender as DockPanel;
                if (dockpanel != null && dockpanel.Tag != null)
                {
                    //获取鼠标当前移入的DockPanel下的删除按钮
                    var cur_btn = VisualTreeHelper.GetChild(dockpanel, 1) as FrameworkElement;
                    if (cur_btn is Button)
                    {
                        cur_btn.Visibility = Visibility.Hidden;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public List<FrameworkElement> resElement;

        /// <summary>
        /// 获取指定类型的子元素集
        /// </summary>
        /// <param name="relate"></param>
        /// <param name="type"></param>
        /// <param name="resElement"></param>
        private void FindChildByType(DependencyObject relate, Type type)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(relate); i++)
            {
                var el = VisualTreeHelper.GetChild(relate, i) as FrameworkElement;
                if (el.GetType() == type)
                {
                    resElement.Add(el);
                    return;
                }
                else
                {
                    FindChildByType(el, type);
                }
            }
        }

        /// <summary>
        /// 拖动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TODO_ListView_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (Mouse.LeftButton != MouseButtonState.Pressed) return;

                Point pos = e.GetPosition(TODO_ListView);
                HitTestResult result = VisualTreeHelper.HitTest(TODO_ListView, pos);
                if (result == null) return;

                ListBoxItem listBoxItem = Utils.FindVisualParent<ListBoxItem>(result.VisualHit);
                if (listBoxItem == null || listBoxItem.Content != TODO_ListView.SelectedItem || (TODO_ListView.SelectedItem as todo_list).IsEnabled_TODO == true) return;

                if (!hadAddEvent)
                {
                    TODO_ListView.QueryContinueDrag += TODO_ListView_QueryContinueDrag;
                    hadAddEvent = true;
                }

                //添加修饰器：拖动时跟随鼠标移动显示
                DragDropAdorner adorner = new DragDropAdorner(listBoxItem);
                mAdornerLayer = AdornerLayer.GetAdornerLayer(mGrid);
                mAdornerLayer.Add(adorner);

                //获取数据上下文
                DataObject dataObject = new DataObject(typeof(ContentControl), listBoxItem.DataContext);
                //启动拖放
                DragDrop.DoDragDrop(TODO_ListView, dataObject, DragDropEffects.Copy);

                mAdornerLayer.Remove(adorner);
                mAdornerLayer = null;
                hadAddEvent = false;

                DropShadowEffect outerGlow = new DropShadowEffect();
                outerGlow.ShadowDepth = 0;
                outerGlow.Color = Color.FromRgb(255, 255, 255);
                outerGlow.BlurRadius = 0;
                Shine.Effect = outerGlow;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 鼠标拖动时更新修饰器位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TODO_ListView_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {
            if (mAdornerLayer == null) return;
            mAdornerLayer.Update();
        }

        private void DONE_ListView_DragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.None;

            Point pos = e.GetPosition(DONE_ListView);
            HitTestResult result = VisualTreeHelper.HitTest(DONE_ListView,pos);
            if (result == null) return;

            e.Effects = DragDropEffects.Copy;

            //添加阴影效果
            DropShadowEffect outerGlow = new DropShadowEffect();
            outerGlow.ShadowDepth = 0;
            outerGlow.Color = Color.FromRgb(102, 102, 102);
            outerGlow.BlurRadius = 50;
            outerGlow.Opacity = 0.5;
            Shine.Effect = outerGlow;
        }

        private void DONE_ListView_Drop(object sender, DragEventArgs e)
        {
            try
            {
                Point pos = e.GetPosition(DONE_ListView);
                HitTestResult result = VisualTreeHelper.HitTest(DONE_ListView, pos);
                if (result == null) return;

                //获取拖动的控件的数据
                todo_list data = (todo_list)e.Data.GetData(typeof(ContentControl));

                var model = DONE_ListView.DataContext as MainWindowViewModel;
                done_list list = new done_list();
                list.done = data.todo;
                list.num = data.num;
                list.box_color = "#BBBBBB";
                model.done_list.Add(list);
                DONE_ListView.Height = DONE_ListView.Height + 50;//递加DONE_ListView高度

                model.todo_list.Remove(data);
                var i = 0;

                foreach (var item in model.todo_list)
                {
                    item.num = i;
                    i += 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }  
}
