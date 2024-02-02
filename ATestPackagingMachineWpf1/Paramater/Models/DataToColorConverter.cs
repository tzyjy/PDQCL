using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace ATestPackagingMachineWpf1.Paramater.Models
{
    public class DataToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SolidColorBrush background = Brushes.Black;

            if (value != null && int.TryParse(value.ToString(), out int hour))
            {
                if (hour < 20)
                {
                    background = Brushes.Green;
                }
                else if (hour < 40)
                {
                    background = Brushes.Blue;
                }
                else if (hour < 60)
                {
                    background = Brushes.Orange;
                }
                else if (hour < 80)
                {
                    background = Brushes.Red;
                }
                else if (hour < 90)
                {
                    background = Brushes.Purple;
                }
                else
                {
                    background = Brushes.Gray;
                }
            }

            return background;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
