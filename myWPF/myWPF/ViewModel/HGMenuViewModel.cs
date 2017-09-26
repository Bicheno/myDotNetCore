using GalaSoft.MvvmLight;
using myWPF.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace myWPF.ViewModel
{
    public class HGMenuViewModel : ViewModelBase
    {
        public IList<HGMenuModel> MenuList { get; set; }
        public string Claims = "1,2";//用户拥有的菜单

        private ObservableCollection<HGMenuModel> _menuCollection;
        public ObservableCollection<HGMenuModel> MenuCollection
        {
            get { return _menuCollection; }
            set
            {
                if (_menuCollection != value)
                {
                    _menuCollection = value;
                    RaisePropertyChanged("MenuCollection");
                }
            }
        }

        public HGMenuViewModel()
        {
            MenuList = GetNodeList();
            var menuList = GetNavItems();
            MenuCollection = new ObservableCollection<HGMenuModel>(menuList);
        }

        public void ProductsList(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("✌  商品");
        }

        public void ProductTypeList(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("✌  商品分类");
        }

        public void OrderList(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("✌  订单");
        }
        public void CustomerList(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("✌  客户列表");
        }

        public void UserManagement(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("✌  用户管理");
        }
        public void RoleManagement(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("✌  角色管理");
        }
            
        #region 私有辅助方法

        /// <summary>
        /// 获取所有节点
        /// </summary>
        /// <returns></returns>
        private IList<HGMenuModel> GetNodeList()
        {
            List<HGMenuModel> list = null;
            try
            {
                var menuFileFullPath = AppDomain.CurrentDomain.BaseDirectory + @"Resource\menu.json";
                var sm = new FileStream(menuFileFullPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                var reader = new StreamReader(sm);
                string menuJson = reader.ReadToEnd();
                list = JsonConvert.DeserializeObject<List<HGMenuModel>>(menuJson).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
            return list;
        }

        /// <summary>
        /// 根据当前用户权限加载菜单集合
        /// </summary>
        /// <returns></returns>
        private IList<HGMenuModel> GetNavItems()
        {
            IList<HGMenuModel> newList = new List<HGMenuModel>();
            IList<HGMenuModel> newList2 = new List<HGMenuModel>();
            var menuList = CreateHierarchyTreeItems("9999", 1, 22, MenuList);

            try
            {
                for (int i = 0; i < menuList.Count; i++)
                {
                    var item = menuList[i];
                    var navItem = new HGMenuModel
                    {
                        id = item.id,
                        cliam_id = item.cliam_id,
                        display_name = item.display_name,
                        icon = item.icon,
                        parent_id = item.parent_id,
                        method = item.method,
                        submenu_list = new List<HGMenuModel>(),
                    };
                    newList2.Add(navItem);
                    //遍历该节点，如果有子菜单就再递归，如果没有，直接加入
                    foreach (var item2 in item.submenu_list)
                    {
                        //没有子菜单
                        if (null == item2.submenu_list || 0 == item2.submenu_list.Count)
                        {
                            //有权限
                            if (Claims.Contains(item2.cliam_id))
                            {
                                newList2[i].submenu_list.Add(item2);
                            }
                        }
                        else
                        {
                            var list = new List<HGMenuModel>();//用于保存子菜单
                            //有子菜单
                            foreach (HGMenuModel nag in item2.submenu_list)
                            {
                                //有权限
                                if (Claims == nag.cliam_id)
                                {
                                    list.Add(nag);
                                }
                            }
                            //如果子菜单至少有1个
                            if (0 < list.Count)
                            {
                                item2.submenu_list = list;
                                newList2[i].submenu_list.Add(item2);
                            }
                        }
                    }
                }

                foreach (var item in newList2)
                {
                    if (item.submenu_list.Count > 0)
                    {
                        newList.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return newList;
        }

        /// <summary>
        /// 生成树状层次的菜单数据
        /// </summary>
        /// <param name="parentId">父级ID</param>
        /// <param name="minDepth"></param>
        /// <param name="maxDepth"></param>
        /// <returns></returns>
        private IList<HGMenuModel> CreateHierarchyTreeItems(string parentId, int minDepth, int maxDepth, IList<HGMenuModel> nodeList)
        {
            IList<HGMenuModel> result = new List<HGMenuModel>();
            var rootNodeList = nodeList.Where(a => a.parent_id == parentId);//父级的菜单

            foreach (var item in rootNodeList)
            {
                var navItem = new HGMenuModel
                {
                    id = item.id,
                    cliam_id = item.cliam_id,
                    display_name = item.display_name,
                    icon = item.icon,
                    parent_id = item.parent_id,
                    method = item.method,
                    submenu_list = CreateHierarchyTreeItems(item.id, minDepth, maxDepth, nodeList),
                };
                result.Add(navItem);
            }

            return result;
        }

        #endregion

    }
}
