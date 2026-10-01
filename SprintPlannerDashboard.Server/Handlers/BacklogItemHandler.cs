using Microsoft.AspNetCore.Mvc;
using SprintPlannerDashboard.Application.DTOs.BacklogItemDtos;
using SprintPlannerDashboard.Server.Repositories;
using SprintPlannerDashboard.Application.Mappers;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using PrintPlannerDashboard.Domain.Entites;

namespace SprintPlannerDashboard.Server.Handlers
{
    public static class BacklogItemHandler
    {
        public static async Task<IResult> GetAllBacklogItemsAsync([FromServices] BacklogItemRepository backlogItemRepository)
        {
            try
            {
                // Assuming BacklogItemMapper.ToDto is available for mapping
                var backlogItems = await backlogItemRepository.GetAllAsync();
                var backlogItemDtos = backlogItems.Select(bi => BacklogItemMapper.ToDto(bi)).ToList();
                return TypedResults.Ok(backlogItemDtos);
            }
            catch (Exception ex)
            {
                return TypedResults.Problem("An unexpected database error has occured.", ex.Message);
            }
        }

        public static async Task<IResult> CreateBacklogItemAsync(
            [FromRoute] string sprintPublicId, 
            [FromBody] CreateBacklogItemDto createBacklogItemDto, 
            [FromServices] BacklogItemRepository backlogItemRepository,
            [FromServices] SprintRepository sprintRepository)
        {
            var selectedSprint = await sprintRepository.GetByPublicIdAsync(sprintPublicId);

            if (createBacklogItemDto is null)
                return TypedResults.BadRequest("createBacklogItemDto is null");
            if (string.IsNullOrEmpty(sprintPublicId))
                return TypedResults.BadRequest("sprintPublicId is empty or null");

            try
            {
                BacklogItem backlogItem = BacklogItemMapper.ToEntity(createBacklogItemDto);
                backlogItem.SprintId = selectedSprint.Id;
            
                await backlogItemRepository.AddAsync(backlogItem);
                return TypedResults.Created();
            }
            catch (Exception ex)
            {
                return TypedResults.Problem("An unexpected database error has occured.", ex.Message);
            }

            
        }

        public static async Task<IResult> GetBacklogItemAsync([FromRoute] string backlogItemPublicId, [FromServices] BacklogItemRepository backlogItemRepository)
        {
            var backlogItem = await backlogItemRepository.GetByPublicIdAsync(backlogItemPublicId);

            if (backlogItem is null)
                return TypedResults.BadRequest("backlogitem is null");

            try
            {
                var backlogItemDto = BacklogItemMapper.ToDto(backlogItem);
                return TypedResults.Ok(backlogItemDto);
            }
            catch (Exception ex)
            {
                return TypedResults.Problem("An unexpected database error has occured.", ex.Message);
            }
           
        }

        public static async Task<IResult> UpdateBacklogItemAsync([FromRoute] string backlogItemPublicId, [FromBody] UpdateBacklogItemDto updateBacklogItemDto, [FromServices] BacklogItemRepository backlogItemRepository)
        {
            var backlogItem = await backlogItemRepository.GetByPublicIdAsync(backlogItemPublicId);

            if (backlogItem is null)
                return TypedResults.BadRequest("backlogitem is null");

            try
            {
                // Mapping properties from DTO to Entity
                backlogItem.Title = updateBacklogItemDto.Title;
                backlogItem.Description = updateBacklogItemDto.Description;
                backlogItem.StoryPoints = updateBacklogItemDto.StoryPoints;
                // Add mapping for any other fields updated via DTO...

                await backlogItemRepository.UpdateAsync(backlogItem);
                return TypedResults.Ok();
            }
            catch (Exception ex)
            {
                return TypedResults.Problem("An unexpected database error has occured.", ex.Message);
            }

            
        }

        // Equivalent to DeleteSprintProject in SprintHandler.cs
        public static async Task<IResult> DeleteBacklogItemAsync([FromRoute] string backlogItemPublicId, [FromServices]BacklogItemRepository backlogItemRepository)
        {
            var backlogItem = await backlogItemRepository.GetByPublicIdAsync(backlogItemPublicId);

            if (backlogItem is null)
                return TypedResults.BadRequest("backlogitem is null");

            backlogItemRepository.Delete(backlogItem);
            return TypedResults.NoContent();

        }

        // Equivalent to GetBacklogItemsFromSprintAsync in SprintHandler.cs
        public static async Task<IResult> GetBacklogItemsForSprintAsync([FromRoute] string sprintPublicId, [FromServices] SprintRepository sprintRepository)
        {
            var sprint = await sprintRepository.GetByPublicIdAsync(sprintPublicId);

            if (sprint is null)
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
