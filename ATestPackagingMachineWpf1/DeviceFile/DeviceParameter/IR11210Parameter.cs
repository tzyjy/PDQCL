using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATestPackagingMachineWpf1.DeviceFile.DeviceParameter
{
    public class IR11210Parameter
    {
        /// <summary>
        /// 电压
        /// </summary>
        public string Voltage { get; set; }
        /// <summary>
        /// 下限
        /// </summary>
        public string LowerLimit { get; set; }
        /// <summary>
        /// 上限
        /// </summary>
        public string HighLimit { get; set; }
        /// <summary>
        /// 测试时间
        /// </summary>
        public decimal TestTime { get; set; }

        /// <summary>
        /// 触发方式
        /// </summary>
        public string TriggerMode { get; set; }





    }
}
