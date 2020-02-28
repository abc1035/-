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
    /// Page1.xaml 的交互逻辑
    /// </summary>
    public partial class Page1 : Page
    {
        public static String Studentid;
        ContentControl PageChange1;
        ScrollingTextControl now;
        public Page1(ref ContentControl PageChange1,ref ScrollingTextControl now)
        {
            InitializeComponent();
            this.PageChange1 = PageChange1;
            this.now = now;
        }
        public Page1(ref ContentControl PageChange1)
        {
            InitializeComponent();
            this.PageChange1 = PageChange1;
        }
        String Password;
        private Label label1;

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Page3 page3 = new Page3(ref PageChange1,ref now);
            PageChange1.Content = new Frame()
            {
                Content = page3
            };
        }
        public bool judgestate()
        {
            Class1 a = new Class1();
            int state = a.getstudentexitstate(Studentid);
            if(state==-1)
            {
                a.updatestudentexitstate(Studentid, "0");
                return false;
            }
            else if(state==0)
            {
                String str="您之前存在非法关闭应用程序情况，现在已被限制登录，请您联系考场管理员";
                MessageBox.Show(str, "在线考试系统提示");
                return true;
            }
            else if(state==1)
            {
                a.updatestudentexitstate(Studentid, "0");
                return false;
            }
            else
            {
                MessageBox.Show("未知异常", "在线考试系统提示");
                return true;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Studentid = TextBox1.Text;
            Password = PasswordBox1.Password;
            Class1 class1 = new Class1();
            if(class1.studentlogin(Studentid, Password)==true)
            {
                String str= "您已经进入检测状态，请不要打开使用任何浏览器及通讯软件，也不要试图做其他非法行动，我们会上报您的违规行为";
                MessageBox.Show(str, "在线考试系统提示");
                Page2 page2 = new Page2(ref PageChange1,Studentid);
                PageChange1.Content = new Frame()
                {
                    Content = page2
                };
            }
            else
            {
                Window2 window2 = new Window2("账号密码错误，请重新登录");
                window2.ShowDialog();
            }
        }
    }
}
