using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace myWPF.Model
{
    public class ItemListModel : ModelBase
    {
        private string _item_id;
        public string item_id
        {
            get { return _item_id; }
            set
            {
                if (_item_id != value)
                {
                    _item_id = value;
                    RaisePropertyChanged("item_id");
                }
            }
        }

        private string _item_name;
        public string item_name
        {
            get { return _item_name; }
            set
            {
                if (_item_name != value)
                {
                    _item_name = value;
                    RaisePropertyChanged("item_name");
                }
            }
        }

        private string _val_select;
        /// <summary>
        /// 选中的属性值
        /// </summary>
        public string val_select
        {
            get { return _val_select; }
            set
            {
                if (_val_select != value)
                {
                    _val_select = value;
                    RaisePropertyChanged("val_select");
                }
            }
        }

        private List<ItemListModel> _item_child;
        public List<ItemListModel> item_child
        {
            get { return _item_child; }
            set
            {
                if (_item_child != value)
                {
                    _item_child = value;
                    RaisePropertyChanged("item_child");
                }
            }
        }
    }
}
