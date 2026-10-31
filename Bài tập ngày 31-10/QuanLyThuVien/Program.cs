using QuanLyThuVienDAO.Business;
using QuanLyThuVienDAO.Entity;
using QuanLyThuVienDAO.Views;

namespace QuanLyThuVienDAO;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        try
        {
            using var context = new QLTVDBContext();
            context.Database.EnsureCreated();
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category { CategoryId = "VH", Description = "Văn học" },
                    new Category { CategoryId = "LS", Description = "Lịch sử" },
                    new Category { CategoryId = "KT", Description = "Kinh tế" },
                    new Category { CategoryId = "KH", Description = "Khoa học tự nhiên" });
                context.SaveChanges();
                new ManageBookBuss(context).Add(new Book
                {
                    BookId = "B006", Title = "Tuyển tập truyện ngắn quê hương", Author = "Trần Ngọc Lan",
                    Publisher = "NXB Văn học", CategoryId = "VH", Price = 110000, PublicationYear = 2020, NumberOfCopies = 5
                });
            }
            Application.Run(new ManageBookForm(context));
        }
        catch (Exception ex) { MessageBox.Show(ex.Message, "Không thể mở ứng dụng", MessageBoxButtons.OK, MessageBoxIcon.Error); }
    }
}
