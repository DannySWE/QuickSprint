using PrintPlannerDashboard.Domain.Entites;
using SprintPlannerDashboard.Application.DTOs;
using SprintPlannerDashboard.Application.DTOs.BacklogItemDtos;
using SprintPlannerDashboard.Application.DTOs.ProjectBoardDtos;
using System;

namespace SprintPlannerDashboard.Application.Mappers
{
    public static class BacklogItemMapper
    {
        public static BacklogItemDto ToDto(BacklogItem backlogItem)
        {
            if (backlogItem == null)
            {
                throw new ArgumentNullException(nameof(backlogItem));
            }

            return new BacklogItemDto(PublicId: backlogItem.PublicId, Title: backlogItem.Title, Description: backlogItem.Description, StoryPoints: backlogItem.StoryPoints, SprintId: backlogItem.SprintId, Tasks: backlogItem.Tasks.Select(t => ProjectTaskMapper.ToDto(t)).ToList()!);
        }

        public static BacklogItem? ToEntity(BaseDto dto)
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            switch (dto)
            {
                case BacklogItemDto backlogItemDto:
                    return ToEntity(backlogItemDto);

                case CreateBacklogItemDto createBacklogItemDto:
                    return ToEntityFromCreateDto(createBacklogItemDto);

                case UpdateBacklogItemDto updateBacklogItemDto:
                    return ToEntityFromUpdateDto(updateBacklogItemDto);
            }
            return null;
        }
        private static BacklogItem ToEntity(BacklogItemDto backlogItemDto)
        {
            if (backlogItemDto == null)
            {
                throw new ArgumentNullException(nameof(backlogItemDto));
            }

            return new BacklogItem
            {
                PublicId = backlogItemDto.PublicId,
                Title = backlogItemDto.Title,
                Description = backlogItemDto.Description,
                StoryPoints = backlogItemDto.StoryPoints,
                SprintId = backlogItemDto.SprintId
            };
        }
        
        public static BacklogItem ToEntityFromUpdateDto(UpdateBacklogItemDto updateBacklogItemDto)
        {
            if (updateBacklogItemDto == null)
            {
                throw new ArgumentNullException(nameof(updateBacklogItemDto));
            }

            return new BacklogItem()
            {
                Title = updateBacklogItemDto.Title,
                Description = updateBacklogItemDto.Description,
                StoryPoints = updateBacklogItemDto.StoryPoints,
            };
            
        }

        public static BacklogItem ToEntityFromCreateDto(CreateBacklogItemDto createBacklogItemDto)
        {
            if (createBacklogItemDto == null)
            {
                throw new ArgumentNullException(nameof(createBacklogItemDto));
            }

            return new BacklogItem()
            {
                Title = createBacklogItemDto.Title,
                Description = createBacklogItemDto.Description,
                StoryPoints = createBacklogItemDto.StoryPoints,
            };

        }
    }
}