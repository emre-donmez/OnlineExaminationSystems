using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineExaminationSystems.API.Models.Entities
{
    [Table("Answers")]
    public class Answer : IEntity
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("question_id")]
        public int QuestionId { get; set; }
        [Column("given_answer")]
        public string GivenAnswer { get; set; }
        [Column("is_correct")]
        public bool IsCorrect { get; set; }
    }
}
