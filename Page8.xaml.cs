using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace 在线考试系统
{
    /// <summary>
    /// Page8.xaml 的交互逻辑
    /// </summary>
    public partial class Page8 : Page
    {
        ContentControl PageChange1;
        public Page8(ref ContentControl PageChange1)
        {
            InitializeComponent();
            this.PageChange1 = PageChange1;
            Page2.islogin = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Page1 page1 = new Page1(ref PageChange1);
            PageChange1.Content = new Frame()
            {
                Content = page1
            };
        }
    }
}
