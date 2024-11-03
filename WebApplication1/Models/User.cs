using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class User
    {
        // UserId: Thuộc tính tự tăng
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Tự động tăng giá trị
        public int UserId { get; set; }

        // UserName: Tên người dùng (nvarchar(50))
        [StringLength(50)]
        public string? UserName { get; set; }

        // UserGender: Giới tính người dùng (nchar(10))
        [StringLength(10)]
        public string? UserGender { get; set; }

        // UserDep: Bộ phận người dùng (nchar(10))
        [StringLength(10)]
        public string? UserDep { get; set; }

        // UserAdmNo: Số quản trị viên (int)
        public int? UserAdmNo { get; set; }

        // UserEmail: Email người dùng (nvarchar(50))
        [StringLength(50)]
        public string? UserEmail { get; set; }

        // UserPass: Mật khẩu người dùng (nvarchar(50))
        [StringLength(50)]
        public string? UserPass { get; set; }
    }
}
