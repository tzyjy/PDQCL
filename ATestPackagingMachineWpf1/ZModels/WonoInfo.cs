using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATestPackagingMachineWpf1.ZModels
{
    public class WonoInfo
    {
        /// <summary>
        /// 日期时间
        /// </summary>
        public string DateTime { get; set; }


        /// <summary>
        /// 作业员
        /// </summary>
        public string op_name { get; set; }

        /// <summary>
        /// 批号
        /// </summary>
        public string wo { get; set; }

        /// <summary>
        /// 干膜型号
        /// </summary>
        public string gmxh { get; set; }

        /// <summary>
        /// 干膜厚度
        /// </summary>
        public string gmhd { get; set; }


    }
}
