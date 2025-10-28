using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoLeasingApp.Data;

namespace ProiectPractica.Services
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsync(string username, string password);
        Task<User?> AuthenticateUserAsync(string username, string password);
    }
}
