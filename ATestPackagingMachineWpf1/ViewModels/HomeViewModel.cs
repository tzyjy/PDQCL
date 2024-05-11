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
        private HTTPHuShi hTTPHuShi;
        private IDialogService dialogService;
        public HomeViewModel(IDialogService dialogService)
        {
            hTTPHuShi = new HTTPHuShi();
            this.dialogService = dialogService;
            LoadDevice();
        }



        /// <summary>
        /// 记录
        /// </summary>
        private ObservableCollection<ReturnWorkOrderInfo> _ReturnWorkOrderInfoList = new ObservableCollection<ReturnWorkOrderInfo>();

        public ObservableCollection<ReturnWorkOrderInfo> ReturnWorkOrderInfoList
        {
            get { return _ReturnWorkOrderInfoList; }
            set { SetProperty(ref _ReturnWorkOrderInfoList, value); }
        }


        private ReturnWorkOrderInfo _ReturnWorkOrderInfo;
        public ReturnWorkOrderInfo ReturnWorkOrderInfo
        {
            get { return _ReturnWorkOrderInfo; }
            set { SetProperty(ref _ReturnWorkOrderInfo, value); }
        }



        public string StartDateTime { get; set; }


        /// <summary>
        /// 日志
        /// </summary>
        private ObservableCollection<OperateLog> _operateLogList;

        public ObservableCollection<OperateLog> OperateLogList
        {
            get { return _operateLogList; }
            set { SetProperty(ref _operateLogList, value); }
        }
        private RequestWorkOrderInfoPra _RequestWorkOrderInfoPra = new RequestWorkOrderInfoPra();
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
                StartDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss FFF");
                ReturnWorkOrderInfo = hTTPHuShi.Get(RequestWorkOrderInfoPra);

                if (ReturnWorkOrderInfo != null)
                {
                    ReturnWorkOrderInfoList.Add(ReturnWorkOrderInfo);
                    DV.PLC.WriteData(ReturnWorkOrderInfo, RequestWorkOrderInfoPra.wo);
                }
                MessageBox.Show("获取数据成功！");





            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }


        }




        private DelegateCommand _HuiChuan;
        public DelegateCommand HuiChuan =>
            _HuiChuan ?? (_HuiChuan = new DelegateCommand(ExecuteHuiChuan));

        void ExecuteHuiChuan()
        {
            try
            {
                UploadData uploadData = new UploadData()
                {
                    cp_rev = ReturnWorkOrderInfo.cp_rev,
                    dept_code = ReturnWorkOrderInfo.dept_code,
                    end_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss FFF"),
                    gysx_switch_off = ReturnWorkOrderInfo.wc_switch_off,
                    mach_code = RequestWorkOrderInfoPra.mach_code,
                    op_name = RequestWorkOrderInfoPra.op_name,
                    speed = ReturnWorkOrderInfo.speed,
                    start_time = StartDateTime,
                    wc_switch_off = ReturnWorkOrderInfo.wc_switch_off,
                    wo = RequestWorkOrderInfoPra.wo
                };



                var resut = hTTPHuShi.Post(uploadData);


                MessageBox.Show(resut.message + resut.status_code);





            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
