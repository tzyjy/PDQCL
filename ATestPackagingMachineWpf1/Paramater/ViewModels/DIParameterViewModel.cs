using ATestPackagingMachineWpf1.Common;
using ATestPackagingMachineWpf1.DeviceFile;
using ATestPackagingMachineWpf1.DeviceFile.Mes;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ZModels;

namespace ATestPackagingMachineWpf1.Paramater.ViewModels
{
    public class DIParameterViewModel : BindableBase, IConfirmNavigationRequest
    {
        CancellationTokenSource cts;
        public DIParameterViewModel()
        {
         

        }




        #region 属性


        private string _pLCAdress;
        public string PLCAdress
        {
            get { return _pLCAdress; }
            set { SetProperty(ref _pLCAdress, value); }
        }



        /// <summary>
        /// 绑定Combox
        /// </summary>
        private List<byte> _longIntegerList= CommonMethod.ComboxData();
        public List<byte> LongIntegerList
        {
            get { return _longIntegerList; }
            set { SetProperty(ref _longIntegerList, value); }
        }

        /// <summary>
        /// 绑定设置参数
        /// </summary>
        /// 
        private IOReadY _iOReadY ;
        public IOReadY IOReadYData
        {
            get { return _iOReadY; }
            set { SetProperty(ref _iOReadY, value); }
        }

        /// <summary>
        /// DI数组显示
        /// </summary>
        private ObservableCollection<bool> diboolArray =new ObservableCollection<bool>()
        {
            true, false, false, false, false, true, false, false,
            false, false,false,false,false,false,false,true
        };
        public ObservableCollection<bool> DiboolArray
        {
            get { return diboolArray; }
            set { SetProperty(ref diboolArray, value); }
        }


        #endregion

        #region 命令
        /// <summary>
        /// 保存数据
        /// </summary>
        private DelegateCommand _saveData;

   

        public DelegateCommand SaveData =>
            _saveData ?? (_saveData = new DelegateCommand(ExecuteSaveData));

        public string Title => throw new NotImplementedException();

        void ExecuteSaveData()
        {
            JsonSaveEXT.deviceParameterJsonGv.iOReadY = IOReadYData;
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
        #endregion

        #region 多线程显示状态
        public void GetDIArray()
        {
            Random random = new Random();
            int i = 0;
            while (!cts.IsCancellationRequested) 
            {
                try
                {
                    if (DV.IO1730 == null)
                    {
                        MessageBox.Show("检查IO连接！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                        
                        break;
                    }
                    bool[] bools = DV.IO1730.ReadAllDi();
                    DiboolArray[0] = bools[0];
                    DiboolArray[1] = bools[1];
                    DiboolArray[2] = bools[2];
                    DiboolArray[3] = bools[3];
                    DiboolArray[4] = bools[4];
                    DiboolArray[5] = bools[5];
                    DiboolArray[6] = bools[6];
                    DiboolArray[7] = bools[7];
                    DiboolArray[8] = bools[8];
                    DiboolArray[9] = bools[9];
                    DiboolArray[10] = bools[10];
                    DiboolArray[11] = bools[11];
                    DiboolArray[12] = bools[12];
                    DiboolArray[13] = bools[13];
                    DiboolArray[14] = bools[14];
                    DiboolArray[15] = bools[15];

                    Thread.Sleep(100);
                    Console.WriteLine("多线程执行中" + i++.ToString());
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                }
            
            }


        }

        /// <summary>
        /// 导航进入
        /// </summary>
        /// <param name="navigationContext"></param>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (JsonSaveEXT.deviceParameterJsonGv.iOReadY == null)
            {
                IOReadYData = new IOReadY();
            }
            else
            {
                IOReadYData = JsonSaveEXT.deviceParameterJsonGv.iOReadY;
            }
            cts = new CancellationTokenSource();
            Task.Run(() => { GetDIArray(); }, cts.Token);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            Uri uri = navigationContext.Uri;
            Console.WriteLine(uri.ToString());
        }

        /// <summary>
        /// 导航拦截
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <param name="continuationCallback"></param>
        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            cts.Cancel();
            if (cts.IsCancellationRequested)
            {
                continuationCallback(true);
            }
        }





        #endregion

        #region 写PLC测试
        private DelegateCommand _testByPLC;
        public DelegateCommand TestByPLC =>
            _testByPLC ?? (_testByPLC = new DelegateCommand(ExecuteTestByPLC));

        void ExecuteTestByPLC()
        {
            if (PLCAdress==null)
            {
                return;
            }
            if (!PLCAdress.Contains("Y"))
            {
              
                MessageBox.Show("请输入Y1,Y2等正确PLC地址", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                DV.PLC5U?.WriteYTest(PLCAdress, true);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        #endregion

    }
}
