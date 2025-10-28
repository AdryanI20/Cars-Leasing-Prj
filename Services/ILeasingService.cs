using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoLeasingApp.Data;

namespace ProiectPractica.Services
{
    public interface ILeasingService
    {
        Task<List<Car>> GetCarsAsync(string? searchTerm = null, string? sortBy = null);
        Task<List<LeaseContract>> GetUserActiveContractsAsync(int userId);
        decimal CalculateLeaseCost(decimal dailyRate, int numberOfDays);
        Task<bool> CreateLeaseContractAsync(int userId, int carId, DateTime startDate, DateTime endDate);
        Task<bool> TerminateLeaseContractAsync(int contractId, int userId);
        Task<Car?> GetCarByIdAsync(int carId);  
    }
}
