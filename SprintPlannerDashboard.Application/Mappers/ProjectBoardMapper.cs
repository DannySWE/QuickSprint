using PrintPlannerDashboard.Domain.Entites;
using System.Collections.Generic;
using System.Linq;
using System;
using SprintPlannerDashboard.Application.DTOs.ProjectBoardDtos;
using SprintPlannerDashboard.Application.DTOs;

namespace SprintPlannerDashboard.Application.Mappers
{
    public static class ProjectBoardMapper
    {
        /// <summary>
        /// Maps a ProjectBoard entity to ProjectBoardDto.
        /// </summary>
        /// <param name=\"projectBoard\">The source ProjectBoard entity.</param>
        /// <returns>The corresponding ProjectBoardDto.</returns>
        public static ProjectBoardDto? ToDto(ProjectBoard projectBoard)
        {
            if (projectBoard == null)
            {
                return null;
            }

            // Create the DTO instance with the direct mappings
            var dto = new ProjectBoardDto(Name:  projectBoard.Name, Columns: projectBoard.Columns, PublicId: projectBoard.PublicId, projectId: projectBoard.Id, SprintDtos: projectBoard.Sprints.Select(s => SprintMapper.ToDto(s)).ToList(), BacklogItemDtos: projectBoard.ProductBacklog.Select(b => BacklogItemMapper.ToDto(b)).ToList());

            return dto;
        }
        public static ProjectBoard? ToEntity(BaseDto dto)
        {
            if (dto == null)
            {
                return null;
            }

            switch (dto)
            {
                case ProjectBoardDto projectBoardDto:
                    return ToEntity(projectBoardDto);

                case CreateProjectBoardDto createProjectBoardDto:
                    return ToEntityFromCreateDto(createProjectBoardDto);

                case UpdateProjectBoardDto updateProjectBoardDto:
                    return ToEntityFromUpdateDto(updateProjectBoardDto);
            }
            return null;

        }

        private static ProjectBoard? ToEntity(ProjectBoardDto projectBoardDto)
        {
            if (projectBoardDto == null)
            {
                return null;
            }

            // Create the DTO instance with the direct mappings
            var entity = new ProjectBoard
            {
                PublicId = projectBoardDto.PublicId,
                ProjectId = projectBoardDto.projectId,
                Name = projectBoardDto.Name,
                Columns = projectBoardDto.Columns,
                Sprints = projectBoardDto.SprintDtos.Select(s => SprintMapper.ToEntity(s)).ToList(),
                ProductBacklog = projectBoardDto.BacklogItemDtos.Select(b => BacklogItemMapper.ToEntity(b)).ToList()!,
            };

            return entity;
        }

        public static ProjectBoard? ToEntityFromCreateDto(CreateProjectBoardDto createProjectBoardDto)
        {
            if (createProjectBoardDto == null)
            {
                return null;
            }

            // Create the DTO instance with the direct mappings
            var entity = new ProjectBoard 
            {
                Name = createProjectBoardDto.Name,
                Columns = createProjectBoardDto.Columns,
                Sprints = new List<Sprint>(),
                ProductBacklog = new List<BacklogItem>()
            };

            return entity;
        }

        public static ProjectBoard? ToEntityFromUpdateDto(UpdateProjectBoardDto updateProjectBoardDto)
        {
            if (updateProjectBoardDto == null)
            {
                return null;
            }

            // Create the DTO instance with the direct mappings
            var entity = new ProjectBoard
            {
                Name = updateProjectBoardDto.Name,
                Columns = updateProjectBoardDto.Columns,
                Sprints = updateProjectBoardDto.SprintDtos.Select(s => SprintMapper.ToEntity(s)).ToList(),
            };

            return entity;
        }
    }
}
