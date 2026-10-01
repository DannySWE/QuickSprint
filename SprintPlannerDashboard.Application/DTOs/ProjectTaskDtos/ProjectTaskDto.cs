using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SprintPlannerDashboard.Application.DTOs.ProjectTaskDtos
{
    public record ProjectTaskDto(string Title, bool InProgress, bool Complete, decimal EstimatedHoursForCompletion, int BacklogItemId, int CompletionPercentage):BaseDto
    {
       
    }
}
