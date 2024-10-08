﻿using ATestPackagingMachineWpf1.Common;

using ATestPackagingMachineWpf1.InterfaceData;

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
using System.Diagnostics;
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
            string processName = Process.GetCurrentProcess().ProcessName;
            if (Process.GetProcessesByName(processName).Length > 1)
            {
                MessageBox.Show("软件已经运行！", "系统运行", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0);
            }
            //containerRegistry.Register<MainWindow>();
            LoadConfig();
            //注册导航
            containerRegistry.RegisterForNavigation<MainWindow, MainWindowViewModel>();
            containerRegistry.RegisterForNavigation<HomeView, HomeViewModel>();
            containerRegistry.RegisterForNavigation<HandView, HandViewModel>();
            containerRegistry.RegisterForNavigation<IOView, IOViewModel>();
            containerRegistry.RegisterForNavigation<UserView, UserViewModel>();
            containerRegistry.RegisterForNavigation<SysTemParameterView, SysTemParameterViewModel>();
            containerRegistry.RegisterForNavigation<ManufacturerView, ManufacturerViewModel>();

            containerRegistry.RegisterForNavigation<SetView, SetViewModel>();

            containerRegistry.RegisterForNavigation<SetView2, SetView2Model>();
            containerRegistry.RegisterForNavigation<DeviceParameterView, DeviceParameterViewModel>();

            //注册对话框
            containerRegistry.RegisterDialog<EditView>();
            containerRegistry.RegisterDialog<LogonView>();

            containerRegistry.RegisterDialog<MinLoginView>();

            containerRegistry.RegisterDialog<AboutView>();
            containerRegistry.RegisterDialog<LoadShowView>();
        }

        protected override void OnInitialized()
        {
            bool result = true;

            if (!result)
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
            Activation();
        }

        private static void Activation()
        {
            //try
            //{
            //    List<int> list = new List<int>() { 1, 2, 3 };
            //    var result = list[4];
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}

            if (HslCommunication.Authorization.SetAuthorizationCode("8cb26b16-6848-46b8-a9e4-6f57336b2872"))
            {
                Console.WriteLine("激活成功！");
            }
            {
                Console.WriteLine("Authorization failed! The current program can only be used for 8 hours!");
                return;
            }
        }
    }
}