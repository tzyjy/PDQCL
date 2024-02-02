using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATestPackagingMachineWpf1.DeviceFile.DeviceParameter
{
    public class Wave19301Parameter
    {
        /// <summary>
        /// 电压
        /// </summary>
        public decimal Voltage { get; set; }

        /// <summary>
        /// 面积上限
        /// </summary>
        public decimal AreaHigh { get; set; }

        /// <summary>
        /// 面积下限
        /// </summary>
        public decimal AreaLow { get; set; }


        /// <summary>
        /// 脉冲次数
        /// </summary>
        public decimal PULSe { get; set; }

        /// <summary>
        /// 二次微分
        /// </summary>
        public decimal LAPLac { get; set; }
    }
}
