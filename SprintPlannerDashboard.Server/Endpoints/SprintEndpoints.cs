using SprintPlannerDashboard.Server.Config;
using SprintPlannerDashboard.Server.Handlers;

namespace SprintPlannerDashboard.Server.Endpoints
{
    public static class SprintEndPoints
    {
        public static IEndpointRouteBuilder MapSprintEndpoints(this IEndpointRouteBuilder app)
        {
            var baseSprintSubRoute = app.MapGroup(RoutingConfig.BASE_SPRINT_SUBRESOURCE_ROUTE);

            baseSprintSubRoute.MapPost("", SprintHandler.CreateSprintAsync).WithName("CreateSprint").RequireAuthorization();


            var baseSprintRoute = app.MapGroup(RoutingConfig.BASE_SPRINT_ROUTE);

            // GET all sprints from 
            baseSprintRoute.MapGet("", SprintHandler.GetAllSprintsAsync).WithName("GetAllSprints").RequireAuthorization();

            // GET single sprint
            baseSprintRoute.MapGet($"/{RoutingConfig.SPRINT_ROUTE_ID}", SprintHandler.GetSprintAsync).WithName("GetSprint").RequireAuthorization();

            // PUT/PATCH update sprint
            baseSprintRoute.MapPatch($"/{RoutingConfig.SPRINT_ROUTE_ID}", SprintHandler.UpdateAsync).WithName("UpdateSprint").RequireAuthorization();

            // DELETE sprint
            baseSprintRoute.MapDelete($"/{RoutingConfig.SPRINT_ROUTE_ID}", SprintHandler.DeleteAsync).WithName("DeleteSprint").RequireAuthorization();

            // GET backlog items for a sprint
            //REMOVE THIS
            baseSprintRoute.MapGet($"{RoutingConfig.SPRINT_ROUTE_ID}/backlog", SprintHandler.GetBacklogItemsFromSprintAsync).WithName("GetBacklogItemsFromASprint").RequireAuthorization();

            return app;
        }
    }
}
