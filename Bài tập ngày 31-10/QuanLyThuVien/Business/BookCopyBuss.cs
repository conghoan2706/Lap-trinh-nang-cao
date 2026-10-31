using QuanLyThuVienDAO.Entity;
using QuanLyThuVienDAO.Reposistory;

namespace QuanLyThuVienDAO.Business;

public class BookCopyBuss
{
    public static readonly string[] Statuses = ["Sẵn sàng", "Đang mượn", "Ngừng phục vụ"];
    public static readonly string[] Conditions = ["Mới", "Tốt", "Cũ", "Hỏng", "Mất"];
    private readonly BookCopyRepository _dao;
    public BookCopyBuss(QLTVDBContext context) => _dao = new(context);
    public List<BookCopy> GetAll() => _dao.GetAll();
    public BookCopy? GetById(string id) { Validation.Id(id); return _dao.GetById(id); }
    public List<BookCopy> GetByBookId(string id) { Validation.Id(id); return _dao.GetByBookId(id); }
    public void Add(BookCopy copy) { Validate(copy); _dao.Add(copy); }
    public void Update(BookCopy copy) { Validate(copy); Validation.Found(_dao.Update(copy)); }
    public void Delete(string id) { Validation.Id(id); Validation.Found(_dao.Delete(id)); }
    private static void Validate(BookCopy copy)
    {
        Validation.Entity(copy);
        if (!Statuses.Contains(copy.Status) || !Conditions.Contains(copy.Condition))
            throw new ArgumentException("Trạng thái hoặc tình trạng vật lý không hợp lệ.");
        if (copy.Status == "Sẵn sàng" && (copy.Condition == "Hỏng" || copy.Condition == "Mất"))
            throw new ArgumentException("Bản sao hỏng hoặc mất không thể ở trạng thái sẵn sàng.");
    }
}
