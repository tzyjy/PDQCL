using ATestPackagingMachineWpf1.Common;

using ATestPackagingMachineWpf1.ZModels;
using BTest.LogHelper;

using BTest;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZModels;
using System.IO.Ports;
using System.Collections.ObjectModel;
using System.Threading;
using ATestPackagingMachineWpf1.DeviceFile;


namespace ATestPackagingMachineWpf1.ViewModels
{
    public class LoadShowViewModel : BindableBase, IDialogAware
    {   /// <summary>
        /// 线程取消
        /// </summary>
    
        private string CurrentTime
        {
            get { return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); }
        }


        private int _barValue;
        public int BarValue
        {
            get { return _barValue; }
            set { SetProperty(ref _barValue, value); }
        }

        private string _barValuePercentage;
        public string BarValuePercentage
        {
            get { return _barValuePercentage; }
            set { SetProperty(ref _barValuePercentage, value); }
        }



        private const int  intervalValue=100/12;

        /// <summary>
        /// 日志
        /// </summary>
        private ObservableCollection<OperateLog> _operateLogList=new ObservableCollection<OperateLog>();
        public ObservableCollection<OperateLog> OperateLogList
        {
            get { return _operateLogList; }
            set { SetProperty(ref _operateLogList, value); }
        }
        public string Title => "加载中------";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
           
          
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
          Task.Run(new Action(() => { LoadDevice(); }))  ;

           
        }

        public void LoadDevice()
        {

            Stopwatch loadtime = new Stopwatch();
            loadtime.Restart();
            LOG.WriteLog("加载设备中-------------------------------------------------------------------------");

            JsonSaveHelper jsonSaveHelper = new JsonSaveHelper();
            //try
            //{
            //    DV.iODevice = new IODevice();
            //    DV.iODevice.Connect();
            //    AddLog(true, "IO板卡连接成功！", LogTpye.LoadLog);
            //}
            //catch (Exception ex)
            //{
            //    DV.iODevice = null;
            //    AddLog(false,  ex.Message, LogTpye.LoadLog);
            //   ;
            //}
            try
            {
                DV.PLC = new PLC3UTCP();
                DV.PLC.Connect();
                AddLog(true, "PLC连接成功！", LogTpye.LoadLog);
            }
            catch (Exception ex)
            {
                DV.iODevice = null;
                AddLog(false, ex.Message, LogTpye.LoadLog);
                ;
            }




            Thread.Sleep(2000);
            LOG.WriteLog($"加载设备完成-------------------------------------------------------------------------耗时时间为{loadtime.ElapsedMilliseconds}");
            App.Current.Dispatcher.Invoke(() =>
            {
                DialogParameters dialogParameters = new DialogParameters();
                dialogParameters.Add("OperateLogList", OperateLogList);

                RequestClose?.Invoke(new DialogResult(ButtonResult.OK, dialogParameters));

            });

          



        }

      
        private void AddLog(bool OK, string text, LogTpye logTpye = LogTpye.Other)
        {
            App.Current.Dispatcher.Invoke(() =>
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



        private void GetBarValue()
        {
            Thread.Sleep(10);
            BarValue += intervalValue;
            BarValuePercentage = $"{BarValue}%";
        }
    }
}
