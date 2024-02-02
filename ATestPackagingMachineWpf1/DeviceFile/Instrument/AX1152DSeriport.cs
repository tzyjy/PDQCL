using ATestPackagingMachineWpf1.Common;
using ATestPackagingMachineWpf1.DeviceFile.DeviceParameter;
using ATestPackagingMachineWpf1.DeviceFile.Mes;
using BTest.Seriport;
using NationalInstruments.NI4882;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZModels;

namespace ATestPackagingMachineWpf1.DeviceFile
{
    public class AX1152DSeriport : DeviceBase
    {


        public AX11520DParameter aX11520DParameter = null;
        SerialPortHelper serialPortHelper =new SerialPortHelper();
        public string PortName { get; set; }
        public AX1152DSeriport()
        {
            
            InstrumentDataParentClassList = new ObservableCollection<InstrumentDataParentClass>();
                       aX11520DParameter =new AX11520DParameter();
            DeviceType = DeviceType.DCR;

        }
        public override void Conect()
        {
            try
            {
                serialPortHelper.Connect(9600, PortName, 8, Parity.None.ToString(), StopBits.One.ToString());
              
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        
         
        }

    

        public override string ReadTestData()
        {
            SW.Restart();
            Thread.Sleep(115);
            string result = serialPortHelper.Recive();
            SW.Stop();
            this.ReadMS = SW.ElapsedMilliseconds;
            return result;

        }

        public override void WriteDeviceConfig()
        {
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                stringBuilder.Append(Get1152DCommand(ParameterType.TestMode, aX11520DParameter));
                stringBuilder.Append(Get1152DCommand(ParameterType.TestScale, aX11520DParameter));
                stringBuilder.Append(Get1152DCommand(ParameterType.DCRLow, aX11520DParameter));
                stringBuilder.Append(Get1152DCommand(ParameterType.DCRHigh, aX11520DParameter));
                stringBuilder.Append(Get1152DCommand(ParameterType.TriggerMode, aX11520DParameter));
                stringBuilder.Append(Get1152DCommand(ParameterType.TestSpeed, aX11520DParameter));
                stringBuilder.Append(Get1152DCommand(ParameterType.BuzzingMode, aX11520DParameter));

                serialPortHelper.WriteText(stringBuilder.ToString());
                OpenRMT();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        #region 数据处理
        public bool DataDeal(string yuanzhi)
        {
            bool result = false;

            try
            {
             
                List<decimal> decimals = InstrumentCommon.LowHighShow(aX11520DParameter.TestScale, aX11520DParameter.DCRLow, aX11520DParameter.DCRHigh);
                InstrumentDataParentClassList[0].Name = "DCR";
                InstrumentDataParentClassList[0].LowValue = decimals[0];
                InstrumentDataParentClassList[0].HighValue = decimals[1];
                if (yuanzhi.Length<5)
                {
                    throw new Exception("DCR原值测试数据不对，请检查！");
                }
                string a = yuanzhi.Replace("XR", "");
                string value = a.Substring(0, a.IndexOf(","));
                string judge = a.Substring(a.IndexOf(",") + 1).Replace("\r\n", "");
                decimal number = Convert.ToDecimal(InstrumentCommon.ChangeDataToD(value) * 1m);
                InstrumentDataParentClassList[0].TestValue = number;//测试值【3】


                if (judge.Contains("GO"))
                {
                    InstrumentDataParentClassList[0].Judge = "OK";
                }
                else if (judge.Contains("HI"))
                {
                    InstrumentDataParentClassList[0].Judge = "HI";
                }
                else if (judge.Contains("LO"))
                {
                    InstrumentDataParentClassList[0].Judge = "LO";
                }

                else
                {
                    InstrumentDataParentClassList[0].Judge = "CE";
                }


                if (judge.Contains("GO"))
                {
                    result = true;
                    InstrumentDataParentClassList[0].NumberGoodProducts += 1;

                }
                else
                {
                    InstrumentDataParentClassList[0].NumberBadProducts += 1;
                    result = false;
                }
                InstrumentDataParentClassList[0].TotalNuber = InstrumentDataParentClassList[0].NumberGoodProducts + InstrumentDataParentClassList[0].NumberBadProducts; ;//总数【5】

                //写入Csv文件
                base.WriteCsv();

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message + "请重启软件！");
            }

            return result;
        }
        #endregion

        /// <summary>
        /// 触发
        /// </summary>
        public override void Trigger()
        {

            byte[] SendBuf = new byte[] { 0x45, 0x0D, 0x0A, 0x1B, 0x44, 0x0D, 0x0A };
            serialPortHelper.Write(SendBuf);
        }

        
        #region 开启RMT

        public byte[] OpenRMT()
        {

            //开启RMT
            byte[] SendBuf = new byte[] { 0x1B, 0x52, 0x0D, 0x0A };

            serialPortHelper.Write(SendBuf);

            return SendBuf;

        }

        #endregion


        #region AX1152D_扩展


        #region 字符串解析
        public string Get1152DCommand(ParameterType parameterType, AX11520DParameter aD1520XParameter)
        {

            string commandText = string.Empty;
            switch (parameterType)
            {
                case ParameterType.TestMode:
                    commandText = "F" + aD1520XParameter.TestMode;
                    break;

                case ParameterType.TestScale:
                    commandText = aD1520XParameter.TestScale;
                    break;

                case ParameterType.DCRLow:

                    commandText = LowHighCount(aD1520XParameter.DCRLow.ToString(), "L", aD1520XParameter.TestMode);
                    break;

                case ParameterType.DCRHigh:
                    commandText = LowHighCount(aD1520XParameter.DCRHigh.ToString(), "H", aD1520XParameter.TestMode);
                    break;

                case ParameterType.TriggerMode:
                    commandText = "T" + aD1520XParameter.TriggerMode;

                    break;

                case ParameterType.TestSpeed:
                    commandText = "W" + aD1520XParameter.TestSpeed;
                    break;

                case ParameterType.BuzzingMode:
                    commandText = "B" + aD1520XParameter.BuzzingMode + Environment.NewLine;
                    break;

                case ParameterType.FeedbackInformation:
                    break;

                default:
                    break;
            }

            return commandText;
        }

        private string LowHighCount(string value, string isHighorLow, string testMode)
        {
            string commandText;
            StringBuilder stringBuilder = new StringBuilder();
            string testmodeHead = testMode == "R" ? "L" : "D";
            stringBuilder.Append(testmodeHead);
            stringBuilder.Append(isHighorLow);
            stringBuilder.Append(value.PadLeft(5, '0'));
            commandText = stringBuilder.ToString();
            return commandText;

        }
        #endregion

    



        #endregion
    }
}
