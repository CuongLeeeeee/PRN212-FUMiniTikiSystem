using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FUMiniTikiSystem.BLL.Interfaces;
using FUMiniTikiSystem.DAL;
using FUMiniTikiSystem.DAL.Entities;
using FUMiniTikiSystem.DAL.Interfaces;
using FUMiniTikiSystem.DAL.Repositories;

namespace FUMiniTikiSystem.BLL.Services
{
    public class CustomerService: ICustomerService
    {
        private readonly ICustomerRepository _repo;
        private readonly IUnitOfWork _unitOfWork;
        public CustomerService(ICustomerRepository repo, IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> ChangePasswordAsync(int customerId, string oldPassword, string newPassword)
        {
            var result =await _repo.ChangePasswordAsync(customerId, oldPassword, newPassword);
            if(result) await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<Customer?> LoginAsync(string email, string password)
        {
            return await _repo.LoginAsync(email, password);
        }

        public Task LogoutAsync()
        {
            throw new NotImplementedException();
        }
    }
}
