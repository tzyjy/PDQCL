using ATestPackagingMachineWpf1.Common;
using ATestPackagingMachineWpf1.DeviceFile;
using ATestPackagingMachineWpf1.DeviceFile.DeviceParameter;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ATestPackagingMachineWpf1.ViewModels
{
    public class OpretionViewModel : BindableBase, INavigationAware
    {
        public OpretionViewModel()
        {

        }

        private DelegateCommand _clear;
        public DelegateCommand Clear =>
            _clear ?? (_clear = new DelegateCommand(ExecuteClear));

        void ExecuteClear()
        {

            InstrumentCommon.ClearData();

          
            MessageBox.Show("清零成功！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
        }



        private DelegateCommand _save;
        public DelegateCommand Save =>
            _save ?? (_save = new DelegateCommand(ExecuteCommandName));

        void ExecuteCommandName()
        {
            try
            {
                JsonSaveEXT.deviceParameterJsonGv.LS1_LS2Low = LS1_LS2Low;
                JsonSaveEXT.deviceParameterJsonGv.LS1_LS2High = LS1_LS2High;
                JsonSaveEXT.SaveDeviceJson();

                MessageBox.Show("保存成功", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }


      private decimal _lS1_LS2Low;
        public decimal LS1_LS2Low
        {
            get { return _lS1_LS2Low; }
            set { SetProperty(ref _lS1_LS2Low, value); }
        }

        private decimal _lS1_LS2High;
        public decimal LS1_LS2High
        {
            get { return _lS1_LS2High; }
            set { SetProperty(ref _lS1_LS2High, value); }
        }


        #region 导航
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            LS1_LS2Low = JsonSaveEXT.deviceParameterJsonGv.LS1_LS2Low;
            LS1_LS2High = JsonSaveEXT.deviceParameterJsonGv.LS1_LS2High;

        }
        #endregion
    }
}
