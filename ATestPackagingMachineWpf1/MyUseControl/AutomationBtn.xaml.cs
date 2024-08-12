using ATestPackagingMachineWpf1.Common;
using ATestPackagingMachineWpf1.DeviceFile;
using ATestPackagingMachineWpf1.ViewModels;
using ATestPackagingMachineWpf1.ZModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ATestPackagingMachineWpf1.MyUseControl
{
    /// <summary>
    /// AutomationBtn.xaml 的交互逻辑
    /// </summary>
    public partial class AutomationBtn : UserControl
    {
        public AutomationBtn()
        {
            InitializeComponent();
            //Task.Run(() => { TestMethod(); });
        }

        public bool Open
        {
            get { return (bool)GetValue(OpenProperty); }
            set { SetValue(OpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Open.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OpenProperty =
            DependencyProperty.Register("Open", typeof(bool), typeof(AutomationBtn), new PropertyMetadata(false, ChangeValue));

        private static void ChangeValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AutomationBtn btn = (AutomationBtn)d;
            if (e.NewValue.Equals(true))
            {
                btn.btn1.Background = Brushes.LimeGreen;
                btn.tb2.Text = "ON";
            }
            else
            {
                btn.btn1.Background = Brushes.Gray;
                btn.tb2.Text = "OFF";
            }
        }

        public ICommand CommandMouseDown
        {
            get { return (ICommand)GetValue(CommandMouseDownProperty); }
            set { SetValue(CommandMouseDownProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandMouseDown.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandMouseDownProperty =
            DependencyProperty.Register("CommandMouseDown", typeof(ICommand), typeof(AutomationBtn), new PropertyMetadata(null, CommandChange));

        private static void CommandChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private void btn1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var result = ((PLC3UTCP)DV.PLC).plc.Write("D7211", (short)1);
            if (result.IsSuccess)
            {
                LogPublish(new LogInfo() { OK = true, InfoText = $"作业员{GV.OpName}  重工模式按钮按下！" });
            }
            else
            {
                LogPublish(new LogInfo() { OK = false, InfoText = $"作业员{GV.OpName} 重工模式按钮按下写入PLC失败，请检查" });
            }
        }

        private void btn1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var rsult = ((PLC3UTCP)DV.PLC).plc.Write("D7211", (short)0);
            if (rsult.IsSuccess)
            {
                LogPublish(new LogInfo() { OK = true, InfoText = $"作业员{GV.OpName} 重工模式按钮松开！" });
            }
            else
            {
                LogPublish(new LogInfo() { OK = false, InfoText = $"作业员{GV.OpName} 重工模式按钮松开写入PLC失败，请检查" });
            }
        }

        private static void LogPublish(LogInfo logInfo)
        {
            GV.Event.GetEvent<MessageEvent>().Publish(logInfo);
        }
    }
}