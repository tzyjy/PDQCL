using ATestPackagingMachineWpf1.Common;
using ATestPackagingMachineWpf1.ZModels;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using TDLL;
using ZModels;

namespace ATestPackagingMachineWpf1.ViewModels
{
    public class MinLoginViewModel : BindableBase,IDialogAware
    {

        private LogonService logonService;
        public MinLoginViewModel(LogonService logonService)
        {
                this.logonService = logonService;
        }
        #region 属性
        /// <summary>
        /// 登录名
        /// </summary>
        private string _loginName;
        public string LoginName
        {
            get { return _loginName; }
            set
            {
                SetProperty(ref _loginName, value);
            }
        }
        /// <summary>
        /// 登录提示信息
        /// </summary>
        private string _loginTip;
        public string LoginTip
        {
            get { return _loginTip; }
            set
            {
                SetProperty(ref _loginTip, value);
            }
        }
        /// <summary>
        /// 登录密码
        /// </summary>
        private string _loginPwd;
        public string LoginPwd
        {
            get { return _loginPwd; }
            set { SetProperty(ref _loginPwd, value); }
        }


        #endregion



        #region 弹窗
        public string Title => "用户登录";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.No));
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {

        }

        #endregion

        #region 登录

        private DelegateCommand _loginMethod;
        public DelegateCommand LoginMethod =>
            _loginMethod ?? (_loginMethod = new DelegateCommand(ExecuteLoginMethod));

        void ExecuteLoginMethod()
        {
            if (string.IsNullOrWhiteSpace(LoginName) || string.IsNullOrWhiteSpace(LoginPwd))
            {
                LoginTip = "用户名或密码不能为空!!!";
                return;
            }
            else
            {

                if (LoginName == "777" && LoginPwd == "777")
                {

                    GV.CurrentLogonPeson = GV.ManufacturerLogonPeson;

                    RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
                    return;
                }



                LogonPeson logonPeople = new LogonPeson()
                {
                    LoginAccount = LoginName,
                    LoginPwd = LoginPwd,
                };
                logonPeople = logonService.AdminLogin(logonPeople);
                if (logonPeople == null)
                {
                    LoginTip = "**用户名或密码错误,请重新输入!!!";
             
                    LoginPwd = "";
                    return;
                }
                else
                {
                    GV.CurrentLogonPeson = logonPeople;

                    RequestClose?.Invoke(new DialogResult(ButtonResult.OK));

                }
            }


        }
        #endregion




    }
}
