using ATestPackagingMachineWpf1.Common;
using ATestPackagingMachineWpf1.DeviceFile;
using ATestPackagingMachineWpf1.DeviceFile.CCD;
using ATestPackagingMachineWpf1.DeviceFile.Mes;
using ATestPackagingMachineWpf1.InterfaceData;
using ATestPackagingMachineWpf1.ZModels;
using BTest;
using BTest.LogHelper;

using BTest.TCPIP;
using HslCommunication.Instrument.IEC;
using LiveCharts;
using NationalInstruments.Restricted;
using Newtonsoft.Json.Linq;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using TouchSocket.Core;
using TouchSocket.Sockets;
using ZModels;
using ZModels.DeviceJson;

namespace ATestPackagingMachineWpf1.ViewModels
{
    public class HomeViewModel : BindableBase, IConfirmNavigationRequest
    {
        #region 字段
    

        #endregion

        #region 属性

        private IDialogService dialogService;
        private IEventAggregator eventAggregator;
        private string _replyText;
        public string ReplyText
        {
            get { return _replyText; }
            set { SetProperty(ref _replyText, value); }
        }

        private string CurrentTime
        {
            get { return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); }
        }




        private bool _atuoChecked;
        public bool AtuoChecked
        {
            get { return _atuoChecked; }
            set { SetProperty(ref _atuoChecked, value); }
        }

        private bool _handChecked=true ;
        public bool HandChecked
        {
            get { return _handChecked; }
            set { SetProperty(ref _handChecked, value); }
        }


 


        /// <summary>
        /// 日志
        /// </summary>
        private ObservableCollection<OperateLog> _operateLogList;
        public ObservableCollection<OperateLog> OperateLogList
        {
            get { return _operateLogList; }
            set { SetProperty(ref _operateLogList, value); }
        }


        private ChartValues<int> _listTemp = new ChartValues<int>();
        public ChartValues<int> ListTemp
        {
            get { return _listTemp; }
            set { SetProperty(ref _listTemp, value); }
        }


        /// <summary>
        /// DI数组显示
        /// </summary>
        private ObservableCollection<bool> _boolLedArray = new ObservableCollection<bool>()
        {
            false, true,true,true,true,true,true,true,true,
            true, true,true,true,true,true,true,true,true,
        };
        public ObservableCollection<bool> BoolLedArray
        {
            get { return _boolLedArray; }
            set { SetProperty(ref _boolLedArray, value); }
        }

        /// <summary>
        /// 总耗时
        /// </summary>
        private string _timeCount;
        public string TimeCount
        {
            get { return _timeCount; }
            set { SetProperty(ref _timeCount, value); }
        }
        #endregion


        #region 命令

        #region 手自动切换
        private DelegateCommand<string> _HandAtuobutton;
        public DelegateCommand<string> HandAtuobutton =>
            _HandAtuobutton ?? (_HandAtuobutton = new DelegateCommand<string>(ExecuteHandAtuobutton));

