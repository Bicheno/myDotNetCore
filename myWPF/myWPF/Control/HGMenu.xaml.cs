using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace myWPF.Control
{
    /// <summary>
    /// HGMenu.xaml 的交互逻辑
    /// </summary>
    public partial class HGMenu : UserControl
    {
        public HGMenu()
        {
            InitializeComponent();
        }

        private void Expand(object sender, RoutedEventArgs e)
        {
            if (grid.Width == 50)
            {
                grid.Width = 200;
            }
            else
            {
                grid.Width = 50;
            }
        }

        private void rbProductsManagement_MouseMove(object sender, MouseEventArgs e)
        {

        }
    }
}
