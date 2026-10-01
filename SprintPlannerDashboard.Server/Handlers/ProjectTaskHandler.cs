using Microsoft.AspNetCore.Mvc;
using SprintPlannerDashboard.Application.DTOs.ProjectTaskDtos;
using SprintPlannerDashboard.Server.Repositories;
using SprintPlannerDashboard.Application.Mappers;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using PrintPlannerDashboard.Domain.Entites;

namespace SprintPlannerDashboard.Server.Handlers
{
    public static class ProjectTaskHandler
    {
        // Equivalent to GetAllBacklogItemsAsync
        public static async Task<IResult> GetAllProjectTasksAsync([FromServices] ProjectTaskRepository projectTaskRepository)
        {
            try
            {
                // Assuming ProjectTaskMapper.ToDto is available for mapping
                var projectTasks = await projectTaskRepository.GetAllAsync(); // Assuming repository has GetAllAsync or similar
                var projectTaskDtos = projectTasks.Select(pt => ProjectTaskMapper.ToDto(pt)).ToList();
                return TypedResults.Ok(projectTaskDtos);
            }
            catch (Exception ex)
            {
                return TypedResults.Problem("An unexpected database error has occured.", ex.Message);
            }
        }

        // Equivalent to CreateBacklogItemAsync
        public static async Task<IResult> CreateProjectTaskAsync(
            [FromRoute] string backlogItemPublicId, // Assuming we need a related ID, maybe BacklogItemId
            [FromBody] CreateProjectTaskDto createProjectTaskDto,
            [FromServices] ProjectTaskRepository projectTaskRepository,
            [FromServices] BacklogItemRepository backlogItemRepository) // Might need linking context
        {
            var backlogItem = await backlogItemRepository.GetByPublicIdAsync(backlogItemPublicId); // Assuming we get the parent ID via the route parameter

            if (createProjectTaskDto is null)
                return TypedResults.BadRequest("createProjectTaskDto is null");
            if (backlogItem is null)
                return TypedResults.BadRequest("backlogItem is null");

            try
            {
                ProjectTask projectTask = ProjectTaskMapper.ToEntityFromCreateDto(createProjectTaskDto);
                projectTask.BacklogItemId = backlogItem.Id;
            
                await projectTaskRepository.AddAsync(projectTask);
                return TypedResults.Created();
            }
            catch (Exception ex)
            {
                return TypedResults.Problem("An unexpected database error has occured.", ex.Message);
            }

            
        }

        // Equivalent to GetBacklogItemAsync
        public static async Task<IResult> GetProjectTaskAsync([FromRoute] Guid projectTaskId, [FromServices] ProjectTaskRepository projectTaskRepository)
        {
            var projectTask = await projectTaskRepository.GetByIdAsync(projectTaskId);

            if (projectTask is null)
                return TypedResults.BadRequest("projectTask is null");

            try
            {
                var projectTaskDto = ProjectTaskMapper.ToDto(projectTask);
                return TypedResults.Ok(projectTaskDto);
            }
            catch (Exception ex)
            {
                return TypedResults.Problem("An unexpected database error has occured.", ex.Message);
            }

            
        }

        // Equivalent to UpdateBacklogItemAsync
        public static async Task<IResult> UpdateProjectTaskAsync([FromRoute] Guid projectTaskId, [FromBody] UpdateProjectTaskDto updateProjectTaskDto, [FromServices] ProjectTaskRepository projectTaskRepository)
        {
            var projectTask = await projectTaskRepository.GetByIdAsync(projectTaskId);

            if (projectTask is null)
                return TypedResults.BadRequest("projectTask is null");

            try
            {
                projectTask.Title = updateProjectTaskDto.Title;
                projectTask.CompletionPercentage = updateProjectTaskDto.CompletionPercentage;
                projectTask.InProgress = updateProjectTaskDto.InProgress;
                projectTask.EstimatedHoursForCompletion = updateProjectTaskDto.EstimatedHoursForCompletion;

                await projectTaskRepository.UpdateAsync(projectTask);
                return TypedResults.Ok();
            }
            catch (Exception ex)
            {
                return TypedResults.Problem("An unexpected database error has occured.", ex.Message);
            }

            
        }

        // Equivalent to DeleteBacklogItemAsync
        public static async Task<IResult> DeleteProjectTaskAsync([FromRoute] Guid projectTaskId, [FromServices] ProjectTaskRepository projectTaskRepository)
        {
            var projectTask = await projectTaskRepository.GetByIdAsync(projectTaskId);

            if (projectTask is null)
                return TypedResults.BadRequest("projectTask is null");


            projectTaskRepository.Delete(projectTask);
            return TypedResults.NoContent();
        }

        // Equivalent to GetBacklogItemsForSprintAsync
        public static async Task<IResult> GetProjectTasksForBacklogItemIdAsync([FromRoute] string backlogItemIdPublicId, [FromServices] BacklogItemRepository backlogItemRepository)
        {
            var backlogItem = await backlogItemRepository.GetByPublicIdAsync(backlogItemIdPublicId);

            if (backlogItem is null)
                return TypedResults.BadRequest("projectTask is null");

            try
            {
                var projectTasks = backlogItem.Tasks.ToList();
                var projectTaskDtos = projectTasks.Select(pt => ProjectTaskMapper.ToDto(pt)).ToList();
                return TypedResults.Ok(projectTaskDtos);
            }
            catch (Exception ex)
            {
                return TypedResults.Problem("An unexpected database error has occured.", ex.Message);
            }

            
        }
    }
}