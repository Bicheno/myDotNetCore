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

        private string _image;
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

        private Visibility _is_show;
        public Visibility is_show
        {
            get { return _is_show; }
            set
            {
                if (_is_show != value)
                {
                    _is_show = value;
                    RaisePropertyChanged("is_show");
                }
            }
        }
    }
}
