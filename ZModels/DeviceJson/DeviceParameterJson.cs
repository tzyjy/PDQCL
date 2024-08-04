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
        /// PLC地址
        /// </summary>
        public string PLC_Ipadress { get; set; }
        /// <summary>
        /// PLC端口号
        /// </summary>
        public int PLC_Port { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        public string EquipmentNum { get; set; }


        /// <summary>
        /// 作业员ID
        /// </summary>
        //public string OperatorName { get; set; }




        /// <summary>
        /// WebApiIP地址
        /// </summary>
        public string WebApi_Ipadress { get; set; }
        /// <summary>
        /// Webapi写端口号
        /// </summary>
        public int WebApi_Port { get; set; }

        /// <summary>
        /// NTP
        /// </summary>
        public string NTP_Ipadress { get; set; }

        /// <summary>
        /// 调机模式
        /// </summary>
        public bool IsTestMode { get; set; }



        /// <summary>
        /// 记住密码时间
        /// </summary>
        public int TimeHour { get; set; }

        /// <summary>
        /// Mes测试
        /// </summary>
        public  bool MesTest { get; set; }




        public List<GMXHParameter> GMXHParameterList { get; set; }

    }


}
