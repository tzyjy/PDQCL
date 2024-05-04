using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZModels;

namespace ATestPackagingMachineWpf1.DeviceFile
{
    public class PLC3UTCP
    {
        HslCommunication.Profinet.Melsec.MelsecFxSerialOverTcp plc;

        public bool Connect()
        {


            plc = new HslCommunication.Profinet.Melsec.MelsecFxSerialOverTcp();
            plc.IpAddress = "192.168.1.150";
            plc.Port = 5551;
            plc.ConnectTimeOut = 3000;     // 连接超时，单位毫秒
            plc.ReceiveTimeOut = 5000;     // 接收超时，单位毫秒
            plc.IsNewVersion = true;
            plc.UseGOT = false;
            plc.SleepTime = 20;
            var result = plc.ConnectServer();
            if (result.IsSuccess)
            {
                return true;
            }
            else
            {

                throw new Exception("PLC连接失败！");
            }

        }

        public void WriteData(ReturnWorkOrderInfo returnWorkOrderInfo,string wo)
        {
            int result = int.Parse(returnWorkOrderInfo.speed.Replace("m/min", "").Replace(".", ""));
           var operateResult= plc.Write("D7900", result);
            if (!operateResult.IsSuccess) throw new Exception("写入PLC数据失败1" + operateResult.Message);


            var operateResult2 = plc.Write("D7902", returnWorkOrderInfo.wc_switch_off == "Y" ? 0 : 1);
            if (!operateResult2.IsSuccess) throw new Exception("写入PLC数据失败2" + operateResult2.Message);

            var operateResult3 = plc.Write("D7904", returnWorkOrderInfo.gysx_switch_off == "Y" ? 0 : 1);
            if (!operateResult3.IsSuccess) throw new Exception("写入PLC数据失败3" + operateResult3.Message);

            var operateResult4 = plc.Write("D7930", returnWorkOrderInfo.dept_code);
            if (!operateResult4.IsSuccess) throw new Exception("写入PLC数据失败4" + operateResult4.Message);

            var operateResult5 = plc.Write("D7950", returnWorkOrderInfo.cp_rev);
            if (!operateResult5.IsSuccess) throw new Exception("写入PLC数据失败5" + operateResult5.Message);

            var operateResult6 = plc.Write("D7970", wo);
            if (!operateResult6.IsSuccess) throw new Exception("写入PLC数据失败6"+ operateResult6.Message);

        }

        public List<short> ReadData()
        {
            List<short> shorts = new List<short>();
          var result=  plc.ReadInt16("D7910", 3);
            if (result.IsSuccess)
            {
                shorts.Add(result.Content[0]);
                shorts.Add(result.Content[1]);
                shorts.Add(result.Content[2]);

            }
            return shorts;

        }




    }
}
