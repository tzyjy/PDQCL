using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace ATestPackagingMachineWpf1.Common
{
    public class DataGridConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SolidColorBrush background = Brushes.Transparent;
            if (value != null )
            {
                if (value.ToString() == "OK")
                {
                    background = Brushes.Green;
                }
                else
                {
                    background = Brushes.Red;
                }
             ;

            }

            return background;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
