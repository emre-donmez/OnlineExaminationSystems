namespace OnlineExaminationSystems.UI.Models.Lesson
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public User.User User { get; set; }
        public Exam.Exam Exam { get; set; }
    }
}
