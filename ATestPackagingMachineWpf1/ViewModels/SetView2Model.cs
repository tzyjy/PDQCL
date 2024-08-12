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
using ZModels;

namespace ATestPackagingMachineWpf1.ViewModels
{
    public class SetView2Model : BindableBase, INavigationAware
    {
        private Visibility _IsEnable1;

        public Visibility IsEnable1
        {
            get { return _IsEnable1; }
            set { SetProperty(ref _IsEnable1, value); }
        }

        private Visibility _IsEnable2;

        public Visibility IsEnable2
        {
            get { return _IsEnable2; }
            set { SetProperty(ref _IsEnable2, value); }
        }

        private Visibility _ShowDataGridGLY;

        public Visibility ShowDataGridGLY
        {
            get { return _ShowDataGridGLY; }
            set { SetProperty(ref _ShowDataGridGLY, value); }
        }

        private Visibility _ShowDataGridGCS;

        public Visibility ShowDataGridGCS
        {
            get { return _ShowDataGridGCS; }
            set { SetProperty(ref _ShowDataGridGCS, value); }
        }

        private bool _CanModify = false;

        public bool CanModify
        {
            get { return _CanModify; }
            set { SetProperty(ref _CanModify, value); }
        }

        private DeviceParameterJson _DeviceParameterJson;

        public DeviceParameterJson DeviceParameterJson
        {
            get { return _DeviceParameterJson; }
            set { SetProperty(ref _DeviceParameterJson, value); }
        }

        private List<short> _Data = new List<short>() { 0, 0, 0, 0 };

        public List<short> Data
        {
            get { return _Data; }
            set { SetProperty(ref _Data, value); }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        private DelegateCommand _saveData;

        public DelegateCommand SaveData =>
            _saveData ?? (_saveData = new DelegateCommand(ExecuteSaveData));

        private void ExecuteSaveData()
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

        private void ExecuteRemove(DataGrid dataGrid)
        {
            var result = MessageBox.Show("确定要删除数据吗？", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.OK)
            {
                if (dataGrid.SelectedIndex >= DeviceParameterJson.GMXHParameterList.Count)
                {
                    return;
                }
                DeviceParameterJson.GMXHParameterList.RemoveAt(dataGrid.SelectedIndex);
                JsonSaveEXT.deviceParameterJsonGv.GMXHParameterList = DeviceParameterJson.GMXHParameterList;
                DeviceParameterJson = null;
                DeviceParameterJson = JsonSaveEXT.deviceParameterJsonGv;
            }
        }

        #endregion 删除一行

        #region 导航接口

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            DeviceParameterJson = JsonSaveEXT.deviceParameterJsonGv;
            IsEnable1 = GV.CurrentLogonPeson.LoginAccount == "777" == true ? Visibility.Visible : Visibility.Collapsed;
            IsEnable2 = CommonMethod.Getlimit(Limit.工艺参数, GV.CurrentLogonPeson) == true ? Visibility.Visible : Visibility.Collapsed;
            ShowDataGridGLY = GV.CurrentLogonPeson.LoginAccount == "管理员" ? Visibility.Visible : Visibility.Collapsed;
            ShowDataGridGCS = GV.CurrentLogonPeson.LoginAccount == "工程师" || GV.CurrentLogonPeson.LoginAccount == "777" ? Visibility.Visible : Visibility.Collapsed;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        #endregion 导航接口

        private DelegateCommand _SavePLC;

        public DelegateCommand SavePLC =>
            _SavePLC ?? (_SavePLC = new DelegateCommand(ExecuteSavePLC));

        private void ExecuteSavePLC()
        {
            try
            {
                var Result = DV.PLC.plc.Write("D7230", Data.ToArray());
                if (!Result.IsSuccess)
                {
                    throw new Exception("PLC写入失败！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}