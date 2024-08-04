using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Led2.xaml 的交互逻辑
    /// </summary>
    public partial class Led2 : UserControl
    {
        public Led2()
        {
            InitializeComponent();
        }
        public bool IsOpen2
        {
            get { return (bool)GetValue(IsOpen2Property); }
            set
            {
                SetValue(IsOpen2Property, value);

            }
        }

        // Using a DependencyProperty as the backing store for IsOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsOpen2Property =
            DependencyProperty.Register("IsOpen2", typeof(bool), typeof(Led2), new PropertyMetadata(false
                , ChangeBool2));

        private static void ChangeBool2(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Led2 led = (Led2)d;
            if (e.NewValue.Equals(true))
            {
                led.Dot1.Background = Brushes.Red;
            }
            else
            {
                led.Dot1.Background = Brushes.LightGray;
            }

        }
    }
}
