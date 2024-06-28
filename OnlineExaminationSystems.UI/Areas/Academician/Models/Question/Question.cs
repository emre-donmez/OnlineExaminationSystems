namespace OnlineExaminationSystems.UI.Areas.Academician.Models.Question;

public class Question
{
    public int Id { get; set; }

    public string QuestionText { get; set; }

    public string Option1 { get; set; }

    public string Option2 { get; set; }

    public string Option3 { get; set; }

    public string CorrectAnswer { get; set; }

    public int ExamId { get; set; }
}
