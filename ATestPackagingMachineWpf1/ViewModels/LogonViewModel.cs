using ATestPackagingMachineWpf1.Common;
using ATestPackagingMachineWpf1.ZModels;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using TDLL;
using ZModels;

namespace ATestPackagingMachineWpf1.ViewModels
{
    public class LogonViewModel : BindableBase, IDialogAware
    {


        private string path = AppDomain.CurrentDomain.BaseDirectory + "saveAccountInfo.json";
        private LogonService logonService;
        private IEventAggregator eventAggregator;
        public LogonViewModel(LogonService logonService, IEventAggregator eventAggregator)
        {
            if (File.Exists(path))
            {

                SaveAccountInfo saveAccountInfo = (SaveAccountInfo)JsonSaveEXT.DeserializeObjectFromFile(System.AppDomain.CurrentDomain.BaseDirectory + "saveAccountInfo.json", "tzy7777777qqqqqq");
                IsCheckedOn = true;
                LoginName = saveAccountInfo.SaveLoginId.ToString();
                LoginPwd = saveAccountInfo.SaveLoginPwd.ToString();
                if (DateTime.Now > Convert.ToDateTime(saveAccountInfo.SaveEndDataTime))
                {
                    File.Delete(System.AppDomain.CurrentDomain.BaseDirectory + "saveAccountInfo");
                    LoginName = "";
                    LoginPwd = "";
                    IsCheckedOn = false;
                    File.Delete(path);
              
                    MessageBox.Show("账号保存期有效期已过,请重新输入", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
            else
            {
                IsCheckedOn = false;
            }
            this.logonService = logonService;
            this.eventAggregator = eventAggregator;
        }


        #region 属性

        /// <summary>
        /// 是否被选中
        /// </summary>

        private bool _isCheckedOn;
        public bool IsCheckedOn
        {
            get { return _isCheckedOn; }
            set { SetProperty(ref _isCheckedOn, value); }
        }


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




        #region 方法



        /// <summary>
        /// 记住密码是否被选中
        /// </summary>

        private DelegateCommand _IscheckedCommand;
        public DelegateCommand IscheckedCommand =>
            _IscheckedCommand ?? (_IscheckedCommand = new DelegateCommand(ExecuteIscheckedCommand));

        void ExecuteIscheckedCommand()
        {
            if (!IsCheckedOn)
            {
                File.Delete(path);

            }
        }



        private DelegateCommand _closeForm;
        public DelegateCommand CloseForm =>
            _closeForm ?? (_closeForm = new DelegateCommand(ExecuteCloseForm));

        void ExecuteCloseForm()
        {
            Environment.Exit(0);
        }


        private DelegateCommand _loginMethod;

        public event Action<IDialogResult> RequestClose;

        public DelegateCommand LoginMethod =>
            _loginMethod ?? (_loginMethod = new DelegateCommand(ExecuteLoginMethod));

        public string Title => "登录窗体";

        void ExecuteLoginMethod()
        {

            if (string.IsNullOrWhiteSpace(LoginName) || string.IsNullOrWhiteSpace(LoginPwd))
            {
                LoginTip = "用户名或密码不能为空!!!";
                return;
            }
            else
            {
                LogonPeson logonPeople = new LogonPeson()
                {
                    LoginAccount = LoginName,
                    LoginPwd = LoginPwd,
                };
                logonPeople = logonService.AdminLogin(logonPeople);
                if (logonPeople == null)
                {
                    LoginTip = "**用户名或密码错误,请重新输入!!!";
                    LoginName = "";
                    LoginPwd = "";
                    return;
                }
                else
                {
                    if (IsCheckedOn && !File.Exists(path))
                    {
                        SaveAccountInfo saveAccountInfo = new SaveAccountInfo()
                        {
                            SaveLoginId = logonPeople.LoginAccount,
                            SaveLoginPwd = logonPeople.LoginPwd,
                            SaveStartDataTime = DateTime.Now.ToString(),
                            SaveEndDataTime = DateTime.Now.AddHours(JsonSaveEXT.deviceParameterJsonGv.TimeHour).ToString(),

                        };
                        JsonSaveEXT.SerializeObjectToFile(path, saveAccountInfo, "tzy7777777qqqqqq");

                    }
                    GV.CurrentLogonPeson = logonPeople;



                    RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
                  
                }
            }
        }

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



    }
}
