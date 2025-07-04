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

        public async Task<SignUpDTO> Register(SignUpDTO model)
        {
            return await _repository.Register(model);
        }
    }
}
