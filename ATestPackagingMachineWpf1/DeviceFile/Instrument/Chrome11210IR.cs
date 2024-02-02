using ATestPackagingMachineWpf1.Common;
using ATestPackagingMachineWpf1.DeviceFile.DeviceParameter;
using BTest.Common;
using BTest.TCPIP;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZModels;

namespace ATestPackagingMachineWpf1.DeviceFile
{
    public class Chrome11210IR : DeviceBase
    {
        TouchSocketHelper touchSocketHelper = new TouchSocketHelper();
        public IR11210Parameter ir11210Parameter = null;
        public Chrome11210IR()
        {
            
            InstrumentDataParentClassList = new ObservableCollection<InstrumentDataParentClass>();
              DeviceType = DeviceType.IR;
            ir11210Parameter = new IR11210Parameter();
        }

        public List<decimal> GetAnalysisData(string yuanzhi)
        {

            List<decimal> list = new List<decimal>();
            if (yuanzhi != "false")
            {


                string[] reciveArray = yuanzhi.Split(',');
                if (reciveArray.Length == 4)
                {
                    list.Add(InstrumentCommon.ChangeDataToD(reciveArray[1]));
                    list.Add(decimal.Parse(reciveArray[3]));

                }

            }


            return list;

        }

        public bool DataDeal(string yuanzhi)
        {
            bool result = false;
            if (ir11210Parameter != null)//上下限
            {
                InstrumentDataParentClassList[0].LowValue = InstrumentCommon.ChangeDataToD(ir11210Parameter.LowerLimit);
                InstrumentDataParentClassList[0].HighValue = InstrumentCommon.ChangeDataToD(ir11210Parameter.HighLimit);
                InstrumentDataParentClassList[0].Name = "IR";
            }

            List<decimal> decimals = GetAnalysisData(yuanzhi);

            if (decimals.Count == 2)
            {

                InstrumentDataParentClassList[0].TestValue = decimals[0];
                if (decimals[1] == 0)
                {
                    InstrumentDataParentClassList[0].Judge = "OK";
                }
                else if (decimals[1] == 4)
                {
                    InstrumentDataParentClassList[0].Judge = "HI";
                }
                else if (decimals[1] == 5)
                {
                    InstrumentDataParentClassList[0].Judge = "LO";
                }
                else
                {
                    InstrumentDataParentClassList[0].Judge = decimals[0].ToString();
                }

                if (decimals[1] == 0)
                {
                    result = true;
                    InstrumentDataParentClassList[0].NumberGoodProducts += 1;
                }
                else
                {
                    result = false;
                    InstrumentDataParentClassList[0].NumberBadProducts += 1;
                }
                InstrumentDataParentClassList[0].TotalNuber = InstrumentDataParentClassList[0].NumberGoodProducts + InstrumentDataParentClassList[0].NumberBadProducts;

                base.WriteCsv();
            }

            return result;
        }




        public override void Conect()
        {
         touchSocketHelper.Connect("192.168.1.100", "60000");
        }



        public override string ReadTestData()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("LCTest:MEASure:FETCh? " + Environment.NewLine);
            string result = touchSocketHelper.Send(stringBuilder.ToString());
            return result;
        }

        public override void WriteDeviceConfig()
        {

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("LCTest:SOURce:VOLTage " + ir11210Parameter.Voltage + Environment.NewLine);
            stringBuilder.Append("CALCulate:CONDition[:LCT]:LOWer:DATA " + ir11210Parameter.LowerLimit + Environment.NewLine);
            stringBuilder.Append("CALCulate:CONDition[:LCT]:UPPer:DATA " + ir11210Parameter.HighLimit + Environment.NewLine);
            stringBuilder.Append("LCTest:CONFigure:TIME:TEST " + ir11210Parameter.TestTime + Environment.NewLine);
            stringBuilder.Append("TRIGger:SOURce " + ir11210Parameter.TriggerMode + Environment.NewLine);
            touchSocketHelper.SendOnly(stringBuilder.ToString());
        }

        public override void Trigger()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("TRIGger:IMMediate " + Environment.NewLine);
            touchSocketHelper.SendOnly(stringBuilder.ToString());

        }


        public string TestState()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("LCTest:MEASure:STATe? " + Environment.NewLine);
            string result = touchSocketHelper.Send(stringBuilder.ToString());
            //Console.WriteLine(result);
            return result;
        }
    }
}
