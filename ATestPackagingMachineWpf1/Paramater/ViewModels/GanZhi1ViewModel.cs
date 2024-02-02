using ATestPackagingMachineWpf1.Common;
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
    public class GanZhi1ViewModel : BindableBase,INavigationAware
    {
        public GanZhi1ViewModel()
        {

        }
        #region 保存
        private DelegateCommand _save;
        public DelegateCommand Save =>
            _save ?? (_save = new DelegateCommand(ExecuteSave));

        void ExecuteSave()
        {
            try
            {
                DV.GanZhi1.SetMode = false;
                DV.GanZhi1.WriteDeviceConfigEX(IM3570Prameter);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
         

            DV.GanZhi1.IM3570PrameterList.Select(i => i.PanelId == IM3570Prameter.PanelId ? IM3570Prameter : i);

            DV.SaveListDeviceBase();

        }



        #endregion

        #region 切换
        private DelegateCommand<object> _selChangedCommand;
        public DelegateCommand<object> SelChangedCommand =>
            _selChangedCommand ?? (_selChangedCommand = new DelegateCommand<object>(ExecuteSelChangedCommand));

        void ExecuteSelChangedCommand(object parameter)
        {
            IM3570Prameter= DV.GanZhi1.ReadJsonByPanelId(parameter.ToString());


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
            if (DV.GanZhi1.IM3570PrameterList.Count==2)
            {
                IM3570Prameter = DV.GanZhi1.ReadJsonByPanelId("1");

           
            }

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
