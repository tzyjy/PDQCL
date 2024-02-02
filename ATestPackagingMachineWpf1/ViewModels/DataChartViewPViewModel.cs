using ATestPackagingMachineWpf1.DeviceFile;
using LiveCharts;
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
    public class DataChartViewPViewModel : BindableBase,IDialogAware
    {
        #region  属性
        private ChartValues<float> _listTemp1 = new ChartValues<float>();
        public ChartValues<float> ListTemp1
        {
            get { return _listTemp1; }
            set { SetProperty(ref _listTemp1, value); }
        }



        private ChartValues<float> _listTemp2 = new ChartValues<float>();
        public ChartValues<float> ListTemp2
        {
            get { return _listTemp2; }
            set { SetProperty(ref _listTemp2, value); }
        }

      


        private ChartValues<int> _readPressure1 = new ChartValues<int>();
        public ChartValues<int> ReadPressure1
        {
            get { return _readPressure1; }
            set { SetProperty(ref _readPressure1, value); }
        }

        private ChartValues<int> _readPressure2 = new ChartValues<int>();
        public ChartValues<int> ReadPressure2
        {
            get { return _readPressure2; }
            set { SetProperty(ref _readPressure2, value); }
        }
        #endregion












        CancellationTokenSource cts;
        public DataChartViewPViewModel()
        {
            cts=new CancellationTokenSource();
        }

        public string Title => "数据显示";

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
            Task.Run(() => { ReadTemp(); }, cts.Token);

 
        }
        private void ReadTemp()
        {
            while (!cts.IsCancellationRequested)
            {


              float result1=  DV.DELTATemp1.Read();
                ListTemp1.Add(result1);
                if (ListTemp1.Count>100)
                {
                    ListTemp1.RemoveAt(0);
                }

                float result2 = DV.DELTATemp2.Read();
                ListTemp2.Add(result2);
                if (ListTemp2.Count>100)
                {
                    ListTemp2.RemoveAt(0);

                }

              List<int> ints=  DV.PLC5U.ReadPressure();
                if (ints.Count==2)
                {
                    ReadPressure1.Add(ints[0]);
                    if (ReadPressure1.Count > 100)
                    {
                        ReadPressure1.RemoveAt(0);
                    }

                    ReadPressure2.Add(ints[1]);
                    if (ReadPressure2.Count > 100)
                    {
                        ReadPressure2.RemoveAt(0);
                    }

                }








                Thread.Sleep(1000);  
            }



        }

    }
}
