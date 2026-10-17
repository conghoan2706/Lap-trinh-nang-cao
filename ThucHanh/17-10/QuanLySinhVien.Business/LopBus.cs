using System.ComponentModel.DataAnnotations;
using QuanLySinhVien.Data.DAL;
using QuanLySinhVien.Data.Entity;

namespace QuanLySinhVien.BUL
{
    public class LopBus
    {
        private readonly LopHocDAO dao = new LopHocDAO();

        public List<LopHoc> GetAllLopHoc()
        {
            try
            {
                return dao.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public LopHoc? GetByMa(string maLop)
        {
            try
            {
                return dao.GetByMa(maLop);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Add(LopHoc lop)
        {
            try
            {
                EnsureValid(lop);
                if (dao.GetByMa(lop.MaLop) != null)
                    throw new Exception("Mã lớp đã tồn tại");
                dao.Add(lop);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(LopHoc lop)
        {
            try
            {
                EnsureValid(lop);
                dao.Update(lop);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(string maLop)
        {
            try
            {
                dao.Delete(maLop);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static void EnsureValid(LopHoc lop)
        {
            List<ValidationResult> errors = lop.IsInValid();
            if (errors.Count > 0)
                throw new Exception(string.Join("\n", errors.Select(v => v.ErrorMessage)));
        }
    }
}
