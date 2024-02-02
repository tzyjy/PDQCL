using ATestPackagingMachineWpf1.Common;
using BTest.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZModels;

namespace ATestPackagingMachineWpf1.DeviceFile
{
    public abstract class DeviceBase
    {

        /// <summary>
        /// PCI地址
        /// </summary>
        public int BoardNumber { get; set; } = 0;

        /// <summary>
        /// GPIB地址
        /// </summary>
        public byte PrimaryAddress { get; set; } = 0;
        /// <summary>
        /// csv名称
        /// </summary>
        public List<string> CsvList { get; set; }

        /// <summary>
        /// 仪器名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 仪器禁用
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// 仪器禁用
        /// </summary>
        public DeviceType DeviceType { get; set; }

        /// <summary>
        /// 显示数据处理,通用
        /// </summary>
        public ObservableCollection<InstrumentDataParentClass> InstrumentDataParentClassList=null;


        /// <summary>
        /// 测试时间
        /// </summary>
        [JsonIgnore]
        public Stopwatch SW = new Stopwatch();
        /// <summary>
        /// 触发时间
        /// </summary>
        [JsonIgnore]
        public long WriteMS = 0;
        /// <summary>
        /// 读取时间
        /// </summary>
        [JsonIgnore]
        public long ReadMS = 0;

        /// <summary>
        /// 仪器连接初始化
        /// </summary>
        /// <returns></returns>
        public abstract void Conect();

        /// <summary>
        /// 触发测试
        /// </summary>
        public abstract void Trigger();

        /// <summary>
        /// 写入仪器参数
        /// </summary>
        public abstract void WriteDeviceConfig();

        /// <summary>
        /// 读取量测结果
        /// </summary>
        public abstract string ReadTestData();

        public virtual void WriteCsv()
        {

            Dictionary<string, InstrumentDataParentClass> dicStringInstrumentData = new Dictionary<string, InstrumentDataParentClass>();
            foreach (var item in CsvList)
            {
                dicStringInstrumentData.Add(item, InstrumentDataParentClassList[CsvList.IndexOf(item)]);

            }
            CsvHelper.WriteAllData(GV.MesInfo.Wono, dicStringInstrumentData);

        }


        public  void ClearAllNumber()
        {

            foreach (InstrumentDataParentClass instrumentDataParentClass in InstrumentDataParentClassList)
            {

                instrumentDataParentClass.TotalNuber = 0;
                instrumentDataParentClass.NumberGoodProducts = 0;
                instrumentDataParentClass.NumberBadProducts = 0;
                instrumentDataParentClass.TestValue = 0;
                instrumentDataParentClass.Judge = "";
            }

        }

    }


    public enum DeviceType
    {


        Unknown,
         DCR,
         GanZhi,
         IR,
         BoXing,
         JiXing,
        Spare,
        S
    }
}
