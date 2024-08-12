using ATestPackagingMachineWpf1.Common;
using ATestPackagingMachineWpf1.DeviceFile;
using ATestPackagingMachineWpf1.ZModels;
using Automation.BDaq;
using BTest.LogHelper;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using TouchSocket.Core;
using ZModels;
using static System.Net.Mime.MediaTypeNames;

namespace ATestPackagingMachineWpf1.ViewModels
{
    public class HomeViewModel : BindableBase
    {
        private ObservableCollection<short> _Data;

        public ObservableCollection<short> Data
        {
            get { return _Data; }
            set { SetProperty(ref _Data, value); }
        }

        private string _Wo;

        public string Wo
        {
            get { return _Wo; }
            set { SetProperty(ref _Wo, value); }
        }

        private string CurrentTime
        {
            get { return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); }
        }

        private SolidColorBrush _BorderColor = new SolidColorBrush();

        public SolidColorBrush BorderColor
        {
            get { return _BorderColor; }
            set { SetProperty(ref _BorderColor, value); }
        }

        private PDQCLAPI PDQCLAPI;
        private IDialogService dialogService;

        private string _AalrmInfo;

        public string AalrmInfo
        {
            get { return _AalrmInfo; }
            set { SetProperty(ref _AalrmInfo, value); }
        }

        private int _CGOpen;

        public int CGOpen
        {
            get { return _CGOpen; }
            set { SetProperty(ref _CGOpen, value); }
        }

        private decimal _MatchedData;

        public decimal MatchedData
        {
            get { return _MatchedData; }
            set { SetProperty(ref _MatchedData, value); }
        }

        private short _RealSpeed;

        public short RealSpeed
        {
            get { return _RealSpeed; }
            set { SetProperty(ref _RealSpeed, value); }
        }

        /// <summary>
        /// 显示报警信息
        /// </summary>
        public bool ShowAlarmInfo { get; set; }

        /// <summary>
        /// 重工模式开启  扫工单禁用
        /// </summary>
        private bool _SendEnable;

        public bool SendEnable
        {
            get { return _SendEnable; }
            set { SetProperty(ref _SendEnable, value); }
        }

        public HomeViewModel(IDialogService dialogService)
        {
            Data = new ObservableCollection<short>();
            PDQCLAPI = new PDQCLAPI();
            this.dialogService = dialogService;
            LoadDevice();
            RequestWorkOrderInfoPra = new RequestWorkOrderInfoPra();
            RequestWorkOrderInfoPra.mach_code = JsonSaveEXT.deviceParameterJsonGv.EquipmentNum;
            //订阅事件
            GV.Event.GetEvent<MessageEvent>().Subscribe(AddLog);
            //Task.Run(() => { ReadPLCData(); });
            Task.Run(() => { ReadPDQCLData(); });
        }

        private object myobject = new object();

        private void ReadPDQCLData()
        {
            while (true)
            {
                if (DV.PLC.ConnectSucess)
                {
                    lock (myobject)
                    {
                        var RESULT = DV.PLC.plc.ReadInt16("D7220", 3);
                        if (RESULT.IsSuccess)
                        {
                            Data.Clear();
                            Data.Add(RESULT.Content[0]);
                            Data.Add(RESULT.Content[1]);
                            Data.Add(RESULT.Content[2]);
                        }

                        try
                        {
                            var RESULT2 = DV.PLC.plc.ReadInt16("D7212");
                            if (RESULT2.IsSuccess)
                            {
                                CGOpen = RESULT2.Content;
                                SendEnable = CGOpen == 1 ? false : true;
                            }
                        }
                        catch (Exception ex)
                        {
                            AddLog(new LogInfo() { OK = false, InfoText = "读取PLC数据发生错误：，详情：" + ex.Message });
                        }
                    }
                }
                Thread.Sleep(300);
            }
        }

