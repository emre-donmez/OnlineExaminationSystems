namespace OnlineExaminationSystems.UI.Areas.Student.Models.Exam;

public class AnswerSubmitRequestModel
{
    public int UserId { get; set; }
    public int QuestionId { get; set; }
    public string? GivenAnswer { get; set; }
}