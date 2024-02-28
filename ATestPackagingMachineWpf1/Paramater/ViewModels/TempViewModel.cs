using ATestPackagingMachineWpf1.Common;
using ATestPackagingMachineWpf1.DeviceFile;
using ATestPackagingMachineWpf1.DeviceFile.Mes;
using HslCommunication.Profinet.Panasonic.Helper;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ZModels.DeviceJson;

namespace ATestPackagingMachineWpf1.ViewModels
{
    public class TempViewModel : BindableBase, INavigationAware
    {
        public TempViewModel()
        {

        }
        private TempParameter _tempParameter1;
        public TempParameter TempParameter1
        {
            get { return _tempParameter1; }
            set { SetProperty(ref _tempParameter1, value); }
        }
        private TempParameter _tempParameter2;
        public TempParameter TempParameter2
        {
            get { return _tempParameter2; }
            set { SetProperty(ref _tempParameter2, value); }
        }



        private DelegateCommand _save;
        public DelegateCommand Save =>
            _save ?? (_save = new DelegateCommand(ExecuteSave));

        void ExecuteSave()
        {
            try
            {
              
                JsonSaveEXT.deviceParameterJsonGv.TempParameter1 = TempParameter1;
                JsonSaveEXT.deviceParameterJsonGv.TempParameter2 = TempParameter2;
                JsonSaveEXT.SaveDeviceJson();
                Dictionary<string, short> keyValuePairs1 = new Dictionary<string, short>();
                keyValuePairs1.Add("18177", TempParameter1.SVTemp);
                keyValuePairs1.Add("18178", TempParameter1.AlarmTempHigh);
                keyValuePairs1.Add("18179", TempParameter1.AlarmTempLow);

                Dictionary<string, short> keyValuePairs2 = new Dictionary<string, short>();
                keyValuePairs2.Add("18177", TempParameter2.SVTemp);
                keyValuePairs2.Add("18178", TempParameter2.AlarmTempHigh);
                keyValuePairs2.Add("18179", TempParameter2.AlarmTempLow);

           bool temp1result=     (bool)  DV.DELTATemp1?.Write(keyValuePairs1);
                bool temp2result = DV.DELTATemp2.Write(keyValuePairs2);
                if (temp1result && temp2result)
                {

                    MessageBox.Show("保存成功", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("温度保存失败", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                }
               
            }
            catch (Exception ex)
            {

               
                MessageBox.Show(ex.Message, "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
    
        }

        #region 导航接口
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            TempParameter1= JsonSaveEXT.deviceParameterJsonGv.TempParameter1;
            TempParameter2 = JsonSaveEXT.deviceParameterJsonGv.TempParameter2;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
           
        }
        #endregion

    }
}
