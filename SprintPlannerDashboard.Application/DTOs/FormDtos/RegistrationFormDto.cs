using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SprintPlannerDashboard.Application.DTOs.FormDtos
{
    public class RegistrationFormDto
    {
        [Required]
        public string Email { get; set; } = "test@test.com";
        [Required]
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = "User";
    }
}
