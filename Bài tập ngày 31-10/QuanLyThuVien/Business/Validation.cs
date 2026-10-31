using System.ComponentModel.DataAnnotations;

namespace QuanLyThuVienDAO.Business;

internal static class Validation
{
    public static void Entity(object value)
    {
        ArgumentNullException.ThrowIfNull(value);
        var errors = new List<ValidationResult>();
        if (!Validator.TryValidateObject(value, new ValidationContext(value), errors, true))
            throw new ArgumentException(string.Join(Environment.NewLine, errors.Select(e => e.ErrorMessage)));
    }
    public static void Id(string id, int max = 20)
    {
        if (string.IsNullOrWhiteSpace(id) || id.Length > max)
            throw new ArgumentException($"Mã phải có từ 1 đến {max} ký tự.");
    }
    public static void Found(bool found)
    {
        if (!found) throw new InvalidOperationException("Không tìm thấy dữ liệu cần thao tác. Hãy làm mới danh sách.");
    }
}
