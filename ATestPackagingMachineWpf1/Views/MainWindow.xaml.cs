using ATestPackagingMachineWpf1.Common;
using ATestPackagingMachineWpf1.ViewModels;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System;
using System.Windows;
using System.Threading.Tasks;


namespace ATestPackagingMachineWpf1.Views
{
    /// <summary>
    /// Interaction logic for PrismWindow1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CancellationTokenSource source = new CancellationTokenSource();
        public MainWindow()
        {
            InitializeComponent();
            Task.Run(() => { GetMeraryAndCpu(); }, source.Token);
        }


        #region 获取内存

        private void GetMeraryAndCpu()
        {
            while (!source.IsCancellationRequested)
            {
                var name = Process.GetCurrentProcess().ProcessName;//获取当前进程的名称
                var ramCounter = new PerformanceCounter("Process", "Working Set - Private", name);

                if (lblCursorPosition2.Dispatcher.Thread != System.Threading.Thread.CurrentThread)
                {
                    lblCursorPosition2.Dispatcher.Invoke(new Action(() => { lblCursorPosition2.Text = $"{ramCounter.NextValue() / 1024000}MB"; }));
                }
                else
                {
                    lblCursorPosition2.Text = $"{ramCounter.NextValue() / 1024000}MB";
                }






                if ((ramCounter.NextValue() / 1024000) > 500.0)
                {
                    ClearMemory();
                }
                Thread.Sleep(1000);
            }
        }

        #endregion 获取内存
        #region 内存回收

        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize")]
        public static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);

        /// <summary>
        /// 释放内存
        /// </summary>
        public static void ClearMemory()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                //FrmMian为我窗体的类名
                MainWindow.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            }
        }


        #endregion 内存回收

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            source.Cancel();
 
            Console.WriteLine("取消任务");

        }
    }
}
