using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace QuanLyThuVienDAO.Entity
{
    public class BookCopy
    {
        [Key]
        [Required]
        [StringLength(20)]
        public string BookCopyId { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = string.Empty;

        [Required]
        [StringLength(30)]
        public string Condition { get; set; } = string.Empty;

        // Khóa ngoại đến Book
        [Required]
        [StringLength(20)]
        public string BookId { get; set; } = string.Empty;

        // Mỗi bản sao thuộc một sách
        [ForeignKey(nameof(BookId))]
        public Book? Book { get; set; }
    }
}
