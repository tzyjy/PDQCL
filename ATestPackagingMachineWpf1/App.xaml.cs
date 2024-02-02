using ATestPackagingMachineWpf1.Common;
using ATestPackagingMachineWpf1.DeviceFile;
using ATestPackagingMachineWpf1.InterfaceData;
using ATestPackagingMachineWpf1.Paramater.ViewModels;
using ATestPackagingMachineWpf1.Paramater.Views;
using ATestPackagingMachineWpf1.ViewModels;
using ATestPackagingMachineWpf1.Views;
using DryIoc;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TDLL;
using ZModels;

namespace ATestPackagingMachineWpf1
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>

    public partial class App : PrismApplication
    {

        protected override Window CreateShell()
        {
            // 启动一个窗体MainWindow
                 return Container.Resolve<MainWindow>();
                
                ;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //containerRegistry.Register<MainWindow>();
            LoadConfig();
            //注册导航
            containerRegistry.RegisterForNavigation<MainWindow, MainWindowViewModel>();
            containerRegistry.RegisterForNavigation<UserView, UserViewModel>();
            containerRegistry.RegisterForNavigation<HandView, HandViewModel>();
            containerRegistry.RegisterForNavigation<HomeView, HomeViewModel>();
            containerRegistry.RegisterForNavigation<ParameterView, ParameterViewModel>();


            containerRegistry.RegisterForNavigation<DCRParameter, DCRParameterViewModel>();
            containerRegistry.RegisterForNavigation<DCR2View, DCR2ViewModel>();
            containerRegistry.RegisterForNavigation<DCR3View, DCR3ViewModel>();
            containerRegistry.RegisterForNavigation<DCR4View, DCR4ViewModel>();
            containerRegistry.RegisterForNavigation<IRView, IRViewModel>();
            containerRegistry.RegisterForNavigation<GanZhi1View, GanZhi1ViewModel>();
            containerRegistry.RegisterForNavigation<GanZhi2View, GanZhi2ViewModel>();
            containerRegistry.RegisterForNavigation<GanZhi3View, GanZhi3ViewModel>();
            containerRegistry.RegisterForNavigation<DIParameter, DIParameterViewModel>();
            containerRegistry.RegisterForNavigation<DOParameter, DOParameterViewModel>();
            containerRegistry.RegisterForNavigation<DeviceParameter, DeviceParameterViewModel>();
            containerRegistry.RegisterForNavigation<TempView, TempViewModel>();
            containerRegistry.RegisterForNavigation<WarnView, WarnViewModel>();
            containerRegistry.RegisterForNavigation<OpretionView, OpretionViewModel>();
            containerRegistry.RegisterForNavigation<BoXing1View, BoXing1ViewModel>();
            containerRegistry.RegisterForNavigation<ManufacturerView, ManufacturerViewModel>();
         

            //注册对话框
            containerRegistry.RegisterDialog<EditView>();
            containerRegistry.RegisterDialog<LogonView>();
            containerRegistry.RegisterDialog<DataChartViewP>();
            containerRegistry.RegisterDialog<DataShowView>();
            containerRegistry.RegisterDialog<SystemView>();
            containerRegistry.RegisterDialog<MinLoginView>();
            containerRegistry.RegisterDialog<WarnShowView>();
            containerRegistry.RegisterDialog<AboutView>();
            containerRegistry.RegisterDialog<LoadShowView>();

        }
        protected override void OnInitialized()
        {


            if (!JsonSaveEXT.deviceParameterJsonGv.IsTestMode)
            {
                var dialog = Container.Resolve<IDialogService>();
                dialog.ShowDialog("LogonView", callback =>
                {
                    if (callback.Result != ButtonResult.OK)
                    {
                        Environment.Exit(0);
                        return;
                    }
                });
                //通过自定义的配置服务接口，获取需要显示的内容和用户名
                var service = App.Current.MainWindow.DataContext as IShowLogon;
                if (service != null)
                {
                    service.ShowLogon();
                }
                base.OnInitialized();
            }
            else
            {
                var service = App.Current.MainWindow.DataContext as IOpenHomeView;
                if (service != null)
                {
                    service.Open();
                }
                base.OnInitialized();
            }

        }

        //加载参数
        private void LoadConfig()
        {
            JsonSaveEXT.deviceParameterJsonGv = JsonSaveEXT.ReadDeviceJson() == null ? new DeviceParameterJson() : JsonSaveEXT.ReadDeviceJson();
            // Activation();

            DV.LoadListDeviceBase();
        }





        static void Activation()
        {
            if (HslCommunication.Authorization.SetAuthorizationCode("8cb26b16-6848-46b8-a9e4-6f57336b2872"))
            {
            }
            {
                Console.WriteLine("Authorization failed! The current program can only be used for 8 hours!");
                return;
            }


        }
    }






}
