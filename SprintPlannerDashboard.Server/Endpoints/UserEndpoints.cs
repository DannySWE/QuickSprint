using Microsoft.AspNetCore.Authorization;
using SprintPlannerDashboard.Server.Config;
using SprintPlannerDashboard.Server.Handlers;

namespace SprintPlannerDashboard.Server.Endpoints
{
    public static class UserEndpoints
    {
        public static IEndpointRouteBuilder MapUserAuthEndpoints(this IEndpointRouteBuilder app)
        {
            var userEndpoints = app.MapGroup(RoutingConfig.BASE_USER_ROUTE);
            var adminEndpoints = app.MapGroup(RoutingConfig.ADMIN_ROUTE);

            userEndpoints.MapPost("/register", SprintPlannerUserHandler.RegisterUser).AllowAnonymous();
            userEndpoints.MapPost("/login", SprintPlannerUserHandler.LoginUser).AllowAnonymous();
            userEndpoints.MapDelete("/delete", SprintPlannerUserHandler.DeleteUser).AllowAnonymous();
            userEndpoints.MapGet("/userprofile", SprintPlannerUserHandler.GetUserProfile).RequireAuthorization();
            //adminEndpoints.MapGet("adminonly", AuthorizationHandler.AdminOnly).RequireAuthorization("RequireAdmin");

            return app;
        }

    }
}
