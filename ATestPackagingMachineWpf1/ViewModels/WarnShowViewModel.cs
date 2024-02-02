using ATestPackagingMachineWpf1.Common;
using ATestPackagingMachineWpf1.DeviceFile;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using ZModels.DeviceJson;

namespace ATestPackagingMachineWpf1.ViewModels
{
    public class WarnShowViewModel : BindableBase, IDialogAware
    {
        public WarnShowViewModel()
        {

        }
        private WarningValue _warningValue;
        public WarningValue WarningValue
        {
            get { return _warningValue; }
            set { SetProperty(ref _warningValue, value); }
        }

        private List<string> _nowPassList;
        public List<string> NowPassList
        {
            get { return _nowPassList; }
            set { SetProperty(ref _nowPassList, value); }
        }
        public string Title => "预警提示";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            WarningValue = JsonSaveEXT.deviceParameterJsonGv.WarningValue;


            NowPassList= InstrumentCommon.GetAllPassRateString();

        }
    }
}
