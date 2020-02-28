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
using System.Timers;
using MySql.Data.MySqlClient;

namespace 在线考试系统
{
    /// <summary>
    /// Page5.xaml 的交互逻辑
    /// </summary>
    public partial class Page5 : Page
    {
        static String testid;
        String testname;
        Page6 page6;
        Page7 page7;
        String Chosenid;
        String Subid;
        int Chosenindex;
        int chosennum, subnum;
        static String Studentid;
        int testtime;
        int index;
        int ccnt;
        int scnt;
        int tcnt;
        String[] cid = new String[150];
        String[] chosenarray = new String[150];
        String[] subarray = new String[150];
        int[] flag = new int[150];
        String[] cquestion = new String[150];
        String[] chosena = new String[150];
        String[] chosenb = new String[150];
        String[] chosenc = new String[150];
        String[] chosend = new String[150];
        String[] sid = new String[150];
        String[] squestion = new String[150];
        ContentControl PageChange1;
        public Page5(String Testid,int testtime,String testname,String studentid,int chosennum, int subnum,ref ContentControl PageChange1)
        {
            InitializeComponent();
            testid = Testid;
            this.testtime = testtime;
            this.testname = testname;
            Studentid = studentid;
            this.chosennum = chosennum;
            this.subnum = subnum;
            this.PageChange1 = PageChange1;
            timeinit();
            choseninit();
            subinit();
            Console.WriteLine("fuck1");
            Label2.Content = testname;
            stuchoseninit();
            Console.WriteLine("fuck1.5");
            stusubinit();
            Chosenindex = 0;
            Console.WriteLine("fuck2");
            index = getChosenindex();
            tcnt = ccnt + scnt;
            indexinit();
            pageinit();
            Console.WriteLine("fuck3");
            countdown();
        }
        public void indexinit()
        {
            TextBox1.Text = "1/" + tcnt.ToString();
        }
        public String getChosenid()
        {
            Random ran = new Random();
            String str = "";
            for(int i=1;i<=chosennum;i++)
            {
                while(true)
                {
                    int temp = ran.Next(1, ccnt+1);
                    bool judge = true;
                    for(int j=0;j< str.Length;j++)
                    {
                        if(temp== str[j]-'0')
                        {
                            judge = false;
                            break;
                        }
                    }
                    if(judge)
                    {
                        str += cid[temp]+"a";
                        break;
                    }
                }
            }
            return str;
        }
        public String getsubid()
        {
            Random ran = new Random();
            String str = "";
            int shit = 100;
            Console.WriteLine(subnum.ToString());
            for (int i = 1; i <= subnum; i++)
            {

                while (true)
                {
                    int temp = ran.Next(1, scnt+1);
                    if(--shit>0)Console.WriteLine(temp.ToString());
                    bool judge = true;
                    for (int j = 0; j < str.Length; j++)
                    {
                        if (temp == str[j] - '0')
                        {
                            judge = false;
                            break;
                        }
                    }
                    if (judge)
                    {
                        str += temp.ToString() + "a";
                        break;
                    }
                }
            }
            return str;
        }
        public void getchosenarray()
        {
            chosenarray = Chosenid.Split('a');
        }
        public void getsubarray()
        {
            subarray = Subid.Split('a');
            for(int i=0;i<subarray.Length;i++)
            {
                Console.WriteLine(subarray[i]);
            }
        }
        public void stuchoseninit()
        {
            Class1 a = new Class1();
            Chosenid = a.getstudenttestchosenid(Studentid, testid);
            if(Chosenid==null)
            {
                Chosenid = getChosenid();
                a.updatestudenttestChosenid(Studentid, testid,Chosenid);
            }
            getchosenarray();
        }
        public void stusubinit()
        {
            Class1 a = new Class1();
            Subid = a.getstudenttestsubid(Studentid, testid);
            Console.WriteLine("shit1");
            if (Subid == null)
            {
                Console.WriteLine("shit1.5");
                Subid = getsubid();
                Console.WriteLine("shit2");
                a.updatestudenttestsubid(Studentid, testid, Subid);
                Console.WriteLine("shit3");
            }
            getsubarray();
            Console.WriteLine("shit4");
        }
        public void pageinit()
        {
            page6 = new Page6(cid[index], flag[index], cquestion[index], chosena[index], chosenb[index], chosenc[index], chosend[index],Chosenindex+1,Studentid,testid);
            PageChange2.Content = new Frame()
            {
                Content = page6
            };
        }
        public void choseninit()
        {
            ccnt = 0;
            Class1 a = new Class1();
            MySqlDataReader res= a.getchosenquestion();
            while(res.Read())
            {
                cid[++ccnt] = res[0].ToString();
                flag[ccnt] = int.Parse(res[1].ToString());
                cquestion[ccnt] = res[2].ToString();
                chosena[ccnt] = res[3].ToString();
                chosenb[ccnt] = res[4].ToString();
                chosenc[ccnt] = res[5].ToString();
                chosend[ccnt] = res[6].ToString();
            }
        }
        public void subinit()
        {
            scnt = 0;
            Class1 a = new Class1();
            MySqlDataReader res = a.getsubquestion();
            while (res.Read())
            {
                sid[++scnt] = res[0].ToString();
                squestion[scnt] = res[1].ToString();
            }
            Console.WriteLine("scnt"+scnt.ToString());
        }
        public void timeinit()
        {
            if(testtime==0)
            {
                Window2 window2 = new Window2("考试结束");
                window2.ShowDialog();
            }
            else
            {
                String second = (testtime%60).ToString();
                if(second.Length==1)
                {
                    second = '0' + second;
                }
                testtime /= 60;
                String minute = (testtime % 60).ToString();
                String hour = (testtime / 60).ToString();
                if(minute.Length==1)
                {
                    minute = "0" + minute;
                }
                if(hour.Length==1)
                {
                    hour = "0" + hour;
                }
                Label1.Content = hour + ":" + minute + ":" + second;
            }
            
        }
        public void countdown()
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Enabled = true;
            timer.Interval = 1000; //执行间隔时间,单位为毫秒; 这里实际间隔为10分钟  
            timer.Start();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(coundowndo);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            saveanswer();
            if (Chosenindex == chosennum + subnum - 1)
            {
                Window2 window2 = new Window2("这已经是最后题");
                window2.ShowDialog();
            }
            else if (Chosenindex>=chosennum-1)
            {
                Chosenindex++;
                index = getsubindex();
                page7init();

            }
            else
            {
                Chosenindex++;
                index = getChosenindex();
                pageinit();
            }
        }
        public void savechosenanswer()
        {
            bool[] judge = new bool[10];
            page6.fuck(judge);
            String answer="";
            if (judge[1] == true) answer += "A";
            if (judge[2] == true) answer += "B";
            if (judge[3] == true) answer += "C";
            if (judge[4] == true) answer += "D";
            Class1 a = new Class1();
            a.savestudenttestchosenanswer(Studentid, testid, getChosenindex().ToString(), answer);
        }
        public void savesubanswer()
        {
            Class1 a = new Class1();
            a.savestudenttestsubanswer(Studentid, testid, getsubindex().ToString(),page7.TextBox1.Text);
        }
        public void saveanswer()
        {
            if(Chosenindex>=0&&Chosenindex<=chosennum-1)
            {
                savechosenanswer();
            }
            else
            {
                savesubanswer();
            }
        }
        public void page7init()
        {
            Class1 a = new Class1();
            String answer = a.getstudenttestsubanswer(Studentid,testid, getsubindex().ToString());
            page7 = new Page7(squestion[index], Chosenindex %chosennum,answer);
            PageChange2.Content = new Frame()
            {
                Content = page7
            };
        }
        public int getChosenindex()
        {
            return int.Parse(chosenarray[Chosenindex]);
        }
        public int getsubindex()
        {
            //Console.WriteLine("Chosenindex%ccnt:" + (Chosenindex % ccnt).ToString());
            //Console.WriteLine("subarray[Chosenindex%ccnt]:" + subarray[Chosenindex % ccnt]);
            return int.Parse(subarray[Chosenindex%chosennum]);
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            saveanswer();
            try
            {
                int id = int.Parse(TextBox1.Text.Split('/')[0]);
                if(id<=0||id>tcnt)
                {
                    Window2 window2 = new Window2("请输入合法的题号范围");
                    window2.ShowDialog();
                    TextBox1.Text = (Chosenindex+1).ToString() + "/" + tcnt.ToString();
                }
                else
                {
                    Chosenindex = id-1;
                    if (Chosenindex > chosennum - 1)
                    {
                        index = getsubindex();
                        page7init();
                    }
                    else
                    {
                        index = getChosenindex();
                        pageinit();
                    }
                    TextBox1.Text = id.ToString() + "/" + tcnt.ToString();
                }
            }
            catch(Exception e1)
            {
                Window2 window2 = new Window2("请输入合法数字");
                window2.ShowDialog();
                TextBox1.Text = (Chosenindex + 1).ToString() + "/" + tcnt.ToString();
            }
            
        }
        public static void submit()
        {
            Class1 a = new Class1();
            a.updatestudenttestsubmit(Studentid, testid, "0");
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1(2);
            window1.ShowDialog();
            if(Window1.state==2)
            {
                Class1 a = new Class1();
                a.updatestudenttestsubmit(Studentid, testid, "0");
                Page8 page8 = new Page8(ref PageChange1);
                PageChange1.Content = new Frame()
                {
                    Content = page8
                };
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            saveanswer();
            if (Chosenindex == 0)
            {
                Window2 window2 = new Window2("这已经是第一题");
                window2.ShowDialog();
            }
            else if (Chosenindex > chosennum)
            {
                Chosenindex--;
                index = getsubindex();
                page7init();
            }
            else
            {
                Chosenindex--;
                index = getChosenindex();
                pageinit();
            }
        }
        public void coundowndo(object source, ElapsedEventArgs e)
        {
            this.Label1.Dispatcher.Invoke(
            new Action(
                delegate
                {
                    String time = Label1.Content.ToString();
                    String[] t = time.Split(':');
                    String hour = t[0];
                    String minute = t[1];
                    String second = t[2];
                    if (second.Equals("00"))
                    {
                        if (minute.Equals("00"))
                        {
                            if (hour.Equals("00"))
                            {
                                Window2 window2 = new Window2("考试结束");
                                window2.ShowDialog();
                            }
                            else
                            {
                                hour = (int.Parse(hour) - 1).ToString();
                                if (hour.Length == 1)
                                {
                                    hour = "0" + hour;
                                }
                                second = "59";
                                minute = "59";
                            }
                        }
                        else
                        {
                            minute = (int.Parse(minute) - 1).ToString();
                            if (minute.Length == 1)
                            {
                                minute = "0" + minute;
                            }
                            second = "59";
                        }
                    }
                    else
                    {
                        second = (int.Parse(second) - 1).ToString();
                        if (second.Length == 1)
                        {
                            second = "0" + second;
                        }
                    }
                    Label1.Content = hour + ":" + minute + ":" + second;
                }
                ));
        }
    }
}