        void ExecuteHandAtuobutton(string context)
        {
            try
            {
                if (!JsonSaveEXT.deviceParameterJsonGv.Manufacturer.AllowRun)
                {
                    if (OperateLogList.Where(i => i.LogTpye == LogTpye.LoadLog&&i.IconColor== "Red").Count() > 0) 
                    {
                        string[] strings = OperateLogList.Where(i => i.LogTpye == LogTpye.LoadLog && i.IconColor == "Red").Select(i=>i.OperateInfo).ToArray();
                        string showtext = "请检查加载的设备是否都正常！\r\n" + string.Join("\r\n", strings); // 使用空格作为分隔符进行连接
                        MessageBox.Show(showtext, "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                        HandChecked = true;
                        AtuoChecked = false;
                        return;
                    }
                }
          

                if (context == "手动")
                {
                    HandChecked = true;
                    AtuoChecked = false;
                    GV.ButtonAtuo = false;
                    Thread.Sleep(200);
                    PCI1730WriteAndRead.ReadyOff();

                }
                else  //自动
                {
                    HandChecked = false;
                    AtuoChecked = true;

                    GV.ButtonAtuo = true;

                    Thread.Sleep(200);
                    PCI1730WriteAndRead.ReadyOn();
                    PCI1730WriteAndRead.TestBusyOff();


                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "提示", MessageBoxButton.OK, MessageBoxImage.Error);

            }



        }
        #endregion

        #region 相机发送

        private DelegateCommand<string> _sendCCD;
        public DelegateCommand<string> SendCCD =>
            _sendCCD ?? (_sendCCD = new DelegateCommand<string>(ExecuteSendCCD));

        void ExecuteSendCCD(string parameter)
        {

            if (GV.ButtonAtuo)
            {
                MessageBox.Show("请切换手动", "错误提示", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                ReplyText = DV.CCD?.Send(parameter);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "错误提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }



        #endregion






        private DelegateCommand _test;
        public DelegateCommand Test =>
            _test ?? (_test = new DelegateCommand(ExecuteTest));

        void ExecuteTest()
        {

        }
        #endregion


        #region 构造方法
        public HomeViewModel(IEventAggregator eventAggregator, IDialogService dialogService)
        {
            this.dialogService = dialogService;
            OperateLogList = new ObservableCollection<OperateLog>();
            this.eventAggregator = eventAggregator;
            GV.ChangeEnableColorMethod = new ChangeEnableColor(ChangeColor);
            LoadDevice();
            ChangeColor();

            //订阅事件
            eventAggregator.GetEvent<MessageEvent>().Subscribe(AddLog);
          
            Task.Run(() => { TestEquipment(); });
            Task.Run(() => { MesDeal(); });
            Task.Run(() => { MesReplyMachineStatus(); });
            Task.Run(() => { WarningBad(); });

        }
        #endregion

        #region 测试设备-------------Main

        /// <summary>
        /// 感值1Data
        /// </summary>
        Queue<decimal> GanZhi1QueueData = new Queue<decimal>();



        private void TestEquipment()
        {
            Stopwatch stopwatchAll = new Stopwatch();
            Stopwatch ir = new Stopwatch();
            while (true)
            {

                stopwatchAll.Restart();
                if (GV.ButtonAtuo == false) //自动模式下才可运行
                {
                    continue;
                }
                if (DV.IO1730==null)
                {
                    continue;
                }
                bool[] boolDIATarray = DV.IO1730?.ReadAllDi();

                var datetime = DateTime.Now;
                bool overSingle = boolDIATarray[JsonSaveEXT.deviceParameterJsonGv.iOReadY.BiginOK]; //准备完成信号   接6号引脚
                bool dcr12test = boolDIATarray[JsonSaveEXT.deviceParameterJsonGv.iOReadY.DCR12Triger];//DCR1,2触发
                bool dcr34test = boolDIATarray[JsonSaveEXT.deviceParameterJsonGv.iOReadY.DCR34Triger];//DCR3，4触发
                bool irtest = boolDIATarray[JsonSaveEXT.deviceParameterJsonGv.iOReadY.IRTriger];//IR触发
                bool ganzhi1test = boolDIATarray[JsonSaveEXT.deviceParameterJsonGv.iOReadY.GanZhi1Triger];//感值1
                bool ganzhi2test = boolDIATarray[JsonSaveEXT.deviceParameterJsonGv.iOReadY.GanZhi2Triger];//感值2


                bool DCR12T = (dcr12test && DV.DCR1.Enable) || false;
                bool DCR34T = (dcr34test && DV.DCR3.Enable) || false;
                bool IRT = (irtest && DV.IR.Enable) || false;

                bool GanZhi1T = (ganzhi1test && DV.GanZhi1.Enable) || false;
                bool GanZhi2T = ganzhi2test && DV.GanZhi2.Enable;



                if (overSingle)
                {

                    #region 写入测试Busy
                    PCI1730WriteAndRead.TestBusyOn();
                    #endregion

                    //try
                    //{
                    #region 触发仪器
                    //IR
                    if (IRT)
                    {
                        DV.IR.Trigger();
                    }
                    //DCR12
                    if (DCR12T)
                    {       //极性旋转复位
                        PCI1730WriteAndRead.DCR12XuanZhuan0();
                        DV.DCR1.Trigger();
                        Thread.Sleep(10);
                        DV.DCR2.Trigger();
                    }
                    //DCR34
                    if (DCR34T)
                    {
                        //DCR34报警复位
                        PCI1730WriteAndRead.DCR34Error0();
                        DV.DCR3.Trigger();
                        Thread.Sleep(10);
                        DV.DCR4.Trigger();

                    }
                    //感值1
                    if (GanZhi1T)
                    {
                        DV.GanZhi1.TriggerSingle();

                    }
                    //感值2
                    if (GanZhi2T)
                    {
                        PCI1730WriteAndRead.LS1LS2Value0();
                        PCI1730WriteAndRead.LS1LS2Over0();
                        DV.GanZhi2.TriggerSingle();

                    }


                    #endregion

                    #region 读取并处理数据


                    //-------------读取数据并处理数据

                    string dcr1Data = string.Empty;
                    string dcr2Data = string.Empty;
                    string dcr3Data = string.Empty;
                    string dcr4Data = string.Empty;

                    string ganzhi1Yuanzhi = string.Empty;
                    string ganzhi2Yuanzhi = string.Empty;

                    string iryuanzhi = string.Empty;

                    //依次读取设备数据


                    if (DCR12T)
                    {
                        dcr1Data = DV.DCR1.ReadTestData();
                        dcr2Data = DV.DCR2.ReadTestData();

                    }
                    if (DCR34T)
                    {
                        dcr3Data = DV.DCR3.ReadTestData();
                        dcr4Data = DV.DCR4.ReadTestData();
                    }



                    if (GanZhi1T)
                    {
                        ganzhi1Yuanzhi = DV.GanZhi1.ReadTestData();

                    }
                    if (GanZhi2T)
                    {

                        ganzhi2Yuanzhi = DV.GanZhi2.ReadTestData();

                    }
                    if (IRT)
                    {
                        ir.Restart();
                        while (true)
                        {
                            string stutes = DV.IR.TestState();
                            Console.WriteLine("循环等待空闲  读取仪器111111111111");

                            if (stutes.Contains("IDLE"))
                            {
                                iryuanzhi = DV.IR.ReadTestData();
                                break;
                            }
                            if (ir.ElapsedMilliseconds > 5000)
                            {
                                LOG.WriteLog("读取_IR测试超时，请检查");

                                AddLog(false, "读取_IR测试超时，请检查");
                                break;
                            }
                            Thread.Sleep(10);

                        }
                        ir.Stop();

                    }
                    decimal ganzhi1Value = 0;

                    //处理数据
                    if (DCR12T)
                    {
                        List<decimal> datalist = new List<decimal>();

                        BoolLedArray[0] &= DV.DCR1.DataDeal2(dcr1Data, dcr2Data,DV.DCR1,DV.DCR2, out datalist);
                        if (datalist[1] > datalist[0])
                        {
                            PCI1730WriteAndRead.DCR12XuanZhuan1();
                        }
                    }

                    if (IRT)
                    {
                        BoolLedArray[1] &= DV.IR.DataDeal(iryuanzhi);
                    }

                    if (DCR34T)
                    {
                        List<decimal> datalist = new List<decimal>();
                        BoolLedArray[5] &= DV.DCR3.DataDeal2(dcr3Data, dcr4Data, DV.DCR3, DV.DCR4, out datalist);
                        if (datalist[1] > datalist[0])
                        {
                            PCI1730WriteAndRead.DCR34Error1();
                        }
                    }
                    if (GanZhi1T)
                    {
                        bool result = DV.GanZhi1.SingleHzAtuoTestLS(ganzhi1Yuanzhi, out ganzhi1Value);
                        BoolLedArray[6] &= result;

                        if (result)
                        {
                            GanZhi1QueueData.Enqueue(ganzhi1Value);
                            Console.WriteLine("感值1OK");
                        }


                        string text = string.Empty;
                        foreach (var item in GanZhi1QueueData)
                        {

                            text += item + ";";
                        }
                        Console.WriteLine("感值1队列" + text);

                    }


                    if (GanZhi2T)
                    {
                        decimal data = 0;
                        

                        if (GanZhi1QueueData.Count>0)
                        {
                            data= GanZhi1QueueData.Dequeue();
                        }
                        BoolLedArray[7] &= DV.GanZhi2.SingleHzAtuoTestLS_LS2Compare(ganzhi2Yuanzhi, data);

                    }

                    #endregion
                    //}
                    //    catch (Exception ex)
                    //    {

                    //    LOG.WriteLog("请重启软件！错误：" + ex.Message);
                    //    MessageBox.Show("请重启软件！错误：" + ex.Message);
                    //    break;
                    //}

                    #region 防止只启用了一个仪器，所以加延迟延长到80ms
                    //发送测试命令后，耗时小于80ms，延时到80ms

                    double time = 80 - stopwatchAll.ElapsedMilliseconds;
                    if (time > 0)
                    {
                        Thread.Sleep((int)time);
                    }

                    #endregion

                    #region 数据显示处理

                    BoolLedArray = LeftRotateBool(BoolLedArray, 1);

                    //数据


                    //通知转盘，数据处理完成
                    PCI1730WriteAndRead.TestBusyOff();


                    Thread.Sleep(20);

                    #endregion

                    stopwatchAll.Stop();

                    TimeCount = stopwatchAll.ElapsedMilliseconds.ToString();

                }

                Thread.Sleep(10);

            }
        }

        private ObservableCollection<bool> LeftRotateBool(ObservableCollection<bool> arr, int n)
        {
            bool[] result2 = new bool[arr.Count];
            ObservableCollection<bool> result = new ObservableCollection<bool>(result2);

            for (int i = 0; i < arr.Count; i++)
            {
                int judge = i + (1 % arr.Count);
                if (judge < 18)
                {
                    result[judge] = arr[i];
                }
                else
                {
                    result[0] = true;
                }
            }
            return result;
        }


        #endregion 测试设备-------------Main

        #region 加载设备配置

        
        public void LoadDevice()
        {
            dialogService.ShowDialog("LoadShowView", callback =>
            {
                if (callback.Result == ButtonResult.OK)
                {
                    OperateLogList = callback.Parameters.GetValue<ObservableCollection<OperateLog>>("OperateLogList");
                }
            });

        
            MesInfo = GV.MesInfo;







        }





        #region 日志显示


        private void AddLog(LogInfo logInfo)
        {
            if (logInfo.OK)
            {
                OperateLogList.Add(new OperateLog() { IconColor = "Green", LogIcon = "InformationSlabCircleOutline", OperateInfo = logInfo.InfoText, OperateTime = CurrentTime });

            }
            else
            {
                OperateLogList.Add(new OperateLog() { IconColor = "Red", LogIcon = "InformationSlabCircleOutline", OperateInfo = logInfo.InfoText, OperateTime = CurrentTime });
            }

        }

        private void AddLog(bool OK, string text, LogTpye logTpye= LogTpye.Other)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                if (OK)
                {
                   
                    OperateLogList.Add(new OperateLog() { IconColor = "Green", LogIcon = "InformationSlabCircleOutline", OperateInfo = text, OperateTime = CurrentTime , LogTpye= logTpye });

                }
                else
                {
                 
                    OperateLogList.Add(new OperateLog() { IconColor = "Red", LogIcon = "InformationSlabCircleOutline", OperateInfo = text, OperateTime = CurrentTime, LogTpye = logTpye });
                }
                LOG.WriteLog(text);

            });

        }


        #endregion







        #endregion

        #region 导航接口


        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            if (GV.ButtonAtuo)
            {
             
                MessageBox.Show("请切换手动", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            continuationCallback(true);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {

        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }


        #endregion

        #region 改变颜色

        private ObservableCollection<bool> _listEnble = new ObservableCollection<bool>();
        public ObservableCollection<bool> ListEnble
        {
            get { return _listEnble; }
            set { SetProperty(ref _listEnble, value); }
        }

        public void ChangeColor()
        {
            ListEnble.Clear();
            ListEnble.Add(DV.DCR1.Enable);
            ListEnble.Add(DV.IR.Enable);
            ListEnble.Add(DV.ZZhi.Enable);
            ListEnble.Add(DV.DCR3.Enable);
            ListEnble.Add(DV.GanZhi1.Enable);
            ListEnble.Add(DV.GanZhi2.Enable);
            ListEnble.Add(DV.SZhi.Enable);
            ListEnble.Add(DV.DMianCCD.Enable);

        }
        #endregion

        #region Mes在线离线
        private bool _mesOnlineChecked;
        public bool MesOnlineChecked
        {
            get { return _mesOnlineChecked; }
            set { SetProperty(ref _mesOnlineChecked, value); }
        }


        private bool _mesOfflineChecked = true;
        public bool MesOfflineChecked
        {
            get { return _mesOfflineChecked; }
            set { SetProperty(ref _mesOfflineChecked, value); }
        }

        private DelegateCommand<string> _mesbutton;
        public DelegateCommand<string> Mesbutton =>
            _mesbutton ?? (_mesbutton = new DelegateCommand<string>(ExecuteMesbutton));

        void ExecuteMesbutton(string context)
        {
            if (context == "Mes在线")
            {
                //连接Mes服务器

                CSACollection cSACollection = PCClientMESAPI.Connect();
                CSA zcs = cSACollection?.Get("returncode");
                if (zcs?.CSValue1 == "0")
                {
                    GV.MesOnline = true;
                    MesOfflineChecked = false;
                    MesOnlineChecked = true;
                    AddLog(true, "PC客户端连接Mes服务器成功！");
                }
                else
                {
                    GV.MesOnline = false;

                    MesOnlineChecked = false;
                    MesOfflineChecked = true;
               
                    MessageBox.Show("PC客户端连接Mes服务器失败！", "错误提示", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            else
            {

                GV.MesOnline = false;

                MesOnlineChecked = false;
                MesOfflineChecked = true;
            }

            Console.WriteLine("在线" + MesOnlineChecked + "离线" + MesOfflineChecked);
        }

        #endregion

        #region Mes信息刷新以及标准件测试
        private void MesDeal()
        {

            while (true)
            {

                //Mes信息刷新
                if (GV.MesInfoUpdate)
                {
                    MesInfo = GV.MesInfo;


                }
                //标准件测试
                if (GV.standtestTest)
                {
                    GV.standtestTest = false;

                    string ammeter_name = GV.standtestInfo.Get("ammeter_name").CSValue1;
                    string ammeter_subtype = GV.standtestInfo.Get("ammeter_subtype").CSValue1;
                    CSACollection csa = new CSACollection();
                    switch (ammeter_name)
                    {

                        case "DCR":
                            try
                            {
                                if (ammeter_subtype == "1")
                                {
                                    SendDCR(DV.DCR1, "DCR", "1");
                                    Thread.Sleep(500);
                                    PCClientMESAPI.ReplySTDTestOver();
                                }
                                if (ammeter_subtype == "2")
                                {
                                    SendDCR(DV.DCR2, "DCR", "2");
                                    Thread.Sleep(500);
                                    PCClientMESAPI.ReplySTDTestOver();
                                }
                            }
                            catch (Exception ex)
                            {

                            
                                MessageBox.Show("DCR标准件测试错误" + ex.Message, "提示", MessageBoxButton.OK, MessageBoxImage.Error);

                            }




                            break;
                        case "LCR":
                            try
                            {

                                if (ammeter_subtype == "1")
                                {

                                    SendLCR(DV.GanZhi1, "LS100K", "1");
                                    Thread.Sleep(500);
                                    PCClientMESAPI.ReplySTDTestOver();
                                }
                                if (ammeter_subtype == "2")
                                {
                                    SendLCR(DV.GanZhi2, "LS100K", "2");
                                    Thread.Sleep(500);
                                    PCClientMESAPI.ReplySTDTestOver();

                                }




                            }
                            catch (Exception ex)
                            {
                                throw new Exception(ex.Message);

                            }

                            break;

                        default:
                            break;


                    }

                }

                Thread.Sleep(20);
            }

        }


        private static void SendDCR(AX1152DGPIB instrument1152D, string ItemName, string ammeter_subtype)
        {
            string RAWDATA = String.Empty;
            instrument1152D.Trigger();
            RAWDATA = instrument1152D.ReadTestData();

            string a1 = RAWDATA.Replace("XR", "");
            string value = a1.Substring(0, a1.IndexOf(","));
            string judge = a1.Substring(a1.IndexOf(",")).Replace("\r\n", "");
            bool ispass = false;
            bool istoHigh = false;
            bool istoLow = false;
            if (judge == "GO")
            {
                ispass = true;
                istoHigh = false;
                istoLow = false;
            }
            else if (judge == "HI")
            {
                ispass = false;
                istoHigh = true;
                istoLow = false;
            }
            else if (judge == "LO")
            {
                ispass = false;
                istoHigh = false;
                istoLow = true;
            }


            CSACollection csa = new CSACollection();
            csa.txnname = "ReplyStandTestValue";
            csa.Add("ammeter_name", ItemName);
            csa.Add("ammeter_subtype", ammeter_subtype);
            CSA secondcs = csa.Add("ammeter_value", null);

            secondcs.Add("ItemName", ItemName);
            secondcs.Add("itemValue", value);
            secondcs.Add("isPass", ispass.ToString());
            secondcs.Add("IS_TOO_LOW", istoHigh.ToString());
            secondcs.Add("IS_TOO_HIGH", istoLow.ToString());
            secondcs.Add("JUGEMENT", judge);
            secondcs.Add("SEQ_NO", "0");
            secondcs.Add("AMMETER_DATE", DateTime.Now.ToString());
            secondcs.Add("SUB_TYPE", "1");
            secondcs.Add("RAWDATA", RAWDATA);

            PCClientMESAPI.ReplyStandTestValue(csa);

        }


        private static void SendLCR(HIOKI3570 instrumentGanZhi, string ItemName, string subtype)
        {
            CSACollection csa = new CSACollection();


            instrumentGanZhi.Trigger();


            string yuanzhi = instrumentGanZhi.ReadTestData();//原值


            if (yuanzhi.Contains('/'))
            {
                string[] reciveArray = yuanzhi.Split('/');
                string[] singleReciveArray1 = reciveArray[0].Split(',');

                string[] singleReciveArray2 = reciveArray[1].Split(',');



                if (singleReciveArray1.Length == 3)
                {
                    string LSvalue = singleReciveArray1[1];
                    string LSJudge = singleReciveArray1[2];


                    bool ispass = false;
                    bool istoHigh = false;
                    bool istoLow = false;
                    if (LSJudge.Contains("0"))
                    {
                        ispass = true;
                        istoHigh = false;
                        istoLow = false;
                    }
                    else if (LSJudge.Contains("1"))
                    {
                        ispass = false;
                        istoHigh = true;
                        istoLow = false;
                    }
                    else if (LSJudge.Contains("-1"))
                    {
                        ispass = false;
                        istoHigh = false;
                        istoLow = true;
                    }

                    csa.txnname = "ReplyStandTestValue";

                    csa.Add("ammeter_name", "LCR");
                    csa.Add("ammeter_subtype", subtype);
                    CSA secondcs = csa.Add("ammeter_value", null);


                    secondcs.Add("ItemName", ItemName);
                    secondcs.Add("itemValue", LSvalue);
                    secondcs.Add("isPass", ispass.ToString());
                    secondcs.Add("IS_TOO_LOW", istoLow.ToString());
                    secondcs.Add("IS_TOO_HIGH", istoHigh.ToString());
                    secondcs.Add("JUGEMENT", LSJudge);
                    secondcs.Add("SEQ_NO", "0");
                    secondcs.Add("AMMETER_DATE", DateTime.Now.ToString());
                    secondcs.Add("SUB_TYPE", "1");
                    secondcs.Add("RAWDATA", yuanzhi);

                    PCClientMESAPI.ReplyStandTestValue(csa);



                }
                else
                {
                    MessageBox.Show("LS标准件失败，请检查是否为单频，并且只有Ls测试值！","提示", MessageBoxButton.OK, MessageBoxImage.Error);
                }





            }





        }

        #endregion

        #region Mes信息

        private MesInfo _mesInfo;
        public MesInfo MesInfo
        {
            get { return _mesInfo; }
            set { SetProperty(ref _mesInfo, value); }
        }


        #endregion

        #region 测试
        private DelegateCommand _Test;
        public DelegateCommand Test2222 =>
            _Test ?? (_Test = new DelegateCommand(ExecuteTest2222));

        void ExecuteTest2222()
        {

            //AddLog(new LogInfo() {  OK =true,InfoText="撒大苏打"});
        }
        #endregion

        #region 每分钟向Mes发送机台数据数据
        private void MesReplyMachineStatus()
        {
            while (true)
            {

                if (GV.MesOnline)
                {
                    CSACollection cSA = PCClientMESAPI.ReplyMachineStatus();
                    if (cSA == null)
                    {

                        DV.PLC5U.WriteMesAlarm();
                        MesOnlineChecked = false;
                        MesOfflineChecked = true;
                        AddLog(new LogInfo() { OK = false, InfoText = "PC客户端发送Mes服务器失败！！" }); ;
                        LOG.WriteLog("PC客户端发送Mes服务器失败！！");

                    }
                    else
                    {



                    }

                }
                Thread.Sleep(60000);


            }

        }



        #endregion

        #region 不良多线程警戒

        private void WarningBad()
        {
            while (true)
            {
                if (!GV.ButtonAtuo) //自动模式下才可运行
                {
                    continue;
                }
                WarningValue warningValue = JsonSaveEXT.deviceParameterJsonGv.WarningValue;

                List<decimal> decimals = InstrumentCommon.GetAllPassRate();
                decimal realData1 = decimals[0];
                decimal realData2 = decimals[1];
                decimal realData3 = decimals[2];
                decimal realData4 = decimals[3];
                decimal realData5 = decimals[4];
        

                bool Judgment1 = realData1 < warningValue.WarningValue1;
                bool Judgment2 = realData2 < warningValue.WarningValue2;
                bool Judgment3 = realData3 < warningValue.WarningValue3;
                bool Judgment4 = realData4 < warningValue.WarningValue4;
                bool Judgment5 = realData5 < warningValue.WarningValue5;
     
                if (Judgment1 || Judgment2 || Judgment3 || Judgment4 || Judgment5 )
                {


                    App.Current.Dispatcher.Invoke((Action)(() =>
                    {
                        dialogService.ShowDialog("WarnShowView");
                    }));

                }

                Thread.Sleep(300000);
            }
        }

        #endregion 不良多线程警戒
    }
}
