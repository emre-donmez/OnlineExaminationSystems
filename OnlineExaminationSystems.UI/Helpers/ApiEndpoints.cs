namespace OnlineExaminationSystems.UI.Helpers
{
    public static class ApiEndpoints
    {
        public const string BaseEndpoint = "https://localhost:7100/api/";

        public const string UserEndpoint = BaseEndpoint + "Users";
        public static string UserEndPointWithId(int id) => $"{UserEndpoint}/{id}";

        public const string AuthEndpoint = BaseEndpoint + "Auth/";
        public const string LoginEndpoint = AuthEndpoint + "Login";

        public const string RoleEndpoint = BaseEndpoint + "Roles";

        public const string LessonEndpoint = BaseEndpoint + "Lessons";


    }
}
