using ATestPackagingMachineWpf1.Common;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Windows;
using ZModels;
using ZModels.DeviceJson;

namespace ATestPackagingMachineWpf1.Paramater.ViewModels
{
    public class DeviceParameterViewModel : BindableBase
    {
        public DeviceParameterViewModel()
        {
            if (JsonSaveEXT.deviceParameterJsonGv.TempParameter1 == null)
            {
                DeviceParameterJsonData.TempParameter1 = new TempParameter();
            }

            if (JsonSaveEXT.deviceParameterJsonGv.TempParameter2 == null)
            {
                DeviceParameterJsonData.TempParameter2 = new TempParameter();
            }
            if (JsonSaveEXT.deviceParameterJsonGv.PLCDataConfig == null)
            {
                DeviceParameterJsonData.PLCDataConfig = new PLCDataConfig();
            }

        }
        #region 属性


        /// <summary>
        /// 系统参数
        /// </summary>
        private DeviceParameterJson _DeviceParameterJsonData =JsonSaveEXT.deviceParameterJsonGv;
        public DeviceParameterJson DeviceParameterJsonData
        {
            get { return _DeviceParameterJsonData; }
            set { SetProperty(ref _DeviceParameterJsonData, value); }
        }
        private TempParameter _tempParameter1;
        public TempParameter TempParameter1
        {
            get { return _tempParameter1; }
            set { SetProperty(ref _tempParameter1, value); }
        }


        private List<string> _comNameList = GetComName();
        public List<string> ComNameList
        {
            get { return _comNameList; }
            set { SetProperty(ref _comNameList, value); }
        }
        private List<int> _BaudRateList = GetBaudRate();
        public List<int> BaudRateList
        {
            get { return _BaudRateList; }
            set { SetProperty(ref _BaudRateList, value); }
        }

        private List<int> _dataBits= GetDataBits();
        public List<int> DataBitsList
        {
            get { return _dataBits; }
            set { SetProperty(ref _dataBits, value); }
        }

        private string[] _stopBitsList= GetStopBits();
        public string[] StopBitsList
        {
            get { return _stopBitsList; }
            set { SetProperty(ref _stopBitsList, value); }
        }


        private string[] _ParityList= GetParity();
        public string[] ParityList
        {
            get { return _ParityList; }
            set { SetProperty(ref _ParityList, value); }
        }
        #endregion

        #region 命令
        /// <summary>
        /// 保存数据
        /// </summary>
        private DelegateCommand _saveData;
        public DelegateCommand SaveData =>
            _saveData ?? (_saveData = new DelegateCommand(ExecuteSaveData));

        void ExecuteSaveData()
        {
            JsonSaveEXT.deviceParameterJsonGv= DeviceParameterJsonData;
            try
            {
                JsonSaveEXT.SaveDeviceJson();

                MessageBox.Show("保存成功", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }



        }
        #endregion


        #region 温控绑定
       

        /// <summary>
        /// 端口名称
        /// </summary>
        /// <returns></returns>
        public static List<string> GetComName()
        {
            List<string> strings = new List<string>();
            for (int i = 0; i < 30; i++)
            {
                strings.Add("COM" + i.ToString());
            }
            return strings;
        }

        /// <summary>
        /// 波特率
        /// </summary>
        /// <returns></returns>
        public static List<int> GetBaudRate()
        {
            return new List<int>() { 2400, 4800, 9600, 19200, 38400 };
        }


        /// <summary>
        /// 数据位
        /// </summary>
        /// <returns></returns>
        public static List<int> GetDataBits()
        {

            return new List<int>() { 7, 8 };

        }

        /// <summary>
        /// 停止位
        /// </summary>
        /// <returns></returns>
        public static string[] GetStopBits()
        {

            return Enum.GetNames(typeof(StopBits));

        }
        /// <summary>
        /// 奇偶校验
        /// </summary>
        /// <returns></returns>
        public static string[] GetParity() {

            return Enum.GetNames(typeof(Parity));

        }



        #endregion
    }
}
