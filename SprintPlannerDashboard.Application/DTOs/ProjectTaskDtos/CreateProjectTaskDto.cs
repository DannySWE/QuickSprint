using System;
using System.Collections.Generic;
using System.Text;

namespace SprintPlannerDashboard.Application.DTOs.ProjectTaskDtos
{
    public record CreateProjectTaskDto(string Title, bool InProgress, bool Complete, decimal EstimatedHoursForCompletion, int BacklogItemId, int CompletionPercentage) : BaseDto
    {
    }
}
