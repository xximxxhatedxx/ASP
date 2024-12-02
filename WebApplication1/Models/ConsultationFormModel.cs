using System;
using System.ComponentModel.DataAnnotations;

public class ConsultationFormModel
{
    [Required(ErrorMessage = "Ім'я та прізвище є обов'язковими.")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Email є обов'язковим.")]
    [EmailAddress(ErrorMessage = "Невірний формат Email.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Дата консультації є обов'язковою.")]
    [DataType(DataType.Date)]
    [FutureDate(ErrorMessage = "Дата повинна бути в майбутньому.")]
    [NonWeekend(ErrorMessage = "Консультація не може проходити у вихідний день.")]
    public DateTime ConsultationDate { get; set; }

    [Required(ErrorMessage = "Оберіть продукт.")]
    public string Product { get; set; }
}
