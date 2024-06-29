using OnlineExaminationSystems.UI.Areas.Admin.Models.User;

namespace OnlineExaminationSystems.UI.Areas.Mutual.Models.Lesson;

public class LessonWithUser
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}