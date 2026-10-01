using SprintPlannerDashboard.Application.DTOs.BacklogItemDtos;
using SprintPlannerDashboard.Application.DTOs.SprintDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace SprintPlannerDashboard.Application.DTOs.ProjectBoardDtos
{
    public record UpdateProjectBoardDto(string Name, List<string> Columns, ICollection<SprintDto> SprintDtos):BaseDto
    {
        
    }
}
