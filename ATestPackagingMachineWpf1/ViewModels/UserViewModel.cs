using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using TDLL;
using ZModels;


namespace ATestPackagingMachineWpf1.ViewModels
{
    public class UserViewModel : BindableBase
    {


        private LogonService logonService;
        private IDialogService dialogService = null;
        public UserViewModel(LogonService logonService, IDialogService dialogService)
        {
   
            this.dialogService = dialogService;
            this.logonService = logonService;
            DataRefreh();
        }

     
        #region 属性
        private List<LogonPeson> _logonPesons;

        public List<LogonPeson> LogonPesons
        {
            get { return _logonPesons; }
            set { SetProperty(ref _logonPesons, value); }
        }


        private LogonPeson _logonPeson = new LogonPeson();

        public LogonPeson LogonPesonData
        {
            get { return _logonPeson; }
            set { SetProperty(ref _logonPeson, value); }
        }
        #endregion

        #region 命令
        private DelegateCommand _add;
        public DelegateCommand Add =>
            _add ?? (_add = new DelegateCommand(AddM));


        public void AddM()
        {
            string asd = logonService.ToString();
            //数据验证
            if (LogonPesonData.LoginAccount == null || LogonPesonData.LoginPwd == null)
            {
                MessageBox.Show("请输入账号和密码", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            logonService.Add(LogonPesonData);
            DataRefreh();

        }

        //删除
        private DelegateCommand<object> _remove;
        public DelegateCommand<object> Remove =>
            _remove ?? (_remove = new DelegateCommand<object>(RemoveM));

        void RemoveM(object id)
        {
         var result=   MessageBox.Show("确定要删除数据吗？","提示",MessageBoxButton.OKCancel,MessageBoxImage.Warning);
            if (result == MessageBoxResult.OK)
            {
                logonService.Remove((int)id);
                DataRefreh();


            }
    

        }


    //更新

        private DelegateCommand<object> _update;
        public DelegateCommand<object> Update =>
            _update ?? (_update = new DelegateCommand<object>(UpdateM));

        void UpdateM(object id)
        { // 创建对话框的参数对象
            var parameters = new DialogParameters();
            // 添加参数key和value
            parameters.Add("LoginId", id);
            // 打开修改图书的对话框
            dialogService.ShowDialog("EditView", parameters, CloseDialogAction);
            
    
;        }

        private void CloseDialogAction(IDialogResult result)
        {
            if (result.Result == ButtonResult.OK)
            {
                DataRefreh();
            }
        }




        #endregion


        public void DataRefreh()
        {
            LogonPesons = logonService.GetAllData();
        }

    }
}
