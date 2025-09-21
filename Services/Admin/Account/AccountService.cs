using Data.DTOs;
using Repositories.Admin.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Admin.Account
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<List<AccountListDTO>> GetAllAccountsAsync()
        {
            try
            {
                return await _accountRepository.GetAllAccountsAsync();
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new Exception($"Error retrieving all accounts: {ex.Message}", ex);
            }
        }

        public async Task<AccountDTO?> GetAccountByIdAsync(int userId)
        {
            try
            {
                if (userId <= 0)
                    throw new ArgumentException("User ID must be greater than 0", nameof(userId));

                return await _accountRepository.GetAccountByIdAsync(userId);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new Exception($"Error retrieving account by ID {userId}: {ex.Message}", ex);
            }
        }


        public async Task<bool> BlockAccountAsync(int userId)
        {
            try
            {
                if (userId <= 0)
                    throw new ArgumentException("User ID must be greater than 0", nameof(userId));

                return await _accountRepository.BlockAccountAsync(userId);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new Exception($"Error blocking account {userId}: {ex.Message}", ex);
            }
        }

        public async Task<bool> UnblockAccountAsync(int userId)
        {
            try
            {
                if (userId <= 0)
                    throw new ArgumentException("User ID must be greater than 0", nameof(userId));

                return await _accountRepository.UnblockAccountAsync(userId);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new Exception($"Error unblocking account {userId}: {ex.Message}", ex);
            }
        }

        public async Task<List<AccountListDTO>> SearchAccountsAsync(string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                    throw new ArgumentException("Search term cannot be null or empty", nameof(searchTerm));

                if (searchTerm.Length < 2)
                    throw new ArgumentException("Search term must be at least 2 characters long", nameof(searchTerm));

                return await _accountRepository.SearchAccountsAsync(searchTerm);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new Exception($"Error searching accounts with term '{searchTerm}': {ex.Message}", ex);
            }
        }

        public async Task<List<RoleDTO>> GetRoles()
        {
            return await _accountRepository.GetRoles();
        }

        public async Task<bool> ChangeRoleOfUser(int UserId, int NewRoleId)
        {
            return await _accountRepository.ChangeRoleOfUser(UserId, NewRoleId);
        }
    }
}
