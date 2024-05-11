using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineExaminationSystems.API.Models.Entities
{
    [Table("Questions")]
    public class Question : IEntity
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("question_text")]
        public string QuestionText { get; set; }

        [Column("wrong_answer_1")]
        public string Option1 { get; set; }

        [Column("wrong_answer_2")]
        public string Option2 { get; set; }

        [Column("wrong_answer_3")]
        public string Option3 { get; set; }

        [Column("correct_answer")]
        public string CorrectAnswer { get; set; }

        [Column("exam_id")]
        public int ExamId { get; set; }
    }
}