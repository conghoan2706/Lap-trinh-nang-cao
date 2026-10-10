using System.ComponentModel.DataAnnotations;

namespace QuanLySinhVien.Data.Entity
{
    /// <summary>
    /// One class has many students. The link is SinhVien.MaLop.
    /// </summary>
    public class LopHoc
    {
        [Required(ErrorMessage = "Mã lớp không được để trống")]
        public string MaLop { get; set; } = "";

        [Required(ErrorMessage = "Tên lớp không được để trống")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Tên lớp từ 2 đến 100 ký tự")]
        public string TenLop { get; set; } = "";

        public List<ValidationResult> IsInValid()
        {
            List<ValidationResult> results = new List<ValidationResult>();
            Validator.TryValidateObject(this, new ValidationContext(this), results, true);
            return results;
        }

        public override string ToString() => TenLop;
    }
}
