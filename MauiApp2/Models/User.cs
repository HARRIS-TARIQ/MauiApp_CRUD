using System.ComponentModel.DataAnnotations;

namespace MauiApp2.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
        
        public string FullName { get; set; } = string.Empty;

        public bool RememberMe { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
