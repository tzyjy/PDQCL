using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ATestPackagingMachineWpf1.DeviceFile
{
    public class DeviceParameterToView
    {

        Dictionary<string, string> combox2TestScale = new Dictionary<string, string>();
        Dictionary<string, string> combox3TriggerMode = new Dictionary<string, string>();

        Dictionary<string, string> combox4TestSpeed = new Dictionary<string, string>();
        Dictionary<string, string> combox5BuzzingMode = new Dictionary<string, string>();
        Dictionary<string, string> combox6FeedbackInformation = new Dictionary<string, string>();

        Dictionary<string, string> comboxIRTriggerMode = new Dictionary<string, string>();
        public ObservableCollection<GanzhiParameter> GanZhiParameterList { get; set; } = GetComboxParameter();
        public DataTable GanZhiCombox { get; set; } = GetComboxRange();


        public DataTable DCRTestScale { get; set; }

        public DataTable DCRTriggerMode { get; set; }

        public DataTable DCRTestSpeed { get; set; }

        public DataTable DCRBuzzingMode { get; set; }

        public DataTable DCRFeedbackInformation { get; set; }
        public DataTable IRTriggerMode { get; set; }

        public static ObservableCollection<GanzhiParameter> GetComboxParameter()
        {
            ObservableCollection<GanzhiParameter> ganzhiParameters = new ObservableCollection<GanzhiParameter>();
            ganzhiParameters.Add(new GanzhiParameter() { Key = "Z", Name = "Z" });
            ganzhiParameters.Add(new GanzhiParameter() { Key = "LS", Name = "LS" });
            ganzhiParameters.Add(new GanzhiParameter() { Key = "θ", Name = "PHASE" });
            ganzhiParameters.Add(new GanzhiParameter() { Key = "RS", Name = "RS" });
            ganzhiParameters.Add(new GanzhiParameter() { Key = "OFF", Name = "OFF" });
            return ganzhiParameters;


        }

        public static DataTable GetComboxRange()
        {
            List<string> nameList = new List<string>()
            {
                "100mΩ",
                "1Ω",
                "10Ω",
               "300Ω",
                 "1KΩ",
                  "3KΩ",
                   "10KΩ",
                    "30KΩ",
                     "100KΩ",
                      "1MΩ",
                       "10MΩ",
                         "100MΩ",
            };
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Value");
            for (int i = 1; i < nameList.Count + 1; i++)
            {
                DataRow dataRow = dt.NewRow();
                dataRow["Name"] = nameList[i - 1];
                dataRow["Value"] = i.ToString();
                dt.Rows.Add(dataRow);
            }
            return dt;


        }


        public DeviceParameterToView()
        {
            DCRParameterInit();

            DCRTestScale = GetDataTablebyDic(combox2TestScale);

            DCRTriggerMode= GetDataTablebyDic(combox3TriggerMode);

            DCRTestSpeed = GetDataTablebyDic(combox4TestSpeed);

            DCRBuzzingMode = GetDataTablebyDic(combox5BuzzingMode);

            DCRFeedbackInformation = GetDataTablebyDic(combox6FeedbackInformation);

            IRTriggerMode = GetDataTablebyDic(comboxIRTriggerMode);
        }

        public void DCRParameterInit()
        {
         
            combox2TestScale.Add("10mΩ", "R1");
            combox2TestScale.Add("100mΩ", "R2");
            combox2TestScale.Add("1Ω", "R3");
            combox2TestScale.Add("10Ω", "R4");
            combox2TestScale.Add("100Ω", "R5");
            combox2TestScale.Add("1KΩ", "R6");

            //第三个
          
            combox3TriggerMode.Add("EXT", "0");
            combox3TriggerMode.Add("INI", "1");



            //第四个

            combox4TestSpeed.Add("FAST", "1");
            combox4TestSpeed.Add("SLOW", "0");



            ///第五个
  
            combox5BuzzingMode.Add("B0", "0");
            combox5BuzzingMode.Add("B1", "1");
            combox5BuzzingMode.Add("B2", "2");
            combox5BuzzingMode.Add("B3", "3");

            //第6个

        
            combox6FeedbackInformation.Add("IO", "1");
            combox6FeedbackInformation.Add("EXT", "0");

            //IR
         
            comboxIRTriggerMode.Add("IO触发", "0");
            comboxIRTriggerMode.Add("手动触发", "1");
            comboxIRTriggerMode.Add("远程触发", "2");
        }


        /// <summary>
        /// 生成DataTable
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="combox3TriggerMode"></param>
        public  DataTable GetDataTablebyDic(Dictionary<string, string> comboxDictionary)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Value");
            foreach (var item in comboxDictionary)
            {
                DataRow dataRow = dt.NewRow();
                dataRow["Name"] = item.Key;
                dataRow["Value"] = item.Value;
                dt.Rows.Add(dataRow);
            }
            return dt;
        }
    }
}
