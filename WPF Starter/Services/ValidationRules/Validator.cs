using System.Globalization;
using System.Windows.Controls;

namespace WPF_Starter.Services.ValidationRules
{
    public class Validator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string? input = value as string;
            if (string.IsNullOrEmpty(input))
            {
                return new ValidationResult(false, "String cannot be empty.");
            }
            if (!int.TryParse(input, out int result) || result <= 0)
            {
                return new ValidationResult(false, "Input a positive number.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
