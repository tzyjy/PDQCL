using ATestPackagingMachineWpf1.Common;
using ATestPackagingMachineWpf1.DeviceFile.CCD;
using ATestPackagingMachineWpf1.DeviceFile.DeviceParameter;
using ATestPackagingMachineWpf1.DeviceFile.Instrument;
using ATestPackagingMachineWpf1.DeviceFile.Mes;
using ATestPackagingMachineWpf1.DeviceFile.PLCFile;
using Automation.BDaq;
using BTest;

using BTest.TCPIP;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZModels;

namespace ATestPackagingMachineWpf1.DeviceFile
{
    /// <summary>
    /// 配置信息
    /// </summary>
    public class DV
    {



        public static string Json_DeviceList = "Json\\DeviceList.json";
        public static List<DeviceBase> ListDeviceBase;

        public static AX1152DGPIB DCR1;

        public static AX1152DGPIB DCR2;

        public static AX1152DGPIB DCR3;

        public static AX1152DGPIB DCR4;

        public static Chrome11210IR IR;

        public static HIOKI3570 GanZhi1;

        public static HIOKI3570 GanZhi2;

        public static InstrumentE5071C SZhi;

        public static SpareInstruMent ZZhi;

        public static SpareInstruMent DMianCCD;


        public static AdvantechPCI1730Device IO1730;//IO板卡

        public static CCDDevice CCD;//相机字符

        public static DELTATemp DELTATemp1;//温度1

        public static DELTATemp DELTATemp2;//温度2

        public static PLCBase PLC5U; //PLC

        //public static DELTATemp dELTATemp1 = new DELTATemp();

