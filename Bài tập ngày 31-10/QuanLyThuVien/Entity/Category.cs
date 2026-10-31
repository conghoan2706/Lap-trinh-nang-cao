using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QuanLyThuVienDAO.Entity
{
    public class Category
    {
        [Key]
        [Required]
        [StringLength(10)]
        public string CategoryId { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Description { get; set; } = string.Empty;

        // Một thể loại có nhiều sách
        public List<Book> Books { get; set; } = new();
    }
}
