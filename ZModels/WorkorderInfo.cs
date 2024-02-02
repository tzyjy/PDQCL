using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZModels
{
    public class WorkorderInfo
    {
        /// <summary>
        /// 工单号
        /// </summary>
        public string WONO { get; set; }


        /// <summary>
        /// 料号
        /// </summary>
        public string MATNO { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        public string Equipmentid { get; set; }

        /// <summary>
        /// 作业员
        /// </summary>
        public string USERID { get; set; }


        /// <summary>
        /// 创建日期
        /// </summary>
        public string CREDATEDATE { get; set; }


        /// <summary>
        /// 上限
        /// </summary>
        public string UPPERLIMIT { get; set; }

        /// <summary>
        /// 下限
        /// </summary>
        public string LOWERLIMIT { get; set; }

        /// <summary>
        /// 标准值
        /// </summary>
        public string CENTERVALUE { get; set; }












    }
}
