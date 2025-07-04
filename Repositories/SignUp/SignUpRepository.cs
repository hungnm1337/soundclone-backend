using Data.DTOs;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Repositories.SignUp
{
    public class SignUpRepository : ISignUpRepository
    {
        private readonly SoundcloneContext _soundcloneContext;

        public SignUpRepository(SoundcloneContext soundcloneContext)
        {
            _soundcloneContext = soundcloneContext;
        }
        public async Task<SignUpDTO> Register(SignUpDTO model)
        {
            try
            {
                var checkEmailAndPhoneNumberExisted = await _soundcloneContext.Users.Where(
                x => x.PhoneNumber.Equals(model.PhoneNumber) ||
                x.Email.Equals(model.Email) ||
                x.Username.Equals(model.Username))
                .FirstOrDefaultAsync();
                if (checkEmailAndPhoneNumberExisted != null)
                {
                    return null;
                }
                User newUser = new User()
                {
                    Name = model.Name,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    DayOfBirth = model.DayOfBirth,
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    Username = model.Username,
                    HashedPassword = HashPasswordSHA256(model.HashedPassword),
                    Status = "ACTIVE",
                    RoleId = 1

                };
                _soundcloneContext.Users.Add(newUser);
                await _soundcloneContext.SaveChangesAsync();
                return model;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static string HashPasswordSHA256(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
