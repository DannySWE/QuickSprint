using Microsoft.AspNetCore.Mvc;
using PrintPlannerDashboard.Domain.Entites;
using SprintPlannerDashboard.Application.DTOs.ProjectBoardDtos;
using SprintPlannerDashboard.Application.Mappers;
using SprintPlannerDashboard.Server.Repositories;

namespace SprintPlannerDashboard.Server.Handlers
{
    public static class ProjectBoardHandler
    {
        public static async Task<IResult> GetAllProjectBoardsAsync(
            [FromServices] SprintProjectBoardRepository sprintProjectBoardRepo)
        {
            try
            {
                var projectBoards = await sprintProjectBoardRepo.GetAllWithNavigationProperties();
                var projectBoardsDto = projectBoards.Select(pb => ProjectBoardMapper.ToDto(pb));
                return TypedResults.Ok(projectBoardsDto);
            }
            catch (Exception ex)
            {
                return TypedResults.Problem("An unexpected database error has occured.", ex.Message);
            }
        }

        public static async Task<IResult> GetProjectBoardAsync([FromRoute] string projectBoardPublicId ,[FromServices] SprintProjectBoardRepository sprintProjectBoardRepo)
        {
            if (string.IsNullOrEmpty(projectBoardPublicId))
                return TypedResults.BadRequest("projectBoardPublicId is empty or null");

            try
            {
                //make sure to return the navigation collections
                var projectBoard = await sprintProjectBoardRepo.GetByPublicIdAsync(projectBoardPublicId);
                var projectBoardDto = ProjectBoardMapper.ToDto(projectBoard);
                return TypedResults.Ok(projectBoardDto);
            }
            catch (Exception ex)
            {
                return TypedResults.Problem("Unexpected server error.", ex.Message);
            }
        }
        public static async Task<IResult> CreateProjectBoardAsync([FromRoute] string projectPublicId, [FromBody] CreateProjectBoardDto createProjectBoardDto, [FromServices] SprintProjectBoardRepository sprintProjectBoardRepo, [FromServices] SprintProjectRepository sprintProjectRepository)
        {
            var selectedProject = await sprintProjectRepository.GetByPublicIdAsync(projectPublicId);

            if (string.IsNullOrEmpty(projectPublicId))
                return TypedResults.BadRequest("projectPublicId is empty or null");
            if (createProjectBoardDto is null)
                return TypedResults.BadRequest("createProjectBoardDto is null");
            if (selectedProject is null)
                return TypedResults.BadRequest("selectedProject is null");

            try
            {
                ProjectBoard sprintProjectBoard = new ProjectBoard()
                {
                    Name = createProjectBoardDto.Name,
                    Columns = createProjectBoardDto.Columns,
                    Project = selectedProject,
                    Sprints = new List<Sprint>(),
                    ProductBacklog = new List<BacklogItem>(),
                };


                await sprintProjectBoardRepo.AddAsync(sprintProjectBoard);
                return TypedResults.Created();
            }
            catch (Exception ex)
            {
                return TypedResults.Problem("Unexpected server error.", ex.Message);
            }

                

        }

        public static async Task<IResult> UpdateProjectBoardAsync([FromRoute] string projectPublicId, 
            [FromRoute]string projectBoardPublicId,
            [FromBody] UpdateProjectBoardDto updateProjectBoardDto, 
            [FromServices] SprintProjectBoardRepository sprintProjectBoardRepo, 
            [FromServices] SprintProjectRepository sprintProjectRepository)
        {
            var selectedProject = await sprintProjectRepository.GetByPublicIdAsync(projectPublicId);
            var selectedProjectBoard = await sprintProjectBoardRepo.GetByPublicIdAsync(projectBoardPublicId);

            if (string.IsNullOrEmpty(projectPublicId))
                return TypedResults.BadRequest("projectPublicId is empty or null");
            if (updateProjectBoardDto is null)
                return TypedResults.BadRequest("projectBoardDto is null");
            if (selectedProjectBoard is null)
                return TypedResults.BadRequest("selectedProjectBoard is null");
            if (selectedProject is null)
                return TypedResults.BadRequest("selectedProject is null");

            try
            {
                selectedProjectBoard.Name = updateProjectBoardDto.Name;
                selectedProjectBoard.Columns = updateProjectBoardDto.Columns;
                selectedProjectBoard.Sprints = updateProjectBoardDto.SprintDtos?.Select(s => SprintMapper.ToEntity(s)).ToList()!;

                await sprintProjectBoardRepo.UpdateAsync(selectedProjectBoard);
                return TypedResults.Created();

            }
            catch (Exception ex)
            {
                return TypedResults.Problem("Unexpected server error.", ex.Message);
            }

            


            

        }

        public static async Task<IResult> GetAllProjectBoardsFromProjectAsync(
            [FromRoute] string projectPublicId, 
            [FromServices] SprintProjectRepository sprintProjectRepo)
        {
            var project = await sprintProjectRepo.GetProjectWithProjectBoardsAsync(projectPublicId);

            if (project is null) 
                return TypedResults.BadRequest("Project is null");

            try
            {
                var projecBoardstDto = ProjectMapper.ToDto(project)?.Boards;
                return TypedResults.Ok(projecBoardstDto);
            }
            catch (Exception ex)
            {
                return TypedResults.Problem("Unexpected server error.", ex.Message);
            }

          
        }
    }
}
