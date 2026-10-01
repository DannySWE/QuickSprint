using Microsoft.IdentityModel.Tokens;
using SprintPlannerDashboard.Server.Config;
using SprintPlannerDashboard.Server.Handlers;

namespace SprintPlannerDashboard.Server.Endpoints
{
    public static class ProjectEndpoints
    {
        public static IEndpointRouteBuilder MapSprintProjectEndpoints(this IEndpointRouteBuilder app)
        {
            // Base group: /api/projects
            var group = app.MapGroup(RoutingConfig.BASE_PROJECT_ROUTE);

            group.MapGet("", SprintProjectHandler.GetAllProjectAsync).WithName("GetAllProjects").RequireAuthorization();
            group.MapPost("", SprintProjectHandler.CreateSprintProjectAsync).WithName("CreateProject").RequireAuthorization();
            group.MapGet($"/{RoutingConfig.PROJECT_ROUTE_ID}", SprintProjectHandler.GetSprintProjectAsync).WithName("GetProject").RequireAuthorization();
            group.MapPatch($"/{RoutingConfig.PROJECT_ROUTE_ID}", SprintProjectHandler.UpdateSprintProjectAsync).WithName("UpdateProject").RequireAuthorization();
            group.MapDelete($"/{RoutingConfig.PROJECT_ROUTE_ID}", SprintProjectHandler.DeleteSprintProject).WithName("DeleteProject").RequireAuthorization();

            return app;
        }
        
    }
}
