using ATestPackagingMachineWpf1.MyUseControl;
using System;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace ATestPackagingMachineWpf1.Views
{
    /// <summary>
    /// Interaction logic for HomeView
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();

        }

        private void ListBox_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {

         ListBox listBox= sender as ListBox;
            if (listBox.Items.Count>0)
            {
                listBox.ScrollIntoView(listBox.Items[listBox.Items.Count - 1]);
            }
          
        }
    }
}




