using System.ComponentModel.DataAnnotations;

namespace PiecykPolHurt.Model.Commands
{
    public class CreateSendPointCommand
    {
        [Required]
        public string Code { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        public IEnumerable<int> ProductsIds { get; set; }
    }
}
