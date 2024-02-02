using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZModels.DeviceJson
{
    public class Manufacturer
    {
        /// <summary>
        /// 调机模式
        /// </summary>
        public bool ManufacturerMode { get; set; }

        /// <summary>
        /// 相机屏蔽
        /// </summary>
        public bool CCDEnable { get; set; }

        /// <summary>
        /// 是否允许设备加载异常仍然可以运行
        /// </summary>
        public bool AllowRun { get; set; }



    }
}
