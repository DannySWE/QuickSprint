using SprintPlannerDashboard.Application.DTOs.BacklogItemDtos;
using SprintPlannerDashboard.Application.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SprintPlannerDashboard.Application.DTOs.SprintDtos
{
    public record UpdateSprintDto(string Name, string Goal, ICollection<BacklogItemDto> BacklogItems, SprintStatus Status, DateTime StartDate, DateTime EndDate, int CapacityStoryPoints, int CommittedStoryPoints, int CompletedStoryPoints, DateTime? UpdatedAt) :BaseDto
    {
    }
}
