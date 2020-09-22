using System;
using System.ComponentModel.DataAnnotations;

public class CheckTheDateAttribute : ValidationAttribute
{
    public string GetErrorMessage() =>
        "Date needs to be from past .";
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        DateTime datetime = (DateTime)validationContext.ObjectInstance;
        if (DateTime.Compare(datetime, DateTime.UtcNow) > 0)
        {
            return new ValidationResult(GetErrorMessage());
        }
        return ValidationResult.Success;
    }
}