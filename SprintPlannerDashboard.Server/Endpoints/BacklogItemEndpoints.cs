using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using SprintPlannerDashboard.Server.Config;
using SprintPlannerDashboard.Server.Handlers;

namespace SprintPlannerDashboard.Server.Endpoints
{
    public static class BacklogItemEndpoints
    {
        public static IEndpointRouteBuilder MapBacklogItemEndpoints(this IEndpointRouteBuilder app)
        {
            var baseBacklogSubRoute = app.MapGroup(RoutingConfig.BASE_BACKLOG_SUBRESOURCE_ROUTE);

            // GET backlog items for a specific sprint
            baseBacklogSubRoute.MapGet($"/{RoutingConfig.BACKLOG_ITEM_ROUTE_ID}/backlog",
                BacklogItemHandler.GetBacklogItemsForSprintAsync).WithName("GetBacklogItems").RequireAuthorization();

            // POST create backlog item (Requires SprintId context)
            // Note: The handler uses SPRINT_ROUTE_ID in the path mapping for context.
            baseBacklogSubRoute.MapPost("",
                BacklogItemHandler.CreateBacklogItemAsync).WithName("CreateBacklogItem").RequireAuthorization();

            var baseBacklogRoute = app.MapGroup(RoutingConfig.BASE_BACKLOG_ROUTE);

            // GET all backlog items
            baseBacklogRoute.MapGet("", BacklogItemHandler.GetAllBacklogItemsAsync).WithName("GetAllBacklogItems").RequireAuthorization();

            // GET single backlog item
            baseBacklogRoute.MapGet($"/{RoutingConfig.BACKLOG_ITEM_ROUTE_ID}", BacklogItemHandler.GetBacklogItemAsync).WithName("GetBacklogItem").RequireAuthorization();


            // PUT/PATCH update backlog item
            baseBacklogRoute.MapPatch($"/{RoutingConfig.BACKLOG_ITEM_ROUTE_ID}",
                BacklogItemHandler.UpdateBacklogItemAsync).WithName("UpdateBacklogItem").RequireAuthorization();

            // DELETE backlog item
            baseBacklogRoute.MapDelete($"/{RoutingConfig.BACKLOG_ITEM_ROUTE_ID}",
                BacklogItemHandler.DeleteBacklogItemAsync).WithName("DeleteBacklogItem").RequireAuthorization();


            return app;
        }
    }
}
