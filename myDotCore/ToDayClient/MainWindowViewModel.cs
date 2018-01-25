using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ToDayClient.Helper;

namespace ToDayClient
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region 属性

        HttpHelper hp = new HttpHelper();

        public string Title { get; set; }

        private ObservableCollection<todo_list> _todo_list;
        public ObservableCollection<todo_list> todo_list
        {
            get { return _todo_list; }
            set
            {
                _todo_list = value;
                RaisePropertyChanged("todo_list");
            }
        }

        private ObservableCollection<done_list> _done_list;
        public ObservableCollection<done_list> done_list
        {
            get { return _done_list; }
            set
            {
                _done_list = value;
                RaisePropertyChanged("done_list");
            }
        }

        #endregion

        #region 方法

        public MainWindowViewModel()
        {
            Title = "ToDay - " + DateTime.Now.ToLongDateString().ToString();
            todo_list = new ObservableCollection<todo_list>();
            done_list = new ObservableCollection<done_list>();

            Task task = new Task(new Action(CheckWriteIn));
            task.Start();
        }

        public void Add()
        {
            try
            {
                if (todo_list == null) return;
                todo_list.Add(new todo_list() { user_id = 1, content = "", num = todo_list.Count });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void Del(object sender, RoutedEventArgs e)
        {
            try
            {
                if (todo_list == null || todo_list.Count < 1) return;

                var button = sender as Button;
                if (button != null && button.Tag != null)
                {
                    var tag = int.Parse(button.Tag.ToString());
                    var cur = todo_list.FirstOrDefault(t => t.num == tag);
                    if (cur != null)  
                    {
                        todo_list.Remove(cur);
                        var i = 0;

                        foreach (var item in todo_list)
                        {
                            item.num = i;
                            i += 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void WriteIn(object sender, MouseButtonEventArgs e)
        {
            var listView = sender as ListView;
            todo_list _todo_list = listView.SelectedItem as todo_list;

            if (_todo_list != null && listView != null)
            {
                //当前文本框可用
                foreach (var item in todo_list)
                {
                    if (item == _todo_list)
                    {
                        item.IsEnabled_TODO = true;
                    }
                    else
                    {
                        item.IsEnabled_TODO = false;
                    }
                }

                resElement = new List<FrameworkElement>();
                FindChildByType(listView, typeof(TextBox));
                var cur_box = resElement[_todo_list.num];
                if (cur_box is TextBox)
                {
                    Keyboard.Focus(cur_box);//使得当前文本框获得光标
                }
            }
        }

        public void CheckWriteIn()
        {
            try
            {
                while (true)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(100));

                    if (todo_list == null || todo_list.Count < 1) return;

                    //List<AddToDoParaModel> addToDoPara = new List<AddToDoParaModel>();
                    //todo_list.CopyTo(addToDoPara,0);

                    string result;
                    bool res = hp.Post<object>(todo_list, "/Client/addtodo", out result);
                    if (res)
                    {

                    }
                }
            }
            catch(Exception ex)
            {

            }
        }

        #region 私有辅助方法

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

        #endregion

        #endregion
    }
}
