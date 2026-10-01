using PrintPlannerDashboard.Domain.Entites;
using SprintPlannerDashboard.Application.DTOs;
using SprintPlannerDashboard.Application.DTOs.ProjectTaskDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace SprintPlannerDashboard.Application.Mappers
{
    public static class ProjectTaskMapper
    {
        public static ProjectTaskDto ToDto(ProjectTask projectTask)
        {

            return new ProjectTaskDto(Title: projectTask.Title, InProgress: projectTask.InProgress, Complete: projectTask.Complete, EstimatedHoursForCompletion: projectTask.EstimatedHoursForCompletion, BacklogItemId: projectTask.BacklogItemId, CompletionPercentage: projectTask.CompletionPercentage);
        }

        public static ProjectTask ToEntity(BaseDto dto)
        {
            switch (dto)
            {
                case ProjectTaskDto projectTaskDto:
                    return ToEntity(projectTaskDto);

                case CreateProjectTaskDto createProjectTaskDto:
                    return ToEntityFromCreateDto(createProjectTaskDto);

                case UpdateProjectTaskDto updateProjectTaskDto:
                    return ToEntityFromUpdateDto(updateProjectTaskDto);

                default:
                    return null;
            }
        }

        private static ProjectTask ToEntity(ProjectTaskDto projectTaskDto)
        {
            return new ProjectTask
            {
                Title = projectTaskDto.Title,
                InProgress = projectTaskDto.InProgress,
                Complete = projectTaskDto.Complete,
                EstimatedHoursForCompletion = projectTaskDto.EstimatedHoursForCompletion,
                BacklogItemId = projectTaskDto.BacklogItemId,
                CompletionPercentage = projectTaskDto.CompletionPercentage
            };
        }

        public static ProjectTask ToEntityFromCreateDto(CreateProjectTaskDto createProjectTaskDto)
        {
            return new ProjectTask
            {
                Title = createProjectTaskDto.Title,
                InProgress = createProjectTaskDto.InProgress,
                Complete = createProjectTaskDto.Complete,
                EstimatedHoursForCompletion = createProjectTaskDto.EstimatedHoursForCompletion,
                BacklogItemId = createProjectTaskDto.BacklogItemId,
                CompletionPercentage = createProjectTaskDto.CompletionPercentage
            };
        }

        public static ProjectTask ToEntityFromUpdateDto(UpdateProjectTaskDto updateProjectTaskDto)
        {
            return new ProjectTask
            {
                Title = updateProjectTaskDto.Title,
                InProgress = updateProjectTaskDto.InProgress,
                Complete = updateProjectTaskDto.Complete,
                EstimatedHoursForCompletion = updateProjectTaskDto.EstimatedHoursForCompletion,
                CompletionPercentage = updateProjectTaskDto.CompletionPercentage
            };
        }
    }
}
