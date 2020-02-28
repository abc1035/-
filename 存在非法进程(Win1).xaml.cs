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
using System.Windows.Shapes;

namespace 在线考试系统
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        int judge = 0;
        public static int state=0;
        public Window1()
        {
            InitializeComponent();
        }
        public Window1(int judge)
        {
            InitializeComponent();
            this.judge = judge;
            init();
        }
        public void init()
        {
            if(judge==1)
            {
                Label1.Content = "请确认是否交卷,退出后将默认交卷";
                Label1.FontSize = 24;
                Button1.Content = "退出";
                Button2.Content = "取消";
            }
            else if(judge==2)
            {
                Label1.Content = "请确认是否交卷,交卷后无法再次进行考试";
                Label1.FontSize = 24;
                Button1.Content = "交卷";
                Button2.Content = "取消";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (judge == 1)
            {
                state = 1;
                this.Close();
            }
            else if (judge == 0)
            {
                Class1 a = new Class1();
                a.killthread();
                this.Close();
                Window2 window2 = new Window2("关闭成功");
                window2.ShowDialog();
            }
            else if(judge==2)
            {
                state = 2;
                this.Close();
            }

        }
    }
}
