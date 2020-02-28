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

namespace 在线考试系统
{
    /// <summary>
    /// Page6.xaml 的交互逻辑
    /// </summary>
    public partial class Page6 : Page
    {
        String cid;
        int flag;
        String cquestion;
        String[] chosen = new String[10];//最多有9个选项;
        int num;//纪录选项个数（4)
        public int[] order = new int[10];//顺序
        String chosena;
        String chosenb;
        String chosenc;
        String chosend;
        String Studentid;
        String Testid;
        public bool[] judge=new bool[10];

        private void CheckBox2_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void CheckBox4_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void CheckBox3_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void CheckBox1_Checked(object sender, RoutedEventArgs e)
        {
            
        }
        public void fuck(bool[] shit)
        {
            int[] hashorder = new int[10];
            for(int i=1;i<=4;i++)
            {
                hashorder[order[i]] = i;
            }
            shit[hashorder[1]]= CheckBox1.IsChecked.Value;
            shit[hashorder[2]] = CheckBox2.IsChecked.Value;
            shit[hashorder[3]] = CheckBox3.IsChecked.Value;
            shit[hashorder[4]] = CheckBox4.IsChecked.Value;
        }
        int cnt;
        public Page6(String a,int b,String c, String d, String e, String f, String g,int Chosenindex,String Studentid, String Testid)
        {
            InitializeComponent();
            cnt = 0;
            this.num = 4;
            this.cid = a;
            this.flag = b;
            this.cquestion = c;
            this.chosen[1] = d;
            this.chosen[2] = e;
            this.chosen[3] = f;
            this.chosen[4] = g;
            this.Studentid = Studentid;
            this.Testid = Testid;
            getorder();
            textinit(Chosenindex);
            choseinit();
        }
        public void choseinit()
        {
            CheckBox1.IsChecked=false;
            CheckBox2.IsChecked = false;
            CheckBox3.IsChecked = false;
            CheckBox4.IsChecked = false;
            for(int i=1;i<=4;i++)
            {
                judge[i] = false;
            }
            Class1 a = new Class1();
            String answer = a.getstudenttestchosenanswer(Studentid, Testid, cid);
            if (answer == null) return;
            for(int i=0;i<answer.Length;i++)
            {
                int index = answer[i] - 'A' + 1;
                change(order[index]);
            }
        }
        public void change(int index)
        {
            judge[index] = true;
            if (index == 1)CheckBox1.IsChecked = true;
            else if (index == 2) CheckBox2.IsChecked = true;
            else if (index == 3) CheckBox3.IsChecked = true;
            else if (index == 4) CheckBox4.IsChecked = true;
        }
        public void getorder(int cnt)
        {
            while(cnt!=0)
            {
                cnt--;
                int i=0,j=0;
                for(int k=num;k>=2;k--)
                {
                    if(order[k]>order[k-1])
                    {
                        j = k;
                        i = j - 1;
                        break;
                    }
                }
                for(int k=num;k>=2;k--)
                {
                    if(order[k]>order[i])
                    {
                        swap(ref order[k], ref order[i]);
                        break;
                    }
                }
                bool[] vis = new bool[10];
                for (int k = 1; k <= num; k++) vis[k] = false;
                for(int k=j;k<=num;k++)
                {
                    if (vis[k] == true) break;
                    else
                    {
                        swap(ref order[k], ref order[num - (k - j)]);
                        vis[k] = true;
                        vis[num - (k - j)] = true;
                    }
                }
            }
        }
        public void swap(ref int a1,ref int a2)
        {
            int temp = a1;
            a1 = a2;
            a2 = temp;
        }
        public void createorder()
        {
            for(int i=1;i<=num;i++)
            {
                order[i] = i;
            }
            Random ran = new Random();
            int cnt = ran.Next(1, 24);
            getorder(cnt - 1);
        }
        public void getorder()
        {
            Class1 a = new Class1();
            String Order = a.getstudenttestorder(Studentid, Testid, cid);
            if(Order==null)
            {
                Console.WriteLine("fuck1");
                createorder();
                Console.WriteLine("fuck2");
                a.updatestudenttestorder(Studentid, Testid, cid, order,num);
            }
            else
            {
                for(int i=0;i<Order.Length;i++)
                {
                    order[i + 1] = Order[i]-'0';
                }
            }

        }
        public void textinit(int Chosenindex)
        {
            TextBlock1.Text = "选择题" + Chosenindex.ToString() + "." + cquestion;
            TextBlock2.Text = chosen[order[1]];
            TextBlock3.Text = chosen[order[2]];
            TextBlock4.Text = chosen[order[3]];
            TextBlock5.Text = chosen[order[4]];
        }
        
    }
}
