namespace PiecykPolHurt.Model.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class SendPoint : BaseEntity
    {
        [Required]
        public string Code { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string BuildingNumber { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public ICollection<ProductSendPoint> Products { get; set; }
    }
}
