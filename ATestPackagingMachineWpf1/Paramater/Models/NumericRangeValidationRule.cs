using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ATestPackagingMachineWpf1.Paramater.Models
{
    public class NumericRangeValidationRule : ValidationRule
    {
        public int MinValue { get; set; }
        public int MaxValue { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (int.TryParse(value?.ToString(), out int number))
            {
                if (number < MinValue || number > MaxValue)
                {
                    number= MinValue;
                    return new ValidationResult(false, $"请输入 {MinValue} 到 {MaxValue} 之间的数字");
                }
            }
            return ValidationResult.ValidResult;
        }
    }
}
