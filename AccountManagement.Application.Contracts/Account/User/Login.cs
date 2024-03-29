﻿using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;

namespace AccountManagement.Application.Contracts.Account.User
{
    public class Login
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string UserNameOrEmail { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
