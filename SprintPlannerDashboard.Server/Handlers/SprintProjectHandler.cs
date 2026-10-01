using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using PrintPlannerDashboard.Domain.Entites;
using SprintPlannerDashboard.Application.DTOs.ProjectDtos;
using SprintPlannerDashboard.Application.Mappers;
using SprintPlannerDashboard.Domain.Interfaces;
using SprintPlannerDashboard.Server.Data;
using SprintPlannerDashboard.Server.Repositories;
using System.Security.Claims;

namespace SprintPlannerDashboard.Server.Handlers
{
    public static class SprintProjectHandler
    {
        public static async Task<IResult> GetAllProjectAsync([FromServices] SprintProjectRepository sprintProjectRepository, ClaimsPrincipal claimsPrincipal)
        {
            var userId = claimsPrincipal.Claims.First(c => c.Type == "UserID").Value;

            if(string.IsNullOrEmpty(userId))
                return TypedResults.BadRequest(new {message = "UserID claim is null or empty"});

            try
            {
                var projects = await sprintProjectRepository.GetAllWithNavigationProperties();
                var userProjects = projects.Where(p => p.ProjectOwnerId == userId).ToList();
                var projectsDto = userProjects.Select(p => ProjectMapper.ToDto(p));
                return TypedResults.Ok(projectsDto);
            }
            catch (Exception ex)
            {
                return TypedResults.Problem("An unexpected database error has occured.", ex.Message);
                
            }
        }
        public static async Task<IResult> CreateSprintProjectAsync([FromBody]CreateProjectDto createProjectDto, SprintProjectRepository sprintProjectRepo, [FromServices] ClaimsPrincipal claimsPrincipal)
        {
            var userId = claimsPrincipal.Claims.First(c => c.Type == "UserID").Value;

            if (createProjectDto is null || string.IsNullOrEmpty(userId))
                return TypedResults.BadRequest("reateProjectDto or UserId claim is null or empty.");

            try
            {
                Project sprintProject = ProjectMapper.ToEntity(createProjectDto)!;
                sprintProject.ProjectOwnerId = userId;

                await sprintProjectRepo.AddAsync(sprintProject);
                return TypedResults.Created();
            }
            catch (Exception ex)
            {
                return TypedResults.Problem("An unexpected database error has occured.", ex.Message);
                
            }
             
                

        }

        public static async Task<IResult> GetSprintProjectAsync([FromRoute]string projectPublicId, [FromServices] SprintProjectRepository sprintProjectRepo, ClaimsPrincipal claimsPrincipal)
        {
            var userId = claimsPrincipal.Claims.First(c => c.Type == "UserID").Value;
            var project = await sprintProjectRepo.GetProjectWithProjectBoardsAsync(projectPublicId);

            if (project is null)
               return TypedResults.BadRequest("Project is null");
            if (string.IsNullOrEmpty(userId))
               return TypedResults.BadRequest("UserID claim is empty or  null");
            if (string.IsNullOrEmpty(projectPublicId))
               return TypedResults.BadRequest("projectPublicId is empty or  null");
            if(project.ProjectOwnerId != userId)
                return TypedResults.BadRequest("Selected project does not belong to the user");

            try
            {
                var projectDto = ProjectMapper.ToDto(project);
                return TypedResults.Ok<ProjectDto>(projectDto);
            }
            catch (Exception ex)
            {
                return TypedResults.Problem("An unexpected database error has occured.", ex.Message);
                
            }
                
        }

        public static async Task<IResult> UpdateSprintProjectAsync([FromRoute] string projectPublicId, [FromBody] UpdateProjectDto updateProjectDto, [FromServices] SprintProjectRepository sprintProjectRepo, ClaimsPrincipal claimsPrincipal)
        {
            var userId = claimsPrincipal.Claims.First(c => c.Type == "UserID").Value;
            var project = await sprintProjectRepo.GetByPublicIdAsync(projectPublicId);

            if (project is null)
                return TypedResults.BadRequest("Project is null");
            if (string.IsNullOrEmpty(userId))
                return TypedResults.BadRequest("UserID claim is empty or null");
            if (string.IsNullOrEmpty(projectPublicId))
                return TypedResults.BadRequest("projectPublicId is empty or null");
            if (project.ProjectOwnerId != userId)
                return TypedResults.BadRequest("Selected project does not belong to the user");

            try
            {
                project.Name = updateProjectDto.Name;

                await sprintProjectRepo.UpdateAsync(project);

                var updatedProject = ProjectMapper.ToDto(project);

                return TypedResults.Ok(updatedProject);
            }
            catch (Exception ex)
            {
                return TypedResults.Problem("An unexpected database error has occured.", ex.Message);
                
            }

               
        }

        public static async Task<IResult> DeleteSprintProject([FromRoute] string projectPublicId, SprintProjectRepository sprintProjectRepository, ClaimsPrincipal claimsPrincipal)
        {
            var userId = claimsPrincipal.Claims.First(c => c.Type == "UserID").Value;
            var project = await sprintProjectRepository.GetByPublicIdAsync(projectPublicId);

            if (project is null)
                return TypedResults.BadRequest("Project is null");
            if (string.IsNullOrEmpty(userId))
                return TypedResults.BadRequest("UserID claim is empty or null");
            if (string.IsNullOrEmpty(projectPublicId))
                return TypedResults.BadRequest("projectPublicId is empty or null");
            if (project.ProjectOwnerId != userId)
                return TypedResults.BadRequest("Selected project does not belong to the user");

            sprintProjectRepository.Delete(project);
            return TypedResults.NoContent();
        }

        
    }
}
