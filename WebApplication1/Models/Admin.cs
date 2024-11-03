using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Admin
    {
        // Thuộc tính tự tăng cho AdminId
        [Key] // Đây là primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Tự động tăng giá trị
        public int AdminId { get; set; }

        // Thuộc tính AdminName có thể null
        [StringLength(50)]
        public string? AdminName { get; set; }

        // Thuộc tính AdminEmail có thể null
        [StringLength(50)]
        public string? AdminEmail { get; set; }

        // Thuộc tính AdminPassword có thể null
        [StringLength(50)]
        public string? AdminPassword { get; set; }
    }

}
