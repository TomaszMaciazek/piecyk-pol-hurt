using System.ComponentModel.DataAnnotations;

namespace PiecykPolHurt.Model.Entities
{
    public class DictionaryType : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public ICollection<DictionaryValue> Values { get; set; }
    }
}
