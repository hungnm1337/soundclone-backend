using Data.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Admin.Account
{
    public interface IAccountRepository
    {
        /// <summary>
        /// Lấy tất cả tài khoản với thông tin cơ bản
        /// </summary>
        /// <returns>Danh sách tài khoản</returns>
        Task<List<AccountListDTO>> GetAllAccountsAsync();

        /// <summary>
        /// Lấy thông tin chi tiết của một tài khoản theo ID
        /// </summary>
        /// <param name="userId">ID của tài khoản</param>
        /// <returns>Thông tin chi tiết tài khoản</returns>
        Task<AccountDTO?> GetAccountByIdAsync(int userId);

        /// <summary>
        /// Khóa tài khoản
        /// </summary>
        /// <param name="userId">ID của tài khoản</param>
        /// <returns>True nếu khóa thành công</returns>
        Task<bool> BlockAccountAsync(int userId);

        /// <summary>
        /// Mở khóa tài khoản
        /// </summary>
        /// <param name="userId">ID của tài khoản</param>
        /// <returns>True nếu mở khóa thành công</returns>
        Task<bool> UnblockAccountAsync(int userId);

        /// <summary>
        /// Tìm kiếm tài khoản theo tên hoặc email
        /// </summary>
        /// <param name="searchTerm">Từ khóa tìm kiếm</param>
        /// <returns>Danh sách tài khoản phù hợp</returns>
        Task<List<AccountListDTO>> SearchAccountsAsync(string searchTerm);

        Task<List<RoleDTO>> GetRoles();

        Task<bool> ChangeRoleOfUser(int UserId, int NewRoleId);
    }
}
