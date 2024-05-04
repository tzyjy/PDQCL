using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZModels
{
    [Serializable]
    public class UploadData
    {

        /// <summary>
        /// 厂内工单号
        /// </summary>
        public string wo { get; set; }

        /// <summary>
        /// 厂内工程号
        /// </summary>
        public string cp_rev { get; set; }

        /// <summary>
        /// 设备编码
        /// </summary>
        public string mach_code { get; set; }


        /// <summary>
        /// 当前作业员工号
        /// </summary>
        public string op_name { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string start_time { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string end_time { get; set; }



        /// <summary>
        /// 当前站别
        /// </summary>
        public string dept_code { get; set; }



        /// <summary>
        /// 实际线速 例:2.3~2.7m/min
        /// </summary>
        public string speed { get; set; }


        /// <summary>
        /// 实际是否关闭微蚀 Y:关闭 N:不关闭
        /// </summary>
        public string wc_switch_off { get; set; }


        /// <summary>
        /// 实际是否关闭高压水洗 Y:关闭 N:不关闭
        /// </summary>
        public string gysx_switch_off { get; set; }



    }



    [Serializable]
    public class ReturnUploadDataData
    {

        /// <summary>
        /// 200:成功，201:参数异常，202:数据库异常，203:生产信息错误，204:其他异常
        /// </summary>
        public int status_code { get; set; }

        /// <summary>
        /// API调用结果, 当status_code!=200时， 将此信息提示于屏幕上，提示标题为“来自MES的提示信息：”，用于提示操作员
        /// </summary>
        public string message { get; set; }
    }
}
