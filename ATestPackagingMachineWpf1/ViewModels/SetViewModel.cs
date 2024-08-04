using ATestPackagingMachineWpf1.Common;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using TDLL;
using ZModels;

namespace ATestPackagingMachineWpf1.ViewModels
{
    public class SetViewModel : BindableBase, INavigationAware
    {
    

        private DeviceParameterJson _DeviceParameterJson;
        public DeviceParameterJson DeviceParameterJson
        {
            get { return _DeviceParameterJson; }
            set { SetProperty(ref _DeviceParameterJson, value); }
        }



        /// <summary>
        /// 保存数据
        /// </summary>
        private DelegateCommand _saveData;
        public DelegateCommand SaveData =>
            _saveData ?? (_saveData = new DelegateCommand(ExecuteSaveData));

        void ExecuteSaveData()
        {
            JsonSaveEXT.deviceParameterJsonGv = DeviceParameterJson;

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





        #region 删除一行


        private DelegateCommand<DataGrid> _Remove;
        public DelegateCommand<DataGrid> Remove =>
            _Remove ?? (_Remove = new DelegateCommand<DataGrid>(ExecuteRemove));

        void ExecuteRemove(DataGrid dataGrid)
        {
            var result = MessageBox.Show("确定要删除数据吗？", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.OK)
            {
                if (dataGrid.SelectedIndex>=DeviceParameterJson.GMXHParameterList.Count)
                {
                    return;
                }
                DeviceParameterJson.GMXHParameterList.RemoveAt(dataGrid.SelectedIndex);
                JsonSaveEXT.deviceParameterJsonGv.GMXHParameterList= DeviceParameterJson.GMXHParameterList;
                DeviceParameterJson = null;
                DeviceParameterJson = JsonSaveEXT.deviceParameterJsonGv;


            }

        }




        #endregion


        #region 导航接口
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            DeviceParameterJson = JsonSaveEXT.deviceParameterJsonGv;


        }



        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
        #endregion
    }
}
