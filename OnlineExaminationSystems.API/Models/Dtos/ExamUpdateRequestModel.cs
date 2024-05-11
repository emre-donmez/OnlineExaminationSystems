namespace OnlineExaminationSystems.API.Models.Dtos
{
    public record ExamUpdateRequestModel(int LessonId, int QuestionCount, int Duration, DateTime StartedDate);
}