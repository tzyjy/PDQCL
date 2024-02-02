using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZModels;

namespace TDLL
{
    public class LogonService
    {

        private TestPackagingContext DB = ShareDB.DB;
        public LogonService()
        {


        }


        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="logonPeson"></param>
        /// <returns></returns>
        public int Add(LogonPeson logonPeson)
        {

            DB.Configuration.AutoDetectChangesEnabled = false;     // 局部关闭DetectChanges方法，使EF不会跟踪实体，这样将不会造成全盘扫描而使得我们不会处于漫长的等待
            DB.LogonPesons.Add(logonPeson);
            int count = DB.SaveChanges();
            return count;

        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Remove(int id)
        {
            LogonPeson logonPerSon = DB.LogonPesons.Where(a => a.ID == id).FirstOrDefault();
            DB.LogonPesons.Remove(logonPerSon);
            int count = DB.SaveChanges();
            return count;

        }


        public int UpDate(LogonPeson logonPeson1)
        {

            LogonPeson logonPerSon = DB.LogonPesons.Where(a => a.ID == logonPeson1.ID).FirstOrDefault();
            logonPerSon.LoginAccount = logonPeson1.LoginAccount;
            logonPerSon.LoginPwd = logonPeson1.LoginPwd;
            logonPerSon.FunctionPermission100 = logonPeson1.FunctionPermission100;
            logonPerSon.FunctionPermission101 = logonPeson1.FunctionPermission101;
            logonPerSon.FunctionPermission102 = logonPeson1.FunctionPermission102;
            logonPerSon.FunctionPermission103 = logonPeson1.FunctionPermission103;
            logonPerSon.FunctionPermission104 = logonPeson1.FunctionPermission104;
            logonPerSon.FunctionPermission105 = logonPeson1.FunctionPermission105;
            logonPerSon.FunctionPermission106 = logonPeson1.FunctionPermission106;
            logonPerSon.FunctionPermission777 = logonPeson1.FunctionPermission777;

            int count = DB.SaveChanges();
            return count;

        }

        public List<LogonPeson> GetAllData()
        {
            return DB.LogonPesons.ToList();
        }

        public LogonPeson GetDataById(int id)
        {
            return DB.LogonPesons.Where(a => a.ID == id).FirstOrDefault(); ;
        }

        public LogonPeson AdminLogin(LogonPeson logonPeson)
        {


            return DB.LogonPesons.Where(a => a.LoginAccount == logonPeson.LoginAccount && a.LoginPwd == logonPeson.LoginPwd).FirstOrDefault(); ;


        }

    }
}
