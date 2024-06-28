namespace OnlineExaminationSystems.UI.Areas.Academician.Models.Exam;

public record ExamUpdateRequestModel(string Name, int LessonId, int QuestionCount, int Duration, DateTime StartedDate);
