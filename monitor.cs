using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;

namespace 在线考试系统
{
    class Monitor 
    {
        public static Thread thread1;
        public static Thread thread2;
        public static void monitorsystem()
        {
            int sleeptime = 10000;
            while (true)
            {
                if (Page2.islogin == 1)
                {
                    Thread.Sleep(sleeptime);
                    if (Window3.exit == 0)
                    {
                        monitoring();
                    }
                    else
                    {
                        Monitor.thread1.Abort();
                    }
                }
            }
        }
        public static void monitoring()
        {
            Class1 class1 = new Class1();
            String str = "您当前存在不合法软件或有不合法操作，现已上报管理员，请立即整改，如有误报，请联系管理员";
            if (class1.getpname()==false)
            {
                MessageBox.Show(str, "在线考试系统的提示");
                int vionumber = class1.getstudentvionumber(Page1.Studentid);
                str = "您当前共有" + (vionumber+1).ToString() + "次违规";
                MessageBox.Show(str, "在线考试系统的提示");
                class1.updatestudentvionumber(Page1.Studentid, (vionumber + 1).ToString());
            }
        }
        public static void defend()
        {
            int sleeptime = 2000;
            String Text = "";
            while(true)
            {
                try
                {
                    Clipboard.SetText(Text);
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message,"在线考试系统提示");
                    Thread.Sleep(sleeptime*10);
                }
                Thread.Sleep(sleeptime);
            }
        }
        public static void fun()
        {
            ThreadStart child1 = new ThreadStart(monitorsystem);
            ThreadStart child2 = new ThreadStart(defend);
            thread1 = new Thread(child1);
            thread2 = new Thread(child2);
            thread1.Start();
            thread2.TrySetApartmentState(ApartmentState.STA);
            thread2.Start();
        }
    }
}
