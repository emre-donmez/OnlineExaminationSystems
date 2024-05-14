namespace OnlineExaminationSystems.API.Models.Dtos
{
    public record ExamUpdateRequestModel(string Name, int LessonId ,int QuestionCount, int Duration, DateTime StartedDate);
}