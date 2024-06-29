namespace OnlineExaminationSystems.UI.Areas.Academician.Models.Result;

public class ResultWithUserAndExam
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ExamId { get; set; }
    public int Score { get; set; }
    public Admin.Models.User.User User { get; set; }
    public Exam.Exam Exam { get; set; }
}