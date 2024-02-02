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
    /// UserControlShowLed.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlShowLed : UserControl
    {
        public UserControlShowLed()
        {
            InitializeComponent();
        }





        public bool IsShowLed
        {
            get { return (bool)GetValue(IsShowLedProperty); }
            set { SetValue(IsShowLedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsShowLed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsShowLedProperty =
            DependencyProperty.Register("IsShowLed", typeof(bool), typeof(UserControlShowLed), new PropertyMetadata(true,ChangeIsshow));

        private static void ChangeIsshow(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UserControlShowLed led = (UserControlShowLed)d;
            if (e.NewValue.Equals(true))
            {
                led.Dot2.Visibility = Visibility.Visible;
            }
            else
            {
                led.Dot2.Visibility = Visibility.Collapsed;
            }
        }

        public bool Enble
        {
            get { return (bool)GetValue(EnbleProperty); }
            set { SetValue(EnbleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EnbleProperty =
            DependencyProperty.Register("Enble", typeof(bool), typeof(UserControlShowLed), new PropertyMetadata(true,ChangeEnble));

        private static void ChangeEnble(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UserControlShowLed led = (UserControlShowLed)d;
            if (e.NewValue.Equals(true))
            {
                led.Dot1.Background =new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF21F903"));  
            }
            else
            {
                led.Dot1.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF093C06"));
            }
        }



        public bool Stues
        {
            get { return (bool)GetValue(StuesProperty); }
            set { SetValue(StuesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Stues.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StuesProperty =
            DependencyProperty.Register("Stues", typeof(bool), typeof(UserControlShowLed), new PropertyMetadata(true,ChangeStues));

        private static void ChangeStues(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UserControlShowLed led = (UserControlShowLed)d;
            if (e.NewValue.Equals(true))
            {
                led.Dot2.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF07A44A"));
            }
            else
            {
                led.Dot2.Background = Brushes.Red;
            }

        }

        private string _deviceName;
        [System.ComponentModel.Description("名称")]
        public string DeviceName
        {
            get { return _deviceName; }
            set
            {
                _deviceName = value;

                this.TextbolockName.Text = value;


            }
        }
    }




   


}
