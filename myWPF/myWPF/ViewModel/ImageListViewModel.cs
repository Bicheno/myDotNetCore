using GalaSoft.MvvmLight;
using myWPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

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

        public void AddImg(object sender, RoutedEventArgs e)
        {
            if(image_list.Count == 5)
            {
                MessageBox.Show("最多只能绑定5张图片！");
                return;
            }

            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Title = "选择文件";
            openFileDialog.Filter = "所有图片|*.jpg;*.jpeg;*.png;*.gif";
            openFileDialog.FileName = string.Empty;
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.ValidateNames = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.CheckFileExists = true;
            bool? result = openFileDialog.ShowDialog();
            if (result == null || result.Value == false) return;
            string fileName = openFileDialog.FileName;
            if (new FileInfo(fileName).Length > 5 * 1024 * 1024)
            {
                MessageBox.Show("图片不得大于5M！");
                return;
            }
            image_list.Add(new ImageListModel() { id= Guid.NewGuid().ToString(), image = fileName, is_old = false });
        }

        public void DeleteImg(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null && button.Tag != null)
            {
                var parent = (StackPanel)button.Parent;
                parent.Visibility = Visibility.Collapsed;

                var delitem = image_list.FirstOrDefault(i => i.id == button.Tag.ToString());
                image_list.Remove(delitem);
            }
        }
        public void btnComfirmAddImg(object sender, RoutedEventArgs e)
        {
            if (image_list.Count < 1)
            {
                MessageBox.Show("请先添加图片~");
                return;
            } 
            var newlist = image_list.Where(i => i.is_old == false);
            var item = newlist.Count();
            if (item > 0)
            {
                //todo  这里就是调添加图片接口 把newlist传过去就好
                


                MessageBox.Show("共添加" + item + "图片");
                if (image_list.Count < 0) return;
                foreach (var i in image_list)
                {
                    i.is_old = true;
                }
            }
            else
            {
                MessageBox.Show("这些图片都已经添加了哦");
                return;
            }
        }

        private IList<ImageListModel> getImageList()
        {
            List<ImageListModel> list = new List<ImageListModel>();
            //todo  这里就是获取接口返回的model（图片列表）
                      //list.Add(new ImageListModel(){ id="1", image = AppDomain.CurrentDomain.BaseDirectory + @"Resource\Image\1.jpg", is_old = true });
            return list;
        }
    }
}
