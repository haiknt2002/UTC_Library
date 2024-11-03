using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Book
    {
        // BookId: Thuộc tính tự tăng
        [Key] // Đây là primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Tự động tăng giá trị
        public int BookId { get; set; }

        // BookTitle: Tiêu đề sách (nvarchar(50))
        [StringLength(50)]
        public string? BookTitle { get; set; }

        // BookCategory: Thể loại sách (nvarchar(50))
        [StringLength(50)]
        public string? BookCategory { get; set; }

        // BookAuthor: Tác giả sách (nvarchar(50))
        [StringLength(50)]
        public string? BookAuthor { get; set; }

        // BookCopies: Số bản sao (int)
        public int? BookCopies { get; set; }

        // BookPub: Nhà xuất bản (nvarchar(50))
        [StringLength(50)]
        public string? BookPub { get; set; }

        // BookPubName: Tên nhà xuất bản (nvarchar(50))
        [StringLength(50)]
        public string? BookPubName { get; set; }

        // BookISBN: Mã ISBN (nvarchar(50))
        [StringLength(50)]
        public string? BookISBN { get; set; }

        // Copyright: Năm bản quyền (int)
        public int? Copyright { get; set; }

        // DateAdded: Ngày thêm sách (char(10))
        [StringLength(10)]
        public string? DateAdded { get; set; }

        // Status: Trạng thái sách (nvarchar(50))
        [StringLength(50)]
        public string? Status { get; set; }
    }
}
