using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoLeasingApp.Data;
using Microsoft.EntityFrameworkCore;

namespace ProiectPractica.Services
{
    public class LeasingService : ILeasingService
    {
        private readonly AppDbContext _context;

        public LeasingService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Car>> GetCarsAsync(string? searchTerm = null, string? sortBy = null)
        {
            var query = _context.Cars.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(c => c.Model.Contains(searchTerm) || c.Make.Contains(searchTerm));
            }

            var today = DateTime.Today;
            query = query.Where(c => c.IsAvailable && !c.LeaseContracts.Any(lc => lc.StartDate <= today && lc.EndDate >= today));

            switch (sortBy?.ToLower())
            {
                case "price_asc":
                    query = query.OrderBy(c => c.DailyRate);
                    break;
                case "price_desc":
                    query = query.OrderByDescending(c => c.DailyRate);
                    break;
                case "year_asc":
                    query = query.OrderBy(c => c.Year);
                    break;
                case "year_desc":
                    query = query.OrderByDescending(c => c.Year);
                    break;
                default:
                    query = query.OrderBy(c => c.Id);
                    break;
            }

            return await query.ToListAsync();
        }


        public async Task<List<LeaseContract>> GetUserActiveContractsAsync(int userId)
        {
            var today = DateTime.Today;
            return await _context.LeaseContracts
                .Include(lc => lc.Car)
                .Where(lc => lc.UserId == userId && lc.StartDate <= today && lc.EndDate >= today)
                .ToListAsync();
        }

        public decimal CalculateLeaseCost(decimal dailyRate, int numberOfDays)
        {
            return dailyRate * numberOfDays;
        }

        public async Task<bool> CreateLeaseContractAsync(int userId, int carId, DateTime startDate, DateTime endDate)
        {
            try
            {
                var existingContract = await _context.LeaseContracts
                    .Where(lc => lc.CarId == carId &&
                                 ((startDate <= lc.EndDate && startDate >= lc.StartDate) ||
                                  (endDate <= lc.EndDate && endDate >= lc.StartDate) ||
                                  (startDate <= lc.StartDate && endDate >= lc.EndDate)))
                    .AnyAsync();

                if (existingContract)
                {
                    return false; // Car is already booked
                }

                var car = await _context.Cars.FindAsync(carId);
                if (car == null || !car.IsAvailable)
                {
                    return false; // Car not found or not available
                }

                var dailyRate = car.DailyRate;
                var totalDays = (endDate - startDate).Days + 1;
                var totalCost = CalculateLeaseCost(dailyRate, totalDays);

                var contract = new LeaseContract
                {
                    UserId = userId,
                    CarId = carId,
                    StartDate = startDate.Date,
                    EndDate = endDate.Date,
                    TotalCost = totalCost
                };

                _context.LeaseContracts.Add(contract);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error creating lease contract: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> TerminateLeaseContractAsync(int contractId, int userId)
        {
            try
            {
                var contract = await _context.LeaseContracts
                    .Where(lc => lc.Id == contractId && lc.UserId == userId)
                    .FirstOrDefaultAsync();

                if (contract == null)
                {
                    return false; // Contract not found or doesn't belong to user
                }

                contract.EndDate = DateTime.Today;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error terminating lease contract: {ex.Message}");
                return false;
            }
        }

        public async Task<Car?> GetCarByIdAsync(int carId)
        {
            return await _context.Cars.FindAsync(carId);
        }
    }
}
