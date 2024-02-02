using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace ATestPackagingMachineWpf1.ViewModels
{
    public class AboutViewModel : BindableBase, IDialogAware
    {
        public AboutViewModel()
        {

        }

        public string Title => "关于";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            RequestClose?.Invoke((IDialogResult)null);
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            AddText("Version.txt");
        }

        private ObservableCollection<string> _versionList=new ObservableCollection<string>();
        public  ObservableCollection<string> VersionList
        {
            get { return _versionList; }
            set { SetProperty(ref _versionList, value); }
        }

        private void AddText(string path)
        {
            try
            {
                StreamReader file = new StreamReader(path, Encoding.Default);
                string s = "";
                while (s != null)
                {
                    s = file.ReadLine();
                    //if (!string.IsNullOrEmpty(s))
                    if (s != null)
                    {
                        VersionList.Add(s);
                    }

                }
                file.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }
    }
}