        private void ReadPLCData()
        {
            Stopwatch stopwatch = new Stopwatch();
            UpTrigger upTrigger = new UpTrigger();
            while (true)
            {
                if (DV.PLC.ConnectSucess)
                {
                    lock (myobject)
                    {
                        RealSpeed = DV.PLC.ReadSpeedData();
                        upTrigger.Now = ShowAlarmInfo;
                        if (upTrigger.OutPut)
                        {
                            //时间开始
                            stopwatch.Restart();
                        }
                        if (!ShowAlarmInfo)
                        {
                            //报警解除
                            stopwatch.Reset();
                            stopwatch.Stop();
                            AalrmInfo = "";
                            BorderColor = Brushes.White;
                        }
                        if (stopwatch.ElapsedMilliseconds > 5000)
                        {
                            DV.PLC.WriteAalrm();
                            //报警开始
                            BorderColor = Brushes.Red;
                            AalrmInfo = $"参数未导入！";
                            AddLog(new LogInfo() { OK = false, InfoText = $" 作业员工号：{RequestWorkOrderInfoPra.op_name} 批号：{RequestWorkOrderInfoPra.wo}  参数未导入！" });

                            stopwatch.Reset();
                            stopwatch.Stop();
                        }
                    }
                }

                Thread.Sleep(100);
            }
        }

        private ReadDataInfo _ReadDataInfo;

        public ReadDataInfo ReadDataInfo
        {
            get { return _ReadDataInfo; }
            set { SetProperty(ref _ReadDataInfo, value); }
        }

        /// <summary>
        /// 记录
        /// </summary>
        private ObservableCollection<ReturnYXInfo> _ReturnYXInfoList = new ObservableCollection<ReturnYXInfo>();

        public ObservableCollection<ReturnYXInfo> ReturnYXInfoList
        {
            get { return _ReturnYXInfoList; }
            set { SetProperty(ref _ReturnYXInfoList, value); }
        }

        /// <summary>
        /// WonoInfo记录
        /// </summary>
        private ObservableCollection<WonoInfo> _WonoInfoList = new ObservableCollection<WonoInfo>();

        public ObservableCollection<WonoInfo> WonoInfoList
        {
            get { return _WonoInfoList; }
            set { SetProperty(ref _WonoInfoList, value); }
        }

        private ReturnPDQCLInfo _ReturnPDQCLInfo;

        public ReturnPDQCLInfo ReturnPDQCLInfo
        {
            get { return _ReturnPDQCLInfo; }
            set { SetProperty(ref _ReturnPDQCLInfo, value); }
        }

        public string StartDateTime { get; set; }

        /// <summary>
        /// 日志
        /// </summary>
        private ObservableCollection<OperateLog> _operateLogList;

        public ObservableCollection<OperateLog> OperateLogList
        {
            get { return _operateLogList; }
            set { SetProperty(ref _operateLogList, value); }
        }

        private RequestWorkOrderInfoPra _RequestWorkOrderInfoPra;

