
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace QuanLyThuVienDAO.Entity
{
    public class Book
    {
        [Key]
        [Required]
        [StringLength(20)]
        public string BookId { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(150)]
        public string Publisher { get; set; } = string.Empty;

        [Range(0, double.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int NumberOfCopies { get; set; }

        [Required]
        [StringLength(150)]
        public string Author { get; set; } = string.Empty;

        [Range(1000, 9999)]
        public int PublicationYear { get; set; }

        // Khóa ngoại đến Category
        [Required]
        [StringLength(10)]
        public string CategoryId { get; set; } = string.Empty;

        // Một sách thuộc một thể loại
        [ForeignKey(nameof(CategoryId))]
        public Category? Category { get; set; }

        // Một sách có nhiều bản sao
        public List<BookCopy> BookCopies { get; set; } = new();
        public List<ValidationResult>? IsInValid()
        {
            var valcontext = new ValidationContext(this);
            var results = new List<ValidationResult>();
            var kt = Validator.TryValidateObject(this, valcontext, results, validateAllProperties: true);
            if (!kt)
            {
                return results;
            }
            else
            {
                return null;
            }

        }
    }
}
