using System.Windows.Media.Imaging;

namespace ProiectPractica.Models
{
    public class Car
    {
        public int CarId { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public int Year { get; set; }
        public string ImageUri { get; set; }
    }
}
