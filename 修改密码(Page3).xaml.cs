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
    /// Page3.xaml 的交互逻辑
    /// </summary>
    public partial class Page3 : Page
    {
        ContentControl PageChange1;
        ScrollingTextControl now;
        public Page3(ref ContentControl PageChange1, ref ScrollingTextControl now)
        {
            InitializeComponent();
            this.PageChange1 = PageChange1;
            this.now = now;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(PasswordBox2.Password!=PasswordBox3.Password)
            {
                Window2 window2 = new Window2("请确保两次输入密码一致");
                window2.ShowDialog();
                return ;
            }
            Class1 a = new Class1();
            if(a.updatestudentpassword(TextBox1.Text,PasswordBox1.Password,PasswordBox2.Password))
            {
                Window2 window2 = new Window2("密码修改成功");
                window2.ShowDialog();
                Page1 page1 = new Page1(ref PageChange1,ref now);
                PageChange1.Content = new Frame()
                {
                    Content = page1
                };
            }
            else
            {
                Window2 window2 = new Window2("您输入的原账号密码错误");
                window2.ShowDialog();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Page1 page1 = new Page1(ref PageChange1,ref now);
            PageChange1.Content = new Frame()
            {
                Content = page1
            };
        }
    }
}
