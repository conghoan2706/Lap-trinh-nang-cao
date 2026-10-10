using System.ComponentModel.DataAnnotations;
using QuanLySinhVien.Data.DAL;
using QuanLySinhVien.Data.Entity;

namespace QuanLySinhVien.BUL
{
    /// <summary>
    /// Business layer for classes. The form only needs the full list for the combo box.
    /// </summary>
    public class LopBus
    {
        private readonly LopDAL dal = new LopDAL();

        public List<LopHoc> GetAllLopHoc()
        {
            return dal.GetAll();
        }

        public LopHoc? GetByMa(string maLop)
        {
            return dal.GetByMa(maLop);
        }

        public void Add(LopHoc lop)
        {
            List<ValidationResult> errors = lop.IsInValid();
            if (errors.Count > 0)
                throw new InvalidOperationException(string.Join("\n", errors.Select(v => v.ErrorMessage)));
            if (dal.GetByMa(lop.MaLop) != null)
                throw new InvalidOperationException("Mã lớp đã tồn tại");

            dal.Add(lop);
        }

        public void Update(LopHoc lop)
        {
            List<ValidationResult> errors = lop.IsInValid();
            if (errors.Count > 0)
                throw new InvalidOperationException(string.Join("\n", errors.Select(v => v.ErrorMessage)));

            dal.Update(lop);
        }

        public void Delete(string maLop)
        {
            dal.Delete(maLop);
        }
    }
}
