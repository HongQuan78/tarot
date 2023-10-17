using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Tarot.Validate
{
    public class CustomPhoneAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string phoneNumber = value.ToString();

                // Kiểm tra xem số điện thoại có 10 hoặc 11 số và bắt đầu bằng 0 hay không
                if (phoneNumber.Length == 10 || phoneNumber.Length == 11)
                {
                    if (phoneNumber.StartsWith("0") && phoneNumber.All(char.IsDigit))
                    {
                        return ValidationResult.Success;
                    }
                }
            }
            return new ValidationResult("Invalid phone number format.");
        }
    }
}
