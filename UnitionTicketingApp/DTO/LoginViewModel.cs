﻿using System.ComponentModel.DataAnnotations;

namespace UnitionTicketingApp.DTO
{
    public class LoginViewModel
    {
        [Required]
        public string Email {  get; set; }
        [Required]
        public string Password {  get; set; }
    }
}
