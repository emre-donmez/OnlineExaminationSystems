using OnlineExaminationSystems.UI.Areas.Mutual.Models.Lesson;

namespace OnlineExaminationSystems.UI.Models.Enrollment;

public class Enrollment
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int LessonId { get; set; }
    public Areas.Admin.Models.User.User User { get; set; }
    public Lesson Lesson { get; set; }
}