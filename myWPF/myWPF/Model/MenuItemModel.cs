using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace myWPF.Model
{
    public class MenuItemModel
    {

        /// <summary>
        /// ID，唯一标识
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 权限ID
        /// </summary>
        public string ClaimId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 将在其中查找名为 typeName 的类型的程序集的名称。 有关更多信息，请参见“备注”一节。 如果 assemblyName 为 null，则搜索正在执行的程序集。 
        /// </summary>
        public string AssemlyName { get; set; }

        /// <summary>
        /// 对象的类型名称
        /// </summary>
        public string ObjectTypeName { get; set; }

        /// <summary>
        /// 对象的Prism的Uri
        /// </summary>
        public string ObjectPrismUri { get; set; }


        public int? Depth { get; set; }

        /// <summary>
        /// 图示路径
        /// </summary>
        public string IconPath { get; set; }

        public IList<MenuItemModel> SubList { get; set; }

        private bool? isNew;
    }
}
