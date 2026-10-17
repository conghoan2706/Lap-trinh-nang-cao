using System.Data;
using Microsoft.Data.SqlClient;
using QuanLySinhVien.Data.Entity;

namespace QuanLySinhVien.Data.DAL
{
    public class SinhVienDAO
    {
        private readonly DataHelper dataHelper = new DataHelper();

        public List<SinhVien> GetAllSinhVien()
        {
            try
            {
                List<SinhVien> list = new List<SinhVien>();
                DataTable dt = dataHelper.GetDataTable(
                    "SELECT MaSV, HoTen, NgaySinh, GioiTinh, Email, SoDienThoai, MaLop, Diem, TrangThai FROM SinhVien");
                foreach (DataRow dr in dt.Rows)
                    list.Add(Map(dr));
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi đọc danh sách sinh viên: " + ex.Message);
            }
        }

        public SinhVien? GetSinhVienByMaSV(string maSV)
        {
            try
            {
                DataTable dt = dataHelper.GetDataTable(
                    "SELECT MaSV, HoTen, NgaySinh, GioiTinh, Email, SoDienThoai, MaLop, Diem, TrangThai FROM SinhVien WHERE MaSV = @MaSV",
                    new SqlParameter("@MaSV", maSV));
                if (dt.Rows.Count == 0)
                    return null;
                return Map(dt.Rows[0]);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tìm sinh viên: " + ex.Message);
            }
        }

        public SinhVien? GetSinhVienById(string maSV)
        {
            return GetSinhVienByMaSV(maSV);
        }

        public List<SinhVien> GetSinhViensByMaLop(string maLop)
        {
            try
            {
                List<SinhVien> list = new List<SinhVien>();
                DataTable dt = dataHelper.GetDataTable(
                    "SELECT MaSV, HoTen, NgaySinh, GioiTinh, Email, SoDienThoai, MaLop, Diem, TrangThai FROM SinhVien WHERE MaLop = @MaLop",
                    new SqlParameter("@MaLop", maLop));
                foreach (DataRow dr in dt.Rows)
                    list.Add(Map(dr));
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi lọc sinh viên theo lớp: " + ex.Message);
            }
        }

        public void AddSinhVien(SinhVien sv)
        {
            try
            {
                string sql = @"INSERT INTO SinhVien
                    (MaSV, MaLop, HoTen, GioiTinh, NgaySinh, Diem, SoDienThoai, Email, TrangThai)
                    VALUES (@MaSV, @MaLop, @HoTen, @GioiTinh, @NgaySinh, @Diem, @SoDienThoai, @Email, @TrangThai)";
                dataHelper.ExecuteNonQuery(sql, Params(sv));
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi thêm sinh viên: " + ex.Message);
            }
        }

        public void UpdateSinhVien(SinhVien sv)
        {
            try
            {
                string sql = @"UPDATE SinhVien SET
                    HoTen = @HoTen, MaLop = @MaLop, GioiTinh = @GioiTinh, NgaySinh = @NgaySinh,
                    Diem = @Diem, SoDienThoai = @SoDienThoai, Email = @Email, TrangThai = @TrangThai
                    WHERE MaSV = @MaSV";
                dataHelper.ExecuteNonQuery(sql, Params(sv));
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi sửa sinh viên: " + ex.Message);
            }
        }

        public void DeleteSinhVien(string maSV)
        {
            try
            {
                dataHelper.ExecuteNonQuery(
                    "DELETE FROM SinhVien WHERE MaSV = @MaSV",
                    new SqlParameter("@MaSV", maSV));
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi xóa sinh viên: " + ex.Message);
            }
        }

        private static SqlParameter[] Params(SinhVien sv)
        {
            return new SqlParameter[]
            {
                new SqlParameter("@MaSV", sv.MaSV),
                new SqlParameter("@MaLop", sv.MaLop),
                new SqlParameter("@HoTen", sv.HoTen),
                new SqlParameter("@GioiTinh", sv.GioiTinh),
                new SqlParameter("@NgaySinh", sv.NgaySinh.Date),
                new SqlParameter("@Diem", sv.Diem),
                new SqlParameter("@SoDienThoai", sv.SoDienThoai),
                new SqlParameter("@Email", sv.Email),
                new SqlParameter("@TrangThai", sv.TrangThai)
            };
        }

        private static SinhVien Map(DataRow dr)
        {
            return new SinhVien
            {
                MaSV = dr["MaSV"].ToString() ?? "",
                HoTen = dr["HoTen"].ToString() ?? "",
                GioiTinh = dr["GioiTinh"].ToString() ?? "",
                NgaySinh = Convert.ToDateTime(dr["NgaySinh"]),
                SoDienThoai = dr["SoDienThoai"].ToString() ?? "",
                MaLop = dr["MaLop"].ToString() ?? "",
                Diem = Convert.ToDouble(dr["Diem"]),
                Email = dr["Email"].ToString() ?? "",
                TrangThai = dr.Table.Columns.Contains("TrangThai")
                    ? dr["TrangThai"].ToString() ?? "Đang học"
                    : "Đang học"
            };
        }
    }
}
