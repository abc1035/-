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
    /// Page4.xaml 的交互逻辑
    /// </summary>
    public partial class Page4 : Page
    {
        public Page4(String Testid,String Testname,int cnum, int snum,int time ,int state)
        {
            InitializeComponent();
            Label1.Content = "考试号：" + Testid;
            Label2.Content = "考试名称：" + Testname;
            Label3.Content = "选择题数：" + cnum;
            Label4.Content = "主观题数：" + snum;
            Label5.Content = "考试时间：" + time+"分钟";
            Label6.Content = "考试状态：";
            if(state==0)
            {
                Label6.Content += "未开始";
            }
            else if (state == 1)
            {
                Label6.Content += "正在进行";
            }
            else if (state == 2)
            {
                Label6.Content += "已结束";
            }
        }
    }
}
