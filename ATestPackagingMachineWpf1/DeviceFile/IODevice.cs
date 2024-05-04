using BTest.TCPIP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ATestPackagingMachineWpf1.DeviceFile
{
    public class IODevice
    {

        HslCommunication.ModBus.ModbusTcpNet modbus;


        public IODevice()
        {
           
        }

        public void Connect()
        {
            modbus = new HslCommunication.ModBus.ModbusTcpNet();
            modbus.IpAddress = "192.168.1.77";
            modbus.Port = 502;
            modbus.ConnectTimeOut = 10000;     // 连接超时，单位毫秒
            modbus.ReceiveTimeOut = 5000;     // 接收超时，单位毫秒
            modbus.Station = 1;
            modbus.AddressStartWithZero = true;
            modbus.IsCheckMessageId = true;
            modbus.IsStringReverse = false;
            modbus.DataFormat = HslCommunication.Core.DataFormat.CDAB;
            var result= modbus.ConnectServer();
            if (!result.IsSuccess)
            {
                throw new Exception("IO板卡连接失败！");
            }

        }

        public void Send(string address,bool Value)
        {
            modbus.Write(address, true);
            Thread.Sleep(800);
            modbus.Write(address, false);
        }

        public List<bool> ReadDI()
        {
            List<bool> bools = new List<bool>();
            HslCommunication.OperateResult<bool[]> PressureList = modbus.ReadBool("x=2;0",8);
            if (PressureList.IsSuccess)
            {
                bools.Add(PressureList.Content[0]);
                bools.Add(PressureList.Content[1]);
                bools.Add(PressureList.Content[2]);
                bools.Add(PressureList.Content[3]);
                bools.Add(PressureList.Content[4]);
                bools.Add(PressureList.Content[5]);
                bools.Add(PressureList.Content[6]);
                bools.Add(PressureList.Content[7]);
            }
            return bools;
            
        }







    }
}
