using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineExaminationSystems.UI.Models
{
    public class Result
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ExamId { get; set; }
        public int Score { get; set; }
        public User.User User { get; set; }
        public Exam Exam { get; set; }
    }
}
