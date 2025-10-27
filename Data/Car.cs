using System.ComponentModel.DataAnnotations;

namespace AutoLeasingApp.Data
{
    public class Car
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Model { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Make { get; set; } = string.Empty;

        [Required]
        public int Year { get; set; }

        [Required]
        public decimal DailyRate { get; set; } // cost per day for leasing

        [Required]
        [MaxLength(1000)]
        public string ImagePath { get; set; } = string.Empty;

        public bool IsAvailable { get; set; } = true;

        public List<LeaseContract> LeaseContracts { get; set; } = new List<LeaseContract>();
    }
}