using ATestPackagingMachineWpf1.Common;
using ATestPackagingMachineWpf1.DeviceFile.DeviceParameter;
using ATestPackagingMachineWpf1.InterfaceData;
using BTest.Common;
using NationalInstruments.NI4882;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using ZModels;
using static ImTools.ImMap;

namespace ATestPackagingMachineWpf1.DeviceFile
{
    public class HIOKI3570 : DeviceBase
    {

        /// <summary>
        /// 通讯对象
        /// </summary>
        private Device device = null;


        /// <summary>
        /// 是否连续模式
        /// </summary>
        /// 
       
        public bool SetMode { get; set; } 

        /// <summary>
        /// 参数配置
        /// </summary>

        public List<IM3570Prameter> IM3570PrameterList = null;


        public HIOKI3570()
        {

            IM3570PrameterList = new List<IM3570Prameter>();
            DeviceType = DeviceType.GanZhi;

        }

        public override void Conect()
        {
            try
            {

                device = new Device(BoardNumber, PrimaryAddress);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }


        }

        public override string ReadTestData()
        {
            try
            {

                string txt = device.ReadString();
                return txt;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public override void Trigger()
        {
            try
            {
                device.Write("*TRG;" + Environment.NewLine);
                Thread.Sleep(160);
                device.Write(":MEAS? ALL" + Environment.NewLine);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public  void TriggerSingle()
        {
            try
            {
                device.Write("*TRG;" + Environment.NewLine);
                Thread.Sleep(50);
                device.Write(":MEAS? " + Environment.NewLine);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public override void WriteDeviceConfig()
        {

        }

        public void WriteDeviceConfigEX(IM3570Prameter iM3570Prameter)
        {
            try
            {
                if (SetMode)//连续模式
                {

                    device.Write(TextMontage(iM3570Prameter));
                    device.Write(":MODE CONT;:CONTinuous:TRIGger SEQ;");

                }
                else//普通模式模式
                {
                    device.Write(TextMontage(iM3570Prameter));
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
           

        }


        private string TextMontage(IM3570Prameter prameter3570Json)
        {
            //1.每个命令后：开始  2.下个命令;隔开  3.参数要用空格隔开
            StringBuilder sb = new StringBuilder();
            //模式
            sb.Append(":MODE LCR");
            sb.Append(";");
            //参数1
            sb.Append(":PARameter1 " + prameter3570Json.TestParamete11);
            sb.Append(";");
            //参数2
            sb.Append(":PARameter2 " + prameter3570Json.TestParamete12);
            sb.Append(";");
            //参数3
            sb.Append(":PARameter3 " + prameter3570Json.TestParamete13);
            sb.Append(";");
            //参数4
            sb.Append(":PARameter4 " + prameter3570Json.TestParamete14);
            sb.Append(";");
            //比较功能
            sb.Append(":COMParator " + prameter3570Json.COMParator);
            sb.Append(";");
            //速度
            sb.Append(":SPEEd " + prameter3570Json.Speed);
            sb.Append(";");
            ///设置测试频率
            sb.Append(":FREQuency " + prameter3570Json.TestFrequency);
            sb.Append(";");
            //电压
            sb.Append(":LEVel:VOLTage " + prameter3570Json.Voltage);
            sb.Append(";");
            //触发方式
            sb.Append(":TRIGger " + prameter3570Json.TriggerMode);
            sb.Append(";");
            //测量范围
            sb.Append(":RANGe " + prameter3570Json.TestRange);
            sb.Append(";");
            //LS上下限
            sb.Append(":COMPARATOR:FLIMIT:PERcent " + prameter3570Json.LSstandard + "," + prameter3570Json.LSLow + "," + prameter3570Json.LSHIGH);
            sb.Append(";");
            //RS上下限

            if (prameter3570Json.TestParamete13 != "OFF")
            {
                sb.Append(":COMParator:SLIMit:ABSolute " + prameter3570Json.RSLow + "," + prameter3570Json.RSHIGH);
            }
            else
            {
                sb.Append("");
            }

            //开路频率
            sb.Append(";");
            sb.Append(":CORRection:OPEN:FREQuency 1," + Convert.ToInt32(prameter3570Json.OpenHZ).ToString("E"));

            //短路频率
            sb.Append(";");
            sb.Append(":CORRection:SHORt:FREQuency 1," + Convert.ToInt32(prameter3570Json.PortHZ).ToString("E"));
            //设置第一个参数的模式
            sb.Append(";");
            sb.Append(":COMParator:FLIMit:MODE PERcent");

            //设置面板序号
            sb.Append(";");
            sb.Append(":SAVE " + prameter3570Json.PanelId + "," + prameter3570Json.PanelNmae);

            //设置执行
            sb.Append(";");
            sb.Append(":CONTinuous:EXECution " + prameter3570Json.PanelId + "," + prameter3570Json.ExecuteId);
            string result = sb.ToString();
            Console.WriteLine(result);
            return sb.ToString();
        }


        #region 第一组Ls,第二组RS,07066
        /// <summary>
        /// 07066感值测试
        /// </summary>
        public bool DataDeal(string yuanzhi, out decimal testValue)
        {
            bool result = false;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            testValue = 0;

            if (IM3570PrameterList != null)//上下限
            {

                //LS值
                decimal Biaozhunzhi = Convert.ToDecimal(IM3570PrameterList[0].LSstandard) * 1000000;

                //LS1求出下限
                decimal ls1low = Biaozhunzhi + (Biaozhunzhi * IM3570PrameterList[0].LSLow / 100);

                decimal ls1High = Biaozhunzhi + (Biaozhunzhi * IM3570PrameterList[0].LSHIGH / 100);

                //LS值
                InstrumentDataParentClassList[0].LowValue = ls1low;//下限【1】
                InstrumentDataParentClassList[0].HighValue = ls1High;//上限【2】
                InstrumentDataParentClassList[0].Name = "LS";


                //RS上下限

                InstrumentDataParentClassList[1].LowValue = IM3570PrameterList[1].RSLow;
                InstrumentDataParentClassList[1].HighValue = IM3570PrameterList[1].RSHIGH;
                InstrumentDataParentClassList[1].Name = "RS";

            }

            string[] reciveArray = yuanzhi.Split('/');
            string[] singleReciveArray1 = reciveArray[0].Split(',');

            string[] singleReciveArray2 = reciveArray[1].Split(',');

            string LS100K = String.Empty;
            string LS100KJudge = String.Empty;

            string RS1M = String.Empty;
            string RS1MJudge = String.Empty;

            if (singleReciveArray1.Length == 3)
            {
                LS100K = singleReciveArray1[1];
                LS100KJudge = singleReciveArray1[2];

            }
            if (singleReciveArray2.Length == 3)
            {
                RS1M = singleReciveArray2[1];
                RS1MJudge = singleReciveArray2[2].Replace("\n","");

            }

            if (LS100K.Contains("E+28"))
            {
                LS100K = "-999999";
            }

            if (RS1M.Contains("E+28"))
            {
                RS1M = "-999999";
            }
            Decimal conVar = 1000000m;
            testValue = InstrumentDataParentClassList[0].TestValue = Math.Round(InstrumentCommon.ChangeDataToD(LS100K) * conVar, 3);
            InstrumentDataParentClassList[1].TestValue = Math.Round(InstrumentCommon.ChangeDataToD(RS1M) * conVar, 3);



            if (LS100KJudge=="0")
            {
                InstrumentDataParentClassList[0].Judge = "OK";
            }
            else if(LS100KJudge == "1")

            {
                InstrumentDataParentClassList[0].Judge = "HI";

            }
            else if (LS100KJudge == "-1")
            {
                InstrumentDataParentClassList[0].Judge = "LO";
            }
            else
            {
                InstrumentDataParentClassList[0].Judge = "未知";
            }



            if (RS1MJudge == "0")
            {
                InstrumentDataParentClassList[1].Judge = "OK";
            }
            else if (RS1MJudge == "1")

            {
                InstrumentDataParentClassList[1].Judge = "HI";

            }
            else if (RS1MJudge == "-1")
            {
                InstrumentDataParentClassList[1].Judge = "LO";
            }
            else
            {
                InstrumentDataParentClassList[1].Judge = "未知";
            }


            if (LS100KJudge.Contains("0") & RS1MJudge.Contains("0"))
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

            //写入Csv文件
            base.WriteCsv();

            return result;






        }





        #endregion


        #region 双拼频切换

        public IM3570Prameter ReadJsonByPanelId(string panleid)
        {
            if (IM3570PrameterList == null)
            {
                return null;

            }
            IM3570Prameter prameter3570Json = null;
            foreach (var item in IM3570PrameterList)
            {
                if (item.PanelId == panleid.ToString())
                {
                    prameter3570Json = item;
                    break;
                }
            }
            if (prameter3570Json == null)
            {
                return null;
            }
            return prameter3570Json;
        }

        #endregion 读取Json



        #region 单频采集一个LS数据        


        public bool SingleHzAtuoTestLS(string yuanzhi, out decimal testValue)
        {


            bool result = false;
            testValue = 0;
            if (IM3570PrameterList != null)//上下限
            {
                //LS值
                decimal Biaozhunzhi = Convert.ToDecimal(IM3570PrameterList[0].LSstandard) * 1000000;

                //LS1求出下限
                decimal ls1low = Biaozhunzhi + (Biaozhunzhi * IM3570PrameterList[0].LSLow / 100);

                decimal ls1High = Biaozhunzhi + (Biaozhunzhi * IM3570PrameterList[0].LSHIGH / 100);

                //LS值
                InstrumentDataParentClassList[0].LowValue = ls1low;//下限【1】
                InstrumentDataParentClassList[0].HighValue = ls1High;//上限【2】

            }

            if (!yuanzhi.Contains('/'))
            {

                string[] singleReciveArray = yuanzhi.Split(',');

                if (singleReciveArray.Length == 3)
                {

                    string LSvalue = singleReciveArray[1];
                    string LSJudge = singleReciveArray[2];
                    if (LSvalue.Contains("E+28"))
                    {
                        LSvalue = "-999999";
                    }
                    Decimal conVar = 1000000m;

                    testValue = InstrumentDataParentClassList[0].TestValue = Math.Round(InstrumentCommon.ChangeDataToD(LSvalue) * conVar, 5);

                    if (LSJudge.Contains("0"))
                    {
                        InstrumentDataParentClassList[0].Judge = "OK";

                    }
                    else if (LSJudge.Contains("1"))
                    {
                        InstrumentDataParentClassList[0].Judge = "HI";

                    }
                    else if (LSJudge.Contains("-1"))
                    {
                        InstrumentDataParentClassList[0].Judge = "LO";
                    }
                    else
                    {
                        InstrumentDataParentClassList[0].Judge = "未知";
                    }

                    if (LSJudge.Contains("0"))
                    {
                        InstrumentDataParentClassList[0].NumberGoodProducts += 1;
                        result = true;
                    }
                    else
                    {
                        InstrumentDataParentClassList[0].NumberBadProducts += 1;
                        result = false;
                    }

                    InstrumentDataParentClassList[0].TotalNuber = InstrumentDataParentClassList[0].NumberGoodProducts + InstrumentDataParentClassList[0].NumberBadProducts;


                }
                //写入Csv文件
                base.WriteCsv();





            }

            return result;
        }






        #endregion


        #region 单频采集一个LS数据+K值        


        public bool SingleHzAtuoTestLS(string yuanzhi, decimal ganzhi1, decimal ganzhi2, out decimal testValue)
        {


            bool result = false;
            testValue = 0;
            if (IM3570PrameterList != null)//上下限
            {
                //LS值
                decimal Biaozhunzhi = Convert.ToDecimal(IM3570PrameterList[0].LSstandard) * 1000000;

                //LS1求出下限
                decimal ls1low = Biaozhunzhi + (Biaozhunzhi * IM3570PrameterList[0].LSLow / 100);

                decimal ls1High = Biaozhunzhi + (Biaozhunzhi * IM3570PrameterList[0].LSHIGH / 100);

                //LS值
                InstrumentDataParentClassList[0].LowValue = ls1low;//下限【1】
                InstrumentDataParentClassList[0].HighValue = ls1High;//上限【2】
                InstrumentDataParentClassList[0].Name = "LS";
            }

            if (!yuanzhi.Contains('/'))
            {
                double Kvalue = 0;
                string[] singleReciveArray = yuanzhi.Split(',');

                if (singleReciveArray.Length == 3)
                {
                 
                    string LSvalue = singleReciveArray[1];
                    string LSJudge = singleReciveArray[2];
                    if (LSvalue.Contains("E+28"))
                    {
                        LSvalue = "-999999";
                    }
                    Decimal conVar = 1000000m;

                    testValue = InstrumentDataParentClassList[0].TestValue = Math.Round(InstrumentCommon.ChangeDataToD(LSvalue) * conVar, 5);

                    if (LSJudge.Contains("0"))
                    {
                        InstrumentDataParentClassList[0].Judge = "OK";

                    }
                    else if (LSJudge.Contains("1"))
                    {
                        InstrumentDataParentClassList[0].Judge = "HI";

                    }
                    else if (LSJudge.Contains("-1"))
                    {
                        InstrumentDataParentClassList[0].Judge = "LO";
                    }
                    else
                    {
                        InstrumentDataParentClassList[0].Judge = "未知";
                    }

                    if (LSJudge.Contains("0"))
                    {

                        result = true;
                    }
                    else
                    {

                        result = false;
                    }
                }
                if (result)
                {
                
                  
                    InstrumentDataParentClassList[1].Name = "K值";
                    InstrumentDataParentClassList[1].LowValue = Convert.ToDecimal(JsonSaveEXT.deviceParameterJsonGv.KValue);
                    if (ganzhi1 <= 0|| ganzhi2<=0)
                    {
                        Kvalue = 0;
                        InstrumentDataParentClassList[1].TestValue = 0;
                        InstrumentDataParentClassList[1].Judge = "CE";
                        InstrumentDataParentClassList[0].NumberBadProducts += 1;
                      //  PCI1730WriteAndRead.KValueNG();
                        Thread.Sleep(20);
                      //  PCI1730WriteAndRead.KOver1();
                    }
                    else
                    {
                        double MValue = Convert.ToDouble((testValue * 1000) / 2);
                        double SQ = (Math.Sqrt(Convert.ToDouble((ganzhi1 * 1000) * (ganzhi2 * 1000))));
                        Kvalue = MValue / SQ-1;
     
                        InstrumentDataParentClassList[1].TestValue = Convert.ToDecimal(Kvalue);
                        if (Kvalue > JsonSaveEXT.deviceParameterJsonGv.KValue)
                        {
                            InstrumentDataParentClassList[1].Judge = "OK";
                            InstrumentDataParentClassList[0].NumberGoodProducts += 1;
                         //   PCI1730WriteAndRead.KValueOK();
                            Thread.Sleep(20);
                         //   PCI1730WriteAndRead.KOver1();
                        }
                        else
                        {
                            InstrumentDataParentClassList[1].Judge = "CE";
                            InstrumentDataParentClassList[0].NumberBadProducts += 1;
                        //    PCI1730WriteAndRead.KValueNG();
                            Thread.Sleep(20);
                        //    PCI1730WriteAndRead.KOver1();
                        }
                    }
                    
                }
                else
                {
                 //   PCI1730WriteAndRead.KValueNG();
                    Thread.Sleep(20);
               //     PCI1730WriteAndRead.KOver1();
                    InstrumentDataParentClassList[0].NumberBadProducts += 1;
                }
                InstrumentDataParentClassList[0].TotalNuber = InstrumentDataParentClassList[0].NumberGoodProducts + InstrumentDataParentClassList[0].NumberBadProducts;
                //写入Csv文件
                base.WriteCsv();
                Console.WriteLine($"感值1值{ganzhi1 * 1000}  +感值2值{ganzhi2 * 1000}+感值3值{testValue * 1000}+K值{Kvalue}");
            }
            return result;
        }






        #endregion

        #region 单频采集一个LS数据+LS1LS2对比值        


        public bool SingleHzAtuoTestLS_LS2Compare(string yuanzhi, decimal ganzhi1)
        {
            bool result = false;
            decimal ganzhi2Value = 0;
            if (IM3570PrameterList != null)//上下限
            {
                //LS值
                decimal Biaozhunzhi = Convert.ToDecimal(IM3570PrameterList[0].LSstandard) * 1000000;

                //LS1求出下限
                decimal ls1low = Biaozhunzhi + (Biaozhunzhi * IM3570PrameterList[0].LSLow / 100);

                decimal ls1High = Biaozhunzhi + (Biaozhunzhi * IM3570PrameterList[0].LSHIGH / 100);

                //LS值
                InstrumentDataParentClassList[0].LowValue = ls1low;//下限【1】
                InstrumentDataParentClassList[0].HighValue = ls1High;//上限【2】
                InstrumentDataParentClassList[0].Name = "LS";
            }

            if (!yuanzhi.Contains('/'))
            {
           
                string[] singleReciveArray = yuanzhi.Split(',');

                if (singleReciveArray.Length == 3)
                {

                    string LSvalue = singleReciveArray[1];
                    string LSJudge = singleReciveArray[2];
                    if (LSvalue.Contains("E+28"))
                    {
                        LSvalue = "-999999";
                    }
                    Decimal conVar = 1000000m;

                    ganzhi2Value= InstrumentDataParentClassList[0].TestValue = Math.Round(InstrumentCommon.ChangeDataToD(LSvalue) * conVar, 5);

                    if (LSJudge.Contains("0"))
                    {
                        InstrumentDataParentClassList[0].Judge = "OK";

                    }
                    else if (LSJudge.Contains("1"))
                    {
                        InstrumentDataParentClassList[0].Judge = "HI";

                    }
                    else if (LSJudge.Contains("-1"))
                    {
                        InstrumentDataParentClassList[0].Judge = "LO";
                    }
                    else
                    {
                        InstrumentDataParentClassList[0].Judge = "未知";
                    }

                    if (LSJudge.Contains("0"))
                    {

                        result = true;
                    }
                    else
                    {

                        result = false;
                    }
                }
                if (result)
                {


                    InstrumentDataParentClassList[1].Name = "LS1_LS2对比";
                    InstrumentDataParentClassList[1].LowValue = Convert.ToDecimal(JsonSaveEXT.deviceParameterJsonGv.LS1_LS2Low);
                    InstrumentDataParentClassList[1].HighValue = Convert.ToDecimal(JsonSaveEXT.deviceParameterJsonGv.LS1_LS2High);

                    InstrumentDataParentClassList[1].TestValue=   CountLS1_LS2(ganzhi1, ganzhi2Value);
                    if (InstrumentDataParentClassList[1].TestValue> JsonSaveEXT.deviceParameterJsonGv.LS1_LS2Low&& InstrumentDataParentClassList[1].TestValue< JsonSaveEXT.deviceParameterJsonGv.LS1_LS2High)
                    {
                        InstrumentDataParentClassList[1].Judge = "OK";
                        InstrumentDataParentClassList[0].NumberGoodProducts += 1;
                        PCI1730WriteAndRead.LS1LS2Value1();
                        Thread.Sleep(20);
                        PCI1730WriteAndRead.LS1LS2Over1();
                        result = true;
                    }
                    else
                    {
                        InstrumentDataParentClassList[1].Judge = "CE";
                        InstrumentDataParentClassList[0].NumberBadProducts += 1;
                        PCI1730WriteAndRead.LS1LS2Value0();
                        Thread.Sleep(20);
                        PCI1730WriteAndRead.LS1LS2Over1();
                        result = false;
                    }

           

                }
                else
                {
                    PCI1730WriteAndRead.LS1LS2Value0();
                    Thread.Sleep(20);
                    PCI1730WriteAndRead.LS1LS2Over1();
                    InstrumentDataParentClassList[0].NumberBadProducts += 1;
                  
                }
                InstrumentDataParentClassList[0].TotalNuber = InstrumentDataParentClassList[0].NumberGoodProducts + InstrumentDataParentClassList[0].NumberBadProducts;
                //写入Csv文件
                base.WriteCsv();
        
            }
            return result;
        }






        #endregion
        private static decimal CountLS1_LS2(decimal LS1Data, decimal LS1Data2)
        {

            return Math.Round((1 - (LS1Data / LS1Data2)) * 100, 3);


        }


        #region 第一组Ls,第一组RS
        /// <summary>
        /// 07066感值测试
        /// </summary>
        public bool DataDealLSRS(string yuanzhi, out decimal testValue)
        {
            bool result = false;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            testValue = 0;

            if (IM3570PrameterList != null)//上下限
            {

                //LS值
                decimal Biaozhunzhi = Convert.ToDecimal(IM3570PrameterList[0].LSstandard) * 1000000;

                //LS1求出下限
                decimal ls1low = Biaozhunzhi + (Biaozhunzhi * IM3570PrameterList[0].LSLow / 100);

                decimal ls1High = Biaozhunzhi + (Biaozhunzhi * IM3570PrameterList[0].LSHIGH / 100);

                //LS值
                InstrumentDataParentClassList[0].LowValue = ls1low;//下限【1】
                InstrumentDataParentClassList[0].HighValue = ls1High;//上限【2】
                InstrumentDataParentClassList[0].Name = "LS";


                //RS上下限

                InstrumentDataParentClassList[1].LowValue = IM3570PrameterList[0].RSLow;
                InstrumentDataParentClassList[1].HighValue = IM3570PrameterList[0].RSHIGH;
                InstrumentDataParentClassList[1].Name = "RS";

            }

          
            string[] singleReciveArray1 = yuanzhi.Split(',');

        

            string LS100K = String.Empty;
            string LS100KJudge = String.Empty;

            string RS1M = String.Empty;
            string RS1MJudge = String.Empty;

            if (singleReciveArray1.Length == 5)
            {
                LS100K = singleReciveArray1[1];
                LS100KJudge = singleReciveArray1[2];
                RS1M = singleReciveArray1[3];
                RS1MJudge = singleReciveArray1[4].Replace("\n", "");

            }
         
            if (LS100K.Contains("E+28"))
            {
                LS100K = "-999999";
            }

            if (RS1M.Contains("E+28"))
            {
                RS1M = "-999999";
            }
            Decimal conVar = 1000000m;
            testValue = InstrumentDataParentClassList[0].TestValue = Math.Round(InstrumentCommon.ChangeDataToD(LS100K) * conVar, 5);
            InstrumentDataParentClassList[1].TestValue = Math.Round(InstrumentCommon.ChangeDataToD(RS1M) * 1, 5);

            if (LS100KJudge == "0")
            {
                InstrumentDataParentClassList[0].Judge = "OK";
            }
            else if (LS100KJudge == "1")

            {
                InstrumentDataParentClassList[0].Judge = "HI";

            }
            else if (LS100KJudge == "-1")
            {
                InstrumentDataParentClassList[0].Judge = "LO";
            }
            else
            {
                InstrumentDataParentClassList[0].Judge = "未知";
            }



            if (RS1MJudge == "0")
            {
                InstrumentDataParentClassList[1].Judge = "OK";
            }
            else if (RS1MJudge == "1")

            {
                InstrumentDataParentClassList[1].Judge = "HI";

            }
            else if (RS1MJudge == "-1")
            {
                InstrumentDataParentClassList[1].Judge = "LO";
            }
            else
            {
                InstrumentDataParentClassList[1].Judge = "未知";
            }


            if (LS100KJudge.Contains("0") & RS1MJudge.Contains("0"))
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

            //写入Csv文件
            base.WriteCsv();

            return result;






        }





        #endregion

    }
}
