
using ATestPackagingMachineWpf1.ViewModels;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZModels;

namespace ATestPackagingMachineWpf1.Common
{
    public class GV
    {
 

        /// <summary>
        /// 登录人员
        /// </summary>
        public static LogonPeson CurrentLogonPeson;



        public static LogonPeson ManufacturerLogonPeson { get; set; } = new LogonPeson()
        {
            LoginAccount = "777",
            LoginPwd = "777",
            FunctionPermission100 = true,
            FunctionPermission101 = true,
            FunctionPermission102 = true,
            FunctionPermission103 = true,
            FunctionPermission104 = true,
            FunctionPermission105 = true,
            FunctionPermission106 = true,
            FunctionPermission777 = true,


        };
    }
}
