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
    public class ComboBoxListViewModel : ViewModelBase
    {
        private ObservableCollection<ItemListModel> _item_list;
        public ObservableCollection<ItemListModel> item_list
        {
            get { return _item_list; }
            set
            {
                if (_item_list != value)
                {
                    _item_list = value;
                    RaisePropertyChanged("item_list");
                }
            }
        }
        public ComboBoxListViewModel()
        {
            var itemlist = getItemList();
            item_list = new ObservableCollection<ItemListModel>(itemlist);
        }

        public void Confirm(object sender, RoutedEventArgs e)
        {
            var select = item_list.Where(i => i.val_select != null);
            if (select.Count() > 0)
            {
                MessageBox.Show("映射成功");
                return;
            }
        }


        private IList<ItemListModel> getItemList()
        {
            //todo  这里就是获取接口返回的model

            //模拟接口返回数据
            var ban = getChildList1();
            var di = getChildList2();

            List<ItemListModel> list = new List<ItemListModel>();
            list.Add(new ItemListModel() { item_id = "1", item_name = "版本", item_child = new List<ItemListModel>(ban) });
            list.Add(new ItemListModel() { item_id = "2", item_name = "底色", item_child = new List<ItemListModel>(di) });
            return list;
        }

        private List<ItemListModel> getChildList1()
        {
            List<ItemListModel> list = new List<ItemListModel>();
            list.Add(new ItemListModel() { item_id = "1.1", item_name = "青春" });
            list.Add(new ItemListModel() { item_id = "1.2", item_name = "激战" });
            list.Add(new ItemListModel() { item_id = "1.3", item_name = "热血" });
            return list;
        }

        private List<ItemListModel> getChildList2()
        {
            List<ItemListModel> list = new List<ItemListModel>();
            list.Add(new ItemListModel() { item_id = "2.1", item_name = "单色" });
            list.Add(new ItemListModel() { item_id = "2.2", item_name = "双色" });
            list.Add(new ItemListModel() { item_id = "2.3", item_name = "三色" });
            return list;
        }
    }
}
