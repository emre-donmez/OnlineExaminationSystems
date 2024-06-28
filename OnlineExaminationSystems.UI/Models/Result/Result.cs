using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineExaminationSystems.UI.Models.Result
{
    public class Result
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ExamId { get; set; }
        public int Score { get; set; }
        public Areas.Admin.Models.User.User User { get; set; }
        public Exam.Exam Exam { get; set; }
    }
}
