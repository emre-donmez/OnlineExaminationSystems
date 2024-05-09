using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineExaminationSystems.API.Models.Entities
{
    [Table("Lessons")]
    public class Lesson : IEntity
    {
        [Column("id")]
        public int Id { get; set; }
        public string Name { get; set; }

        [Column("responsible_user_id")]
        public int UserId { get; set; }
    }
}
