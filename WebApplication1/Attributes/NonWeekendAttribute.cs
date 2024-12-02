using System.ComponentModel.DataAnnotations;

public class NonWeekendAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is DateTime date && (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday))
        {
            return new ValidationResult(ErrorMessage);
        }
        return ValidationResult.Success;
    }
}