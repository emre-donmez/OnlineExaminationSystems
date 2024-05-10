using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineExaminationSystems.API.Models.Entities
{
    [Table("Roles")]
    public class Role : IEntity
    {
        [Column("id")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
