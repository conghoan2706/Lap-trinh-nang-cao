using Microsoft.EntityFrameworkCore;
using QuanLyThuVienDAO.Entity;

namespace QuanLyThuVienDAO.Reposistory;

public class BookCopyRepository
{
    private readonly QLTVDBContext _context;
    public BookCopyRepository(QLTVDBContext context) => _context = context;
    public List<BookCopy> GetAll() => _context.BookCopies.AsNoTracking().OrderBy(c => c.BookCopyId).ToList();
    public BookCopy? GetById(string id) => _context.BookCopies.AsNoTracking().SingleOrDefault(c => c.BookCopyId == id);
    public List<BookCopy> GetByBookId(string id) => _context.BookCopies.AsNoTracking()
        .Where(c => c.BookId == id).OrderBy(c => c.BookCopyId).ToList();
    public void Add(BookCopy copy)
    {
        if (_context.BookCopies.Any(c => c.BookCopyId == copy.BookCopyId))
            throw new InvalidOperationException("Mã bản sao đã tồn tại.");
        var book = _context.Books.Find(copy.BookId) ?? throw new InvalidOperationException("Sách không tồn tại.");
        book.NumberOfCopies = _context.BookCopies.Count(c => c.BookId == copy.BookId) + 1;
        _context.BookCopies.Add(copy);
        _context.SaveChanges();
    }
    public bool Update(BookCopy copy)
    {
        var existing = _context.BookCopies.Find(copy.BookCopyId);
        if (existing == null) return false;
        if (existing.BookId != copy.BookId)
            throw new InvalidOperationException("Không được chuyển bản sao sang đầu sách khác.");
        existing.Status = copy.Status;
        existing.Condition = copy.Condition;
        _context.SaveChanges();
        return true;
    }
    public bool Delete(string id)
    {
        var existing = _context.BookCopies.Find(id);
        if (existing == null) return false;
        if (existing.Status == "Đang mượn")
            throw new InvalidOperationException("Không thể xóa bản sao đang được mượn.");
        var book = _context.Books.Find(existing.BookId)!;
        book.NumberOfCopies = _context.BookCopies.Count(c => c.BookId == existing.BookId) - 1;
        _context.BookCopies.Remove(existing);
        _context.SaveChanges();
        return true;
    }
    // Dùng hậu tố lớn nhất, không dùng Count: xóa bản sao giữa danh sách không gây trùng mã.
    public int NextSequence(string bookId)
    {
        var prefix = bookId + ".";
        return GetByBookId(bookId).Select(c => c.BookCopyId.StartsWith(prefix, StringComparison.Ordinal)
            && int.TryParse(c.BookCopyId[prefix.Length..], out int number) ? number : 0)
            .DefaultIfEmpty(0).Max() + 1;
    }
}
