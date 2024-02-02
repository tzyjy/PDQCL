using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZModels;
using static System.Net.Mime.MediaTypeNames;

namespace ATestPackagingMachineWpf1.Common
{
    public class CsvHelper
    {
        #region 字段

        private static string CurrentTime
        {
            get { return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); }
        }

        #endregion
        #region 存仪器测试数据


        #region 数据记录表头数据（前几行）

        private static List<DataSaveTemplet> FirstFewLines(WorkorderInfo workorderInfo)
        {
            List<DataSaveTemplet> dataSaveTemplet = new List<DataSaveTemplet>();
            dataSaveTemplet.Add(new DataSaveTemplet()
            {
                ParameterName = "#工單號",
                ParameterValue = workorderInfo.WONO
            });

            dataSaveTemplet.Add(new DataSaveTemplet()
            {
                ParameterName = "#料號",
                ParameterValue = workorderInfo.MATNO
            });
            dataSaveTemplet.Add(new DataSaveTemplet()
            {
                ParameterName = "#設備機臺號",
                ParameterValue = workorderInfo.Equipmentid
            });
         
            dataSaveTemplet.Add(new DataSaveTemplet()
            {
                ParameterName = "#建立時間",
                ParameterValue = workorderInfo.CREDATEDATE
            });
            dataSaveTemplet.Add(new DataSaveTemplet()
            {
                ParameterName = "#規格上限",
                ParameterValue = workorderInfo.UPPERLIMIT
            });
            dataSaveTemplet.Add(new DataSaveTemplet()
            {
                ParameterName = "#規格下限",
                ParameterValue = workorderInfo.LOWERLIMIT
            });

            dataSaveTemplet.Add(new DataSaveTemplet()
            {
                ParameterName = "#規格單位",
                ParameterValue = workorderInfo.CENTERVALUE
            });
            dataSaveTemplet.Add(new DataSaveTemplet()
            {
                ParameterName = "#規格值",
                ParameterValue = workorderInfo.CENTERVALUE
            });
            return dataSaveTemplet;
        }

        #endregion MyRegion

        #region 数据标题项
        public static void WriteSingleTemplateCSV(string WorkOrderNumbe, string instrumentName, WorkorderInfo workorderInfo)
        {
            string path1 = @"D:\Cyntec\dll\MESIDE_HF_CMC_TAPING_IT\XML\";
            string path = path1 + WorkOrderNumbe + "_" + instrumentName + ".csv";
            if (!Directory.Exists(path1))
            {
                Directory.CreateDirectory(path1);
            }

            StreamWriter sw;

            try
            {
                if (!File.Exists(path))
                {
                    //创建写入器
                    sw = new StreamWriter(path, false, Encoding.Default);
                    //写入第前几行
                    List<DataSaveTemplet> dataSaveTempletlist = FirstFewLines(workorderInfo); ;
                    foreach (DataSaveTemplet item in dataSaveTempletlist)
                    {
                        sw.WriteLine(item.ParameterName + "," + item.ParameterValue);
                    }
                    //在写入一行

                    string[] title = new string[] {
                 "#序號",
                 "#量測值",
                 "#電表判定",
                 "#時間",
                 "#ReelID",
                 "#規格上限",
                 "#規格下限",
                };

                    sw.WriteLine(string.Join(",", title), Encoding.Default);
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region 写入行数据
        private static void WriteSingleCsvData(string WorkOrderNumbe, string instrumentName, InstrumentDataParentClass instrumentDataParentClass)
        {
            string path1 = @"D:\Cyntec\dll\MESIDE_HF_CMC_TAPING_IT\XML\";
            string path = path1 + WorkOrderNumbe + "_" + instrumentName + ".csv";
            StreamWriter sw;

            try
            {
                if (!File.Exists(path))
                {
                    // throw new Exception("没有文件，写入失败");
                }

                if (File.Exists(path))
                {
                    List<string> dataList = new List<string>();
                    dataList.Add(instrumentDataParentClass.TotalNuber.ToString());//第一列
                    dataList.Add(instrumentDataParentClass.TestValue.ToString());//第2列
                    dataList.Add(instrumentDataParentClass.Judge);//第3列
                    dataList.Add(CurrentTime);//第4列
                    dataList.Add("");//第5列
                    dataList.Add(instrumentDataParentClass.HighValue.ToString());//第6列
                    dataList.Add(instrumentDataParentClass.LowValue.ToString());//第7列

                    //创建写入器
                    sw = new StreamWriter(path, true, Encoding.Default);

                    sw.WriteLine(string.Join(",", dataList.ToArray()), Encoding.Default);
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region 写入多个模版的数据
        /// <summary>
        /// 写入多个仪器的csv值
        /// </summary>
        /// <param name="keyValuePairs"></param>
        /// <param name="WorkOrderNumbe"></param>
        public static void WriteAllData(string WorkOrderNumbe, Dictionary<string, InstrumentDataParentClass> dicStringInstrumentData)
        {
            foreach (var item in dicStringInstrumentData)
            {
                WriteSingleCsvData(WorkOrderNumbe, item.Key, item.Value);
            }
        }

        #endregion

        #region 写入数据模版
        /// <summary>
        /// 写入多个仪表的模版
        /// </summary>
        public static void WriteAllTemplateCSV(string WorkOrderNumbe, Dictionary<string, WorkorderInfo> keyValuePairs)
        {
            foreach (var item in keyValuePairs)
            {
                CsvHelper.WriteSingleTemplateCSV(WorkOrderNumbe, item.Key, item.Value);
            }
        }
        #endregion

        #endregion

   

        #region Mes记录

        public static void WriteToCSVMesInfo(string path, List<string> dataList)
        {

            string path1 = @"D:\MesInfo\";
            string path2 = path1 + path + "Mes记录.csv";
            if (!Directory.Exists(path1))
            {
                Directory.CreateDirectory(path1);
            }

            StreamWriter sw;

            try
            {
                if (!File.Exists(path2))
                {
                    //创建写入器
                    sw = new StreamWriter(path2, false, Encoding.Default);

                    //写入第1行
                    string[] title = new string[] {
                    "时间",
                    "信息",

                 };

                    sw.WriteLine(string.Join(",", title), Encoding.Default);
                }
                else
                {
                    sw = new StreamWriter(path2, true, Encoding.Default);
                }

                sw.WriteLine(string.Join(",", dataList.ToArray()), Encoding.Default);
                sw.Flush();
                sw.Close();
            }
            catch (Exception)
            {

            }
        }
        #endregion

        #region 刷工单记录

        public static void RefrehWono(string path, List<string> dataList)
        {

            string path1 = "D:\\生产日志\\工单记录\\";
            string path2 = path1 + path + "工单记录.csv";
            if (!Directory.Exists(path1))
            {
                Directory.CreateDirectory(path1);
            }

            StreamWriter sw;

            try
            {
                if (!File.Exists(path2))
                {
                    //创建写入器
                    sw = new StreamWriter(path2, false, Encoding.Default);

                    //写入第1行
                    string[] title = new string[] {
                    "时间",
                    "工单号",

                 };

                    sw.WriteLine(string.Join(",", title), Encoding.Default);
                }
                else
                {
                    sw = new StreamWriter(path2, true, Encoding.Default);
                }

                sw.WriteLine(string.Join(",", dataList.ToArray()), Encoding.Default);
                sw.Flush();
                sw.Close();
            }
            catch (Exception)
            {

            }







        }

        #endregion

   

     
    }

    /// <summary>
    /// csv文件前几行
    /// </summary>
    public class DataSaveTemplet
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        public string ParameterName { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        public string ParameterValue { get; set; }


    }
}
