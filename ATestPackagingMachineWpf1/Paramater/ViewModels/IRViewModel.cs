using ATestPackagingMachineWpf1.DeviceFile;
using ATestPackagingMachineWpf1.DeviceFile.DeviceParameter;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ATestPackagingMachineWpf1.ViewModels
{
    public class IRViewModel : BindableBase, INavigationAware
    {
        public IRViewModel()
        {

        }

        private DelegateCommand _save;
        public DelegateCommand Save =>
            _save ?? (_save = new DelegateCommand(ExecuteCommandName));

        void ExecuteCommandName()
        {
            try
            {
                DV.IR.ir11210Parameter = IR11210Parameter;
                DV.SaveListDeviceBase();
                DV.IR.WriteDeviceConfig();

                MessageBox.Show("保存成功", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }



        /// <summary>
        /// 整体参数属性
        /// </summary>
        private IR11210Parameter _iR11210Parameter;
        public IR11210Parameter IR11210Parameter
        {
            get { return _iR11210Parameter; }
            set { SetProperty(ref _iR11210Parameter, value); }
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
            IR11210Parameter = DV.IR.ir11210Parameter;


        }
        #endregion

    }
}
