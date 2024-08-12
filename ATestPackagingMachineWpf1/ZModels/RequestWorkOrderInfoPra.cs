using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATestPackagingMachineWpf1.ZModels
{
    [Serializable]
    public class RequestWorkOrderInfoPra : BindableBase
    {
        /// <summary>
        /// 厂内批号
        /// </summary>
        private string _wo;

        public string wo
        {
            get { return _wo; }
            set { SetProperty(ref _wo, value); }
        }

        /// <summary>
        /// 设备编码
        /// </summary>
        public string mach_code { get; set; }

        /// <summary>
        /// 当前作业员工号
        /// </summary>
        public string op_name { get; set; }
    }
}