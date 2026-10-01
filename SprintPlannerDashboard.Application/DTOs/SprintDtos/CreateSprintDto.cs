using SprintPlannerDashboard.Application.DTOs.BacklogItemDtos;
using SprintPlannerDashboard.Application.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SprintPlannerDashboard.Application.DTOs.SprintDtos
{
    public record CreateSprintDto(string Name, string Goal, int ProjectBoardId, ICollection<BacklogItemDto> BacklogItems, SprintStatus Status, string PublicId, DateTime StartDate, DateTime EndDate, int CapacityStoryPoints, int CommittedStoryPoints, int CompletedStoryPoints, DateTime CreatedAt) : BaseDto
    {
    }
}
