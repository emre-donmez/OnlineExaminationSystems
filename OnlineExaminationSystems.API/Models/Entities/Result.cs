using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineExaminationSystems.API.Models.Entities
{
    [Table("Results")]
    public class Result : IEntity
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("exam_id")]
        public int ExamId { get; set; }
        public int Score { get; set; }
    }
}
