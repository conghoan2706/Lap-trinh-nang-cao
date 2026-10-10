using QuanLySinhVien.Data.Entity;

namespace QuanLySinhVien.Data.DAL
{
    /// <summary>
    /// Data access for SinhVien. CRUD runs against a list kept in memory.
    /// </summary>
    public class SinhVienDAL
    {
        private readonly List<SinhVien> sinhViens = new List<SinhVien>();

        public SinhVienDAL()
        {
            sinhViens.Add(new SinhVien
            {
                MaSV = "SV000123",
                HoTen = "Nguyễn Văn An",
                NgaySinh = new DateTime(2006, 8, 15),
                GioiTinh = "Nam",
                Email = "an.nv@vju.ac.vn",
                SoDienThoai = "0912345678",
                MaLop = "CSE0001",
                Diem = 8.5,
                TrangThai = "Đang học"
            });
            sinhViens.Add(new SinhVien
            {
                MaSV = "SV000124",
                HoTen = "Trần Minh Anh",
                NgaySinh = new DateTime(2006, 1, 22),
                GioiTinh = "Nữ",
                Email = "anh.tm@vju.ac.vn",
                SoDienThoai = "0987654321",
                MaLop = "CSE0002",
                Diem = 9.0,
                TrangThai = "Đang học"
            });
            sinhViens.Add(new SinhVien
            {
                MaSV = "SV000125",
                HoTen = "Lê Hoàng Bình",
                NgaySinh = new DateTime(2006, 5, 9),
                GioiTinh = "Nam",
                Email = "binh.lh@vju.ac.vn",
                SoDienThoai = "0355556677",
                MaLop = "CSE0001",
                Diem = 7.4,
                TrangThai = "Đang học"
            });
            sinhViens.Add(new SinhVien
            {
                MaSV = "SV000126",
                HoTen = "Đỗ Thị Hồng",
                NgaySinh = new DateTime(2006, 11, 30),
                GioiTinh = "Nữ",
                Email = "hong.dt@vju.ac.vn",
                SoDienThoai = "0777888999",
                MaLop = "CSE0003",
                Diem = 8.1,
                TrangThai = "Đang học"
            });
        }

        public SinhVien? GetSinhVienByMaSV(string maSV)
        {
            if (string.IsNullOrWhiteSpace(maSV))
                return null;

            return sinhViens.FirstOrDefault(s => s.MaSV == maSV);
        }

        public SinhVien? GetSinhVienById(string maSV)
        {
            return GetSinhVienByMaSV(maSV);
        }

        public List<SinhVien> GetSinhViensByMaLop(string maLop)
        {
            return sinhViens.FindAll(s => s.MaLop == maLop);
        }

        public List<SinhVien> GetAllSinhVien()
        {
            return sinhViens.ToList();
        }

        public void AddSinhVien(SinhVien sv)
        {
            if (GetSinhVienByMaSV(sv.MaSV) != null)
                throw new InvalidOperationException("Mã sinh viên đã tồn tại");

            sinhViens.Add(sv);
        }

        public void UpdateSinhVien(SinhVien sv)
        {
            SinhVien? current = sinhViens.FirstOrDefault(s => s.MaSV == sv.MaSV);
            if (current == null)
                throw new InvalidOperationException("Không tìm thấy sinh viên để sửa");

            current.HoTen = sv.HoTen;
            current.NgaySinh = sv.NgaySinh;
            current.GioiTinh = sv.GioiTinh;
            current.Email = sv.Email;
            current.SoDienThoai = sv.SoDienThoai;
            current.MaLop = sv.MaLop;
            current.Diem = sv.Diem;
            current.TrangThai = sv.TrangThai;
        }

        public void DeleteSinhVien(string maSV)
        {
            SinhVien? sv = sinhViens.FirstOrDefault(s => s.MaSV == maSV);
            if (sv == null)
                throw new InvalidOperationException("Không tìm thấy sinh viên để xóa");

            sinhViens.Remove(sv);
        }
    }
}
