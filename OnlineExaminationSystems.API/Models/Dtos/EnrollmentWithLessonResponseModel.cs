using OnlineExaminationSystems.API.Models.Entities;

namespace OnlineExaminationSystems.API.Models.Dtos;

public class EnrollmentWithLessonResponseModel : IEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int LessonId { get; set; }
    public Lesson Lesson { get; set; }
}