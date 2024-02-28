using ATestPackagingMachineWpf1.Common;
using ATestPackagingMachineWpf1.DeviceFile.Mes;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ZModels;
using ZModels.DeviceJson;

namespace ATestPackagingMachineWpf1.ViewModels
{
    public class ManufacturerViewModel : BindableBase, INavigationAware
    {
        public ManufacturerViewModel()
        {

        }

        private TestInfo _testInfo;
        public TestInfo TestInfo
        {
            get { return _testInfo; }
            set { SetProperty(ref _testInfo, value); }
        }

        private Manufacturer _manufacturer;
        public Manufacturer Manufacturer
        {
            get { return _manufacturer; }
            set { SetProperty(ref _manufacturer, value); }
        }


        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
           
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (JsonSaveEXT.deviceParameterJsonGv.Manufacturer == null)
            {
                Manufacturer = new Manufacturer();
            }
            else
            {
                Manufacturer = JsonSaveEXT.deviceParameterJsonGv.Manufacturer;
            }

            if (JsonSaveEXT.deviceParameterJsonGv.TestInfo == null)
            {
                TestInfo = new TestInfo();
            }
            else
            {
                TestInfo = JsonSaveEXT.deviceParameterJsonGv.TestInfo;
            }
        }


        /// <summary>
        /// 保存数据
        /// </summary>
        private DelegateCommand _saveData;
        public DelegateCommand SaveData =>
            _saveData ?? (_saveData = new DelegateCommand(ExecuteSaveData));

        void ExecuteSaveData()
        {
            JsonSaveEXT.deviceParameterJsonGv.Manufacturer = Manufacturer;
            JsonSaveEXT.deviceParameterJsonGv.TestInfo = TestInfo;
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


        private DelegateCommand _getCSV;
        public DelegateCommand GetCSV =>
            _getCSV ?? (_getCSV = new DelegateCommand(ExecuteGetCSV));

        void ExecuteGetCSV()
        {
            try
            {
                MesInfo mesInfo = new MesInfo()
                {
                    Wono = "5M23Y1723128",
                    Matno = "CYCHPNUMB6TR10LA",
                    Equipmentid = "TP1911",

                };
                MESServrMESAPI.SaveCsv(mesInfo);
                MessageBox.Show("生成CSV成功！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

      

        private DelegateCommand<object> _asd;
        public DelegateCommand<object> asd =>
            _asd ?? (_asd = new DelegateCommand<object>(Executeasd));

        void Executeasd(object parameter)
        {
            MessageBox.Show(parameter.ToString());
        }










    }
}
