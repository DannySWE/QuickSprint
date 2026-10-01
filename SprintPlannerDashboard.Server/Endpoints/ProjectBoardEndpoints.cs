using SprintPlannerDashboard.Server.Config;
using SprintPlannerDashboard.Server.Handlers;

namespace SprintPlannerDashboard.Server.Endpoints
{
    public static class ProjectBoardEndpoints
    {
        public static IEndpointRouteBuilder MapProjectBoardEndpoints(this IEndpointRouteBuilder app)
        {
            // Scope A: Project-dependent routes (Sub-resources)
            var projectContextGroup = app.MapGroup(RoutingConfig.BASE_PROJECTBOARD_SUBRESOURCE_ROUTE);

            projectContextGroup.MapPatch($"/{RoutingConfig.PROJECTBOARD_ROUTE_ID}", ProjectBoardHandler.UpdateProjectBoardAsync).WithName("UpdateProjectBoard").RequireAuthorization();

            // POST /api/projects/{projectPublicId}/projectboards
            projectContextGroup.MapPost("", ProjectBoardHandler.CreateProjectBoardAsync).WithName("CreateProjectBoard").RequireAuthorization();

            // Move this from ProjectEndpoints to here to keep Board logic together
            projectContextGroup.MapGet("", ProjectBoardHandler.GetAllProjectBoardsFromProjectAsync).WithName("GetAllProjectBoardsForProject").RequireAuthorization();


            // Scope B: Global or Direct Board interactions (when you already have the Board ID)
            var boardContextGroup = app.MapGroup(RoutingConfig.BASE_PROJECTBOARD_ROUTE);

            boardContextGroup.MapGet("", ProjectBoardHandler.GetAllProjectBoardsAsync).WithName("GetAllProjectBoards").RequireAuthorization();
            boardContextGroup.MapGet($"/{RoutingConfig.PROJECTBOARD_ROUTE_ID}", ProjectBoardHandler.GetProjectBoardAsync).WithName("GetProjectBoard").RequireAuthorization();

            return app;
        }
    }
}
