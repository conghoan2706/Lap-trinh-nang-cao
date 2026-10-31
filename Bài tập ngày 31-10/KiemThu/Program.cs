using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVienDAO;
using QuanLyThuVienDAO.Business;
using QuanLyThuVienDAO.Entity;
using QuanLyThuVienDAO.Views;

internal static class Program
{
    private static int _passed;
    [STAThread]
    private static void Main(string[] args)
    {
        // SQLite thật trong bộ nhớ, không đụng vào dữ liệu bài làm.
        using var connection = new SqliteConnection("Data Source=:memory:"); connection.Open();
        var options = new DbContextOptionsBuilder<QLTVDBContext>().UseSqlite(connection).Options;
        using var db = new QLTVDBContext(options); db.Database.EnsureCreated();
        var categories = new CategoryBuss(db); var books = new ManageBookBuss(db); var copies = new BookCopyBuss(db);
        categories.Add(new Category { CategoryId = "VH", Description = "Văn học" });
        categories.Add(new Category { CategoryId = "KT", Description = "Kinh tế" });
        books.Add(Book("B006", 3));
        Check(copies.GetByBookId("B006").Select(c => c.BookCopyId).SequenceEqual(new[] { "B006.001", "B006.002", "B006.003" }), "Sinh đủ bản sao từ 001");
        Check(books.GetById("B006")!.NumberOfCopies == 3, "Số bản sao ban đầu");
        Throws(() => books.Add(Book("B006", 1)), "Chặn mã sách trùng");
        Throws(() => books.Delete("B006"), "Chặn xóa sách còn bản sao");
        copies.Delete("B006.002"); books.ImportCopies("B006", 2);
        Check(copies.GetById("B006.004") != null && copies.GetById("B006.005") != null, "Nhập nhiều bản sao sau khi xóa không trùng mã");
        Check(books.GetById("B006")!.NumberOfCopies == 4, "Đồng bộ số bản sao sau thêm/xóa");
        var updated = Book("B006", 999); updated.Title = "Sách đã sửa"; updated.Price = 220000;
        books.Update(updated);
        Check(books.GetById("B006") is { Title: "Sách đã sửa", Price: 220000, NumberOfCopies: 4 }, "Sửa đầy đủ, giữ số bản sao thực tế");
        Check(books.Search("b006", "VH").Count == 1 && books.Search("b006", "KT").Count == 0, "Kết hợp từ khóa và thể loại");
        copies.Update(new BookCopy { BookCopyId = "B006.001", BookId = "B006", Status = "Đang mượn", Condition = "Tốt" });
        Throws(() => copies.Delete("B006.001"), "Chặn xóa bản đang mượn");
        Throws(() => copies.Update(new BookCopy { BookCopyId = "B006.003", BookId = "B006", Status = "Sẵn sàng", Condition = "Mất" }), "Chặn trạng thái/tình trạng không phù hợp");
        Throws(() => categories.Delete("VH"), "Chặn xóa thể loại còn sách");
        Throws(() => books.ImportCopies("B006", 0), "Chặn số lượng nhập bằng 0");
        var invalid = Book("FAIL", 2); invalid.CategoryId = "NONE";
        Throws(() => books.ImportBill(new List<Book> { Book("ROLLBACK", 2), invalid }), "Phiếu nhập lỗi phải báo lỗi");
        Check(books.GetById("ROLLBACK") == null && copies.GetByBookId("ROLLBACK").Count == 0, "Hoàn tác toàn bộ phiếu nhập khi dòng sau lỗi");
        books.ImportBill(new List<Book> { Book("B006", 2), Book("NEW", 1) });
        Check(books.GetById("B006")!.NumberOfCopies == 6 && books.GetById("NEW")!.NumberOfCopies == 1, "Phiếu nhập vừa bổ sung vừa thêm đầu sách");
        Throws(() => books.Add(Book("ABCDEFGHIJKLMNOPQRST", 1)), "Mã bản sao quá dài gây hoàn tác");
        Check(books.GetById("ABCDEFGHIJKLMNOPQRST") == null, "Không còn đầu sách dở dang sau lỗi sinh mã");
        copies.Delete("NEW.001"); books.Delete("NEW");
        Check(books.GetById("NEW") == null, "Xóa sách sau khi xóa hết bản sao");
        // Đọc lại qua context khác để xác minh dữ liệu đã được lưu thật.
        using (var other = new QLTVDBContext(options))
            Check(other.Books.Single(b => b.BookId == "B006").NumberOfCopies == 6, "Dữ liệu tồn tại qua context khác");
        ApplicationConfiguration.Initialize();
        using (var form = new ManageBookForm(db))
        {
            form.Show(); Application.DoEvents();
            Check(form.Visible && form.Controls.Count > 0, "Khởi tạo và tải giao diện WinForms");
            if (args.Length > 0)
            {
                using var preview = new Bitmap(form.Width, form.Height);
                form.DrawToBitmap(preview, new Rectangle(Point.Empty, form.Size));
                preview.Save(args[0], System.Drawing.Imaging.ImageFormat.Png);
            }
            form.Close();
        }
        Console.WriteLine($"PASS: {_passed} checks.");
    }
    private static Book Book(string id, int count) => new() { BookId = id, Title = "Sách thử nghiệm", Author = "Tác giả",
        Publisher = "NXB", CategoryId = "VH", Price = 110000, PublicationYear = 2020, NumberOfCopies = count };
    private static void Check(bool condition, string message)
    {
        if (!condition) throw new Exception("FAIL: " + message);
        _passed++; Console.WriteLine("PASS: " + message);
    }
    private static void Throws(Action action, string message)
    {
        try { action(); }
        catch (ArgumentException) { Check(true, message); return; }
        catch (InvalidOperationException) { Check(true, message); return; }
        throw new Exception("FAIL: Không báo lỗi - " + message);
    }
}
