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
using System.Timers;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace 在线考试系统
{
    /// <summary>
    /// Window3.xaml 的交互逻辑
    /// </summary>
    public partial class Window3 : Window
    {
        public static int exit = 0;
        public Window3()
        {
            InitializeComponent();
            init();
        }

        void init()
        {
            Monitor.fun();
            Page1 page1 = new Page1(ref PageChange1,ref ScrollingTextControl1);
            PageChange1.Content = new Frame()
            {
                Content = page1
            };
        }

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
            Page5.submit();
            Monitor.thread1.Abort();
            Monitor.thread2.Abort();
            Window1 window1 = new Window1(1);
            window1.ShowDialog();
            if(Window1.state==1)
            {
                exit = 1;
                this.Close();
            }
            else
            {

            }
        }
    }
}
