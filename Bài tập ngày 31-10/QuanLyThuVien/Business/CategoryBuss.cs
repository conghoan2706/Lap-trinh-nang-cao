using QuanLyThuVienDAO.Entity;
using QuanLyThuVienDAO.Reposistory;

namespace QuanLyThuVienDAO.Business;

public class CategoryBuss
{
    private readonly CategoryRepository _dao;
    public CategoryBuss(QLTVDBContext context) => _dao = new(context);
    public List<Category> GetAll() => _dao.GetAll();
    public List<Category> GetByName(string? name) => GetAll().Where(c =>
        string.IsNullOrWhiteSpace(name) || c.Description.Contains(name.Trim(), StringComparison.OrdinalIgnoreCase)).ToList();
    public Category? GetById(string id) { Validation.Id(id, 10); return _dao.GetById(id); }
    public void Add(Category category) { Validation.Entity(category); _dao.Add(category); }
    public void Edit(Category category) { Validation.Entity(category); Validation.Found(_dao.Update(category)); }
    public void Delete(string id) { Validation.Id(id, 10); Validation.Found(_dao.Delete(id)); }
}
