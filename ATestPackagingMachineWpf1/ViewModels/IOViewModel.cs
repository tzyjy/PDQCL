using ATestPackagingMachineWpf1.Common;
using ImTools;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ATestPackagingMachineWpf1.ViewModels
{
    public class IOViewModel : BindableBase, IConfirmNavigationRequest
    {
        private CancellationTokenSource cts;
        public IOViewModel()
        {

        }
        private ObservableCollection<bool> _DiboolArray=new ObservableCollection<bool>()
        {
            false, false, false, false, false, false, false, false,
          
        };
        public ObservableCollection<bool> DiboolArray
        {
            get { return _DiboolArray; }
            set { SetProperty(ref _DiboolArray, value); }
        }

        private DelegateCommand<string> _DoHandTest;
        public DelegateCommand<string> DoHandTest =>
            _DoHandTest ?? (_DoHandTest = new DelegateCommand<string>(ExecuteDoHandTest));

        void ExecuteDoHandTest(string parameter)
        {

            DV.iODevice.Send(parameter, true);

        }
        private DelegateCommand _DITest;
        public DelegateCommand DITest =>
            _DITest ?? (_DITest = new DelegateCommand(ExecuteDITest));

        void ExecuteDITest()
        {
            DV.iODevice.ReadDI();
        }

        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            if (cts!=null)
            {
                cts.Cancel();
                if (cts.IsCancellationRequested)
                {
                    continuationCallback(true);
                }
            }
            else
            {
                continuationCallback(true);
            }
       
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (DV.iODevice == null)
            {
                MessageBox.Show("检查IO连接！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }
          
            cts = new CancellationTokenSource();
            Task.Run(() => { GetDIArray(); }, cts.Token);
        }

        private void GetDIArray()
        {
            while (!cts.IsCancellationRequested)
            {
                try
                {
                    List<bool> bools = DV.iODevice.ReadDI();
                    DiboolArray[0] = bools[0];
                    DiboolArray[1] = bools[1];
                    DiboolArray[2] = bools[2];
                    DiboolArray[3] = bools[3];
                    DiboolArray[4] = bools[4];
                    DiboolArray[5] = bools[5];
                    DiboolArray[6] = bools[6];
                    DiboolArray[7] = bools[7];
                   
                    Thread.Sleep(30);
             
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                }
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
          
        }
    }
}
