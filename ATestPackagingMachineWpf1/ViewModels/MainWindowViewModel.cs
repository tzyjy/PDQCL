using ATestPackagingMachineWpf1.Common;using ATestPackagingMachineWpf1.InterfaceData;
using ATestPackagingMachineWpf1.Views;using ATestPackagingMachineWpf1.ZModels;using BTest;
using Prism.Commands;using Prism.Events;using Prism.Ioc;using Prism.Mvvm;using Prism.Regions;using Prism.Services.Dialogs;
using System;using System.Collections.Generic;using System.Collections.ObjectModel;
using System.Diagnostics;using System.Linq;using System.Runtime.InteropServices;using System.Threading;using System.Windows;using System.Windows.Controls.Primitives;namespace ATestPackagingMachineWpf1.ViewModels{    public class MainWindowViewModel : BindableBase, IShowLogon, INavigationAware, IOpenHomeView    {        #region 变量              private string CurrentTime        {            get { return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); }        }







        #endregion
        #region 属性                           private bool _isLeftDrawerOpenValue;
        public bool IsLeftDrawerOpenValue
        {
            get { return _isLeftDrawerOpenValue; }
            set { SetProperty(ref _isLeftDrawerOpenValue, value); }
        }
        private bool _menuToggleButtonChecked;
        public bool MenuToggleButtonChecked
        {
            get { return _menuToggleButtonChecked; }
            set { SetProperty(ref _menuToggleButtonChecked, value); }
        }


        private int _mySelectIndex=-1;
        public int MySelectIndex
        {
            get { return _mySelectIndex; }
            set { SetProperty(ref _mySelectIndex, value); }
        }

        private MenuBar selectitem;        public MenuBar Selectitem        {            get { return selectitem; }            set { SetProperty(ref selectitem, value); }        }        private string _title = "测包机软件";        public string Title        {            get { return _title; }            set { SetProperty(ref _title, value); }        }        private string _LogonName;        public string LogonName        {            get { return _LogonName; }            set { SetProperty(ref _LogonName, value); }        }        private List<MenuBar> _menuBars;        public List<MenuBar> MenuBars        {            get { return _menuBars; }            set { SetProperty(ref _menuBars, value); }        }        #endregion        #region 构造方法            private readonly IRegionManager _regionManager;
        private IEventAggregator eventAggregator;        private IDialogService dialogService;        public MainWindowViewModel( IRegionManager regionManager,IEventAggregator eventAggregator, IDialogService dialogService)        {            this._regionManager = regionManager;            this.eventAggregator = eventAggregator;            this.dialogService = dialogService;            MenuBars = new List<MenuBar>();            CreateMenuBar();
     

        }           #endregion        #region 菜单栏赋值        void CreateMenuBar()        {            MenuBars.Add(new MenuBar { Icon = "Home", Title = "主页", ViewName = "HomeView" , IsDiolog=false });            MenuBars.Add(new MenuBar { Icon = "HandBackLeft", Title = "手动", ViewName = "HandView", IsDiolog = false });
            MenuBars.Add(new MenuBar { Icon = "Cog", Title = "参数设置", ViewName = "ParameterView", IsDiolog = false });            MenuBars.Add(new MenuBar { Icon = "Vhs", Title = "系统状态", ViewName = "SystemView", IsDiolog = true });            MenuBars.Add(new MenuBar { Icon = "ChartAreaspline", Title = "数据图表", ViewName = "DataChartViewP", IsDiolog = true });
            MenuBars.Add(new MenuBar { Icon = "Database", Title = "测试数据", ViewName = "DataShowView", IsDiolog = true });        }














        #endregion

        #region 命令


        private DelegateCommand _openMeau;
        public DelegateCommand OpenMeau =>
            _openMeau ?? (_openMeau = new DelegateCommand(ExecuteOpenMeau));

        void ExecuteOpenMeau()
        {
           
           
        }                           private DelegateCommand _close;
        public DelegateCommand Close =>
            _close ?? (_close = new DelegateCommand(ExecuteClose));

        void ExecuteClose()
        {

            //return;
            //Environment.Exit(0);

        }


        private DelegateCommand _openHandview;
        public DelegateCommand OpenHandview =>
            _openHandview ?? (_openHandview = new DelegateCommand(ExecuteOpenHandview));

        void ExecuteOpenHandview()
        {
            OpenView("HandView");
            Title = "手动";
        }        /// <summary>                           /// 测试快捷跳转                           /// </summary>                           /// 

        private DelegateCommand _test2;
        public DelegateCommand Test2 =>
            _test2 ?? (_test2 = new DelegateCommand(ExecuteTest2));

        void ExecuteTest2()
        {
            dialogService.ShowDialog("AboutView");
        }        private DelegateCommand _Test;        public DelegateCommand Test =>            _Test ?? (_Test = new DelegateCommand(ExecuteTest));        void ExecuteTest()        {
            if (!JsonSaveEXT.deviceParameterJsonGv.Manufacturer.ManufacturerMode)
            {
                dialogService.ShowDialog("MinLoginView", callback =>
                {
                    if (callback.Result == ButtonResult.OK)
                    {
                        if (GV.CurrentLogonPeson!=null)
                        {
                            OpenView("ParameterView");
                            Title = "参数设置";
                        }
                       

                    }


                });
            }
            else
            {
                GV.CurrentLogonPeson = null;
                GV.CurrentLogonPeson = GV.ManufacturerLogonPeson;
                OpenView("ParameterView");
                Title = "参数设置";
            }


           

        }        /// <summary>        /// Home下拉列表        /// </summary>        private DelegateCommand<object> _selChangedCommande;        public DelegateCommand<object> SelChangedCommand =>            _selChangedCommande ?? (_selChangedCommande = new DelegateCommand<object>(ExecuteSelChangedCommand));        void ExecuteSelChangedCommand(object myobject)        {            if (MySelectIndex==-1)
            {
                return;
            }            MenuBar menuBar = (MenuBar)myobject;
                     OpenView(menuBar);                     MySelectIndex = -1;
            MenuToggleButtonChecked = false;        }        private DelegateCommand<string> _homeCommand;        public DelegateCommand<string> HomeCommand =>            _homeCommand ?? (_homeCommand = new DelegateCommand<string>(HomeCommandMethod));        void HomeCommandMethod(string obj)        {            OpenView("HomeView");
            Title = "主页";        }        private bool CanHomeCommandMethod()        {            return true;        }        private DelegateCommand _showUseManage;

        public event Action<IDialogResult> RequestClose;

        public DelegateCommand ShowUseManage =>            _showUseManage ?? (_showUseManage = new DelegateCommand(ExecuteShowUseManage));        void ExecuteShowUseManage()        {
                                GV.CurrentLogonPeson = null;            dialogService.ShowDialog("MinLoginView", callback =>
            {
                if (callback.Result==ButtonResult.OK)
                {
                    if (GV.CurrentLogonPeson.FunctionPermission106)
                    {
                        OpenView("UserView");

                    }
                    else
                    {
                        MessageBox.Show("权限不足！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                   
                }


            });                 }        #endregion        #region 打开窗体方法        private void OpenView(MenuBar menuBar)        {            if (menuBar.IsDiolog)
            {
                dialogService.ShowDialog(menuBar.ViewName);
            }            else
            {
                if (menuBar.ViewName== "ParameterView")
                {
              
                    if (GV.ButtonAtuo)
                    {
                  
                        MessageBox.Show("请切换手动！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (!JsonSaveEXT.deviceParameterJsonGv.Manufacturer.ManufacturerMode)
                    {
                        GV.CurrentLogonPeson = null;
                        dialogService.ShowDialog("MinLoginView");

                        if (GV.CurrentLogonPeson == null)
                        {

                            return;
                        }
                    }
                    else
                    {
                        GV.CurrentLogonPeson = null;
                        GV.CurrentLogonPeson= GV.ManufacturerLogonPeson;
                    }
                   
                   
                }
                
                //通过区域去设置需要显示的内容
                _regionManager.Regions["ContentRegion"].RequestNavigate(menuBar.ViewName);
                Title = menuBar.Title;
            }        }        private void OpenView(string obj)
        {
            _regionManager.Regions["ContentRegion"].RequestNavigate(obj);

        }        #endregion        #region 显示用户        public void ShowLogon()        {            LogonName = GV.CurrentLogonPeson.LoginAccount;            //NavigationParameters keys = new NavigationParameters();            //keys.Add("AddLogList", AddLog);            //_regionManager.Regions["ContentRegion"].RequestNavigate("HomeView",keys);        }

        #region 导航
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            OpenView("HomeView");

        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            RequestClose.Invoke(new DialogResult(ButtonResult.OK));
        }

        public void Open()
        {
            OpenView("HomeView");
        }

        #endregion



        #endregion






    }}