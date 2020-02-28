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
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        bool islogin;//0表示未登陆,1表示已登陆
        public MainWindow()
        {
            InitializeComponent();
            islogin = false;
            //init();
        }
       /* void init()
        {
            Page1 page1 = new Page1();
            MainContent.Content  = new Frame();
            {
                Content = page1;
            };
        }*/
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Class1 a = new Class1();
            if (a.getpname())
            {
                Console.WriteLine("OK");
            }
            else
            {
                Window1 window1 = new Window1();
                window1.ShowDialog();
                Console.WriteLine("NO");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window3 window3 = new Window3();
            this.Close();
            window3.ShowDialog();
        }
    }
}
