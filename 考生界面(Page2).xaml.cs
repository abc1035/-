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
using MySql.Data.MySqlClient;
using System.Collections;

namespace 在线考试系统
{
    /// <summary>
    /// Page2.xaml 的交互逻辑
    /// </summary>
    public partial class Page2 : Page
    {
        public static int islogin=0;
        ContentControl PageChange1;
        String Studentid;
        int index;
        int cnt;
        Hashtable name_index = new Hashtable();
        Hashtable id_index = new Hashtable();
        private String[] testid = new String[50];
        private String[] testname = new String[50];
        private int[] chosennum = new int[50];
        private int[] subjectnum = new int[50];
        private int[] time = new int[50];
        private int[] state = new int[50];
        public Page2(ref ContentControl PageChange1,String Studentid)
        {
            InitializeComponent();
            islogin = 1;
            this.PageChange1 = PageChange1;
            this.Studentid = Studentid;
            index = 1;
            init();
            show();
            Label1.Content = "你好考生，当前共有" + cnt.ToString() + "场考试";
        }
        public void show()
        {
            TextBox2.Text = index.ToString() + "/" + cnt.ToString() + "页";
            Page4 page4 = new Page4(testid[index], testname[index], chosennum[index], subjectnum[index], time[index], state[index]);
            PageChange2.Content = new Frame()
            {
                Content = page4
            };
        }
        public void init()
        {
            Class1 a = new Class1();
            MySqlDataReader res = a.gettestinfo();
            cnt = 0;
            while(res.Read())
            {
                try
                {
                    testid[++cnt] = res[0].ToString();
                    id_index.Add(testid[cnt], cnt);
                    testname[cnt] = res[1].ToString();
                    name_index.Add(testname[cnt], cnt);
                    chosennum[cnt] = int.Parse(res[2].ToString());
                    subjectnum[cnt] = int.Parse(res[3].ToString());
                    time[cnt] = int.Parse(res[4].ToString());
                    state[cnt] = int.Parse(res[5].ToString());
                }catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            res.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(index==1)
            {
                Window2 window2 = new Window2("这已经是第一页");
                window2.ShowDialog();
            }
            else
            {
                index--;
                show();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (index == cnt)
            {
                Window2 window2 = new Window2("这已经是最后一页");
                window2.ShowDialog();
            }
            else
            {
                index++;
                show();
            }
        }
        public int gettime()
        {
            Class1 a = new Class1();
            String now = a.getcurrenttime();
            Console.WriteLine(now);
            String begin = a.getbegintime(Studentid,testid[index],now);
            if(begin==null)
            {
                //a.updatestudenttestbegintime(begin);
                return time[index] * 60;
            }
            else
            {
                return subtact(begin,now);
            }
        }
        public int subtact(String begin,String now)
        {
            Console.WriteLine(begin);
            Console.WriteLine(now);
            begin = begin.Replace('/', ':');
            begin = begin.Replace(' ', ':');
            now = now.Replace('/', ':');
            now = now.Replace(' ', ':');
            String[] Btime = begin.Split(':');
            String[] Ntime = now.Split(':');
            int bday = int.Parse(Btime[2]);
            int nday = int.Parse(Ntime[2]);
            int bhour = int.Parse(Btime[3]);
            int nhour= int.Parse(Ntime[3]);
            int bminute= int.Parse(Btime[4]);
            int nminute=int.Parse(Ntime[4]);
            int bsecond=int.Parse(Btime[5]);
            int nsecond=int.Parse(Ntime[5]);
            int btime = bsecond + bminute * 60 + bhour * 3600 + bday * 3600 * 24;
            int ntime = nsecond + nminute * 60 + nhour * 3600 + nday * 3600 * 24;
            return time[index]*60-ntime +btime>0? time[index]*60-ntime +btime:0;

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if(state[index]==0)
            {
                Window2 window2 = new Window2("当前考试暂未开始");
                window2.ShowDialog();
            }
            else if (state[index] == 1)
            {
                Window2 window2 = new Window2("请再次确认您以关闭全部非法软件",24);
                window2.ShowDialog();
                int Time = gettime();
                Class1 a = new Class1();
                String state = a.getstudenttestsubmit(Studentid, testid[index]);
                if(state!=null)
                {
                    window2 = new Window2("您以交卷，无法考试",34);
                    window2.ShowDialog();
                    return;
                }
                if (Time<=0)
                {
                    window2 = new Window2("您的考试时间已经用完");
                    window2.ShowDialog();
                    return ;
                }
                //MessageBox.Show("fuck1");
                Page5 page5 = new Page5(testid[index],Time,testname[index],Studentid,chosennum[index],subjectnum[index],ref PageChange1);
                PageChange1.Content = new Frame()
                {
                    Content = page5
                };
            }
            else if (state[index] == 2)
            {
                Window2 window2 = new Window2("当前考试已结束");
                window2.ShowDialog();
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            String[] str = TextBox2.Text.Split('/');
            try
            {
                int temp = int.Parse(str[0]);
                if(temp>=1&&temp<=cnt)
                {
                    index = temp;
                }
                else
                {
                    Window2 window2 = new Window2("请输入1到"+cnt.ToString()+"页数范围");
                    window2.ShowDialog();
                }
                show();
            }
            catch(Exception ee)
            {
                Window2 window2 = new Window2("请输入合法数字");
                window2.ShowDialog();
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            String str = TextBox1.Text;
            Class1 a = new Class1();
            ListView1.Items.Clear();
            if(a.isdigit(str)==true)
            {
                for (int i = 1; i <= cnt; i++)
                {
                    String name = testid[i];
                    for (int j = 0; j < name.Length; j++)
                    {
                        if (j + str.Length - 1 < name.Length && name.Substring(j, str.Length).Equals(str))
                        {
                            ListView1.Items.Add("考试号 "+name);
                            break;
                        }
                    }
                }
            }
            else
            {
                for (int i = 1; i <= cnt; i++)
                {
                    String name = testname[i];
                    for(int j=0;j<name.Length;j++)
                    {
                        if(j+str.Length-1<name.Length&&name.Substring(j,str.Length).Equals(str))
                        {
                            ListView1.Items.Add("考试名 "+name);
                            break;
                        }
                    }
                }
            }
            if(ListView1.Items.IsEmpty==true)
            {
                Window2 window2 = new Window2("无查询结果");
                window2.ShowDialog();
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            String str;
            try
            {
                str = ListView1.SelectedItem.ToString().Split(' ')[1];
            }
            catch(Exception eee)
            {
                Window2 window2 = new Window2("请先选中结果");
                window2.ShowDialog();
                return ;
            }
            Class1 a = new Class1();
            if(a.isdigit(str)==true)
            {
                index = int.Parse(id_index[str].ToString());
            }
            else
            {
                index = int.Parse(name_index[str].ToString());
            }
            show();
        }
    }
}
