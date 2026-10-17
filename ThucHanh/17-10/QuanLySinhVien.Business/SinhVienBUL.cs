using System.ComponentModel.DataAnnotations;
using QuanLySinhVien.Data.DAL;
using QuanLySinhVien.Data.Entity;

namespace QuanLySinhVien.BUL
{
    public class SinhVienBUL
    {
        private readonly SinhVienDAO dao = new SinhVienDAO();

        public SinhVien? GetSinhVienByMaSV(string maSV)
        {
            try
            {
                return dao.GetSinhVienByMaSV((maSV ?? "").Trim().ToUpperInvariant());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<SinhVien> GetSinhVienByMaLop(string maLop)
        {
            try
            {
                return dao.GetSinhViensByMaLop(maLop);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<SinhVien> GetAllSinhVien()
        {
            try
            {
                return dao.GetAllSinhVien();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddSinhVien(SinhVien sv)
        {
            try
            {
                EnsureValid(sv);
                if (dao.GetSinhVienByMaSV(sv.MaSV) != null)
                    throw new Exception("Mã sinh viên đã tồn tại");
                dao.AddSinhVien(sv);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateSinhVien(SinhVien sv)
        {
            try
            {
                EnsureValid(sv);
                if (dao.GetSinhVienByMaSV(sv.MaSV) == null)
                    throw new Exception("Không tìm thấy sinh viên để sửa");
                dao.UpdateSinhVien(sv);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteSinhVien(string maSV)
        {
            try
            {
                string ma = (maSV ?? "").Trim().ToUpperInvariant();
                if (dao.GetSinhVienByMaSV(ma) == null)
                    throw new Exception("Không tìm thấy sinh viên để xóa");
                dao.DeleteSinhVien(ma);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static void EnsureValid(SinhVien sv)
        {
            sv.ChuanHoa();
            List<ValidationResult> results = sv.IsInValid();
            if (results.Count > 0)
                throw new Exception(string.Join("\n", results.Select(v => v.ErrorMessage)));
        }
    }
}
