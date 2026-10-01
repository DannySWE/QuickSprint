using System;
using System.Collections.Generic;
using System.Text;

namespace SprintPlannerDashboard.Application.DTOs.BacklogItemDtos
{
    public record CreateBacklogItemDto(string Title, string Description, int StoryPoints):BaseDto
    {
    }
}
