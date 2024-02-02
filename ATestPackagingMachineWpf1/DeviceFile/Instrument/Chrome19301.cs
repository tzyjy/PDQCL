using ATestPackagingMachineWpf1.DeviceFile.DeviceParameter;
using BTest.Seriport;
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
    public class Chrome19301 : DeviceBase
    {
        SerialPortHelper serialPortHelper = new SerialPortHelper();

        public Wave19301Parameter wave19301Parameter = null;
        public Chrome19301()
        {

            InstrumentDataParentClassList = new ObservableCollection<InstrumentDataParentClass>();
            wave19301Parameter = new Wave19301Parameter();
            DeviceType = DeviceType.BoXing;

        }
        public override void Conect()
        {

            bool isSucess = serialPortHelper.Connect(9600, "COM3", 8, Parity.None.ToString(), StopBits.One.ToString());

         
        }

        public override string ReadTestData()
        {
            throw new NotImplementedException();
        }

        public override void Trigger()
        {
            throw new NotImplementedException();
        }

        public override void WriteDeviceConfig()
        {
            var wave19301Parameter = this.wave19301Parameter;

            string commandtext1 = Get1152DCommand(ParameterWaveType.Voltage, wave19301Parameter.Voltage.ToString()); //测试电压
            serialPortHelper.WriteText(commandtext1);

            string commandtext2 = Get1152DCommand(ParameterWaveType.AreaHigh, wave19301Parameter.AreaHigh.ToString()); //面积上限
            serialPortHelper.WriteText(commandtext2);

            string commandtext3 = Get1152DCommand(ParameterWaveType.AreaLow, wave19301Parameter.AreaLow.ToString()); //面积下限
            serialPortHelper.WriteText(commandtext3);

            string commandtext4 = Get1152DCommand(ParameterWaveType.PULSe, wave19301Parameter.PULSe.ToString()); //脉冲次数
            serialPortHelper.WriteText(commandtext4);

            string commandtext5 = Get1152DCommand(ParameterWaveType.LAPLac, wave19301Parameter.LAPLac.ToString()); //二次微分
            serialPortHelper.WriteText(commandtext5);

        }


        #region 发送测试指令

        public bool TestIDN()
        {

            serialPortHelper.WriteText("*IDN?" + Environment.NewLine);
            Thread.Sleep(150);
            string result = serialPortHelper.Recive();
            if (result.Contains("Chroma"))
            {
                return true;
            }
            else
            {
                return false;
            }

        }



        #endregion


        private string Get1152DCommand(ParameterWaveType voltage, string value)
        {
            string commandText = string.Empty;
            switch (voltage)
            {
                case ParameterWaveType.Voltage:

                    commandText = "SOURce:SAFety:STEP:IWT:LEV " + value + Environment.NewLine;
                    break;
                case ParameterWaveType.AreaHigh:
                    commandText = ":SOURce:SAFety:STEP:MAIN:IWT:AREA:LIMit:PLUS " + value + Environment.NewLine;
                    break;
                case ParameterWaveType.AreaLow:
                    commandText = ":SOURce:SAFety:STEP:MAIN:IWT:AREA:LIMit:MINus " + value + Environment.NewLine;
                    break;
                case ParameterWaveType.PULSe:
                    commandText = ":SOURce:SAFety:STEP:MAIN:IWT:PULSe " + value + Environment.NewLine;
                    break;
                case ParameterWaveType.LAPLac:
                    commandText = ":SOURce:SAFety:STEP:MAIN:IWT:LAPLac:LIMit " + value + Environment.NewLine;
                    break;

                default:
                    break;

            }

            return commandText;

        }


        public void DealData(bool testValue)
        {
            InstrumentDataParentClassList[0].LowValue = Convert.ToDecimal(wave19301Parameter.AreaLow) * 100m;
            InstrumentDataParentClassList[0].HighValue = Convert.ToDecimal(wave19301Parameter.AreaHigh) * 100m;
            InstrumentDataParentClassList[0].TestValue = 1;
            InstrumentDataParentClassList[0].Name = "波形";
            if (testValue)
            {
                InstrumentDataParentClassList[0].Judge = "OK";
                InstrumentDataParentClassList[0].NumberGoodProducts += 1;
            }
            else
            {
                InstrumentDataParentClassList[0].Judge = "NG";
                InstrumentDataParentClassList[0].NumberBadProducts += 1;
            }
            InstrumentDataParentClassList[0].TotalNuber = InstrumentDataParentClassList[0].NumberGoodProducts + InstrumentDataParentClassList[0].NumberBadProducts;

        }
    }

    public enum ParameterWaveType
    {
        Voltage,
        AreaHigh,
        AreaLow,
        PULSe,
        LAPLac

    }
}
