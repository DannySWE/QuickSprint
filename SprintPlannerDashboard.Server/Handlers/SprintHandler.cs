using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using PrintPlannerDashboard.Domain.Entites;
using SprintPlannerDashboard.Application.DTOs.ProjectDtos;
using SprintPlannerDashboard.Application.Mappers;
using SprintPlannerDashboard.Domain.Interfaces;
using SprintPlannerDashboard.Server.Data;
using SprintPlannerDashboard.Server.Repositories;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using SprintPlannerDashboard.Application.DTOs.SprintDtos;

namespace SprintPlannerDashboard.Server.Handlers
{
    public static class SprintHandler
    {
        // Equivalent to GetAllProjectAsync in SprintProjectHandler.cs
        public static async Task<IResult> GetAllSprintsAsync([FromServices] SprintRepository sprintRepository)
        {
            try
            {
                return TypedResults.Ok(await sprintRepository.GetAllAsync());
            }
            catch (Exception ex)
            {
                return TypedResults.Problem("An unexpected database error has occured.", ex.Message);
            }
        }

        // Equivalent to CreateSprintProjectAsync in SprintProjectHandler.cs
        public static async Task<IResult> CreateSprintAsync(
            [FromRoute] string projectBoardPublicId,
            [FromBody] CreateSprintDto createSprintDto, 
            [FromServices] SprintRepository sprintRepository, 
            SprintProjectBoardRepository sprintProjectBoardRepository)
        {
            var selectedProjectBoard = await sprintProjectBoardRepository.GetByPublicIdAsync(projectBoardPublicId);

            if (createSprintDto is null)
                return TypedResults.BadRequest("createSprintDto is null");
            if (selectedProjectBoard is null)
                return TypedResults.BadRequest("selectedProjectBoard is null");

            try
            {
                Sprint sprint = SprintMapper.ToEntity(createSprintDto);
                sprint.ProjectBoardId = selectedProjectBoard.Id;

                await sprintRepository.AddAsync(sprint);
                return TypedResults.Created();
            }
            catch (Exception ex)
            {
                return TypedResults.Problem("An unexpected database error has occured.", ex.Message);
            }

            
        }

        // Equivalent to GetSprintProjectAsync in SprintProjectHandler.cs
        public static async Task<IResult> GetSprintAsync([FromRoute] string sprintPublicId, [FromServices] SprintRepository sprintRepository)
        {
            var sprint = await sprintRepository.GetByPublicIdAsync(sprintPublicId);

            if (sprint is null)
                return TypedResults.BadRequest("sprint is null");

            try
            {
                var sprintDto = SprintMapper.ToDto(sprint);
                return TypedResults.Ok(sprintDto);
            }
            catch (Exception ex)
            {
                return TypedResults.Problem("An unexpected database error has occured.", ex.Message);
            }
            
        }

        // Equivalent to UpdateSprintProjectAsync in SprintProjectHandler.cs
        public static async Task<IResult> UpdateAsync(
            [FromRoute] string sprintPublicId, 
            [FromBody] UpdateSprintDto updateSprintDto, 
            [FromServices] SprintRepository sprintRepository)
        {
            var sprint = await sprintRepository.GetByPublicIdAsync(sprintPublicId);

            if (sprint is null)
                return TypedResults.BadRequest("sprint is null");

            try
            {
                sprint.Name = updateSprintDto.Name;
                sprint.Goal = updateSprintDto.Goal;
                sprint.Status = updateSprintDto.Status;
                sprint.CapacityStoryPoints = updateSprintDto.CapacityStoryPoints;
                sprint.CommittedStoryPoints = updateSprintDto.CommittedStoryPoints;
                sprint.CompletedStoryPoints = updateSprintDto.CompletedStoryPoints;
                sprint.StartDate = updateSprintDto.StartDate;
                sprint.EndDate = updateSprintDto.EndDate;

                await sprintRepository.UpdateAsync(sprint);
                return TypedResults.Ok<Sprint>(sprint);
            }
            catch (Exception ex)
            {
                return TypedResults.Problem("An unexpected database error has occured.", ex.Message);
            }
           
        }

        public static async Task<IResult> DeleteAsync([FromRoute] string sprintPublicId, SprintRepository sprintRepository)
        {
            var sprint = await sprintRepository.GetByPublicIdAsync(sprintPublicId);

            if (sprint is null)
                return TypedResults.BadRequest("sprint is null");


            sprintRepository.Delete(sprint);
            return TypedResults.NoContent();
        }

        public static async Task<IResult> GetBacklogItemsFromSprintAsync([FromRoute] string sprintPublicId, [FromServices] SprintRepository sprintRepository)
        {
            var sprint = await sprintRepository.GetByPublicIdAsync(sprintPublicId);

            if (sprint is  null)
                return TypedResults.BadRequest("sprint is null");

            try
            {
                var backlogItemsDto = sprint.BacklogItems.Select(bi => BacklogItemMapper.ToDto(bi)).ToList();
                return TypedResults.Ok(backlogItemsDto);
            }
            catch (Exception ex)
            {
                return TypedResults.Problem("An unexpected database error has occured.", ex.Message);
            }
            
        }
    }
}
