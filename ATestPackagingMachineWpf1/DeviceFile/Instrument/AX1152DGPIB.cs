using ATestPackagingMachineWpf1.DeviceFile.DeviceParameter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalInstruments.NI4882;
using ATestPackagingMachineWpf1.DeviceFile.Mes;

namespace ATestPackagingMachineWpf1.DeviceFile
{
    public class AX1152DGPIB : DeviceBase
    {
        #region 定义变量
        public AX11520DParameter aX11520DParameter = null;

        private NationalInstruments.NI4882.Device device = null;

        #endregion

        public AX1152DGPIB()
        {
            aX11520DParameter = new AX11520DParameter();
            DeviceType = DeviceType.DCR;
        }

        public override void Conect()
        {
            try
            {
                device = new Device(this.BoardNumber, this.PrimaryAddress);
              
            }
            catch (Exception)
            {

            }
        }
      

        public override string ReadTestData()
        {
            SW.Restart();
            try
            {
                string txt = device.ReadString();
                SW.Stop();
                this.ReadMS = SW.ElapsedMilliseconds;
                return txt;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
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

                device.Write(stringBuilder.ToString());
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        #region 数据处理
        #region 数据处理
        public bool DataDeal(string yuanzhi)
        {
          
            bool result = false;
            List<decimal> decimals = InstrumentCommon.LowHighShow(aX11520DParameter.TestScale, aX11520DParameter.DCRLow, aX11520DParameter.DCRHigh);
            InstrumentDataParentClassList[0].LowValue = decimals[0];
            InstrumentDataParentClassList[0].HighValue = decimals[1];

            try
            {
                string a = yuanzhi.Replace("XR", "");
                string value = a.Substring(0, a.IndexOf(","));
                string judge = a.Substring(a.IndexOf(",") + 1).Replace("\r\n", "");
                decimal number = Convert.ToDecimal(InstrumentCommon.ChangeDataToD(value) * 1m);
                InstrumentDataParentClassList[1].TestValue = number;//测试值【3】
              

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
        #endregion

        #region DCR触发
        public override void Trigger()
        {
            SW.Restart();
            try
            {
                device.Write("E" + Environment.NewLine);
                SW.Stop();
                this.WriteMS = SW.ElapsedMilliseconds;
              

            }
            catch (Exception ex)
            {
              
                throw new Exception(ex.Message);
            }


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

        #region 数据处理
        public bool DataDeal2(string yuanzhi1, string yuanzhi2, AX1152DGPIB aX1152DGPIB1, AX1152DGPIB aX1152DGPIB2, out List<decimal> datalist)
        {
            datalist = new List<decimal>();
            bool result = false;
            List<decimal> decimals = InstrumentCommon.LowHighShow(aX1152DGPIB1.aX11520DParameter.TestScale, aX1152DGPIB1.aX11520DParameter.DCRLow, aX1152DGPIB1.aX11520DParameter.DCRHigh);

            InstrumentDataParentClassList[0].LowValue = decimals[0];
            InstrumentDataParentClassList[0].HighValue = decimals[1];

            List<decimal> decimals2 = InstrumentCommon.LowHighShow(aX1152DGPIB2.aX11520DParameter.TestScale, aX1152DGPIB2.aX11520DParameter.DCRLow, aX1152DGPIB2.aX11520DParameter.DCRHigh);


            InstrumentDataParentClassList[1].LowValue = decimals2[0];//下限【1】
            InstrumentDataParentClassList[1].HighValue = decimals2[1];//上限【2】
            try
            {
                if (yuanzhi1.Length<3)
                {
                    throw new Exception("DCR测试无值异常，请检查");
                }
                string a1 = yuanzhi1.Replace("XR", "");
                string value = a1.Substring(0, a1.IndexOf(","));
                string judge = a1.Substring(a1.IndexOf(",") + 1).Replace("\r\n", "");
                decimal number = Convert.ToDecimal(InstrumentCommon.ChangeDataToD(value) * 1m);
                InstrumentDataParentClassList[0].TestValue = number;//测试值【3】



                string a2 = yuanzhi2.Replace("XR", "");
                string value2 = a2.Substring(0, a2.IndexOf(","));
                string judge2 = a2.Substring(a2.IndexOf(",") + 1).Replace("\r\n", "");
                decimal number2 = Convert.ToDecimal(InstrumentCommon.ChangeDataToD(value2) * 1m);
                InstrumentDataParentClassList[1].TestValue = number2;//测试值【3】

                datalist.Add(number);
                datalist.Add(number2);


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

                if (judge2.Contains("GO"))
                {
                    InstrumentDataParentClassList[1].Judge = "OK";
                }
                else if (judge2.Contains("HI"))
                {

                    InstrumentDataParentClassList[1].Judge = "HI";


                }
                else if (judge2.Contains("LO"))
                {

                    InstrumentDataParentClassList[1].Judge = "LO";


                }

                else
                {

                    InstrumentDataParentClassList[1].Judge = "CE";


                }


                if (judge.Contains("GO") && judge2.Contains("GO"))
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



    }


    #region 参数枚举类型

    public enum ParameterType
    {
        TestMode,
        TestScale,
        DCRLow,
        DCRHigh,
        TriggerMode,
        TestSpeed,
        BuzzingMode,
        FeedbackInformation
    }

    #endregion 参数枚举类型
}
