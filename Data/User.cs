using System.ComponentModel.DataAnnotations;

namespace AutoLeasingApp.Data
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        public List<LeaseContract> LeaseContracts { get; set; } = new List<LeaseContract>();
    }
}