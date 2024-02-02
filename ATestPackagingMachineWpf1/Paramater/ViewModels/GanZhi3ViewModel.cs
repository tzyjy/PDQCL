using ATestPackagingMachineWpf1.DeviceFile.DeviceParameter;
using ATestPackagingMachineWpf1.DeviceFile;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ATestPackagingMachineWpf1.ViewModels
{
    public class GanZhi3ViewModel : BindableBase, INavigationAware
    {
        public GanZhi3ViewModel()
        {

        }

        #region 保存
        private DelegateCommand _save;
        public DelegateCommand Save =>
            _save ?? (_save = new DelegateCommand(ExecuteSave));

        void ExecuteSave()
        {


        }



        #endregion

        #region 切换
        private DelegateCommand<object> _selChangedCommand;
        public DelegateCommand<object> SelChangedCommand =>
            _selChangedCommand ?? (_selChangedCommand = new DelegateCommand<object>(ExecuteSelChangedCommand));

        void ExecuteSelChangedCommand(object parameter)
        {
         


        }
        #endregion

        #region 绑定Combox属性


        /// <summary>
        /// 整体参数属性
        /// </summary>
        private IM3570Prameter _iM3570Prameter;
        public IM3570Prameter IM3570Prameter
        {
            get { return _iM3570Prameter; }
            set { SetProperty(ref _iM3570Prameter, value); }
        }






        #region 导航
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            

        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
        #endregion



        #endregion
    }
}
