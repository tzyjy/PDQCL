using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZModels.DeviceJson;

namespace ZModels
{
    public class DeviceParameterJson
    {
        /// <summary>
        /// PLCIP地址
        /// </summary>
        public string PLC_Ipadress { get; set; }
        /// <summary>
        /// PLC写端口号
        /// </summary>
        public int PLC_WritePort { get; set; }

        /// <summary>
        /// PLC读端口号
        /// </summary>
        public int PLC_ReadPort { get; set; }

        /// <summary>
        /// 调机模式
        /// </summary>
        public bool IsTestMode { get; set; }

        /// <summary>
        /// IO板卡输出地址，给PLCX点写值
        /// </summary>
        public IOWriteX iOWriteX { get; set; }

        /// <summary>
        /// IO板卡输入地址，PLC给Y点
        /// </summary>
        public IOReadY iOReadY { get; set; }

        /// <summary>
        /// 温控1参数设置
        /// </summary>
        public TempParameter TempParameter1 { get; set; }

        /// <summary>
        /// 温控2参数设置
        /// </summary>
        public TempParameter TempParameter2 { get; set; }

        /// <summary>
        /// 警戒值
        /// </summary>
        public WarningValue WarningValue { get; set; }


        /// <summary>
        /// 厂家用
        /// </summary>
        public Manufacturer Manufacturer { get; set; }

        /// <summary>
        /// PLC参数
        /// </summary>
        public PLCDataConfig PLCDataConfig { get; set; }

        /// <summary>
        /// 调试清单
        /// </summary>
        public TestInfo TestInfo { get; set; }

        /// <summary>
        /// 记住密码时间
        /// </summary>
        public int TimeHour { get; set; }

        /// <summary>
        /// K值
        /// </summary>
        public double KValue { get; set; }

        /// <summary>
        /// LS1_LS2上限设置
        /// </summary>
        public decimal LS1_LS2High { get; set; }

        /// <summary>
        /// LS1_LS2下限设置
        /// </summary>
        public decimal LS1_LS2Low { get; set; }

    }


}
