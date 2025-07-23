using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FUMiniTikiSystem.DAL.Entities;

namespace FUMiniTikiSystem.BLL.Interfaces
{
    public interface ICustomerService
    {
        Task<Customer?> LoginAsync(string email, string password);
        Task<bool> ChangePasswordAsync(int customerId, string oldPassword, string newPassword);
        Task LogoutAsync();
    }
}
