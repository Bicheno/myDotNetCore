using Newtonsoft.Json;
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
        public int user_id { get; set; }

        private string _content;
        /// <summary>
        /// TODO内容
        /// </summary>
        public string content
        {
            get { return _content; }
            set
            {
                _content = value;
                RaisePropertyChanged("content");
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
        [JsonIgnore]
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
        public int user_id { get; set; }

        private string _content;
        /// <summary>
        /// DONE内容
        /// </summary>
        public string content
        {
            get { return _content; }
            set
            {
                _content = value;
                RaisePropertyChanged("content");
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
        [JsonIgnore]
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

    /// <summary>
    /// http返回结果Json对象
    /// </summary>
    public class ResultModel
    {
        public int code { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }

    public enum ApiStatus
    {
        [Description("返回成功！")]
        Success = 200,

        [Description("配置失败！")]
        ConfigFair = 500,

        [Description("参数解析失败！")]
        ParaParseFair = 501,

        [Description("服务器内部错误！")]
        ServerInternalError = 502,

        [Description("数据库操作失败！")]
        DatabaseError = 503,
    }

    public class IPModel
    {
        public string ip { get; set; }
    }
    public class ModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    #region 请求model

    public class AddToDoParaModel
    {
        public int user_id { get; set; }

        public int num { get; set; }

        public string content { get; set; }

    }

    #endregion
}
