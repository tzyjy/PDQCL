using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZModels
{
    public class MesInfo
    {

        /// <summary>
        /// 工单号
        /// </summary>
        public string Wono { get; set; }

        /// <summary>
        /// 工单数量
        /// </summary>
        public string Woqty { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string Equipmentid { get; set; }

        /// <summary>
        /// 料号
        /// </summary>
        public string Matno { get; set; }

        /// <summary>
        /// 作业员
        /// </summary>
        public string Tpno { get; set; }


        /// <summary>
        /// 相机信息
        /// </summary>
        public string MarkString { get; set; }

        /// <summary>
        /// 包装数量
        /// </summary>
        public int Packageqty { get; set; }

        /// <summary>
        /// 前空格数量
        /// </summary>
        public int FrontSpace { get; set; }

        /// <summary>
        /// 后空格数量
        /// </summary>
        public int AfterSpace { get; set; }

        /// <summary>
        /// 不封后空格数量
        /// </summary>
        public int Blankqty { get; set; }

        /// <summary>
        /// 尾数
        /// </summary>
        public int Checkqty { get; set; }

        /// <summary>
        /// 封刀时间
        /// </summary>
        public float SealingTime { get; set; }

    


    }
}
