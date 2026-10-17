using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace QuanLySinhVien.Data.Entity
{
    public class SinhVien
    {
        [Required(ErrorMessage = "Mã sinh viên không được để trống")]
        [RegularExpression(@"^SV\d{4,6}$", ErrorMessage = "Mã sinh viên có dạng SV và 4-6 chữ số, ví dụ SV000123")]
        public string MaSV { get; set; } = "";

        [Required(ErrorMessage = "Họ tên không được để trống")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Họ tên từ 2 đến 50 ký tự")]
        public string HoTen { get; set; } = "";

        [BirthDate]
        public DateTime NgaySinh { get; set; }

        [Required(ErrorMessage = "Giới tính không được để trống")]
        [RegularExpression(@"^(Nam|Nữ)$", ErrorMessage = "Giới tính phải là Nam hoặc Nữ")]
        public string GioiTinh { get; set; } = "Nam";

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Điện thoại không được để trống")]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Điện thoại gồm 10 số và bắt đầu bằng 0")]
        public string SoDienThoai { get; set; } = "";

        [Required(ErrorMessage = "Chọn lớp học")]
        public string MaLop { get; set; } = "";

        [Range(0, 10, ErrorMessage = "Điểm phải từ 0 đến 10")]
        public double Diem { get; set; }

        [Required(ErrorMessage = "Chọn trạng thái")]
        [RegularExpression(@"^(Đang học|Bảo lưu|Nghỉ học)$", ErrorMessage = "Trạng thái không hợp lệ")]
        public string TrangThai { get; set; } = "Đang học";

        /// <summary>
        /// Trim text and collapse repeated spaces in the name. MaSV is stored in uppercase.
        /// </summary>
        public void ChuanHoa()
        {
            MaSV = (MaSV ?? "").Trim().ToUpperInvariant();
            HoTen = Regex.Replace((HoTen ?? "").Trim(), @"\s+", " ");
            Email = (Email ?? "").Trim();
            SoDienThoai = (SoDienThoai ?? "").Trim();
            GioiTinh = (GioiTinh ?? "").Trim();
            MaLop = (MaLop ?? "").Trim();
            TrangThai = (TrangThai ?? "").Trim();
        }

        public List<ValidationResult> IsInValid()
        {
            List<ValidationResult> results = new List<ValidationResult>();
            Validator.TryValidateObject(this, new ValidationContext(this), results, true);
            return results;
        }
    }
}
