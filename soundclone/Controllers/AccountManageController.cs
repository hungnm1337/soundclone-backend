using Data.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Admin.Account;

namespace soundclone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountManageController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountManageController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Lấy tất cả tài khoản với thông tin cơ bản
        /// </summary>
        /// <returns>Danh sách tài khoản</returns>
        [HttpGet("accounts")]
        [Authorize(Roles = "6")]

        public async Task<ActionResult<List<AccountListDTO>>> GetAllAccounts()
        {
            try
            {
                var accounts = await _accountService.GetAllAccountsAsync();
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Lấy thông tin chi tiết tài khoản theo ID
        /// </summary>
        /// <param name="userId">ID của tài khoản</param>
        /// <returns>Thông tin chi tiết tài khoản</returns>
        [HttpGet("detail/{userId:int}")]
        [Authorize(Roles = "6")]

        public async Task<ActionResult<AccountDTO>> GetAccountById(int userId)
        {
            try
            {
                var account = await _accountService.GetAccountByIdAsync(userId);
                if (account == null)
                {
                    return NotFound(new { message = "Account not found" });
                }
                return Ok(account);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Khóa tài khoản
        /// </summary>
        /// <param name="userId">ID của tài khoản</param>
        /// <returns>Kết quả khóa tài khoản</returns>
        [HttpPut("block/{userId:int}")]
        [Authorize(Roles = "6")]

        public async Task<ActionResult<bool>> BlockAccount(int userId)
        {
            try
            {
                var result = await _accountService.BlockAccountAsync(userId);
                if (result)
                {
                    return Ok(new { success = true, message = "Account blocked successfully" });
                }
                return BadRequest(new { success = false, message = "Failed to block account" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Mở khóa tài khoản
        /// </summary>
        /// <param name="userId">ID của tài khoản</param>
        /// <returns>Kết quả mở khóa tài khoản</returns>
        [HttpPut("unblock/{userId:int}")]
        [Authorize(Roles = "6")]

        public async Task<ActionResult<bool>> UnblockAccount(int userId)
        {
            try
            {
                var result = await _accountService.UnblockAccountAsync(userId);
                if (result)
                {
                    return Ok(new { success = true, message = "Account unblocked successfully" });
                }
                return BadRequest(new { success = false, message = "Failed to unblock account" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Tìm kiếm tài khoản
        /// </summary>
        /// <param name="searchTerm">Từ khóa tìm kiếm</param>
        /// <returns>Danh sách tài khoản phù hợp</returns>
        [HttpGet("search")]
        [Authorize(Roles = "6")]

        public async Task<ActionResult<List<AccountListDTO>>> SearchAccounts([FromQuery] string searchTerm)
        {
            try
            {
                var accounts = await _accountService.SearchAccountsAsync(searchTerm);
                return Ok(accounts);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("roles")]
        [Authorize(Roles = "6")]

        public async Task<ActionResult<List<RoleDTO>>> GetRoles()
        {
            try
            {
                var roles = await _accountService.GetRoles();
                return Ok(roles);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPatch("change-role")]
        [Authorize(Roles = "6")]
        public async Task<ActionResult<bool>> ChangeRoleOfUser([FromBody] ChangeRoleDTO changeRole)
        {
            try
            {
                var resultOfChange = await _accountService.ChangeRoleOfUser(changeRole.UserId, changeRole.RoleId);
                return Ok(resultOfChange);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
