using ATestPackagingMachineWpf1.DeviceFile.Mes;
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
        /// 委托改变仪器禁用颜色
        /// </summary>
        public static ChangeEnableColor ChangeEnableColorMethod;

        /// <summary>
        /// 登录人员
        /// </summary>
        public static LogonPeson CurrentLogonPeson;


        public static MesInfo MesInfo;

        /// <summary>
        /// 自动
        /// </summary>
        public static bool ButtonAtuo;


        /// <summary>
        /// Mes在线
        /// </summary>
        public static bool MesOnline;



        //标准件测试
        public static bool standtestTest = false;


        //标准件接受信息
        public static CSACollection standtestInfo = new CSACollection();

        //Mes标准件测试完成
        public static bool ReplySTDTestOver = false;

        //Mes信息刷新
        public static bool MesInfoUpdate = false;

        //产品总数
        public static AllProductNumber AllProductNumber = new AllProductNumber();

        /// <summary>
        /// 感值1Data
        /// </summary>
        public static ObservableCollection<decimal> Gnzhi1OCData = new ObservableCollection<decimal>();
        /// <summary>
        /// 感值2Data
        /// </summary>
        public static ObservableCollection<decimal> Gnzhi2OCData = new ObservableCollection<decimal>();

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
