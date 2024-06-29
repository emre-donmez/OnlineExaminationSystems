using OnlineExaminationSystems.API.Models.Entities;

namespace OnlineExaminationSystems.API.Models.Dtos;

public class EnrollmentWithUserResponseModel : IEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int LessonId { get; set; }
    public User User { get; set; }
}