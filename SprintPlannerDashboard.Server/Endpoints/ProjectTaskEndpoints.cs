using SprintPlannerDashboard.Server.Config;
using SprintPlannerDashboard.Server.Handlers;

namespace SprintPlannerDashboard.Server.Endpoints
{
    public static class ProjectTaskEndpoints
    {
        public static IEndpointRouteBuilder MapProjectTaskEndpoints(this IEndpointRouteBuilder app)
        {
            // 1. Sub-route for context-dependent operations (e.g., tasks belonging to a specific backlog item)
            var baseProjectSubRoute = app.MapGroup(RoutingConfig.BASE_TASK_SUBRESOURCE_ROUTE);

            // POST create project task (Mirroring CreateBacklogItemAsync)
            // Note: The handler uses BACKLOG_ITEM_ROUTE_ID in the path mapping for context.
            baseProjectSubRoute.MapPost("",
                ProjectTaskHandler.CreateProjectTaskAsync).WithName("CreateProjectTask").RequireAuthorization();

            // 2. Main route for collection operations (e.g., listing all tasks)
            var baseProjectRoute = app.MapGroup(RoutingConfig.BASE_TASK_ROUTE);

            // GET all project tasks (Mirroring GetAllBacklogItemsAsync)
            baseProjectRoute.MapGet("", ProjectTaskHandler.GetAllProjectTasksAsync).WithName("GetAllProjectTasks").RequireAuthorization();

            // GET single project task (Mirroring GetBacklogItemAsync)
            baseProjectRoute.MapGet($"/{RoutingConfig.TASK_ROUTE_ID}", ProjectTaskHandler.GetProjectTaskAsync).WithName("GetProjectTask").RequireAuthorization();


            // PUT/PATCH update project task (Mirroring UpdateBacklogItemAsync)
            baseProjectRoute.MapPatch($"/{RoutingConfig.TASK_ROUTE_ID}",
                ProjectTaskHandler.UpdateProjectTaskAsync).WithName("UpdateProjectTask").RequireAuthorization();

            // DELETE project task (Mirroring DeleteBacklogItemAsync)
            baseProjectRoute.MapDelete($"/{RoutingConfig.TASK_ROUTE_ID}",
                ProjectTaskHandler.DeleteProjectTaskAsync).WithName("DeleteProjectTask").RequireAuthorization();


            return app;
        }
    }
}
