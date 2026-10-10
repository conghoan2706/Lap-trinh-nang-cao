using System.ComponentModel.DataAnnotations;
using QuanLySinhVien.Data.DAL;
using QuanLySinhVien.Data.Entity;

namespace QuanLySinhVien.BUL
{
    /// <summary>
    /// Business rules for students: normalize, validate, then call the data layer.
    /// </summary>
    public class SinhVienBUL
    {
        private readonly SinhVienDAL svd = new SinhVienDAL();

        public SinhVien? GetSinhVienByMaSV(string maSV)
        {
            return svd.GetSinhVienByMaSV((maSV ?? "").Trim().ToUpperInvariant());
        }

        public List<SinhVien> GetSinhVienByMaLop(string maLop)
        {
            return svd.GetSinhViensByMaLop(maLop);
        }

        public List<SinhVien> GetAllSinhVien()
        {
            return svd.GetAllSinhVien();
        }

        public void AddSinhVien(SinhVien sv)
        {
            EnsureValid(sv);
            if (svd.GetSinhVienByMaSV(sv.MaSV) != null)
                throw new InvalidOperationException("Mã sinh viên đã tồn tại");

            try
            {
                svd.AddSinhVien(sv);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Lỗi khi thêm sinh viên: " + ex.Message);
            }
        }

        public void UpdateSinhVien(SinhVien sv)
        {
            EnsureValid(sv);
            if (svd.GetSinhVienByMaSV(sv.MaSV) == null)
                throw new InvalidOperationException("Không tìm thấy sinh viên để sửa");

            try
            {
                svd.UpdateSinhVien(sv);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Lỗi khi cập nhật sinh viên: " + ex.Message);
            }
        }

        public void DeleteSinhVien(string maSV)
        {
            string ma = (maSV ?? "").Trim().ToUpperInvariant();
            if (svd.GetSinhVienByMaSV(ma) == null)
                throw new InvalidOperationException("Không tìm thấy sinh viên để xóa");

            try
            {
                svd.DeleteSinhVien(ma);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Lỗi khi xóa sinh viên: " + ex.Message);
            }
        }

        private static void EnsureValid(SinhVien sv)
        {
            sv.ChuanHoa();
            List<ValidationResult> results = sv.IsInValid();
            if (results.Count > 0)
            {
                string message = string.Join("\n", results.Select(v => v.ErrorMessage));
                throw new InvalidOperationException(message);
            }
        }
    }
}
