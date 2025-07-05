using Data.DTOs;
using Repositories.SignUp;
using Services.SignUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.SignUp
{
    public class SignUpService : ISignUpService
    {
        private readonly ISignUpRepository _repository;
        public SignUpService(ISignUpRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> checkEmail(string email)
        {
            return await _repository.checkEmail(email);
        }

        public async Task<bool> checkPhoneNumber(string phoneNumber)
        {
            return await _repository.checkPhoneNumber(phoneNumber);
        }

        public async Task<bool> checkUsername(string username)
        {
            return await _repository.checkUsername(username);
        }

        public async Task<SignUpDTO> Register(SignUpDTO model)
        {
            return await _repository.Register(model);
        }
    }
}
