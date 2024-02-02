using ATestPackagingMachineWpf1.Common;
using ATestPackagingMachineWpf1.DeviceFile;
using ATestPackagingMachineWpf1.DeviceFile.Mes;
using BTest;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using ZModels;

namespace ATestPackagingMachineWpf1.Paramater.ViewModels
{
    public class DOParameterViewModel : BindableBase, INavigationAware
    {

       


     
        public DOParameterViewModel()
        {

           

           
      
          
        }
        #region 属性
        /// <summary>
        /// 绑定Combox
        /// </summary>
        private List<byte> _longIntegerList=CommonMethod.ComboxData();
        public List<byte> LongIntegerList
        {
            get { return _longIntegerList; }
            set { SetProperty(ref _longIntegerList, value); }
        }



        private IOWriteX _IOWriteXData=new IOWriteX();
        public IOWriteX IOWriteXData
        {
            get { return _IOWriteXData; }
            set { SetProperty(ref _IOWriteXData, value); }
        }


        #endregion




        #region 命令
        /// <summary>
        /// 保存数据
        /// </summary>
        private DelegateCommand _saveData;
        public DelegateCommand SaveData =>
            _saveData ?? (_saveData = new DelegateCommand(ExecuteSaveData));

        void ExecuteSaveData()
        {
            JsonSaveEXT.deviceParameterJsonGv.iOWriteX = IOWriteXData;
            try
            {
                JsonSaveEXT.SaveDeviceJson();

                MessageBox.Show("保存成功", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }



        }





        /// <summary>
        /// 测试
        /// </summary>
        private DelegateCommand<string> _TestCommand;
        public DelegateCommand<string> TestCommand =>
            _TestCommand ?? (_TestCommand = new DelegateCommand<string>(ExecuteTestCommand));

        void ExecuteTestCommand(string parameter)
        {
             
            try
            {

                if (DV.IO1730 == null)
                {
                 
                    MessageBox.Show("检查IO连接！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                DV.IO1730?.WriteDObit(byte.Parse(parameter), 1);
                Thread.Sleep(200);
                DV.IO1730?.WriteDObit(byte.Parse(parameter), 0);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (JsonSaveEXT.deviceParameterJsonGv.iOWriteX == null)
            {
                IOWriteXData = new IOWriteX();
            }
            else
            {
                IOWriteXData = JsonSaveEXT.deviceParameterJsonGv.iOWriteX;
            }
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
