namespace PiecykPolHurt.Model.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class DictionaryType : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public ICollection<DictionaryValue> Values { get; set; }
    }
}
