using GalaSoft.MvvmLight;
using myWPF.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace myWPF.ViewModel
{
    public class MenuViewModel : ViewModelBase
    {
        public IList<MenuItemModel> MenuList { get; set; }
        public string Claims = "1";

        private ObservableCollection<MenuItemModel> _menuCollection;
        public ObservableCollection<MenuItemModel> MenuCollection
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

        public MenuViewModel()
        {
            MenuList = GetNodeList();

            var menuList = GetNavItems();
            MenuCollection = new ObservableCollection<MenuItemModel>(menuList);
        }

        #region 私有辅助方法

        /// <summary>
        /// 获取所有节点
        /// </summary>
        /// <returns></returns>
        private IList<MenuItemModel> GetNodeList()
        {
            List<MenuItemModel> list = null;
            try
            {
                var menuFileFullPath = AppDomain.CurrentDomain.BaseDirectory + @"Resource\menu.json";
                var sm = new FileStream(menuFileFullPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                var reader = new StreamReader(sm);
                string menuJson = reader.ReadToEnd();
                list = JsonConvert.DeserializeObject<List<MenuItemModel>>(menuJson).ToList();
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
        private IList<MenuItemModel> GetNavItems()
        {
            IList<MenuItemModel> newList = new List<MenuItemModel>();
            IList<MenuItemModel> newList2 = new List<MenuItemModel>();
            var menuList = CreateHierarchyTreeItems(null, 1, 22, MenuList);

            try
            {
                for (int i = 0; i < menuList.Count; i++)
                {
                    var item = menuList[i];
                    var navItem = new MenuItemModel
                    {
                        Id = item.Id,
                        ClaimId = item.ClaimId,
                        Name = item.Name,
                        DisplayName = item.DisplayName,
                        AssemlyName = item.AssemlyName,
                        IsEnabled = item.IsEnabled,
                        ObjectTypeName = item.ObjectTypeName,
                        ParentId = item.ParentId,
                        Remark = item.Remark,
                        ObjectPrismUri = item.ObjectPrismUri,
                        IconPath = item.IconPath,
                        Depth = item.Depth,
                        SubList = new List<MenuItemModel>(),
                        //IsNew = item.IsNew
                    };
                    newList2.Add(navItem);
                    //遍历该节点，如果有子菜单就再递归，如果没有，直接加入
                    foreach (var item2 in item.SubList)
                    {
                        //没有子菜单
                        if (null == item2.SubList || 0 == item2.SubList.Count)
                        {
                            //有权限
                            if (Claims == item2.ClaimId)
                            {
                                newList2[i].SubList.Add(item2);
                            }
                        }
                        else
                        {
                            var list = new List<MenuItemModel>();//保存子菜单
                                                                 //有子菜单
                            foreach (MenuItemModel nag in item2.SubList)
                            {
                                //有权限
                                if (Claims == nag.ClaimId)
                                {
                                    list.Add(nag);
                                }
                            }
                            //如果子菜单至少有1个
                            if (0 < list.Count)
                            {
                                item2.SubList = list;
                                newList2[i].SubList.Add(item2);
                            }
                        }
                    }
                }

                foreach (var item in newList2)
                {
                    if (item.SubList.Count > 0)
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
        private IList<MenuItemModel> CreateHierarchyTreeItems(string parentId, int minDepth, int maxDepth, IList<MenuItemModel> nodeList)
        {
            IList<MenuItemModel> result = new List<MenuItemModel>();
            var rootNodeList = nodeList.Where(a => a.ParentId == parentId);//最高级的菜单

            //int idx = -9999;
            //var currentParentItem = nodeList.FirstOrDefault(a => a.ParentId == parentId);
            //if (currentParentItem != null)
            //{
            //    idx = currentParentItem.Depth ?? -9999;
            //}

            //if (idx <= maxDepth && idx >= minDepth)
            //{
            foreach (var item in rootNodeList)
            {
                var navItem = new MenuItemModel
                {
                    Id = item.Id,
                    ClaimId = item.ClaimId,
                    Name = item.Name,
                    DisplayName = item.DisplayName,
                    AssemlyName = item.AssemlyName,
                    IsEnabled = item.IsEnabled,
                    ObjectTypeName = item.ObjectTypeName,
                    ParentId = item.ParentId,
                    Remark = item.Remark,
                    ObjectPrismUri = item.ObjectPrismUri,
                    IconPath = item.IconPath,
                    Depth = item.Depth,
                    SubList = CreateHierarchyTreeItems(item.Id, minDepth, maxDepth, nodeList),

                };
                result.Add(navItem);
            }
            //}
            return result;
        }

        #endregion
    }
}
