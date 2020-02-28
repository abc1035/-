using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Timers;
using System.Windows;

namespace 在线考试系统
{
    class Class1
    {
        private MySqlConnection con = null;
        private string ConStr = "server =192.168.0.5;database = DB;uid = abc1035;pwd = 123456abc;pooling=true;port=3306;charset=utf8";
        private bool openconnection()
        {
            if (con == null)
            {
                con = new MySqlConnection(ConStr);
            }
            try
            {
                con.Open();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {

            }
        }
        private void closeconnection()
        {
            con.Close();
            con.Dispose();
        }
        public bool judgepname(String str)
        {
            if (str.Equals("explorer"))
            {
                return true;
            }
            else
            {
                String[] illegal = new String[] { "explorer", "chrome", "browser", "qq", "wechat", "tim" };
                //黑名单
                str = str.ToLower();
                int len = str.Length;
                for (int i = 0; i < len; i++)
                {
                    for (int j = 0; j < illegal.Length; j++)
                    {
                        int L = illegal[j].Length;
                        if (i + L - 1 < len && str.Substring(i, L).Equals(illegal[j]))
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }
        public bool getpname()
        {
            Process[] pro = Process.GetProcesses();
            foreach (var item in pro)
            {
                if (judgepname(item.ProcessName) == false)
                {
                    return false;
                }
            }
            return true;
        }
        public void killthread()
        {
            Process[] pro = Process.GetProcesses();
            foreach (var item in pro)
            {
                if (judgepname(item.ProcessName) == false)
                {
                    item.Kill();
                }
            }
        }
        private MySqlCommand cminit()
        {
            MySqlCommand cm = new MySqlCommand();
            openconnection();
            cm.Connection = con;
            return cm;
        }
        private MySqlCommand cminit(String sql)
        {
            openconnection();
            MySqlCommand cm = new MySqlCommand(sql,con);
            //cm.CommandType = System.Data.CommandType.StoredProcedure;
            return cm;
        }
        
        public bool updatestudentpassword(String Studentid, String Opassword, String Npassword)
        {
            if (!studentlogin(Studentid, Opassword))
            {
                return false;
            }
            MySqlCommand cm = cminit();
            Npassword = "'" + Npassword + "'";
            Studentid = "'" + Studentid + "'";
            cm.CommandText = "update student set password=" + Npassword + "where studentid=" + Studentid;
            if (cm.ExecuteNonQuery() == 1)
            {
                closeconnection();
                return true;
            }
            else
            {
                closeconnection();
                return false;
            }
        }
        public MySqlDataReader gettestinfo()
        {
            MySqlCommand cm = cminit();
            cm.CommandText = "select * from exam";
            try
            {
                MySqlDataReader res = cm.ExecuteReader();
                //closeconnection();
                return res;
            }
            catch (Exception e)
            {
                closeconnection();
                Console.WriteLine(e.Message);
                return null;
            }
            finally
            {

            }
        }
        public bool studentlogin(String Studentid, String Password)
        {
            MySqlCommand cm = cminit();
            Studentid = "'" + Studentid + "'";
            cm.CommandText = "select * from student where studentid=" + Studentid;
            try
            {
                MySqlDataReader res = cm.ExecuteReader();
                String password = "";
                while (res.Read())
                {
                    password = res[1].ToString();
                }
                res.Close();
                closeconnection();
                if (Password == password)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            } 
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            finally
            {

            }
        }
        public bool isdigit(String str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] <= '9' && str[i] >= '0')
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        public void updatestudenttestChosenid(String Studentid, String Testid, String Chosenid)
        {
            //Chosenid = Chosenid.Substring(0, Chosenid.Length - 1);
            Studentid = "'" + Studentid + "'";
            Testid = "'" + Testid + "'";
            Chosenid = "'" + Chosenid + "'";
            Console.WriteLine(Chosenid);
            //cm.CommandText = "update studenttestchosenid set Chosenid=" +Chosenid+ "where studentid=" + Studentid + "and testid=" + Testid;
            String str = "insert into studenttestchosenid values(" + Studentid + "," + Testid + "," + Chosenid + ");";
            MySqlCommand cm = cminit(str);
            //Console.WriteLine(cm.CommandText);
            try
            {
                //Console.WriteLine("fuck"+cm.CommandText);
                cm.ExecuteNonQuery();
                //closeconnection();
                return ;
            }
            catch (Exception e)
            {
                closeconnection();
                Console.WriteLine(e.Message);
                return ;
            }
            finally
            {

            }
        }
        public void updatestudenttestsubmit(String Studentid, String Testid, String state)
        {
            //Chosenid = Chosenid.Substring(0, Chosenid.Length - 1);
            Studentid = "'" + Studentid + "'";
            Testid = "'" + Testid + "'";
            state = "'" + state + "'";
            Console.WriteLine(state);
            //cm.CommandText = "update studenttestchosenid set Chosenid=" +Chosenid+ "where studentid=" + Studentid + "and testid=" + Testid;
            String str = "insert into studentestsubmit values(" + Studentid + "," + Testid + "," + state + ");";
            MySqlCommand cm = cminit(str);
            //Console.WriteLine(cm.CommandText);
            try
            {
                //Console.WriteLine("fuck"+cm.CommandText);
                cm.ExecuteNonQuery();
                //closeconnection();
                return;
            }
            catch (Exception e)
            {
                closeconnection();
                Console.WriteLine(e.Message);
                return;
            }
            finally
            {

            }
        }
        public void updatestudenttestsubid(String Studentid, String Testid, String subid)
        {
            Studentid = "'" + Studentid + "'";
            Testid = "'" + Testid + "'";
            subid = "'" + subid + "'";
            Console.WriteLine(subid);
            String str = "insert into studenttestsubid values(" + Studentid + "," + Testid + "," + subid + ");";
            MySqlCommand cm = cminit(str);
            try
            {
                cm.ExecuteNonQuery();
                return;
            }
            catch (Exception e)
            {
                closeconnection();
                Console.WriteLine(e.Message);
                return;
            }
            finally
            {

            }
        }

        public MySqlDataReader getchosenquestion()
        {
            MySqlCommand cm = cminit();
            cm.CommandText = "select * from chosenquestion";
            try
            {
                MySqlDataReader res = cm.ExecuteReader();
                //closeconnection();
                return res;
            }
            catch (Exception e)
            {
                closeconnection();
                Console.WriteLine(e.Message);
                return null;
            }
            finally
            {

            }
        }
        public MySqlDataReader getsubquestion()
        {
            MySqlCommand cm = cminit();
            cm.CommandText = "select * from subjectivequestion";
            try
            {
                MySqlDataReader res = cm.ExecuteReader();
                //closeconnection();
                return res;
            }
            catch (Exception e)
            {
                closeconnection();
                Console.WriteLine(e.Message);
                return null;
            }
            finally
            {

            }
        }
        public String getstudenttestchosenid(String Studentid, String Testid)
        {
            MySqlCommand cm = cminit();
            Studentid = "'" + Studentid + "'";
            Testid = "'" + Testid + "'";
            cm.CommandText = "select * from studenttestchosenid where studentid=" + Studentid + "and testid=" + Testid;
            try
            {
                MySqlDataReader res = cm.ExecuteReader();
                if (res.Read())
                //closeconnection();
                {
                    Console.WriteLine(cm.CommandText);
                    Console.WriteLine(res[2].ToString());
                    return res[2].ToString();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                closeconnection();
                //Window2 window2 = new Window2(e.Message);
                //window2.ShowDialog();
                Console.WriteLine(e.Message);
                return null;
            }
            finally
            {

            }
        }
        public String getstudenttestsubmit(String Studentid, String Testid)
        {
            MySqlCommand cm = cminit();
            Studentid = "'" + Studentid + "'";
            Testid = "'" + Testid + "'";
            cm.CommandText = "select * from studentestsubmit where studentid=" + Studentid + "and testid=" + Testid;
            try
            {
                MySqlDataReader res = cm.ExecuteReader();
                if (res.Read())
                //closeconnection();
                {
                    Console.WriteLine(cm.CommandText);
                    Console.WriteLine(res[2].ToString());
                    return res[2].ToString();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                closeconnection();
                //Window2 window2 = new Window2(e.Message);
                //window2.ShowDialog();
                Console.WriteLine(e.Message);
                return null;
            }
            finally
            {

            }
        }
        public String getstudenttestorder(String Studentid, String Testid, String cid)
        {
            Studentid = "'" + Studentid + "'";
            Testid = "'" + Testid + "'";
            cid = "'" + cid + "'";
            String sql = "select * from studenttestchosenorder where studentid=" + Studentid + "and testid=" + Testid + "and chosenid=" + cid;
            //openconnection();
            MySqlCommand cm = cminit();
            MySqlDataReader res = null;
            cm.CommandText = sql;
            try
            {
                res = cm.ExecuteReader();
                Console.WriteLine(cm.CommandText);
                //if (res == null) return null;
                if (res.Read())
                {
                    String order= res[3].ToString();
                    Console.WriteLine(order);
                    return order;
                }
                else return null;
            }
            catch(Exception e2)
            {
                Console.WriteLine("fuck"+e2.Message);
                closeconnection();
                return null;
            }
            finally
            {
                try
                {
                    res.Close();
                }
                catch(Exception e)
                {

                }
                finally
                {

                }
            }
        }
        public String getstudenttestchosenanswer(String Studentid, String Testid, String cid)
        {
            Studentid = "'" + Studentid + "'";
            Testid = "'" + Testid + "'";
            cid = "'" + cid + "'";
            String sql= "select * from studenttestchosenanswer where studentid=" + Studentid + "and testid=" + Testid + "and chosenid=" + cid;
            openconnection();
            MySqlCommand cm = new MySqlCommand(sql, con);
            MySqlDataReader res = null;
            try
            {
                res = cm.ExecuteReader();
                if (res.Read())
                {
                    return res[3].ToString();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e2)
            {
                Console.WriteLine(e2.Message);
                return null;
            }
            finally
            {
                if(res!=null)
                {
                    res.Close();
                }
            }
        }
        public String getstudenttestsubanswer(String Studentid, String Testid, String cid)
        {
            Studentid = "'" + Studentid + "'";
            Testid = "'" + Testid + "'";
            cid = "'" + cid + "'";
            String sql = "select * from studenttestsubanswer where studentid=" + Studentid + "and testid=" + Testid + "and subjectid=" + cid;
            openconnection();
            MySqlCommand cm = new MySqlCommand(sql, con);
            MySqlDataReader res = null;
            try
            {
                res = cm.ExecuteReader();
                if (res.Read())
                {
                    return res[3].ToString();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e2)
            {
                Console.WriteLine(e2.Message);
                return null;
            }
            finally
            {
                if (res != null)
                {
                    res.Close();
                }
            }
        }
        public int getstudentvionumber(String Studentid)
        {
            Studentid = "'" + Studentid + "'";
            String sql = "select * from studentvio where studentid=" + Studentid;
            MySqlCommand cm = cminit();
            MySqlDataReader res = null;
            cm.CommandText = sql;
            try
            {
                res = cm.ExecuteReader();
                Console.WriteLine(cm.CommandText);
                //if (res == null) return null;
                if (res.Read())
                {
                    String Left = res[1].ToString();
                    Console.WriteLine(Left);
                    return int.Parse(Left);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "在线考试系统提示");
                return 0;
            }
            finally
            {
                res.Close();
            }
        }
        public int getstudentexitstate(String Studentid)
        {
            Studentid = "'" + Studentid + "'";
            String sql = "select * from studentexit where studentid=" + Studentid;
            MySqlCommand cm = cminit();
            MySqlDataReader res = null;
            cm.CommandText = sql;
            try
            {
                res = cm.ExecuteReader();
                Console.WriteLine(cm.CommandText);
                //if (res == null) return null;
                if (res.Read())
                {
                    String Left = res[1].ToString();
                    Console.WriteLine(Left);
                    return int.Parse(Left);
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "在线考试系统提示");
                return 0;
            }
            finally
            {
                res.Close();
            }
        }
        public void updatestudentexitstate(String Studentid,String exitstate)
        {
            Studentid = "'" + Studentid + "'";
            exitstate = "'" + exitstate + "'";
            String sql = "update studentexit set exitstate=" + exitstate + "where studentid=" + Studentid;
            MySqlCommand cm = cminit(sql);
            if (cm.ExecuteNonQuery() != 0)
            {
                
            }
            else
            {
                //MessageBox.Show(e.Message, "在线考试系统提示");
                String Sql = "insert into studentexit values(" + Studentid + "," + exitstate +  ")";
                openconnection();
                MySqlCommand CM = new MySqlCommand(Sql, con);
                try
                {
                    Console.WriteLine(Sql);
                    CM.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception e2)
                {

                }
                finally
                {

                }
            }
        }
        public void updatestudentvionumber(String Studentid, String vionumber)
        {
            Studentid = "'" + Studentid + "'";
            vionumber = "'" + vionumber + "'";
            String sql = "update studentvio set vionumber=" + vionumber + "where studentid=" + Studentid;
            MySqlCommand cm = cminit(sql);
            if(cm.ExecuteNonQuery()!=0)
            {
                
            }
            else
            {
                String Sql = "insert into studentvio values(" + Studentid + "," + vionumber + ")";
                openconnection();
                MySqlCommand CM = new MySqlCommand(Sql, con);
                try
                {
                    Console.WriteLine(Sql);
                    CM.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception e2)
                {
                    MessageBox.Show(e2.Message);
                }
                finally
                {

                }
            }
        }
        public String getbegintime(String Studentid, String Testid,String now)
        {
            Studentid = "'" + Studentid + "'";
            Testid = "'" + Testid + "'";
            now = "'" + now + "'";
            String sql = "select * from studenttestlefttime where studentid=" + Studentid + "and testid=" + Testid ;
            //openconnection();
            MySqlCommand cm = cminit();
            MySqlDataReader res = null;
            cm.CommandText = sql;
            try
            {
                res = cm.ExecuteReader();
                Console.WriteLine(cm.CommandText);
                //if (res == null) return null;
                if (res.Read())
                {
                    String Left = res[2].ToString();
                    Console.WriteLine(Left);
                    return Left;
                }
                else
                {
                    res.Close();
                    String Sql = "insert into studenttestlefttime values(" + Studentid + "," + Testid + "," + now + ")";
                    openconnection();
                    MySqlCommand CM = new MySqlCommand(Sql, con);
                    try
                    {
                        Console.WriteLine(Sql);
                        CM.ExecuteNonQuery();
                        con.Close();
                        return null;
                    }
                    catch (Exception e2)
                    {
                        Console.WriteLine(e2.Message);
                        return null;
                    }
                    finally
                    {
                        
                    }
                }
            }
            catch (Exception e2)
            {
                Console.WriteLine("fuck" + e2.Message);
                closeconnection();
                return null;
            }
            finally
            {
                try
                {
                    res.Close();
                }
                catch (Exception e)
                {

                }
                finally
                {

                }
            }
        }
        public String getcurrenttime()
        {
            String sql = "select current_timestamp() from dual";
            openconnection();
            MySqlCommand cm = new MySqlCommand(sql, con);
            MySqlDataReader res = null;
            try
            {
                res = cm.ExecuteReader();
                if (res.Read())
                {
                    return res[0].ToString();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e2)
            {
                Console.WriteLine(e2.Message);
                return null;
            }
            finally
            {
                if (res != null)
                {
                    res.Close();
                }
            }
        }
        public void savestudenttestchosenanswer(String Studentid, String Testid, String cid, String answer)
        {
            Studentid = "'" + Studentid + "'";
            Testid = "'" + Testid + "'";
            cid = "'" + cid + "'";
            answer = "'" + answer + "'";
            String sql = "insert into studenttestchosenanswer values(" + Studentid + "," + Testid + "," + cid + "," + answer + ");";
            MySqlCommand cm = cminit(sql);
            try
            {
                Console.WriteLine(sql);
                cm.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e1)
            {
                Console.WriteLine(e1.Message);
                String Sql= "update studenttestchosenanswer set answer=" + answer + "where studentid=" + Studentid+ "and Testid="+ Testid+ "and chosenid=" + cid;
                openconnection();
                MySqlCommand CM = new MySqlCommand(Sql, con);
                try
                {
                    Console.WriteLine(Sql);
                    CM.ExecuteNonQuery();
                    con.Close();
                }catch(Exception e2)
                {
                    Console.WriteLine(e2.Message);
                }
            }
            finally
            {

            }
        }
        public void savestudenttestsubanswer(String Studentid, String Testid, String cid, String answer)
        {
            Studentid = "'" + Studentid + "'";
            Testid = "'" + Testid + "'";
            cid = "'" + cid + "'";
            answer = "'" + answer + "'";
            String sql = "insert into studenttestsubanswer values(" + Studentid + "," + Testid + "," + cid + "," + answer + ");";
            MySqlCommand cm = cminit(sql);
            try
            {
                Console.WriteLine(sql);
                cm.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e1)
            {
                Console.WriteLine(e1.Message);
                String Sql = "update studenttestsubanswer set answer=" + answer + "where studentid=" + Studentid + "and Testid=" + Testid + "and subjectid=" + cid;
                openconnection();
                MySqlCommand CM = new MySqlCommand(Sql, con);
                try
                {
                    Console.WriteLine(Sql);
                    CM.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception e2)
                {
                    Console.WriteLine(e2.Message);
                }
                finally
                {

                }
            }
        }
        public void updatestudenttestorder(String Studentid, String Testid, String cid, int[] order,int num)
        {
            Studentid = "'" + Studentid + "'";
            Testid = "'" + Testid + "'";
            cid = "'" + cid + "'";
            String Order = "";
            for(int i=1;i<=num;i++)
            {
                Order += order[i].ToString();
            }
            Order = "'" + Order + "'";
            String sql = "insert into studenttestchosenorder values(" + Studentid + "," + Testid + "," + cid + "," + Order + ");";
            MySqlCommand cm = cminit(sql);
            try
            {
                Console.WriteLine(sql);
                cm.ExecuteNonQuery();
            }
            catch(Exception e1)
            {
                Console.WriteLine(e1.Message);
            }
            finally
            {

            }

        }
        public String getstudenttestsubid(String Studentid, String Testid)
        {
            MySqlCommand cm = cminit();
            Studentid = "'" + Studentid + "'";
            Testid = "'" + Testid + "'";
            cm.CommandText = "select * from studenttestsubid where studentid=" + Studentid + "and testid=" + Testid;
            try
            {
                MySqlDataReader res = cm.ExecuteReader();
                if (res.Read())
                //closeconnection();
                {
                    Console.WriteLine(cm.CommandText);
                    Console.WriteLine(res[2].ToString());
                    return res[2].ToString();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                closeconnection();
                //Window2 window2 = new Window2(e.Message);
                //window2.ShowDialog();
                Console.WriteLine(e.Message);
                return null;
            }
            finally
            {

            }
        }
    }
}
