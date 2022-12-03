using System.ComponentModel.DataAnnotations;

namespace PiecykPolHurt.Model.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
