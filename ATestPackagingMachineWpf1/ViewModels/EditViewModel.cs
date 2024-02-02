using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using TDLL;
using ZModels;


namespace ATestPackagingMachineWpf1.ViewModels
{
    public class EditViewModel : BindableBase, IDialogAware
    {
        private LogonService LoginService;

        public EditViewModel(LogonService LoginService)
        {
            this.LoginService = LoginService;
        }
        private LogonPeson _logonPeson = new LogonPeson();


      
       

        public LogonPeson LogonPesonData
        {
            get { return _logonPeson; }
            set { SetProperty(ref _logonPeson, value); }
        }

        private DelegateCommand _modify;

        public event Action<IDialogResult> RequestClose;

        public DelegateCommand Modify =>
            _modify ?? (_modify = new DelegateCommand(ExecuteModify));

        public string Title => "修改账户信息";


  
        void ExecuteModify()
        {
            try
            {
                LoginService.UpDate(LogonPesonData);
                RequestClose.Invoke(new DialogResult(ButtonResult.OK));
                

              
            }
            catch (Exception ex)
            {
         
                MessageBox.Show(ex.Message, "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public bool CanCloseDialog()
        {
            return true;
        }
        /// <summary>
        /// 关闭窗体的方法
        /// </summary>
        public void OnDialogClosed()
        {
           
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            // 获取传递bookId
            string logonId = parameters.GetValue<string>("LoginId");
            // 根据图书ID获取图书信息
            LogonPesonData = LoginService.GetDataById(int.Parse(logonId));
        }
    }
}
