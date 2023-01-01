using System.ComponentModel.DataAnnotations;

namespace PiecykPolHurt.Model.Entities
{
    public class DictionaryValue : BaseEntity
    {
        [Required]
        public int Value { get; set; }
        [Required]
        public int DictionaryTypeId { get; set; }
        public DictionaryType Type { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
