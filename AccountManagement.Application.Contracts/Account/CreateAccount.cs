﻿using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Http;

namespace AccountManagement.Application.Contracts.Account
{
    public class CreateAccount
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Username { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Password { get; set; }
        
        public string Fullname { get; set; }
        
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Mobile { get; set; }
        
        [Range(1, int.MaxValue , ErrorMessage = ValidationMessages.IsRequired)]
        public int RoleId { get; set; }
        
        public IFormFile ProfilePhoto { get; set; }

        public List<RoleViewModel> Roles { get; set; }
    }
}
