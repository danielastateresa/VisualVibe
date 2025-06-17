using System.ComponentModel.DataAnnotations;

namespace VisualVibe.Models
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "";

        [Required]
        public string Address { get; set; } = "";

        [Required]
        public string Email { get; set; } = "";

        [Required]
        public string Number { get; set; } = "";

        [Required]
        public string Position { get; set; } = "";

        [Required]
        public string PasswordHash { get; set; } = "";
        public string Role { get; internal set; }

    }
}