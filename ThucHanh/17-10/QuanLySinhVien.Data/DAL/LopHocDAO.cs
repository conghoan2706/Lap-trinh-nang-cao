using System.Data;
using Microsoft.Data.SqlClient;
using QuanLySinhVien.Data.Entity;

namespace QuanLySinhVien.Data.DAL
{
    public class LopHocDAO
    {
        private readonly DataHelper dataHelper = new DataHelper();

        public List<LopHoc> GetAll()
        {
            try
            {
                List<LopHoc> list = new List<LopHoc>();
                DataTable dt = dataHelper.GetDataTable("SELECT MaLop, TenLop FROM LopHoc ORDER BY MaLop");
                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(new LopHoc
                    {
                        MaLop = dr["MaLop"].ToString() ?? "",
                        TenLop = dr["TenLop"].ToString() ?? ""
                    });
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi đọc danh sách lớp: " + ex.Message);
            }
        }

        public LopHoc? GetByMa(string maLop)
        {
            try
            {
                DataTable dt = dataHelper.GetDataTable(
                    "SELECT MaLop, TenLop FROM LopHoc WHERE MaLop = @MaLop",
                    new SqlParameter("@MaLop", maLop));
                if (dt.Rows.Count == 0)
                    return null;
                return new LopHoc
                {
                    MaLop = dt.Rows[0]["MaLop"].ToString() ?? "",
                    TenLop = dt.Rows[0]["TenLop"].ToString() ?? ""
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi tìm lớp: " + ex.Message);
            }
        }

        public void Add(LopHoc lop)
        {
            try
            {
                dataHelper.ExecuteNonQuery(
                    "INSERT INTO LopHoc (MaLop, TenLop) VALUES (@MaLop, @TenLop)",
                    new SqlParameter("@MaLop", lop.MaLop),
                    new SqlParameter("@TenLop", lop.TenLop));
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi thêm lớp: " + ex.Message);
            }
        }

        public void Update(LopHoc lop)
        {
            try
            {
                dataHelper.ExecuteNonQuery(
                    "UPDATE LopHoc SET TenLop = @TenLop WHERE MaLop = @MaLop",
                    new SqlParameter("@MaLop", lop.MaLop),
                    new SqlParameter("@TenLop", lop.TenLop));
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi sửa lớp: " + ex.Message);
            }
        }

        public void Delete(string maLop)
        {
            try
            {
                dataHelper.ExecuteNonQuery(
                    "DELETE FROM LopHoc WHERE MaLop = @MaLop",
                    new SqlParameter("@MaLop", maLop));
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi xóa lớp: " + ex.Message);
            }
        }
    }
}
