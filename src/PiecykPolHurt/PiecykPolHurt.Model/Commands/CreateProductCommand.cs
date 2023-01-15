using System.ComponentModel.DataAnnotations;

namespace PiecykPolHurt.Model.Commands
{
    public class CreateProductCommand
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public IEnumerable<int> SendPointsIds { get; set; }
    }
}
