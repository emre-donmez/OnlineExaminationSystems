using OnlineExaminationSystems.API.Models.Entities;

namespace OnlineExaminationSystems.API.Models.Dtos;

public class ResultWithUserAndExamResponseModel : IEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ExamId { get; set; }
    public int Score { get; set; }
    public User User { get; set; }
    public Exam Exam { get; set; }
}