using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoLeasingApp.Data
{
    public class LeaseContract
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int CarId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public decimal TotalCost { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; } = null;

        [ForeignKey("CarId")]
        public Car Car { get; set; } = null;
    }
}