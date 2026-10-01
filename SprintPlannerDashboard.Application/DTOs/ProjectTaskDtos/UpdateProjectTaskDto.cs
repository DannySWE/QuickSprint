using System;
using System.Collections.Generic;
using System.Text;

namespace SprintPlannerDashboard.Application.DTOs.ProjectTaskDtos
{
    public record UpdateProjectTaskDto(string Title, bool InProgress, bool Complete, decimal EstimatedHoursForCompletion, int CompletionPercentage):BaseDto
    {
    }
}
