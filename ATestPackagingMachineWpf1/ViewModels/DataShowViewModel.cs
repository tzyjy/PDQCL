using ATestPackagingMachineWpf1.Common;
using ATestPackagingMachineWpf1.DeviceFile;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using TDLL;
using ZModels;

namespace ATestPackagingMachineWpf1.ViewModels
{
    public class DataShowViewModel : BindableBase, IDialogAware
    {
        CancellationTokenSource cts;
        public DataShowViewModel()
        {

        }


        private Visibility _isShowButton= Visibility.Collapsed;
        public Visibility IsShowButton
        {
            get { return _isShowButton; }
            set { SetProperty(ref _isShowButton, value); }
        }






        private AllProductNumber _allProductNumber;
        public AllProductNumber AllProductNumber
        {
            get { return _allProductNumber; }
            set { SetProperty(ref _allProductNumber, value); }
        }



        private List<string> _nowPassList;
        public List<string> NowPassList
        {
            get { return _nowPassList; }
            set { SetProperty(ref _nowPassList, value); }
        }
        private ObservableCollection<InstrumentDataParentClass> _dCR12Data;
        public ObservableCollection<InstrumentDataParentClass> DCR12Data
        {
            get { return _dCR12Data; }
            set { SetProperty(ref _dCR12Data, value); }
        }

        private ObservableCollection<InstrumentDataParentClass> _dCR34Data;
        public ObservableCollection<InstrumentDataParentClass> DCR34Data
        {
            get { return _dCR34Data; }
            set { SetProperty(ref _dCR34Data, value); }
        }

        private ObservableCollection<InstrumentDataParentClass> _iRData;
        public ObservableCollection<InstrumentDataParentClass> IRData
        {
            get { return _iRData; }
            set { SetProperty(ref _iRData, value); }
        }

        private ObservableCollection<InstrumentDataParentClass> _BoXingData;
        public ObservableCollection<InstrumentDataParentClass> BoXingData
        {
            get { return _BoXingData; }
            set { SetProperty(ref _BoXingData, value); }
        }

        private ObservableCollection<InstrumentDataParentClass> _ganZhi1Data;
        public ObservableCollection<InstrumentDataParentClass> GanZhi1Data
        {
            get { return _ganZhi1Data; }
            set { SetProperty(ref _ganZhi1Data, value); }
        }


        private ObservableCollection<InstrumentDataParentClass> _ganZhi2Data;
        public ObservableCollection<InstrumentDataParentClass> GanZhi2Data
        {
            get { return _ganZhi2Data; }
            set { SetProperty(ref _ganZhi2Data, value); }
        }


        private ObservableCollection<InstrumentDataParentClass> _ganZhi3Data;
        public ObservableCollection<InstrumentDataParentClass> GanZhi3Data
        {
            get { return _ganZhi3Data; }
            set { SetProperty(ref _ganZhi3Data, value); }
        }




  
        public string Title => "测试数据界面";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
            cts.Cancel();
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            if (JsonSaveEXT.deviceParameterJsonGv.Manufacturer.ManufacturerMode)
            {
                IsShowButton = Visibility.Visible;
            }
            else
            {
                IsShowButton = Visibility.Collapsed;

            }


            cts = new CancellationTokenSource();
            Task.Run(() => { DataUpdate(); }, cts.Token);
        }



        private void DataUpdate()

        {

            while (!cts.IsCancellationRequested)
            {
                DCR12Data = DV.DCR1.InstrumentDataParentClassList;
                DCR34Data = DV.DCR3.InstrumentDataParentClassList;
                IRData = DV.IR.InstrumentDataParentClassList;
                GanZhi1Data = DV.GanZhi1.InstrumentDataParentClassList;
                GanZhi2Data = DV.GanZhi2.InstrumentDataParentClassList;

                NowPassList = InstrumentCommon.GetAllPassRateString();

                AllProductNumber = InstrumentCommon.GetAllNumber();


                Thread.Sleep(100);

            }


        }


        private DelegateCommand _clear;
        public DelegateCommand Clear =>
            _clear ?? (_clear = new DelegateCommand(ExecuteClear));

        void ExecuteClear()
        {

            var result = MessageBox.Show("确定要删除数据吗？", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.OK)
            {

                InstrumentCommon.ClearData();

            }

        }
    }
}
