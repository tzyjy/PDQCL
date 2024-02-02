using ATestPackagingMachineWpf1.Common;
using BTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ATestPackagingMachineWpf1.DeviceFile
{
    public class PCI1730WriteAndRead
    {

        /// <summary>
        /// Mes扫工单
        /// </summary>
        public static void RrefeshWono()
        {
            DV.IO1730.WriteDObit(JsonSaveEXT.deviceParameterJsonGv.iOWriteX.MesWono, 1);
            Thread.Sleep(200);
            DV.IO1730.WriteDObit(JsonSaveEXT.deviceParameterJsonGv.iOWriteX.MesWono, 0);

        }

        /// <summary>
        /// Ready信号开
        /// </summary>
        public static void ReadyOn()
        {
            DV.IO1730?.WriteDObit(JsonSaveEXT.deviceParameterJsonGv.iOWriteX.Ready, 1);
            Thread.Sleep(100);

        }

        /// <summary>
        /// Ready信号关
        /// </summary>
        public static void ReadyOff()
        {
          
                DV.IO1730?.WriteDObit(JsonSaveEXT.deviceParameterJsonGv.iOWriteX.Ready, 0);
          
       
            Thread.Sleep(100);

        }

        /// <summary>
        /// 测试On
        /// </summary>
        public static void TestBusyOn()
        {
            DV.IO1730.WriteDObit(JsonSaveEXT.deviceParameterJsonGv.iOWriteX.Busy, 1);


        }

        /// <summary>
        /// 测试Off
        /// </summary>
        public static void TestBusyOff()
        {
            DV.IO1730?.WriteDObit(JsonSaveEXT.deviceParameterJsonGv.iOWriteX.Busy, 0);


        }




        /// <summary>
        /// 感值对比结束写1
        /// </summary>
        public static void LS1LS2Over1()
        {
            DV.IO1730.WriteDObit(JsonSaveEXT.deviceParameterJsonGv.iOWriteX.LS1LS2Over, 1);


        }

        /// <summary>
        /// 感值对比结束写0
        /// </summary>
        public static void LS1LS2Over0()
        {
            DV.IO1730.WriteDObit(JsonSaveEXT.deviceParameterJsonGv.iOWriteX.LS1LS2Over, 0);


        }


        /// <summary>
        /// 感值对比结果1
        /// </summary>
        public static void LS1LS2Value1()
        {
            DV.IO1730.WriteDObit(JsonSaveEXT.deviceParameterJsonGv.iOWriteX.LS1LS2Value, 1);


        }

        /// <summary>
        /// 感值对比结果0
        /// </summary>
        public static void LS1LS2Value0()
        {
            DV.IO1730.WriteDObit(JsonSaveEXT.deviceParameterJsonGv.iOWriteX.LS1LS2Value, 0);


        }



        /// <summary>
        /// DCR12旋转结果1
        /// </summary>
        public static void DCR12XuanZhuan1()
        {
            DV.IO1730.WriteDObit(JsonSaveEXT.deviceParameterJsonGv.iOWriteX.DCR12XuanZhuan, 1);


        }

        /// <summary>
        /// DCR12旋转结果0
        /// </summary>
        public static void DCR12XuanZhuan0()
        {
            DV.IO1730.WriteDObit(JsonSaveEXT.deviceParameterJsonGv.iOWriteX.DCR12XuanZhuan, 0);


        }




        /// <summary>
        /// DCR34报警1
        /// </summary>
        public static void DCR34Error1()
        {
            DV.IO1730.WriteDObit(JsonSaveEXT.deviceParameterJsonGv.iOWriteX.DCR12XuanZhuan, 1);


        }

        /// <summary>
        /// DCR34报警0
        /// </summary>
        public static void DCR34Error0()
        {
            DV.IO1730.WriteDObit(JsonSaveEXT.deviceParameterJsonGv.iOWriteX.DCR12XuanZhuan, 0);


        }







    }
}
