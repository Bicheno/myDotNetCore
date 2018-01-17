using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDayClient
{
    public class todo_list : ModelBase
    {
        private string _todo;
        /// <summary>
        /// TODO内容
        /// </summary>
        public string todo
        {
            get { return _todo; }
            set
            {
                _todo = value;
                RaisePropertyChanged("todo");
            }
        }

        private int _num;
        /// <summary>
        /// 排序号
        /// </summary>
        public int num
        {
            get { return _num; }
            set
            {
                _num = value;
                RaisePropertyChanged("num");
            }
        }

        private bool _isEnabled_TODO;
        /// <summary>
        /// 文本框是否可用
        /// </summary>
        public bool IsEnabled_TODO
        {
            get { return _isEnabled_TODO; }
            set
            {
                _isEnabled_TODO = value;
                RaisePropertyChanged("IsEnabled_TODO");
            }
        }
    }

    public class done_list: ModelBase
    {
        private string _done;
        /// <summary>
        /// DONE内容
        /// </summary>
        public string done
        {
            get { return _done; }
            set
            {
                _done = value;
                RaisePropertyChanged("done");
            }
        }

        private int _num;
        /// <summary>
        /// 排序号
        /// </summary>
        public int num
        {
            get { return _num; }
            set
            {
                _num = value;
                RaisePropertyChanged("num");
            }
        }

        private string _box_color;
        /// <summary>
        /// 文本框颜色
        /// </summary>
        public string box_color
        {
            get { return _box_color; }
            set
            {
                _box_color = value;
                RaisePropertyChanged("box_color");
            }
        }
    }

    public class ModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
