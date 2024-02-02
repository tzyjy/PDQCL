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
    public class DCR3ViewModel : BindableBase, INavigationAware
    {
        public DCR3ViewModel()
        {

        }
        private AX11520DParameter _aX11520DParameterP;
        public AX11520DParameter AX11520DParameterP
        {
            get { return _aX11520DParameterP; }
            set { SetProperty(ref _aX11520DParameterP, value); }
        }
        #region 保存
        private DelegateCommand _save;
        public DelegateCommand Save =>
            _save ?? (_save = new DelegateCommand(ExecuteCommandName));

        void ExecuteCommandName()
        {
            try
            {
                List<int> ints = InstrumentCommon.LowHighDeal(AX11520DParameterP.TestScale, DCRHiAndLowShow[0], DCRHiAndLowShow[1]);
                AX11520DParameterP.DCRLow = ints[0];
                AX11520DParameterP.DCRHigh = ints[1];
                DV.SaveListDeviceBase();
                DV.DCR3.WriteDeviceConfig();

                MessageBox.Show("保存成功", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            
            }


        }
        #endregion





        private List<decimal> _dCRHiAndLowShow;
        public List<decimal> DCRHiAndLowShow
        {
            get { return _dCRHiAndLowShow; }
            set { SetProperty(ref _dCRHiAndLowShow, value); }
        }


        #region 导航接口
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            AX11520DParameterP = DV.DCR3.aX11520DParameter;
            DCRHiAndLowShow = InstrumentCommon.LowHighShow(AX11520DParameterP.TestScale, AX11520DParameterP.DCRLow, AX11520DParameterP.DCRHigh);
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
