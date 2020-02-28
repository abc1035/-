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
    /// Page7.xaml 的交互逻辑
    /// </summary>
    public partial class Page7 : Page
    {
        public Page7(String str,int id,String answer)
        {
            InitializeComponent();
            TextBlock1.Text = "主观题"+(id+1).ToString()+":"+str;
            TextBox1.Text = answer;
        }
    }
}
