using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTest.LogHelper
{
    public class LOG
    {
        private static string DealInfo(string info)
        {

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("当前时间:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"));
            stringBuilder.Append("     信息:" + info + "\r\n");
            return stringBuilder.ToString();


        }





        #region 日志
        public static void WriteLog(string text)
        {

            string text2 = DealInfo(text);

            string path = DateTime.Now.ToString("yyyy_MM_dd");
            string path1 = @"D:\PCLogs\";
            string path2 = path1 + path + "Log.log";
            if (!Directory.Exists(path1))
            {
                Directory.CreateDirectory(path1);
            }
            StreamWriter sw;
            try
            {
                sw = new StreamWriter(path2, true, Encoding.Default);
                sw.Write(text2, Encoding.Default);
                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }



        #endregion


    }
}
