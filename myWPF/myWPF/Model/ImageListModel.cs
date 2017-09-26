using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;

namespace myWPF.Model
{
    public class ImageListModel : ModelBase
    {
        private string _id;
        /// <summary>
        /// 返回id
        /// </summary>
        public string id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    RaisePropertyChanged("id");
                }
            }
        }

        private string _image;
        /// <summary>
        /// 图片Source
        /// </summary>
        public string image
        {
            get { return _image; }
            set
            {
                if (_image != value)
                {
                    _image = value;
                    RaisePropertyChanged("image");
                }
            }
        }

        private bool _is_old;
        /// <summary>
        /// 标记该图片是否是新增的
        /// </summary>
        public bool is_old
        {
            get { return _is_old; }
            set
            {
                if (_is_old != value)
                {
                    _is_old = value;
                    RaisePropertyChanged("is_old");
                }
            }
        }
    }
}
