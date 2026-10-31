using QuanLyThuVienDAO.Entity;
using QuanLyThuVienDAO.Reposistory;

namespace QuanLyThuVienDAO.Business;

public class ManageBookBuss
{
    private readonly QLTVDBContext _context;
    private readonly BookRepository _books;
    private readonly BookCopyRepository _copies;
    public ManageBookBuss(QLTVDBContext context)
    {
        _context = context;
        _books = new(context);
        _copies = new(context);
    }
    public List<Book> GetAll() => _books.GetAll();
    public List<Book> getBooks() => GetAll();
    public List<Category> getCategories() => new CategoryRepository(_context).GetAll();
    public Book? GetById(string id) { Validation.Id(id); return _books.GetById(id); }
    public List<Book> Search(string? keyword, string? categoryId) => _books.Search(keyword, categoryId);
    public void AddBook(Book book) => Add(book);
    public void Add(Book book)
    {
        Validate(book);
        if (book.BookCopies.Count > 0) throw new ArgumentException("Dùng số bản sao để tự sinh các bản sao ban đầu.");
        int count = book.NumberOfCopies;
        Atomic(() => { book.NumberOfCopies = 0; _books.Add(book); AddCopies(book.BookId, count); });
    }
    public void Update(Book book) { Validate(book); Validation.Found(_books.Update(book)); }
    public void Delete(string id) { Validation.Id(id); Validation.Found(_books.Delete(id)); }
    public void ImportCopies(string bookId, int quantity)
    {
        Validation.Id(bookId);
        if (quantity < 1 || quantity > 1000) throw new ArgumentException("Số bản sao nhập thêm phải từ 1 đến 1000.");
        Atomic(() => AddCopies(bookId, quantity));
    }
    public void ImportBill(List<Book> books)
    {
        ArgumentNullException.ThrowIfNull(books);
        foreach (var book in books)
        {
            Validate(book);
            if (book.NumberOfCopies < 1) throw new ArgumentException("Số lượng nhập phải lớn hơn 0.");
            if (book.BookCopies.Count > 0) throw new ArgumentException("Không truyền danh sách bản sao khi nhập phiếu.");
        }
        Atomic(() =>
        {
            foreach (var book in books)
            {
                int count = book.NumberOfCopies;
                if (_books.GetById(book.BookId) == null)
                {
                    book.NumberOfCopies = 0;
                    _books.Add(book);
                }
                AddCopies(book.BookId, count);
            }
        });
    }
    public void ImportBill1(List<Book> books) => ImportBill(books);
    public void UpdateNumCopy(string id)
    {
        var book = GetById(id) ?? throw new InvalidOperationException("Sách không tồn tại.");
        Validation.Found(_books.Update(book));
    }
    private void AddCopies(string id, int count)
    {
        if (_books.GetById(id) == null) throw new InvalidOperationException("Sách không tồn tại.");
        int sequence = _copies.NextSequence(id);
        for (int i = 0; i < count; i++)
        {
            string copyId = $"{id}.{sequence + i:D3}";
            if (copyId.Length > 20) throw new ArgumentException("Mã bản sao sinh ra vượt quá 20 ký tự. Hãy dùng mã sách ngắn hơn.");
            _copies.Add(new BookCopy { BookCopyId = copyId, BookId = id, Status = "Sẵn sàng", Condition = "Mới" });
        }
    }
    private void Atomic(Action action)
    {
        using var transaction = _context.Database.BeginTransaction();
        try { action(); transaction.Commit(); }
        catch { transaction.Rollback(); _context.ChangeTracker.Clear(); throw; }
    }
    private static void Validate(Book book)
    {
        Validation.Entity(book);
        if (book.NumberOfCopies > 1000) throw new ArgumentException("Mỗi lần chỉ nhập tối đa 1000 bản sao.");
        if (book.PublicationYear > DateTime.Today.Year) throw new ArgumentException("Năm xuất bản không được lớn hơn năm hiện tại.");
    }
}
