using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineExaminationSystems.API.Models.Entities
{
    [Table("Exams")]
    public class Exam : IEntity
    {
        [Column("id")]
        public int Id { get; set; }
        public string Name { get; set; }

        [Column("lesson_id")]
        public int LessonId { get; set; }

        [Column("question_count")]
        public int QuestionCount { get; set; }

        public int Duration { get; set; }

        [Column("started_date")]
        public DateTime StartedDate { get; set; }
    }
}