        public RequestWorkOrderInfoPra RequestWorkOrderInfoPra
        {
            get
            {
                GV.OpName = _RequestWorkOrderInfoPra.op_name;

                return _RequestWorkOrderInfoPra;
            }
            set
            {
                SetProperty(ref _RequestWorkOrderInfoPra, value);
                GV.OpName = _RequestWorkOrderInfoPra.op_name;
            }
        }

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
        }

        #endregion 加载设备配置

        private DelegateCommand _Send;

        public DelegateCommand Send =>
            _Send ?? (_Send = new DelegateCommand(ExecuteSend));

        private void ExecuteSend()
        {
            try
            {
                if (RequestWorkOrderInfoPra.wo == null) { return; }

                LOG.WriteMesLog("API:GetWorkOrderInfo+发送：" + new JsonSaveHelper().EntityToJSON(RequestWorkOrderInfoPra));
                ReturnPDQCLInfo = PDQCLAPI.Get(RequestWorkOrderInfoPra);

                if (ReturnPDQCLInfo != null)
                {
                    List<short> shorts = new List<short>();
                    //解析Mes数据发给PLC
                    if (ReturnPDQCLInfo.bc.IsNullOrWhiteSpace() || ReturnPDQCLInfo.bk.IsNullOrWhiteSpace() || ReturnPDQCLInfo.ksts.IsNullOrWhiteSpace())
                    {
                        throw new Exception("Mes发送数据不对！");
                    }
                    shorts.Add(short.Parse(ReturnPDQCLInfo.bc));
                    shorts.Add(short.Parse(ReturnPDQCLInfo.bk));
                    shorts.Add(short.Parse(ReturnPDQCLInfo.ksts));
                    var result1 = DV.PLC.plc.Write("D7200", shorts.ToArray());

                    Thread.Sleep(100);
                    var result2 = DV.PLC.plc.Write("D7210", 1);

                    if (!result1.IsSuccess || !result2.IsSuccess) throw new Exception("PLC写入失败" + result2.Message);
                    AddLog(new LogInfo() { OK = true, InfoText = $"获取Mes数据成功！并写入PLC：，详情：批号 {RequestWorkOrderInfoPra.wo}    板长{ReturnPDQCLInfo.bc} 板宽{ReturnPDQCLInfo.bk}  开刷提示{ReturnPDQCLInfo.ksts} 作业员:{RequestWorkOrderInfoPra.op_name} " });
                    //清空界面数据。
                    Wo = RequestWorkOrderInfoPra.wo;
                    RequestWorkOrderInfoPra.wo = "";
                }
                else
                {
                    throw new Exception("Mes数据解析为空！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("发生错误：，详情：" + ex.Message, "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                AddLog(new LogInfo() { OK = false, InfoText = "Mes发送工单发生错误：，详情：" + ex.Message });
            }
        }

        #region 导入

        private DelegateCommand _Import;

        public DelegateCommand Import =>
            _Import ?? (_Import = new DelegateCommand(ExecuteImport));

        private void ExecuteImport()
        {
        }

        #endregion 导入

        #region 日志显示

        public void AddLog(LogInfo logInfo)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                if (logInfo.OK)
                {
                    OperateLogList.Add(new OperateLog() { IconColor = "Green", LogIcon = "InformationSlabCircleOutline", OperateInfo = logInfo.InfoText, OperateTime = CurrentTime });
                }
                else
                {
                    OperateLogList.Add(new OperateLog() { IconColor = "Red", LogIcon = "InformationSlabCircleOutline", OperateInfo = logInfo.InfoText, OperateTime = CurrentTime });
                }
                LOG.WriteLog(logInfo.InfoText);

                //SetBar();
            });
        }

        private void AddLog(bool OK, string text, LogTpye logTpye = LogTpye.Other)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                if (OK)
                {
                    OperateLogList.Add(new OperateLog() { IconColor = "Green", LogIcon = "InformationSlabCircleOutline", OperateInfo = text, OperateTime = CurrentTime, LogTpye = logTpye });
                }
                else
                {
                    OperateLogList.Add(new OperateLog() { IconColor = "Red", LogIcon = "InformationSlabCircleOutline", OperateInfo = text, OperateTime = CurrentTime, LogTpye = logTpye });
                }
                LOG.WriteLog(text);
            });
        }

        #endregion 日志显示

        #region ListBOX滚动

        private System.Windows.Controls.ListBox listBox = null;

        private DelegateCommand<System.Windows.Controls.ListBox> _ListBoxLoaded;

        public DelegateCommand<System.Windows.Controls.ListBox> ListBoxLoaded =>
            _ListBoxLoaded ?? (_ListBoxLoaded = new DelegateCommand<System.Windows.Controls.ListBox>(ExecuteCommandName));

        private void ExecuteCommandName(System.Windows.Controls.ListBox listBox)
        {
            this.listBox = listBox;
        }

        private void SetBar()
        {
            //if (listBox==null)
            //{
            //    return;

            //}
            //if (listBox.Items.Count > 0)
            //{
            //    listBox.ScrollIntoView(listBox.Items[listBox.Items.Count - 1]);
            //}
        }

        #endregion ListBOX滚动

        #region 导入2

        private decimal _WriteSpeed;

        public decimal WriteSpeed
        {
            get { return _WriteSpeed; }
            set { SetProperty(ref _WriteSpeed, value); }
        }

        private DelegateCommand _Import2;

        public DelegateCommand Import2 =>
            _Import2 ?? (_Import2 = new DelegateCommand(ExecuteImport2));

        private void ExecuteImport2()
        {
            if (WriteSpeed > 10) WriteSpeed = 10.0m;
            if (WriteSpeed < 0) WriteSpeed = 0.0m;
            try
            {
                var result = (short)(WriteSpeed * 100);
                DV.PLC.WriteIntData("D7001", result);
                Thread.Sleep(100);
                DV.PLC.WriteIntData("D7021", 1);
                AddLog(new LogInfo() { OK = true, InfoText = "手动导入速度成功！" + $"导入信息：批号：{RequestWorkOrderInfoPra.wo} 作业员工号：{RequestWorkOrderInfoPra.op_name} 导入速度:{WriteSpeed}" });
            }
            catch (Exception ex)
            {
                MessageBox.Show("PLC写入失败！请检查" + ex.Message, "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                AddLog(new LogInfo() { OK = false, InfoText = "发生错误：，详情：" + ex.Message });
            }
        }

        #endregion 导入2
    }
}