        public static void LoadListDeviceBase()
        {
            try
            {
                if (!File.Exists(Json_DeviceList))
                {
                    ListDeviceBase = new List<DeviceBase>();

                    ZZhi =new SpareInstruMent();
                    ZZhi.DeviceType = DeviceType.Spare;
                    ZZhi.Name = "ZZhi1";

                    DMianCCD = new SpareInstruMent();
                    DMianCCD.DeviceType = DeviceType.Spare;
                    DMianCCD.Name = "DMianCCD";

                    DCR1 = new AX1152DGPIB();
                    DCR1.aX11520DParameter = new AX11520DParameter()
                    {
                        TestMode = "R",
                        TestScale = "R1",
                        DCRLow = 100,
                        DCRHigh = 13000,
                        TriggerMode = "0",
                        TestSpeed = "1",
                        BuzzingMode = "1",
                        FeedbackInformation = "1",



                    };
                    DCR1.BoardNumber = 0;
                    DCR1.PrimaryAddress = 1;
                   
                    DCR1.InstrumentDataParentClassList = new ObservableCollection<InstrumentDataParentClass>() { new InstrumentDataParentClass()
                    {
                          Name="DCR1",
                           LowValue=0,
                            HighValue=0,
                             Judge="OK",
                              NumberBadProducts=1,
                               NumberGoodProducts=99,
                                TestValue=0,
                                 TotalNuber=100



                    }, new InstrumentDataParentClass()
                    {
                      Name="DCR2",
                           LowValue=0,
                            HighValue=0,
                             Judge="OK",
                              NumberBadProducts=0,
                               NumberGoodProducts=0,
                                TestValue=0,
                                 TotalNuber=0

                    }};
                    DCR1.Name = "DCR1";
                    DCR1.Conect();









                    DCR2 = new AX1152DGPIB();
                    DCR2.aX11520DParameter = new AX11520DParameter()
                    {
                        TestMode = "R",
                        TestScale = "R1",
                        DCRLow = 100,
                        DCRHigh = 13000,
                        TriggerMode = "0",
                        TestSpeed = "1",
                        BuzzingMode = "1",
                        FeedbackInformation = "1",



                    };
                    DCR2.Name = "DCR2";
                    DCR2.BoardNumber = 0;
                    DCR2.PrimaryAddress = 2;
                    DCR2.Conect();





                    DCR3 = new AX1152DGPIB();
                    DCR3.aX11520DParameter = new AX11520DParameter()
                    {
                        TestMode = "R",
                        TestScale = "R1",
                        DCRLow = 100,
                        DCRHigh = 13000,
                        TriggerMode = "0",
                        TestSpeed = "1",
                        BuzzingMode = "1",
                        FeedbackInformation = "1",



                    };
                    DCR3.BoardNumber = 0;
                    DCR3.PrimaryAddress = 3;
               
                    DCR3.InstrumentDataParentClassList = new ObservableCollection<InstrumentDataParentClass>() { new InstrumentDataParentClass() {
                       Name="DCR3",
                           LowValue=0,
                            HighValue=0,
                             Judge="OK",
                              NumberBadProducts=4,
                               NumberGoodProducts=95,
                                TestValue=0,
                                 TotalNuber=99

                    }, new InstrumentDataParentClass() {

                       Name="DCR4",
                           LowValue=0,
                            HighValue=0,
                             Judge="OK",
                              NumberBadProducts=0,
                               NumberGoodProducts=0,
                                TestValue=0,
                                 TotalNuber=0} };


                    DCR3.Name = "DCR3";
                    DCR3.Conect();











                    DCR4 = new AX1152DGPIB();
                    DCR4.aX11520DParameter = new AX11520DParameter()
                    {
                        TestMode = "R",
                        TestScale = "R1",
                        DCRLow = 100,
                        DCRHigh = 13000,
                        TriggerMode = "0",
                        TestSpeed = "1",
                        BuzzingMode = "1",
                        FeedbackInformation = "1",



                    };
                    DCR4.Name = "DCR4";
                    DCR4.BoardNumber = 0;
                    DCR4.PrimaryAddress = 4;
                    DCR4.Conect();









                    IR = new Chrome11210IR();
                    IR.ir11210Parameter = new IR11210Parameter()
                    {
                        Voltage = "400",
                        LowerLimit = "100000000",
                        HighLimit = "9.000000e+014",
                        TestTime = 0.2m,
                        TriggerMode = "0",
                    };
                    IR.InstrumentDataParentClassList = new ObservableCollection<InstrumentDataParentClass>() { new InstrumentDataParentClass()
                    {
                           Name="IR",
                           LowValue=0,
                            HighValue=0,
                             Judge="OK",
                              NumberBadProducts=4,
                               NumberGoodProducts=91,
                                TestValue=0,
                                 TotalNuber=95
                    }
                    };
                  
                    IR.Name = "IR";


                    GanZhi1 = new HIOKI3570();
                    GanZhi1.IM3570PrameterList = new List<IM3570Prameter>()
                    {

                        new IM3570Prameter()
                        {
                    TestParamete11 = "LS",
                    TestParamete12 = "OFF",
                    TestParamete13 ="OFF",
                    TestParamete14 = "OFF",
                    COMParator = "ON",
                    Speed = "FAST",
                    TestFrequency = "1000000",
                    ExecuteId = "ON",
                    LSLow = -24.0m,
                    LSHIGH =24.0m,
                    LSstandard = "0.000000080",
                    RSHIGH = 0.4m,
                    RSLow = 0.0m,
                    OpenHZ = "4500000",
                    PortHZ = "4500000",
                    PanelNmae = "1",
                    PanelId = "1",
                    TestRange ="3",
                    TestSignal ="V",
                    TriggerMode = "EXTernal",
                    TriggerTime ="0",
                    Voltage = "1",

                        },
                              new IM3570Prameter()
                        {
                    TestParamete11 = "LS",
                    TestParamete12 = "OFF",
                    TestParamete13 ="OFF",
                    TestParamete14 = "OFF",
                    COMParator = "ON",
                    Speed = "FAST",
                    TestFrequency = "1000000",
                    ExecuteId = "ON",
                    LSLow = -24.0m,
                    LSHIGH =24.0m,
                    LSstandard = "0.000000080",
                    RSHIGH = 0.4m,
                    RSLow = 0.0m,
                    OpenHZ = "4500000",
                    PortHZ = "4500000",
                    PanelNmae = "1",
                    PanelId = "1",
                    TestRange ="3",
                    TestSignal ="V",
                    TriggerMode = "EXTernal",
                    TriggerTime ="0",
                    Voltage = "1",

                        }








                    };



                    GanZhi1.BoardNumber = 0;
                    GanZhi1.InstrumentDataParentClassList = new ObservableCollection<InstrumentDataParentClass>() { new InstrumentDataParentClass()

                    {
                         Name="LS1",
                           LowValue=0,
                            HighValue=0,
                             Judge="OK",
                              NumberBadProducts=4,
                               NumberGoodProducts=87,
                                TestValue=0,
                                 TotalNuber=91




                    } };
                    GanZhi1.PrimaryAddress = 5;
                    GanZhi1.Name = "GanZhi1";
                 
                    GanZhi1.Conect();

                    GanZhi2 = new HIOKI3570();
                    GanZhi2.IM3570PrameterList = new List<IM3570Prameter>()
                    {

                        new IM3570Prameter()
                        {
                    TestParamete11 = "LS",
                    TestParamete12 = "OFF",
                    TestParamete13 ="OFF",
                    TestParamete14 = "OFF",
                    COMParator = "ON",
                    Speed = "FAST",
                    TestFrequency = "1000000",
                    ExecuteId = "ON",
                    LSLow = -24.0m,
                    LSHIGH =24.0m,
                    LSstandard = "0.000000080",
                    RSHIGH = 0.4m,
                    RSLow = 0.0m,
                    OpenHZ = "4500000",
                    PortHZ = "4500000",
                    PanelNmae = "1",
                    PanelId = "1",
                    TestRange ="3",
                    TestSignal ="V",
                    TriggerMode = "EXTernal",
                    TriggerTime ="0",
                    Voltage = "1",

                        },
                              new IM3570Prameter()
                        {
                    TestParamete11 = "LS",
                    TestParamete12 = "OFF",
                    TestParamete13 ="OFF",
                    TestParamete14 = "OFF",
                    COMParator = "ON",
                    Speed = "FAST",
                    TestFrequency = "1000000",
                    ExecuteId = "ON",
                    LSLow = -24.0m,
                    LSHIGH =24.0m,
                    LSstandard = "0.000000080",
                    RSHIGH = 0.4m,
                    RSLow = 0.0m,
                    OpenHZ = "4500000",
                    PortHZ = "4500000",
                    PanelNmae = "1",
                    PanelId = "1",
                    TestRange ="3",
                    TestSignal ="V",
                    TriggerMode = "EXTernal",
                    TriggerTime ="0",
                    Voltage = "1",

                        }








                    };
               
                    GanZhi2.InstrumentDataParentClassList = new ObservableCollection<InstrumentDataParentClass>() { new InstrumentDataParentClass()
                    {
                          Name="LS2",
                           LowValue=0,
                            HighValue=0,
                             Judge="OK",
                              NumberBadProducts=2,
                               NumberGoodProducts=85,
                                TestValue=0,
                                 TotalNuber=87


                    },

                    new InstrumentDataParentClass()
                    {
                         Name="感值对比",
                           LowValue=0,
                            HighValue=0,
                             Judge="OK",
                              NumberBadProducts=0,
                               NumberGoodProducts=0,
                                TestValue=0,
                                 TotalNuber=0



                    }
                    };
                    GanZhi2.BoardNumber = 0;
                    GanZhi2.PrimaryAddress = 6;
                    GanZhi2.Name = "GanZhi2";
                    GanZhi2.Conect();

                    SZhi = new InstrumentE5071C();
             
                    SZhi.Name = "Szhi";
                    SZhi.DeviceType = DeviceType.S;



                    ListDeviceBase.Add(DCR1);
                    ListDeviceBase.Add(DCR2);

                    ListDeviceBase.Add(DCR3);
                    ListDeviceBase.Add(DCR4);
                    ListDeviceBase.Add(IR);

                    ListDeviceBase.Add(GanZhi1);
                    ListDeviceBase.Add(GanZhi2);
                    ListDeviceBase.Add(SZhi);

                    ListDeviceBase.Add(ZZhi);

                    ListDeviceBase.Add(DMianCCD);

                    SaveListDeviceBase();
                }
                else
                {
                    ListDeviceBase = new List<DeviceBase>();

                    JArray jArray = (JArray)JsonConvert.DeserializeObject(File.ReadAllText(Json_DeviceList));
                    foreach (var item in jArray)
                    {
                        JObject jo = (JObject)JsonConvert.DeserializeObject(item.ToString());
                        DeviceType deviceType = (DeviceType)(int)(jo["DeviceType"]);
                        switch (deviceType)
                        {

                            case DeviceType.DCR:
                                ListDeviceBase.Add(JsonConvert.DeserializeObject<AX1152DGPIB>(item.ToString()));
                                break;
                            case DeviceType.IR:
                                ListDeviceBase.Add(JsonConvert.DeserializeObject<Chrome11210IR>(item.ToString()));
                                break;
                            case DeviceType.BoXing:
                                ListDeviceBase.Add(JsonConvert.DeserializeObject<Chrome19301>(item.ToString()));
                                break;
                            case DeviceType.GanZhi:
                                ListDeviceBase.Add(JsonConvert.DeserializeObject<HIOKI3570>(item.ToString()));
                                break;

                            case DeviceType.Spare:
                                ListDeviceBase.Add(JsonConvert.DeserializeObject<SpareInstruMent>(item.ToString()));
                                break;

                            case DeviceType.S:
                                ListDeviceBase.Add(JsonConvert.DeserializeObject<InstrumentE5071C>(item.ToString()));
                                break;

                            default:
                                break;
                        }
                    }

                    if (ListDeviceBase == null)
                    {
                     
                        MessageBox.Show("设备配置文件错误！请检查", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                        Environment.Exit(0);
                    }


                    ZZhi= ListDeviceBase.Where(i => i.DeviceType == DeviceType.Spare && i.Name == "ZZhi1").FirstOrDefault() as SpareInstruMent;

                    DMianCCD = ListDeviceBase.Where(i => i.DeviceType == DeviceType.Spare && i.Name == "DMianCCD").FirstOrDefault() as SpareInstruMent;

                    SZhi= ListDeviceBase.Where(i => i.DeviceType == DeviceType.S && i.Name == "Szhi").FirstOrDefault() as InstrumentE5071C;

     
                 

                    DCR1 = ListDeviceBase.Where(i => i.DeviceType == DeviceType.DCR && i.Name == "DCR1").FirstOrDefault() as AX1152DGPIB;
                    DCR1.CsvList = new List<string> {
                        "DCR_1",  "DCR_2" };

                    DCR2 = ListDeviceBase.Where(i => i.DeviceType == DeviceType.DCR && i.Name == "DCR2").FirstOrDefault() as AX1152DGPIB;
                   

                    DCR3 = ListDeviceBase.Where(i => i.DeviceType == DeviceType.DCR && i.Name == "DCR3").FirstOrDefault() as AX1152DGPIB;
                    DCR3.CsvList = new List<string> {
                        "DCR_3" , "DCR_4"};


                    DCR4 = ListDeviceBase.Where(i => i.DeviceType == DeviceType.DCR && i.Name == "DCR4").FirstOrDefault() as AX1152DGPIB;
              



                    IR = ListDeviceBase.Where(i => i.DeviceType == DeviceType.IR && i.Name == "IR").FirstOrDefault() as Chrome11210IR;
                    IR.CsvList = new List<string> { "IR_R_1" };



                    GanZhi1 = ListDeviceBase.Where(i => i.DeviceType == DeviceType.GanZhi && i.Name == "GanZhi1").FirstOrDefault() as HIOKI3570;
                    GanZhi1.CsvList = new List<string> { "LS100K_1" };
                    GanZhi1.SetMode = false;
                    // GanZhi1.Conect();
                    GanZhi2 = ListDeviceBase.Where(i => i.DeviceType == DeviceType.GanZhi && i.Name == "GanZhi2").FirstOrDefault() as HIOKI3570;
                    //  GanZhi2.Conect();
                    GanZhi2.CsvList = new List<string> { "LS100K_2", "LS1_LS2对比" };
                    GanZhi2.SetMode = false;

                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
                //MessageBox.Show("设备配置文件错误！请检查," + ex.Message);
                //Environment.Exit(0);
            }
        }
        public static void SaveListDeviceBase()
        {
            new JsonSaveHelper().WriteJson(ListDeviceBase, Json_DeviceList);
        }



    }
}
