﻿using System.ComponentModel.DataAnnotations;

namespace Kupa.Api.Dtos
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
