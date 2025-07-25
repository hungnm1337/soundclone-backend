﻿using Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.SignUp
{
    public interface ISignUpService
    {
        public Task<SignUpDTO> Register(SignUpDTO model);

        public Task<bool> checkEmail(string email);

        public Task<bool> checkPhoneNumber(string phoneNumber);

        public Task<bool> checkUsername(string username);
    }
}
