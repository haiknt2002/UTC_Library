using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Transaction
    {
        // TranId: Thuộc tính tự tăng
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Tự động tăng giá trị
        public int TranId { get; set; }

        // Foreign Key trỏ đến bảng Book
        [ForeignKey("Book")]
        public int? BookId { get; set; }

        // BookTitle: Tiêu đề sách (nvarchar(50))
        [StringLength(50)]
        public string? BookTitle { get; set; }

        // BookISBN: Mã ISBN của sách (nvarchar(50))
        [StringLength(50)]
        public string? BookISBN { get; set; }

        // Khóa ngoại trỏ đến class Book
        public virtual Book? Book { get; set; }

        // TranStatus: Trạng thái giao dịch (nvarchar(50))
        [StringLength(50)]
        public string? TranStatus { get; set; }

        // TranDate: Ngày giao dịch (nvarchar(50))
        [StringLength(50)]
        public string? TranDate { get; set; }

        // Foreign Key trỏ đến bảng User
        [ForeignKey("User")]
        public int? UserId { get; set; }

        // UserName: Tên người dùng (nvarchar(50))
        [StringLength(50)]
        public string? UserName { get; set; }
        // Khóa ngoại trỏ đến class User
        public virtual User? User { get; set; }
    }
}
