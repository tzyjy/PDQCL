using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZModels
{
    public class IOReadY
    {
        /// <summary>
        /// DCR12触发
        /// </summary>
        public byte DCR12Triger { get; set; }

        /// <summary>
        /// DCR34触发
        /// </summary>
        public byte DCR34Triger { get; set; }

        /// <summary>
        /// IR触发
        /// </summary>
        public byte IRTriger { get; set; }

        /// <summary>
        /// 感值1触发
        /// </summary>
        public byte GanZhi1Triger { get; set; }

        /// <summary>
        /// 感值2触发
        /// </summary>
        public byte GanZhi2Triger { get; set; }

        /// <summary>
        /// 准备OK
        /// </summary>
        public byte BiginOK { get; set; }

        /// <summary>
        /// IR结束
        /// </summary>
        public byte IROver { get; set; }

        /// <summary>
        /// 备用
        /// </summary>
        public byte ShareTest1 { get; set; }
        /// <summary>
        /// 备用
        /// </summary>
        public byte ShareTest2 { get; set; }
        /// <summary>
        /// 备用
        /// </summary>
        public byte ShareTest3 { get; set; }
        /// <summary>
        /// 备用
        /// </summary>
        public byte ShareTest4 { get; set; }

        /// <summary>
        /// 备用
        /// </summary>
        public byte ShareTest5 { get; set; }
        /// <summary>
        /// 备用
        /// </summary>
        public byte ShareTest6 { get; set; }
        /// <summary>
        /// 备用
        /// </summary>
        public byte ShareTest7 { get; set; }
    }
}
