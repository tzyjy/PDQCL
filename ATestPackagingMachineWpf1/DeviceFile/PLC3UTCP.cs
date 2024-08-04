using ATestPackagingMachineWpf1.ZModels;
using BTest.LogHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TouchSocket.Core;
using ZModels;

namespace ATestPackagingMachineWpf1.DeviceFile
{
    public class PLC3UTCP
    {
        public HslCommunication.Profinet.Melsec.MelsecFxSerialOverTcp plc;
        public bool ConnectSucess { get; set; }

        public bool Connect(string IP, int port)
        {
            plc = new HslCommunication.Profinet.Melsec.MelsecFxSerialOverTcp();
            plc.IpAddress = IP;
            plc.Port = port;
            plc.ConnectTimeOut = 3000;     // 连接超时，单位毫秒
            plc.ReceiveTimeOut = 5000;     // 接收超时，单位毫秒
            plc.IsNewVersion = true;
            plc.UseGOT = false;
            plc.SleepTime = 20;
            var result = plc.ConnectServer();
            if (result.IsSuccess)
            {
                ConnectSucess = true;
                return true;
            }
            else
            {
                ConnectSucess = false;
                throw new Exception("PLC连接失败！");
            }
        }

        public short ReadSpeedData()
        {
            short data = 0;
            var result = plc.ReadInt16("D7010");
            if (result.IsSuccess)
            {
                data = result.Content;
            }
            return data;
        }

        public void WriteAalrm()
        {
            var result = plc.Write("D7011", 1);
            if (!result.IsSuccess) throw new Exception("写入D地址数据失败" + result.Message);
        }

        public void WriteData(string address)
        {
            var operateResult = plc.Write(address, 1);
            if (!operateResult.IsSuccess) throw new Exception("写入D地址数据失败" + operateResult.Message);
            Thread.Sleep(500);
            var operateResult2 = plc.Write(address, 0);
            if (!operateResult2.IsSuccess) throw new Exception("写入D地址数据失败" + operateResult.Message);
        }

        public void WriteIntData(string address, short value)
        {
            var operateResult = plc.Write(address, value);
            if (!operateResult.IsSuccess) throw new Exception("写入D地址数据失败" + operateResult.Message);
        }

        public void WriteData(ReturnWorkOrderInfo returnWorkOrderInfo, string wo)
        {
            float result = float.Parse(returnWorkOrderInfo.speed.Replace("m/min", ""));
            var operateResult = plc.Write("D7900", result);
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
            if (!operateResult6.IsSuccess) throw new Exception("写入PLC数据失败6" + operateResult6.Message);
        }

        public List<short> ReadData()
        {
            List<short> shorts = new List<short>();
            var result = plc.ReadInt16("D7910", 3);
            if (result.IsSuccess)
            {
                shorts.Add(result.Content[0]);
                shorts.Add(result.Content[1]);
                shorts.Add(result.Content[2]);
            }
            return shorts;
        }

        public ReadDataInfo ReadPLCData()
        {
            ReadDataInfo readDataInfo = new ReadDataInfo();

            var result0 = plc.ReadFloat("D7800");
            if (result0.IsSuccess)
            {
                readDataInfo.CDSpeedXF = result0.Content;
            }
            else
            {
                LOG.WriteLog("读取PLC数据失败！D7800" + result0.Message);
            }
            Thread.Sleep(20);

            var result = plc.ReadInt32("D7802", 2);
            if (result.IsSuccess)
            {
                readDataInfo.WXUseXF = result.Content[0];
                readDataInfo.GYUseXF = result.Content[1];
            }
            else
            {
                LOG.WriteLog("读取PLC数据失败！D7900" + result.Message);
            }

            Thread.Sleep(20);

            var result2 = plc.ReadInt16("D2055", 4);
            if (result2.IsSuccess)
            {
                readDataInfo.WS1 = result2.Content[0] == 1 ? true : false;
                readDataInfo.WS2 = result2.Content[1] == 1 ? true : false;
                readDataInfo.GYSX1 = result2.Content[2] == 1 ? true : false;
                readDataInfo.GYSX2 = result2.Content[3] == 1 ? true : false;
            }
            else
            {
                LOG.WriteLog("读取D2055数据失败！" + result2.Message);
            }

            Thread.Sleep(20);

            var result4 = plc.ReadFloat("D2050", 2);
            if (result4.IsSuccess)
            {
                readDataInfo.CDSpeed = result4.Content[0];
                readDataInfo.PSSpeed = result4.Content[1];
            }
            else
            {
                LOG.WriteLog("读取PLC数据失败！D2050" + result4.Message);
            }

            Thread.Sleep(20);

            return readDataInfo;
        }
    }
}