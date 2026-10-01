using SprintPlannerDashboard.Application.DTOs.BacklogItemDtos;
using SprintPlannerDashboard.Application.DTOs.SprintDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace SprintPlannerDashboard.Application.DTOs.ProjectBoardDtos
{
    public record ProjectBoardDto(string PublicId, string Name, int projectId, List<string> Columns, ICollection<SprintDto> SprintDtos,
        ICollection<BacklogItemDto> BacklogItemDtos):BaseDto;
     
}
