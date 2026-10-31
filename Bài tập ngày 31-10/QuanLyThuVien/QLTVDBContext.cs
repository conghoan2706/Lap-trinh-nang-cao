using Microsoft.EntityFrameworkCore;
using QuanLyThuVienDAO.Entity;

namespace QuanLyThuVienDAO;

public class QLTVDBContext : DbContext
{
    public DbSet<Book> Books => Set<Book>();
    public DbSet<BookCopy> BookCopies => Set<BookCopy>();
    public DbSet<Category> Categories => Set<Category>();

    public QLTVDBContext() { }
    public QLTVDBContext(DbContextOptions<QLTVDBContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
            options.UseSqlite($"Data Source={Path.Combine(AppContext.BaseDirectory, "QuanLyThuVien.db")};Foreign Keys=True");
    }

    protected override void OnModelCreating(ModelBuilder model)
    {
        model.Entity<Book>().HasOne(b => b.Category).WithMany(c => c.Books)
            .HasForeignKey(b => b.CategoryId).OnDelete(DeleteBehavior.Restrict);
        model.Entity<BookCopy>().HasOne(c => c.Book).WithMany(b => b.BookCopies)
            .HasForeignKey(c => c.BookId).OnDelete(DeleteBehavior.Restrict);
        model.Entity<Book>().Property(b => b.Price).HasPrecision(18, 2);
    }
}
