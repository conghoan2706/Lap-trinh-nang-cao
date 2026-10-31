using Microsoft.EntityFrameworkCore;
using QuanLyThuVienDAO.Entity;

namespace QuanLyThuVienDAO.Reposistory;

public class BookRepository
{
    private readonly QLTVDBContext _context;
    public BookRepository(QLTVDBContext context) => _context = context;
    public List<Book> GetAll() => Search(null, null);
    public Book? GetById(string id) => _context.Books.AsNoTracking()
        .Include(b => b.Category).Include(b => b.BookCopies).SingleOrDefault(b => b.BookId == id);
    public List<Book> GetByCategoryId(string id) => Search(null, id);
    public List<Book> Search(string? keyword, string? categoryId)
    {
        var query = _context.Books.AsNoTracking().Include(b => b.Category).AsQueryable();
        if (!string.IsNullOrWhiteSpace(keyword))
        {
            var text = keyword.Trim().ToLowerInvariant();
            query = query.Where(b => b.BookId.ToLower().Contains(text)
                || b.Title.ToLower().Contains(text) || b.Author.ToLower().Contains(text));
        }
        if (!string.IsNullOrWhiteSpace(categoryId)) query = query.Where(b => b.CategoryId == categoryId);
        return query.OrderBy(b => b.Title).ToList();
    }
    // Business quản lý giao dịch để đầu sách và các bản sao được lưu cùng nhau.
    public void Add(Book book)
    {
        if (_context.Books.Any(b => b.BookId == book.BookId))
            throw new InvalidOperationException("Mã sách đã tồn tại.");
        RequireCategory(book.CategoryId);
        _context.Books.Add(book);
        _context.SaveChanges();
    }
    public bool Update(Book book)
    {
        var existing = _context.Books.Find(book.BookId);
        if (existing == null) return false;
        RequireCategory(book.CategoryId);
        existing.Title = book.Title;
        existing.Author = book.Author;
        existing.Publisher = book.Publisher;
        existing.Price = book.Price;
        existing.PublicationYear = book.PublicationYear;
        existing.CategoryId = book.CategoryId;
        // Số bản sao lấy từ bảng BookCopies, không lấy từ ô sửa thông tin sách.
        existing.NumberOfCopies = _context.BookCopies.Count(c => c.BookId == book.BookId);
        _context.SaveChanges();
        return true;
    }
    public bool Delete(string id)
    {
        var existing = _context.Books.Find(id);
        if (existing == null) return false;
        if (_context.BookCopies.Any(c => c.BookId == id))
            throw new InvalidOperationException("Hãy xóa hết bản sao trước khi xóa đầu sách.");
        _context.Books.Remove(existing);
        _context.SaveChanges();
        return true;
    }
    private void RequireCategory(string id)
    {
        if (!_context.Categories.Any(c => c.CategoryId == id))
            throw new InvalidOperationException("Thể loại không tồn tại.");
    }
    public List<Book> getBooks() => GetAll();
    public List<Book> getBookbyCategory(string id) => GetByCategoryId(id);
    public void AddBoo(Book book) => Add(book);
    public bool updateBook(Book book) => Update(book);
    public bool deleteBook(string id) => Delete(id);
}
