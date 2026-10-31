using Microsoft.EntityFrameworkCore;
using QuanLyThuVienDAO.Entity;

namespace QuanLyThuVienDAO.Reposistory;

public class CategoryRepository
{
    private readonly QLTVDBContext _context;
    public CategoryRepository(QLTVDBContext context) => _context = context;
    public List<Category> GetAll() => _context.Categories.AsNoTracking().OrderBy(c => c.Description).ToList();
    public Category? GetById(string id) => _context.Categories.AsNoTracking().SingleOrDefault(c => c.CategoryId == id);
    public void Add(Category category)
    {
        if (_context.Categories.Any(c => c.CategoryId == category.CategoryId))
            throw new InvalidOperationException("Mã thể loại đã tồn tại.");
        _context.Categories.Add(category);
        _context.SaveChanges();
    }
    public bool Update(Category category)
    {
        var existing = _context.Categories.Find(category.CategoryId);
        if (existing == null) return false;
        existing.Description = category.Description;
        _context.SaveChanges();
        return true;
    }
    public bool Delete(string id)
    {
        var existing = _context.Categories.Find(id);
        if (existing == null) return false;
        if (_context.Books.Any(b => b.CategoryId == id))
            throw new InvalidOperationException("Không thể xóa thể loại đang có sách.");
        _context.Categories.Remove(existing);
        _context.SaveChanges();
        return true;
    }
    public List<Category> getCategories() => GetAll();
    public Category? getCategory(string id) => GetById(id);
}
