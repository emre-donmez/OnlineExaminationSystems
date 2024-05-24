namespace OnlineExaminationSystems.UI.Models
{
    public class ExamUpdateRequest
    {
        public string Name { get; set; }
        public int QuestionCount { get; set; }
        public int Duration { get; set; }
        public DateTime StartedDate { get; set; }
    }
}
