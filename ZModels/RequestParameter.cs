using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZModels
{
    /// <summary>
    /// 获取Mes返回参数
    /// </summary>
    ///
    [Serializable]
    public class ReturnWorkOrderInfo
    {
        /// <summary>
        /// 200:成功，201:参数异常，202:数据库异常，203:生产信息错误，204:其他异常
        /// </summary>
        public int status_code { get; set; }

        /// <summary>
        /// API调用结果, 当status_code!=200时， 将此信息提示于屏幕上，提示标题为“来自MES的提示信息：”，用于提示操作员
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 厂内工程号
        /// </summary>
        public string cp_rev { get; set; }

        /// <summary>
        /// 当前站别
        /// </summary>
        public string dept_code { get; set; }

        /// <summary>
        /// 线速 例:2.3~2.7m/min
        /// </summary>
        public string speed { get; set; }

        /// <summary>
        /// 线速参考范围
        /// </summary>
        public string speed_desc { get; set; }

        /// <summary>
        /// 是否关闭微蚀 Y:关闭 N:不关闭
        /// </summary>
        public string wc_switch_off { get; set; }

        /// <summary>
        /// 是否关闭高压水洗 Y:关闭 N:不关闭
        /// </summary>
        public string gysx_switch_off { get; set; }
    }

    /// <summary>
    /// 获取Mes返回参数
    /// </summary>
    ///
    [Serializable]
    public class ReturnYXInfo
    {
        /// <summary>
        /// 200:成功，201:参数异常，202:数据库异常，203:生产信息错误，204:其他异常
        /// </summary>
        public int status_code { get; set; }

        /// <summary>
        /// API调用结果, 当status_code!=200时， 将此信息提示于屏幕上，提示标题为“来自MES的提示信息：”，用于提示操作员
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 干膜型号
        /// </summary>
        public string gmxh { get; set; }

        /// <summary>
        /// 干膜厚度
        /// </summary>
        public string gmhd { get; set; }
    }

    [Serializable]
    public class ReturnPDQCLInfo
    {
        /// <summary>
        /// 200:成功，201:参数异常，202:数据库异常，203:生产信息错误，204:其他异常
        /// </summary>
        public int status_code { get; set; }

        /// <summary>
        /// API调用结果, 当status_code!=200时， 将此信息提示于屏幕上，提示标题为“来自MES的提示信息：”，用于提示操作员
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 板长
        /// </summary>
        public string bc { get; set; }

        /// <summary>
        /// 板宽
        /// </summary>
        public string bk { get; set; }

        /// <summary>
        /// 开刷提示
        /// </summary>
        public string ksts { get; set; }
    }
}