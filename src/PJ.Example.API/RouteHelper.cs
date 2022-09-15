﻿namespace PJ.Example.API
{
    public static class RouteHelper
    {
        public const string BaseRoute = "api/v{v:apiVersion}/[controller]";
        public const string BaseRouteNoController = "api/v{v:apiVersion}";
        public const string Users = "users";
        public const string User = "user/{id}";
        public const string AssignUserRoles = "user/{id}/roles";

        public const string Roles = "roles";
        public const string Role = "role/{id}";
    }
}