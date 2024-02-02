using ATestPackagingMachineWpf1.Common;
using ATestPackagingMachineWpf1.DeviceFile;
using ATestPackagingMachineWpf1.DeviceFile.Mes;
using ATestPackagingMachineWpf1.ZModels;
using LiveCharts.Defaults;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;
using TDLL;

namespace ATestPackagingMachineWpf1.ViewModels
{
    public class HandViewModel : BindableBase, INavigationAware
    {
        private IDialogService dialogService;
        private IEventAggregator eventAggregator;
        public HandViewModel(IDialogService dialogService, IEventAggregator eventAggregator)
        {
            this.dialogService = dialogService;
            this.eventAggregator = eventAggregator;
        }

        private ObservableCollection<bool> _listEnble = new ObservableCollection<bool>();
        public ObservableCollection<bool> ListEnble
        {
            get { return _listEnble; }
            set { SetProperty(ref _listEnble, value); }
        }

        #region 仪器禁用保存
        private DelegateCommand _save;
        public DelegateCommand Save =>
            _save ?? (_save = new DelegateCommand(ExecuteSave));

        void ExecuteSave()
        {
            if (!JsonSaveEXT.deviceParameterJsonGv.Manufacturer.ManufacturerMode)
            {
                GV.CurrentLogonPeson = null;
                dialogService.ShowDialog("MinLoginView");

                if (GV.CurrentLogonPeson == null)
                {

                    return;
                }
                if (!GV.CurrentLogonPeson.FunctionPermission106)
                {

                    MessageBox.Show("权限不足！", "提示", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            //try
            //{

            bool? result = DV.PLC5U?.WriteEnable(ListEnble.ToList());

            DV.DCR1.Enable = ListEnble[0];
            DV.IR.Enable = ListEnble[1];
            DV.ZZhi.Enable = ListEnble[2];
            DV.GanZhi1.Enable = ListEnble[3];
            DV.GanZhi2.Enable = ListEnble[4];
            DV.DCR3.Enable = ListEnble[5];
            DV.SZhi.Enable = ListEnble[6];
            DV.DMianCCD.Enable = ListEnble[7];
            DV.SaveListDeviceBase();

            GV.ChangeEnableColorMethod();

            if ((bool)result)
            {
                MessageBox.Show("保存成功", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("保存仪器禁用参数失败！", "错误提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show(ex.Message);
            //}


        }






        #endregion

        #region DCR1测试

        private string _dCR1Text;
        public string DCR1Text
        {
            get { return _dCR1Text; }
            set { SetProperty(ref _dCR1Text, value); }
        }


        private DelegateCommand _dCR1Triger;
        public DelegateCommand DCR1Triger =>
            _dCR1Triger ?? (_dCR1Triger = new DelegateCommand(ExecuteDCR1Triger));
        void ExecuteDCR1Triger()
        {


            // eventAggregator.GetEvent<MessageEvent>().Publish(new LogInfo() { OK = false, InfoText = "事件发布1" });
            try
            {
                DV.DCR1.Trigger();
                DCR1Text = DV.DCR1.ReadTestData();
                Console.WriteLine(DCR1Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误提示", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }







        #endregion

        #region DCR2测试

        private string _dCR2Text;
        public string DCR2Text
        {
            get { return _dCR2Text; }
            set { SetProperty(ref _dCR2Text, value); }
        }


        private DelegateCommand _dCR2Triger;
        public DelegateCommand DCR2Triger =>
            _dCR2Triger ?? (_dCR2Triger = new DelegateCommand(ExecuteDCR2Triger));

        void ExecuteDCR2Triger()
        {
            try
            {
                DV.DCR2.Trigger();
                DCR2Text = DV.DCR2.ReadTestData();
                Console.WriteLine(DCR2Text);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "错误提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }







        #endregion

        #region DCR3测试

        private string _dCR3Text;
        public string DCR3Text
        {
            get { return _dCR3Text; }
            set { SetProperty(ref _dCR3Text, value); }
        }


        private DelegateCommand _dCR3Triger;
        public DelegateCommand DCR3Triger =>
            _dCR3Triger ?? (_dCR3Triger = new DelegateCommand(ExecuteDCR3Triger));
        void ExecuteDCR3Triger()
        {



            try
            {
                DV.DCR3.Trigger();
                DCR3Text = DV.DCR3.ReadTestData();
                Console.WriteLine(DCR3Text);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "错误提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }







        #endregion

        #region DCR2测试

        private string _dCR4Text;
        public string DCR4Text
        {
            get { return _dCR4Text; }
            set { SetProperty(ref _dCR4Text, value); }
        }


        private DelegateCommand _dCR4Triger;
        public DelegateCommand DCR4Triger =>
            _dCR4Triger ?? (_dCR4Triger = new DelegateCommand(ExecuteDCR4Triger));

        void ExecuteDCR4Triger()
        {
            try
            {
                DV.DCR4.Trigger();
                DCR4Text = DV.DCR4.ReadTestData();
                Console.WriteLine(DCR4Text);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "错误提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }







        #endregion

        #region 感值3测试

        private string _iRText;
        public string IRText
        {
            get { return _iRText; }
            set { SetProperty(ref _iRText, value); }
        }



        private DelegateCommand _IRTriger;
        public DelegateCommand IRTriger =>
            _IRTriger ?? (_IRTriger = new DelegateCommand(ExecuteIRTrigerTriger));

        void ExecuteIRTrigerTriger()
        {
            try
            {
                DV.IR.Trigger();
                IRText = DV.IR.ReadTestData();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "错误提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        #endregion



        #region 感值2测试
        private string _ganZhi2Text;
        public string GanZhi2Text
        {
            get { return _ganZhi2Text; }
            set { SetProperty(ref _ganZhi2Text, value); }
        }



        private DelegateCommand _ganZhi2Triger;
        public DelegateCommand GanZhi2Triger =>
            _ganZhi2Triger ?? (_ganZhi2Triger = new DelegateCommand(ExecuteGanZhi2Triger));

        void ExecuteGanZhi2Triger()
        {
            try
            {
                DV.GanZhi2.TriggerSingle();
                GanZhi2Text = DV.GanZhi2.ReadTestData();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "错误提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        #endregion

        #region 感值1测试
        private string _ganZhi1Text;
        public string GanZhi1Text
        {
            get { return _ganZhi1Text; }
            set { SetProperty(ref _ganZhi1Text, value); }
        }



        private DelegateCommand _ganZhi1Triger;
        public DelegateCommand GanZhi1Triger =>
            _ganZhi1Triger ?? (_ganZhi1Triger = new DelegateCommand(ExecuteGanZhi1Triger));

        void ExecuteGanZhi1Triger()
        {
            try
            {
                DV.GanZhi1.TriggerSingle();

                GanZhi1Text = DV.GanZhi1.ReadTestData();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "错误提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion




        public HandViewModel()
        {

        }
        #region 导航接口
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            ListEnble.Clear();
            ListEnble.Add(DV.DCR1.Enable);
            ListEnble.Add(DV.IR.Enable);
            ListEnble.Add(DV.ZZhi.Enable);
       
            ListEnble.Add(DV.GanZhi1.Enable);
            ListEnble.Add(DV.GanZhi2.Enable);
            ListEnble.Add(DV.DCR3.Enable);
            ListEnble.Add(DV.SZhi.Enable);
            ListEnble.Add(DV.DMianCCD.Enable);

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

    public delegate void ChangeEnableColor();
}
