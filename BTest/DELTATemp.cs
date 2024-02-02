using BTest.LogHelper;
using HslCommunication;
using HslCommunication.ModBus;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTest
{
    public class DELTATemp
    {
        HslCommunication.ModBus.ModbusAscii modbus = new HslCommunication.ModBus.ModbusAscii();
     
        /// <summary>
        /// 端口号
        /// </summary>
        public string PortName { get; set; }

        /// <summary>
        /// 波特率
        /// </summary>
        public int BaudRate { get; set; }
        /// <summary>
        /// 数据位
        /// </summary>
        public int DataBits { get; set; }
        /// <summary>
        /// 停止位
        /// </summary>
        public StopBits StopBits { get; set; }
        /// <summary>
        /// 校验
        /// </summary>
        public Parity Parity { get; set; }

        public DELTATemp( string portName, int baudRate, int dataBits, StopBits stopBits, Parity parity)
        {
            
            PortName = portName;
            BaudRate = baudRate;
            DataBits = dataBits;
            StopBits = stopBits;
            Parity = parity;
        }

        public void Connect()
        {
            try
            {
                modbus.SerialPortInni(sp =>
                {
                    sp.PortName = PortName;
                    sp.BaudRate = BaudRate;
                    sp.DataBits = DataBits;
                    sp.StopBits = StopBits;
                    sp.Parity = Parity;
                    sp.RtsEnable = false;
                });
                modbus.ReceiveTimeout = 5000;   // 接收超时，单位毫秒
                modbus.AddressStartWithZero = true;
                modbus.IsStringReverse = false;
                modbus.DataFormat = HslCommunication.Core.DataFormat.ABCD;
                modbus.Station = 1;
           


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
           
        }
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <returns></returns>
        public float Read()
        {
            OperateResult<ushort> operateResult = modbus.ReadUInt16("18176");
            if (operateResult.IsSuccess)
            {
                return operateResult.Content / 10;
            }
            else
            {
                return 0;
            }

        }

        /// <summary>
        /// 写入数据
        /// </summary>
        public bool Write(Dictionary<string, short> keyValuePairs1)
        {
      

            bool result=false;
            foreach (var item in keyValuePairs1)
            {
                OperateResult operateResult = modbus.Write(item.Key, (short)(item.Value*10));
                if (operateResult.IsSuccess)
                {
                    result &= true;
                }
                else
                {
                    result &= false;


                }
            }
         return result;
            

        }
    }
}
