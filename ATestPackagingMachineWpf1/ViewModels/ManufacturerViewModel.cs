using ATestPackagingMachineWpf1.Common;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZModels;

namespace ATestPackagingMachineWpf1.ViewModels
{
    public class ManufacturerViewModel : BindableBase, INavigationAware
    {
        private DeviceParameterJson _DeviceParameterJson;
        public DeviceParameterJson DeviceParameterJson
        {
            get { return _DeviceParameterJson; }
            set { SetProperty(ref _DeviceParameterJson, value); }
        }


     


        /// <summary>
        /// 保存数据
        /// </summary>
        private DelegateCommand _saveData;
        public DelegateCommand SaveData =>
            _saveData ?? (_saveData = new DelegateCommand(ExecuteSaveData));

        void ExecuteSaveData()
        {
            JsonSaveEXT.deviceParameterJsonGv = DeviceParameterJson;
        
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

        #region 导航接口
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
        
            DeviceParameterJson = JsonSaveEXT.deviceParameterJsonGv;


        }



        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
        #endregion









    }
}
