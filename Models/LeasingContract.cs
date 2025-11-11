using System;

namespace ProiectPractica.Models
{
    public class LeasingContract
    {
        public int ContractId { get; set; }
        public int UserId { get; set; }
        public int CarId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
    }
}
