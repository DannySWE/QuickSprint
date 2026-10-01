using PrintPlannerDashboard.Domain.Entites;
using SprintPlannerDashboard.Application.DTOs;
using SprintPlannerDashboard.Application.DTOs.ProjectBoardDtos;
using SprintPlannerDashboard.Application.DTOs.ProjectDtos;
using System.Collections.Generic;
using System.Linq;

namespace SprintPlannerDashboard.Application.Mappers
{
    public static class ProjectMapper
    {
        /// <summary>
        /// Maps a Project entity to a ProjectDto.
        /// </summary>
        /// <param name="project">The source Project entity.</param>
        /// <returns>The corresponding ProjectDto.</returns>
        public static ProjectDto ToDto(Project project)
        {

            // Mapping the required fields for ProjectDto: Name and Boards
            // We are assuming the ProjectDto record constructor signature is (string Name, ICollection<ProjectBoard> Boards)

            return new ProjectDto(
                        Name: project.Name,
                        Boards: project.Boards.Select(b => ProjectBoardMapper.ToDto(b)).ToList()!
                    );
        }

        public static Project ToEntity(BaseDto dto)
        {
            switch (dto)
            {
                case ProjectDto projectDto:
                    return ToEntity(projectDto);

                case CreateProjectDto createProjectDto:
                    return ToEntityFromCreateDto(createProjectDto);

                case UpdateProjectDto updateProjectDto:
                    return ToEntityFromUpdateDto(updateProjectDto);
            }
            return null;

        }

        private static Project ToEntity(ProjectDto projectDto)
        {
            // Mapping the required fields for ProjectDto: Name and Boards
            // We are assuming the ProjectDto record constructor signature is (string Name, ICollection<ProjectBoard> Boards)
            return new Project()
            {
                Name = projectDto.Name,
                Boards = projectDto.Boards?.Select(b => ProjectBoardMapper.ToEntity(b)).ToList()!,
            };
        }

        private static Project ToEntityFromCreateDto(CreateProjectDto createProjectDto)
        {
            // Mapping the required fields for ProjectDto: Name and Boards
            // We are assuming the ProjectDto record constructor signature is (string Name, ICollection<ProjectBoard> Boards)
            return new Project()
            {
                Name = createProjectDto.Name,
                Boards = new List<ProjectBoard>(),
            };
        }

        private static Project ToEntityFromUpdateDto(UpdateProjectDto updateProjectDto)
        {
            // Mapping the required fields for ProjectDto: Name and Boards
            // We are assuming the ProjectDto record constructor signature is (string Name, ICollection<ProjectBoard> Boards)
            return new Project()
            {
                Name = updateProjectDto.Name,
            };
        }
    }
}