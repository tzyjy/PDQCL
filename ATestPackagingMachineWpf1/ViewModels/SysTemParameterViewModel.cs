using ATestPackagingMachineWpf1.Common;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;

namespace ATestPackagingMachineWpf1.ViewModels
{
    public class SysTemParameterViewModel : BindableBase, INavigationAware
    {
        #region 属性
        private Visibility _manufacturer;
        public Visibility Manufacturer
        {
            get { return _manufacturer; }
            set { SetProperty(ref _manufacturer, value); }
        }




        private Visibility _Technician;
        public Visibility Technician
        {
            get { return _Technician; }
            set { SetProperty(ref _Technician, value); }
        }




        #endregion

        private readonly IRegionManager _regionManager;



        #region 构造方法
        public SysTemParameterViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
        #endregion

        #region 切换视图

        private DelegateCommand<string> _parameterSwich;
        public DelegateCommand<string> ParameterSwich =>
            _parameterSwich ?? (_parameterSwich = new DelegateCommand<string>(ExecuteParameterSwich));

        void ExecuteParameterSwich(string parameter)
        {
            OpenView(parameter);
        }


        #endregion

        #region 打开窗体方法
        private void OpenView(string obj)
        {

            // 先把当前区域的视图列表清空
            //_regionManager.Regions["ParameterRegion"].RemoveAll();
            //通过区域去设置需要显示的内容
            if (obj.Contains('@'))
            {
                string[] strings = obj.Split('@');
                NavigationParameters param = new NavigationParameters();
                param.Add("TestName", strings[1]);
                _regionManager.Regions["ParameterRegion"].RequestNavigate(strings[0], param);
            }
            else
            {
                _regionManager.Regions["ParameterRegion"].RequestNavigate(obj);
            }


        }



        #endregion


        #region 导航接口
        public void OnNavigatedTo(NavigationContext navigationContext)
        {

            if (!GV.CurrentLogonPeson.FunctionPermission777)
            {
                Manufacturer = Visibility.Collapsed;
            }
            else
            {
                Manufacturer = Visibility.Visible;
            }

           // Technician = CommonMethod.Getlimit(Limit.工艺参数, GV.CurrentLogonPeson) == true ? Visibility.Visible : Visibility.Collapsed;

            OpenView("SetView2");
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
