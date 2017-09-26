using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace myWPF.Model
{
    public class HGMenuModel : ModelBase
    {
        /// <summary>
        /// ID，唯一标识
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string display_name { get; set; }

        /// <summary>
        /// 授权菜单ID
        /// </summary>
        public string cliam_id { get; set; }

        /// <summary>
        /// icon
        /// </summary>
        public string icon { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public string parent_id{ get; set; }

        /// <summary>
        /// 绑定按钮方法
        /// </summary>
        public string method { get; set; }

        /// <summary>
        /// 深度
        /// </summary>
        public int? depth { get; set; }


        /// <summary>
        /// 子菜单
        /// </summary>
        public IList<HGMenuModel> submenu_list { get; set; }
    }
}
