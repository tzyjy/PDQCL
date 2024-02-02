using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZModels.DeviceJson
{
    public class TempParameter
    {
        /// <summary>
        /// 端口号
        /// </summary>
        public string PortName { get; set; }

        /// <summary>
        /// 波特率
        /// </summary>
        public int BaudRate { get; set; }
        /// <summary>
        /// 数据位
        /// </summary>
        public int DataBits { get; set; }
        /// <summary>
        /// 停止位
        /// </summary>
        public string StopBits { get; set; }
        /// <summary>
        /// 校验
        /// </summary>
        public string Parity { get; set; }


        /// <summary>
        /// 设定值
        /// </summary>
        public short SVTemp { get; set; }

        /// <summary>
        /// 上限
        /// </summary>
        public short AlarmTempHigh { get; set; }
        /// <summary>
        /// 下限
        /// </summary>
        public short AlarmTempLow { get; set; }

      
    }
}
