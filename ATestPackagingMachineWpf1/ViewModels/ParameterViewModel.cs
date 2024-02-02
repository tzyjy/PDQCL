using ATestPackagingMachineWpf1.Common;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ATestPackagingMachineWpf1.ViewModels
{
    public class ParameterViewModel : BindableBase, INavigationAware
    {



        #region 属性
        private Visibility _manufacturer;
        public Visibility Manufacturer
        {
            get { return _manufacturer; }
            set { SetProperty(ref _manufacturer, value); }
        }
        #endregion

        private readonly IRegionManager _regionManager;
        private IRegionNavigationJournal journal;           //导航日志


        #region 构造方法
        public ParameterViewModel(IRegionManager regionManager)
        {
            _regionManager=regionManager;
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


        private DelegateCommand _ViewGo;
        public DelegateCommand ViewGo =>
            _ViewGo ?? (_ViewGo = new DelegateCommand(ExecuteViewGo, canExecuteViewGo));

        void ExecuteViewGo()
        {
            if (this.journal != null && journal.CanGoForward)
            {
          
                journal.GoForward();
            }
           
              


        }
         bool canExecuteViewGo()
        {
            return true;
            //if (this.journal != null && journal.CanGoForward)
            //{
            //    return true;
            //}
            //{
            //    return false;
            //}
              
        }
        private DelegateCommand _ViewBack;
        public DelegateCommand ViewBack =>
            _ViewBack ?? (_ViewBack = new DelegateCommand(ExecuteViewBack, CanExecuteViewBack));

         bool CanExecuteViewBack()
        {
            return true;
            //if (this.journal != null && journal.CanGoBack)
            //{
            //    return true;
            //}
            //{
            //    return false;
            //}
        }

        void ExecuteViewBack()
        {
            if (this.journal != null && journal.CanGoBack)
                journal.GoBack();

        }




        #endregion

        #region 打开窗体方法
        private void OpenView(string obj)
        {

            // 先把当前区域的视图列表清空
            //_regionManager.Regions["ParameterRegion"].RemoveAll();
            //通过区域去设置需要显示的内容
            _regionManager.Regions["ParameterRegion"].RequestNavigate(obj, callBack =>
            {
                if ((bool)callBack.Result)
                {
                    journal = callBack.Context.NavigationService.Journal;
                }
            });




        }



        #endregion

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
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }




    }
}
