using ATestPackagingMachineWpf1.Common;
using ATestPackagingMachineWpf1.DeviceFile;
using ATestPackagingMachineWpf1.ZModels;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using ZModels;

namespace ATestPackagingMachineWpf1.ViewModels
{
    public class HomeViewModel : BindableBase
    {
        private HTTPHuShi  hTTPHuShi;
        private IDialogService dialogService;
        public HomeViewModel(IDialogService dialogService)
        {
            hTTPHuShi=new HTTPHuShi();
            this.dialogService = dialogService;
            LoadDevice();
        }



        /// <summary>
        /// 记录
        /// </summary>
        private ObservableCollection<ReturnWorkOrderInfo> _ReturnWorkOrderInfoList=new ObservableCollection<ReturnWorkOrderInfo>();

        public ObservableCollection<ReturnWorkOrderInfo> ReturnWorkOrderInfoList
        {
            get { return _ReturnWorkOrderInfoList; }
            set { SetProperty(ref _ReturnWorkOrderInfoList, value); }
        }









        /// <summary>
        /// 日志
        /// </summary>
        private ObservableCollection<OperateLog> _operateLogList;

        public ObservableCollection<OperateLog> OperateLogList
        {
            get { return _operateLogList; }
            set { SetProperty(ref _operateLogList, value); }
        }
        private RequestWorkOrderInfoPra _RequestWorkOrderInfoPra=new RequestWorkOrderInfoPra();
        public RequestWorkOrderInfoPra RequestWorkOrderInfoPra
        {
            get { return _RequestWorkOrderInfoPra; }
            set { SetProperty(ref _RequestWorkOrderInfoPra, value); }
        }

        #region 加载设备配置

        public void LoadDevice()
        {
            dialogService.ShowDialog("LoadShowView", callback =>
            {
                if (callback.Result == ButtonResult.OK)
                {
                    OperateLogList = callback.Parameters.GetValue<ObservableCollection<OperateLog>>("OperateLogList");
                }
            });

           
        }

        #endregion 加载设备配置

        private DelegateCommand _Send;
        public DelegateCommand Send =>
            _Send ?? (_Send = new DelegateCommand(ExecuteSend));

        void ExecuteSend()
        {
            try
            {
                ReturnWorkOrderInfo returnWorkOrderInfo = hTTPHuShi.Get(RequestWorkOrderInfoPra);
                
                if (returnWorkOrderInfo != null)
                {
                    ReturnWorkOrderInfoList.Add(returnWorkOrderInfo);
                    DV.PLC.WriteData(returnWorkOrderInfo, RequestWorkOrderInfoPra.wo);
                }
                MessageBox.Show("获取数据成功！");

             


            
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        
           
        }
    }
}
