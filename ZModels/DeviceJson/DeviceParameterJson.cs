using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
        /// 记住密码时间
        /// </summary>
        public int TimeHour { get; set; }

   

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
