namespace OnlineExaminationSystems.UI.Helpers
{
    public static class ApiEndpoints
    {
        public const string BaseEndpoint = "https://localhost:7100/api/";

        public const string UserEndpoint = BaseEndpoint + "Users";

        public const string AuthEndpoint = BaseEndpoint + "Auth/";
        public const string LoginEndpoint = AuthEndpoint + "Login";

        public const string LessonEndpoint = BaseEndpoint + "Lessons";
        public const string ExamEndpoint = BaseEndpoint + "Exams";
        public static string GetExamsByLessonIdEndPoint(int lessonId) => $"{LessonEndpoint}/{lessonId}/Exams";
        public static string GetQuestionsByExamIdEndPoint(int examId) => $"{ExamEndpoint}/{examId}/Questions";
        public static string GetQuestionsByExamIdForExam(int examId) => $"{ExamEndpoint}/{examId}/Start";

        public const string QuestionEndpoint = BaseEndpoint + "Questions";

        



    }
}
