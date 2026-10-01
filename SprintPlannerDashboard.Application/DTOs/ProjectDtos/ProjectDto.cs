using SprintPlannerDashboard.Application.DTOs.ProjectBoardDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace SprintPlannerDashboard.Application.DTOs.ProjectDtos
{
    public record ProjectDto(string Name, ICollection<ProjectBoardDto> Boards): BaseDto;
   
}
