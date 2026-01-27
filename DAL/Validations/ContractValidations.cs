using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DAL.Validations
{
    public class NoSpecialCharsAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            if (value is string str && Regex.IsMatch(str, @"[@#$%]"))
            {
                return new ValidationResult("Special character @, #, $, % are not allowed");
            }
            return ValidationResult.Success;
        }
    }

    public class NotFutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            if (value is DateOnly date && date > DateOnly.FromDateTime(DateTime.Now))
            {
                return new ValidationResult("Signing date cannot be a future date.");
            }
            return ValidationResult.Success;
        }
    }

    public class AfterSigningDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            var contract = context.ObjectInstance as DAL.Entities.Contract;
            if (contract != null && value is DateOnly endDate)
            {
                if (endDate <= contract.SigningDate)
                {
                    return new ValidationResult("Expiration date must be after signing date.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
