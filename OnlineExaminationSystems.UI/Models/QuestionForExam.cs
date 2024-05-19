namespace OnlineExaminationSystems.UI.Models
{
    public class QuestionForExam
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public int ExamId { get; set; }
        public List<string> Options { get; set; }
    }
}
