using System.Windows.Controls;

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
            ListBox listBox = sender as ListBox;
            if (listBox.Items.Count > 0)
            {
                listBox.ScrollIntoView(listBox.Items[listBox.Items.Count - 1]);
            }
        }
    }
}
