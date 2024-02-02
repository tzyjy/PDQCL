using ATestPackagingMachineWpf1.DeviceFile.DeviceParameter;
using ATestPackagingMachineWpf1.DeviceFile;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ATestPackagingMachineWpf1.ViewModels
{
    public class BoXing1ViewModel : BindableBase, INavigationAware
    {
        public BoXing1ViewModel()
        {

        }
        private DelegateCommand _save;
        public DelegateCommand Save =>
            _save ?? (_save = new DelegateCommand(ExecuteCommandName));

        void ExecuteCommandName()
        {
            

        }



        /// <summary>
        /// 整体参数属性
        /// </summary>
        private Wave19301Parameter _wave19301Parameter;
        public Wave19301Parameter Wave19301Parameter
        {
            get { return _wave19301Parameter; }
            set { SetProperty(ref _wave19301Parameter, value); }
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
          


        }
        #endregion
    }
}
