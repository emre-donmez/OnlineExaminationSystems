namespace OnlineExaminationSystems.UI.Models
{
    public class Exam
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int LessonId { get; set; }

        public int QuestionCount { get; set; }

        public int Duration { get; set; }

        public DateTime StartedDate { get; set; }
    }
}
