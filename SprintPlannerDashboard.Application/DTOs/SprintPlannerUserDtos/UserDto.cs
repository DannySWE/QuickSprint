using System;
using System.Collections.Generic;
using System.Text;

namespace SprintPlannerDashboard.Application.DTOs.SprintPlannerUserDtos
{
    public record UserDto(string Email, string Password) : BaseDto
    {

    }
}
