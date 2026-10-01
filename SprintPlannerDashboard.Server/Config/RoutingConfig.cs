namespace SprintPlannerDashboard.Server.Config
{
    public static class RoutingConfig
    {
        //routing variables for project endpoints
        public  const string BASE_PROJECT_ROUTE = "/api/projects";
        public const string PROJECT_ROUTE_ID = "{projectPublicId}";

        //routing variables for projectboard endpoints
        public const string BASE_PROJECTBOARD_SUBRESOURCE_ROUTE = "/api/projects/{projectPublicId}/projectboards";
        public const string BASE_PROJECTBOARD_ROUTE = "/api/projectboards";
        public const string PROJECTBOARD_ROUTE_ID = "{projectBoardPublicId}";

        //routing variables for sprints endpoints
        public const string BASE_SPRINT_SUBRESOURCE_ROUTE = "/api/projectboards/{projectBoardPublicId}/sprints";
        public const string BASE_SPRINT_ROUTE = "/api/sprints";
        public const string SPRINT_ROUTE_ID = "{sprintPublicId}";

        //routing variables for backlog endpoints
        public const string BASE_BACKLOG_SUBRESOURCE_ROUTE = "/api/sprints/{sprintPublicId}/backlogitems";
        public const string BASE_BACKLOG_ROUTE = "/api/backlogitems";
        public const string BACKLOG_ITEM_ROUTE_ID = "{backlogItemPublicId}";

        //routing variables for backlog endpoints
        public const string BASE_TASK_SUBRESOURCE_ROUTE = "/api/backlogitems/{backlogItemPublicId}/tasks";
        public const string BASE_TASK_ROUTE = "/api/tasks";
        public const string TASK_ROUTE_ID = "{projectTaskId}";

        //routing variables for user endpoints
        public const string BASE_USER_ROUTE = "/api";
        public const string ADMIN_ROUTE = "/api/admin";




    }
}
