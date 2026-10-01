using PrintPlannerDashboard.Domain.Entites;
using SprintPlannerDashboard.Application.DTOs;
using SprintPlannerDashboard.Application.DTOs.BacklogItemDtos;
using SprintPlannerDashboard.Application.DTOs.ProjectDtos;
using SprintPlannerDashboard.Application.DTOs.SprintDtos;
using System.Collections.Generic;
using System.Linq;

namespace SprintPlannerDashboard.Application.Mappers
{
    public static class SprintMapper
    {
        /// <summary>
        /// Maps a Sprint entity to SprintDto.
        /// </summary>
        /// <param name=\"sprint\">The source Sprint entity.</param>
        /// <returns>The corresponding SprintDto.</returns>
        public static SprintDto ToDto(Sprint sprint)
        {
            // Mapping based on the available properties in PostSprintDto.
            var dto = new SprintDto(Name: sprint.Name, Goal: sprint.Goal, BacklogItems: sprint.BacklogItems.Select(p => BacklogItemMapper.ToDto(p)).ToList(), Status: sprint.Status, PublicId: sprint.PublicId, StartDate: sprint.StartDate, EndDate: sprint.EndDate, CapacityStoryPoints: sprint.CapacityStoryPoints, CommittedStoryPoints: sprint.CommittedStoryPoints, CompletedStoryPoints: sprint.CompletedStoryPoints, CreatedAt: sprint.CreatedAt, UpdatedAt: sprint.UpdatedAt, ProjectBoardId: sprint.ProjectBoardId);

            return dto;
        }

        public static Sprint ToEntity(BaseDto dto)
        {
            switch (dto)
            {
                case SprintDto sprintDto:
                    return ToEntity(sprintDto);

                case CreateSprintDto createSprintDto:
                    return ToEntityFromCreateDto(createSprintDto);

                case UpdateSprintDto updateSprinttDto:
                    return ToEntityFromUpdateDto(updateSprinttDto);
            }

            return null;
        }

        private static Sprint ToEntity(SprintDto sprintDto)
        {

            // Mapping based on the available properties in PostSprintDto.
            var entity = new Sprint
            {
                Name = sprintDto.Name,
                Goal = sprintDto.Goal,
                BacklogItems = sprintDto.BacklogItems.Select(b => BacklogItemMapper.ToEntity(b)).ToList()!,
            };

            return entity;
        }

        private static Sprint ToEntityFromUpdateDto(UpdateSprintDto updateSprintDto)
        {
            // Mapping based on the available properties in PostSprintDto.
            var entity = new Sprint
            {
                Name = updateSprintDto.Name,
                Goal = updateSprintDto.Goal,
                BacklogItems = updateSprintDto.BacklogItems.Select(b => BacklogItemMapper.ToEntity(b)).ToList()!,
            };

            return entity;
        }

        private static Sprint ToEntityFromCreateDto(CreateSprintDto createSprintDto)
        {
            // Mapping based on the available properties in PostSprintDto.
            var entity = new Sprint
            {
                Name = createSprintDto.Name,
                Goal = createSprintDto.Goal,
                BacklogItems = new List<BacklogItem>()
            };

            return entity;
        }

        // Utility to map a list of Sprints
        public static IEnumerable<SprintDto> ToDtoList(IEnumerable<Sprint> sprints)
        {
            return sprints.Select(ToDto) ?? Enumerable.Empty<SprintDto>();
        }
    }
}