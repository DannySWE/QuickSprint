using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SprintPlannerDashboard.Application.DTOs.FormDtos
{
    public class LoginFormDto
    {
        [PersonalData]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [PasswordPropertyText]
        public string Password { get; set; } = string.Empty;
    }
}
