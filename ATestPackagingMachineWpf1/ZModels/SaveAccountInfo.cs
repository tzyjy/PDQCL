using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATestPackagingMachineWpf1.ZModels
{
    [Serializable]
    public class SaveAccountInfo
    {

        /// <summary>
        /// 保存登录账号
        /// </summary>
        public string SaveLoginId { get; set; }

        /// <summary>
        /// 保存登录密码
        /// </summary>
        public string SaveLoginPwd { get; set; }
        /// <summary>
        /// 保存登录时间有效期
        /// </summary>
        public string SaveStartDataTime { get; set; }

        /// <summary>
        /// 保存登录时间结束
        /// </summary>
        public string SaveEndDataTime { get; set; }
    }
}
