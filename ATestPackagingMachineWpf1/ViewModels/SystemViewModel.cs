using ATestPackagingMachineWpf1.DeviceFile;
using ATestPackagingMachineWpf1.DeviceFile.PLCFile;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ATestPackagingMachineWpf1.ViewModels
{
    public class SystemViewModel : BindableBase,IDialogAware
    {


        private PLCBase _pLC5U;
        public PLCBase PLC5U
        {
            get { return _pLC5U; }
            set { SetProperty(ref _pLC5U, value); }
        }








        CancellationTokenSource cts;
        public SystemViewModel()
        {

        }

        public string Title =>"系统状态";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            cts.Cancel();
            Thread.Sleep(500);
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK));

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            cts=new CancellationTokenSource();
            if (DV.PLC5U!=null)
            {
                Task.Run(() => {
                    ReadData();
                }, cts.Token);
            }
        
        }
        private void ReadData()
        {
            while (!cts.IsCancellationRequested)
            {

                DV.PLC5U.ReadSystemData();
                PLC5U = DV.PLC5U;
                Thread.Sleep(300);

            }


           
        }



    }
}
