using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ATestPackagingMachineWpf1.UIConfig
{
    public class ProportionConverter : IValueConverter
    {
        // 后端的值处理给前端显示
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int parameterint = ConvertData.ConvertInt32(parameter);
            decimal result = 0;
            switch (parameterint)
            {

        
                case 10:
                    result = ConvertData.ConvertDecimal(value) / 10;
                    break;
                case 100:
                    result = ConvertData.ConvertDecimal(value) / 100;
                    break;
                default:
                    result = ConvertData.ConvertDecimal(value) / 1;
                    break;
            }

            return result;

        }

        //前端的值处理后给后端
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int parameterint = ConvertData.ConvertInt32(parameter);
            short result = 0;
            switch (parameterint)
            {
          

                //case 10:

                //    result = (short)(ConvertData.ConvertDecimal(value) * 10);
                //    break;

                //case 100:
                //    result = (short)(ConvertData.ConvertDecimal(value) * 100);
                //    break;

                default:

                    result = (short)(ConvertData.ConvertDecimal(value) * 1);
                    break;
            }


            return result;
        }


    }

    public class ConvertData
    {
        public static decimal ConvertDecimal(object value)
        {
            return Convert.ToDecimal(value);
        }

        public static int ConvertInt32(object value)
        {
            return Convert.ToInt32(value);
        }
    }
}
