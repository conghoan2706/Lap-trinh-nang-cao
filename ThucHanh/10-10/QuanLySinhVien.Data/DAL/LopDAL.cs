using QuanLySinhVien.Data.Entity;

namespace QuanLySinhVien.Data.DAL
{
    /// <summary>
    /// Data access for LopHoc. The source is an in-memory list, not a database.
    /// </summary>
    public class LopDAL
    {
        private readonly List<LopHoc> lopHocs = new List<LopHoc>();

        public LopDAL()
        {
            lopHocs.Add(new LopHoc { MaLop = "CSE0001", TenLop = "Kỹ thuật phần mềm 01" });
            lopHocs.Add(new LopHoc { MaLop = "CSE0002", TenLop = "Trí tuệ nhân tạo 01" });
            lopHocs.Add(new LopHoc { MaLop = "CSE0003", TenLop = "Khoa học dữ liệu 01" });
        }

        public List<LopHoc> GetAll()
        {
            return lopHocs.ToList();
        }

        public LopHoc? GetByMa(string maLop)
        {
            return lopHocs.FirstOrDefault(l => l.MaLop == maLop);
        }

        public void Add(LopHoc lop)
        {
            lopHocs.Add(lop);
        }

        public void Update(LopHoc lop)
        {
            LopHoc? current = lopHocs.FirstOrDefault(l => l.MaLop == lop.MaLop);
            if (current == null)
                return;

            current.TenLop = lop.TenLop;
        }

        public void Delete(string maLop)
        {
            LopHoc? lop = lopHocs.FirstOrDefault(l => l.MaLop == maLop);
            if (lop != null)
                lopHocs.Remove(lop);
        }
    }
}
