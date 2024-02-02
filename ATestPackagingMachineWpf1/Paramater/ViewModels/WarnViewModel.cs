using ATestPackagingMachineWpf1.Common;
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
    public class WarnViewModel : BindableBase, INavigationAware
    {
        public WarnViewModel()
        {

        }
        private WarningValue _warningValueP;
        public WarningValue WarningValueP
        {
            get { return _warningValueP; }
            set { SetProperty(ref _warningValueP, value); }
        }


        private DelegateCommand _save;
        public DelegateCommand Save =>
            _save ?? (_save = new DelegateCommand(ExecuteSave));

        void ExecuteSave()
        {
           
            try
            {
                JsonSaveEXT.deviceParameterJsonGv.WarningValue = WarningValueP;
                JsonSaveEXT.SaveDeviceJson();
                MessageBox.Show("保存成功", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }

       
        }


        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (JsonSaveEXT.deviceParameterJsonGv.WarningValue == null)
            {
                WarningValueP = new WarningValue();
            }
            else
            {
                WarningValueP = JsonSaveEXT.deviceParameterJsonGv.WarningValue;
            }





        }
    }
}
