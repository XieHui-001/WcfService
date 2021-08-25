using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace WcfService1
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“User”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 User.svc 或 User.svc.cs，然后开始调试。
    public class User : IUser
    {
        SqlConnection strCon = new SqlConnection("server=DESKTOP-3N5JSFQ;database=Test;uid=sa;pwd=323402171");
        private bool isTure = false;
        public string DoWork(int a,int b)
        {
            string valueinfo = string.Empty;
            int Sum = a * b;
            if (a > 0 && a != b)
            {
                valueinfo = "当前值不匹配";
            }
            else {
                return Sum > 0 ? "WCF 结果值为:" + Sum : "空";
            }
            return valueinfo;
        }


        // 开启数据库
        public void openSql() {
            strCon.Open();
        }
        // 关闭数据库
        public void CloseSql() {
            strCon.Close();
        }

        public string Login(string name, string pwd) {
            return querysql(name, pwd) ? "登录名称：" + name + "密码:" + pwd : "登录失败";
        }

        public bool querysql(string name,string password) {
            try
            {
                openSql();
                string sql = "select * from LoginTable where Name='"+ name + "' and Password='"+ password +"'";
                DataSet ds = new DataSet();
                SqlDataAdapter s = new SqlDataAdapter(sql, strCon);
                s.Fill(ds);
                DataTable tbl = ds.Tables[0];
                DataRow row = tbl.Rows[0];
                if (!row.HasErrors) {
                    isTure = true;
                }
                return isTure;
            }
            catch (Exception ex)
            {
                return false;
                //throw ex;
            }
            finally {
                CloseSql();
            }
        }
    }
}
