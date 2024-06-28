namespace OnlineExaminationSystems.UI.Helpers;

public static class ApiEndpoints
{
    public const string BaseEndpoint = "http://localhost:5000/api/";

    public const string UserEndpoint = BaseEndpoint + "Users";

    public static string UserEndPointWithId(int id) => $"{UserEndpoint}/{id}";

    public const string AuthEndpoint = BaseEndpoint + "Auth";
    public const string LoginEndpoint = AuthEndpoint + "/Login";

    public const string LessonEndpoint = BaseEndpoint + "Lessons";
    public const string ExamEndpoint = BaseEndpoint + "Exams";

    public static string GetExamById(int examId) => $"{ExamEndpoint}/{examId}";

    public static string GetExamsByLessonIdEndPoint(int lessonId) => $"{LessonEndpoint}/{lessonId}/Exams";

    public static string GetQuestionsByExamIdEndPoint(int examId) => $"{ExamEndpoint}/{examId}/Questions";

    public static string GetQuestionsByExamIdForExam(int examId) => $"{ExamEndpoint}/{examId}/Start";

    public static string QuestionsEndPointWithId(int id) => $"{QuestionEndpoint}/{id}";

    public static string AnswerEndPoint = BaseEndpoint + "Answers";

    public const string QuestionEndpoint = BaseEndpoint + "Questions";

    public const string RoleEndpoint = BaseEndpoint + "Roles";

    public const string EnrollmentEndpoint = BaseEndpoint + "Enrollments";

    public static string LessonEndPointWithId(int id) => $"{LessonEndpoint}/{id}";

    public static string GetStudentsByLessonIdEndpoint(int id) => $"{LessonEndpoint}/{id}/Students";

    public static string EnrollmentEndPointWithId(int id) => $"{EnrollmentEndpoint}/{id}";

    public static string GetAcademicianLessonsByUserId(int userId) => $"{UserEndpoint}/{userId}/Lessons";

    public static string GetResultsByExamIdEndPoint(int examId) => $"{ExamEndpoint}/{examId}/Results";

    public static string GetEnrollmentByUserIdEndPoint(int userId) => $"{UserEndpoint}/{userId}/Enrollments";

    public static string CalculateResultEndPoint(int examId) => $"{ExamEndpoint}/{examId}/calculate-results";
}