using GalaSoft.MvvmLight;
using myWPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace myWPF.ViewModel
{
    public class ImageListViewModel : ViewModelBase
    {
        private ObservableCollection<ImageListModel> _image_list;
        public ObservableCollection<ImageListModel> image_list
        {
            get { return _image_list; }
            set
            {
                if (_image_list != value)
                {
                    _image_list = value;
                    RaisePropertyChanged("image_list");
                }
            }
        }
      

        public ImageListViewModel()
        {
            var imagelist = getImageList();
            image_list = new ObservableCollection<ImageListModel>(imagelist);
        }

        private IList<ImageListModel> getImageList()
        {
            List<ImageListModel> list = new List<ImageListModel>();
            list.Add(new ImageListModel(){ image = AppDomain.CurrentDomain.BaseDirectory + @"Resource\Image\1.jpg", is_show = Visibility.Visible });
            list.Add(new ImageListModel() { image = AppDomain.CurrentDomain.BaseDirectory + @"Resource\Image\2.jpg", is_show = Visibility.Visible });
            list.Add(new ImageListModel() { image = AppDomain.CurrentDomain.BaseDirectory + @"Resource\Image\3.jpg", is_show = Visibility.Visible });
            return list;
        }
    }
}
