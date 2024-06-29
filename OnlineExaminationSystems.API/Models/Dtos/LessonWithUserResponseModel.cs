using OnlineExaminationSystems.API.Models.Entities;

namespace OnlineExaminationSystems.API.Models.Dtos;

public class LessonWithUserResponseModel : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}