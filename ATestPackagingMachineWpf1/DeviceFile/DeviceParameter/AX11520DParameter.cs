using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATestPackagingMachineWpf1.DeviceFile.DeviceParameter
{
    public class AX11520DParameter
    {
        /// <summary>
        /// 测试模式
        /// </summary>
        public string TestMode { get; set; }

        /// <summary>
        /// 测量Scale
        /// </summary>
        public string TestScale { get; set; }

        /// <summary>
        /// DCR测试下限
        /// </summary>
        public int DCRLow { get; set; }

        /// <summary>
        /// DCR测试上限
        /// </summary>
        public int DCRHigh { get; set; }

        /// <summary>
        /// 触发方式
        /// </summary>
        public string TriggerMode { get; set; }

        /// <summary>
        /// 测试速度
        /// </summary>
        public string TestSpeed { get; set; }

        /// <summary>
        /// 蜂鸣方式
        /// </summary>
        public string BuzzingMode { get; set; }

        /// <summary>
        /// 反馈信息
        /// </summary>
        public string FeedbackInformation { get; set; }

    }
}
