namespace OnlineExaminationSystems.UI.Helpers
{
    public static class ApiEndpoints
    {
        public const string BaseEndpoint = "https://localhost:7100/api/";

        public const string UserEndpoint = BaseEndpoint + "Users";

        public const string AuthEndpoint = BaseEndpoint + "Auth/";
        public const string LoginEndpoint = AuthEndpoint + "Login";


        public const string ExamEndpoint = BaseEndpoint + "Exams/";
        public static string GetQuestionsByExamId(int examId) => $"{ExamEndpoint}{examId}/Questions";

        public const string QuestionEndpoint = BaseEndpoint + "Questions";

    }
}
