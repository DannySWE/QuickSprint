using SprintPlannerDashboard.Application.DTOs.ProjectTaskDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SprintPlannerDashboard.Application.DTOs.BacklogItemDtos
{
    public record UpdateBacklogItemDto(string Title, string Description, int StoryPoints):BaseDto
    {
        
    }
}
