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
    /// Led.xaml 的交互逻辑
    /// </summary>
    public partial class Led : UserControl
    {


        public Led()
        {
            InitializeComponent();
        }


        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value);

                          }
        }

        // Using a DependencyProperty as the backing store for IsOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(Led), new PropertyMetadata(true
                , ChangeBool));

        private static void ChangeBool(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Led led = (Led)d;
            if (e.NewValue.Equals(true))
            {
                led.Dot1.Background= Brushes.LimeGreen;
            }
            else
            {
                led.Dot1.Background = Brushes.Gray;
            }

        }

        private string _diindex;
        [System.ComponentModel.Description("输入")]
        public string DIIndex
        {
            get { return _diindex; }
            set { _diindex = value; 
            
            this.text1Suoyin.Text = value;
            
            
            }
        }


     


    }
}
