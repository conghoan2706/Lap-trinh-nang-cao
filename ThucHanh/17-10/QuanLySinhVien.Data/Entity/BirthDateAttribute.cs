using System.ComponentModel.DataAnnotations;

namespace QuanLySinhVien.Data.Entity
{
    /// <summary>
    /// Rejects a birth date in the future or before 1970.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class BirthDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not DateTime date)
                return new ValidationResult("Ngày sinh không hợp lệ");

            DateTime day = date.Date;
            if (day > DateTime.Today)
                return new ValidationResult("Ngày sinh không được lớn hơn ngày hiện tại");
            if (day.Year < 1970)
                return new ValidationResult("Năm sinh phải từ 1970 trở đi");

            return ValidationResult.Success;
        }
    }
}
