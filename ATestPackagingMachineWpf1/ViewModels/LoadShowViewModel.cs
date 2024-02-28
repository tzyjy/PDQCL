using ATestPackagingMachineWpf1.Common;
using ATestPackagingMachineWpf1.DeviceFile.CCD;
using ATestPackagingMachineWpf1.DeviceFile.Mes;
using ATestPackagingMachineWpf1.DeviceFile;
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
using ATestPackagingMachineWpf1.DeviceFile.PLCFile;

namespace ATestPackagingMachineWpf1.ViewModels
{
    public class LoadShowViewModel : BindableBase, IDialogAware
    {
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
            GV.MesInfo = jsonSaveHelper.ReadOneJson<MesInfo>("Json\\MesInfo.json");
            if (GV.MesInfo == null)
            {
                GV.MesInfo = new MesInfo();

                jsonSaveHelper.WriteJson(GV.MesInfo, "Json\\MesInfo.json");

            }


            try
            {
                DV.DCR1.Conect();

                DV.DCR1.WriteDeviceConfig();
                AddLog(true, "DCR1连接成功！", LogTpye.LoadLog);



            }
            catch (Exception ex)
            {

                AddLog(false, "DCR1连接失败！" + ex.Message, LogTpye.LoadLog);

            }
            GetBarValue();


            try
            {
                DV.DCR2.Conect();

                DV.DCR2.WriteDeviceConfig();

                AddLog(true, "DCR2连接成功！", LogTpye.LoadLog);

            }
            catch (Exception ex)
            {



                AddLog(false, "DCR2连接失败！" + ex.Message, LogTpye.LoadLog);
            }
            GetBarValue();




            try
            {
                DV.DCR3.Conect();

                DV.DCR3.WriteDeviceConfig();

                AddLog(true, "DCR3连接成功！", LogTpye.LoadLog);

            }
            catch (Exception ex)
            {



                AddLog(false, "DCR3连接失败！" + ex.Message, LogTpye.LoadLog);
            }

            GetBarValue();

            try
            {
                DV.DCR4.Conect();

                DV.DCR4.WriteDeviceConfig();

                AddLog(true, "DCR4连接成功！", LogTpye.LoadLog);

            }
            catch (Exception ex)
            {



                AddLog(false, "DCR4连接失败！" + ex.Message, LogTpye.LoadLog);
            }

            GetBarValue();
            try
            {
                DV.IR.Conect();
                AddLog(true, "IR连接成功！", LogTpye.LoadLog);


            }
            catch (Exception ex)
            {


                AddLog(false, "IR连接失败！" + ex.Message, LogTpye.LoadLog);

            }

            GetBarValue();



            try
            {
                DV.GanZhi1.Conect();
                DV.GanZhi1.WriteDeviceConfigEX(DV.GanZhi1.IM3570PrameterList[0]);

                AddLog(true, "感值1连接成功！", LogTpye.LoadLog);


            }
            catch (Exception ex)
            {

                AddLog(false, "感值1连接失败！" + ex.Message, LogTpye.LoadLog);

            }

            GetBarValue();
            try
            {
                DV.GanZhi2.Conect();
                DV.GanZhi2.WriteDeviceConfigEX(DV.GanZhi2.IM3570PrameterList[0]);



                AddLog(true, "感值2连接成功", LogTpye.LoadLog);


            }
            catch (Exception ex)
            {


                AddLog(false, "感值2连接失败！" + ex.Message, LogTpye.LoadLog);
            }



            GetBarValue();


            try
            {
                DV.IO1730 = new AdvantechPCI1730Device();


                AddLog(true, "IO板卡连接成功！", LogTpye.LoadLog);
            }

            catch (Exception ex)
            {

                AddLog(false, "IO板卡连接失败！" + ex.Message, LogTpye.LoadLog);

            }


            GetBarValue();
            try
            {
                string plcip = JsonSaveEXT.deviceParameterJsonGv.PLC_Ipadress;
                int Writeport1 = JsonSaveEXT.deviceParameterJsonGv.PLC_WritePort;
                int Readport2 = JsonSaveEXT.deviceParameterJsonGv.PLC_ReadPort;
                DV.PLC5U = new FX5USon(plcip, Writeport1, Readport2);
                DV.PLC5U?.Connect();
                AddLog(true, "PLC连接成功！", LogTpye.LoadLog);


            }


            catch (Exception ex)
            {
              
                AddLog(false, "PLC连接失败！" + ex.Message, LogTpye.LoadLog);
            }

            GetBarValue();

            try
            {
                DV.CCD = new CCDDevice("192.168.10.1", "8080");
                DV.CCD.Connect();
                AddLog(true, "相机字符CDD连接成功！", LogTpye.LoadLog);


            }

            catch (Exception ex)
            {

                AddLog(false, "相机字符CDD连接失败！" + ex.Message, LogTpye.LoadLog);
            }

            GetBarValue();

            try
            {
                string PortName = JsonSaveEXT.deviceParameterJsonGv.TempParameter1.PortName;
                int BaudRate = JsonSaveEXT.deviceParameterJsonGv.TempParameter1.BaudRate;
                int DataBits = JsonSaveEXT.deviceParameterJsonGv.TempParameter1.DataBits;
                StopBits StopBits = (StopBits)Enum.Parse(typeof(StopBits), JsonSaveEXT.deviceParameterJsonGv.TempParameter1.StopBits, true);
                Parity Parity = (Parity)Enum.Parse(typeof(Parity), JsonSaveEXT.deviceParameterJsonGv.TempParameter1.Parity, true);
                DV.DELTATemp1 = new DELTATemp(PortName, BaudRate, DataBits, StopBits, Parity);
                DV.DELTATemp1?.Connect();
                var a =DV.DELTATemp1?.Read();
                if (a !=0 && a != null)
                {

                    AddLog(true, "温度1连接成功！", LogTpye.LoadLog);
                }
                else
                {

                    AddLog(false, "温度1连接失败！", LogTpye.LoadLog);
                }

            }

            catch (Exception ex)
            {

                AddLog(false, "温度1连接失败！" + ex.Message, LogTpye.LoadLog);

            }

            GetBarValue();

            try
            {
                string PortName = JsonSaveEXT.deviceParameterJsonGv.TempParameter2.PortName;
                int BaudRate = JsonSaveEXT.deviceParameterJsonGv.TempParameter2.BaudRate;
                int DataBits = JsonSaveEXT.deviceParameterJsonGv.TempParameter2.DataBits;
                StopBits StopBits = (StopBits)Enum.Parse(typeof(StopBits), JsonSaveEXT.deviceParameterJsonGv.TempParameter2.StopBits, true);
                Parity Parity = (Parity)Enum.Parse(typeof(Parity), JsonSaveEXT.deviceParameterJsonGv.TempParameter2.Parity, true);
                DV.DELTATemp2 = new DELTATemp(PortName, BaudRate, DataBits, StopBits, Parity);
                DV.DELTATemp2.Connect();
                var a = DV.DELTATemp2?.Read();
                if (a != 0&&a!=null)
                {

                    AddLog(true, "温度2连接成功！", LogTpye.LoadLog);
                }
                else
                {

                    AddLog(false, "温度2连接失败！", LogTpye.LoadLog);
                }


            }

            catch (Exception ex)
            {

                AddLog(false, "温度2连接失败！" + ex.Message, LogTpye.LoadLog);

            }

            GetBarValue();

            try
            {
                MESServrMESAPI.Start();


            }
            catch (Exception ex)
            {

                AddLog(false, "Mes服务器创建失败！" + ex.Message, LogTpye.LoadLog);
            }



            loadtime.Stop();

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
