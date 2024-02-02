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
    /// UserControlTextBox.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlTextBox : UserControl
    {
        public UserControlTextBox()
        {
            InitializeComponent();
        }




        public string TextName
        {
            get { return (string)GetValue(TextNameProperty); }
            set { SetValue(TextNameProperty, value);
             
            
            }
        }

        // Using a DependencyProperty as the backing store for TextName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextNameProperty =
            DependencyProperty.Register("TextName", typeof(string), typeof(UserControlTextBox), new PropertyMetadata("名称",ChangeName));

        private static void ChangeName(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UserControlTextBox userControlTextBox = (UserControlTextBox)d;
            userControlTextBox.TextBlockName.Text= e.NewValue.ToString();


        }

        public string TextValue
        {
            get { return (string)GetValue(TextValueProperty); }
            set { SetValue(TextValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextValueProperty =
            DependencyProperty.Register("TextValue", typeof(string), typeof(UserControlTextBox), new PropertyMetadata("",ChangeValue));

        private static void ChangeValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UserControlTextBox userControlTextBox = (UserControlTextBox)d;
            userControlTextBox.TextBlockValue.Text = e.NewValue.ToString();
        }



        public string TextUnit
        {
            get { return (string)GetValue(TextUnitProperty); }
            set { SetValue(TextUnitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextUnit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextUnitProperty =
            DependencyProperty.Register("TextUnit", typeof(string), typeof(UserControlTextBox), new PropertyMetadata("单位",ChangeUnit));

        private static void ChangeUnit(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UserControlTextBox userControlTextBox = (UserControlTextBox)d;
            userControlTextBox.TextBlockUnit.Text = e.NewValue.ToString();
        }




        public bool IsShowUnit
        {
            get { return (bool)GetValue(IsShowUnitProperty); }
            set { SetValue(IsShowUnitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsShowUnit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsShowUnitProperty =
            DependencyProperty.Register("IsShowUnit", typeof(bool), typeof(UserControlTextBox), new PropertyMetadata(true,ChangeIsShowUnit));

        private static void ChangeIsShowUnit(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UserControlTextBox userControlTextBox = (UserControlTextBox)d;

            if (e.NewValue.Equals(true))
            {
                userControlTextBox.Myborder.Visibility = Visibility.Visible;

            }
            else
            {
                userControlTextBox.Myborder.Visibility = Visibility.Collapsed;


            }
        }



        public double TextSize
        {
            get { return (double)GetValue(TextSizeProperty); }
            set { SetValue(TextSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextSizeProperty =
            DependencyProperty.Register("TextSize", typeof(double), typeof(UserControlTextBox), new PropertyMetadata(15.0,ChangeTextSize));

        private static void ChangeTextSize(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            UserControlTextBox userControlTextBox = (UserControlTextBox)d;
            userControlTextBox.TextBlockName.FontSize = (double)e.NewValue;
            userControlTextBox.TextBlockValue.FontSize = (double)e.NewValue;
            userControlTextBox.TextBlockUnit.FontSize = (double)e.NewValue;
        }
    }
}
