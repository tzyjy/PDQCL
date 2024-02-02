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
    /// myButton.xaml 的交互逻辑
    /// </summary>
    public partial class MyButton : UserControl
    {
        public MyButton()
        {
            InitializeComponent();
        }

        private string _diindex;
        [System.ComponentModel.Description("索引")]
        public string DIIndex
        {
            get { return _diindex; }
            set
            {
                _diindex = value;

                this.text1Suoyin.Text = value;


            }
        }


        private string _diyinjiao;
        [System.ComponentModel.Description("引脚")]
        public string DiYinjiao
        {
            get { return _diyinjiao; }
            set
            {
                _diyinjiao = value;

                this.text2Yinjiao.Text = value;


            }
        }
    }
}
