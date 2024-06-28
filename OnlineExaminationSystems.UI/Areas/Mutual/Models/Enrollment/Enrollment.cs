namespace OnlineExaminationSystems.UI.Areas.Mutual.Models.Enrollment;

public class Enrollment
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int LessonId { get; set; }
    public Admin.Models.User.User User { get; set; }
    public Lesson.Lesson Lesson { get; set; }
}