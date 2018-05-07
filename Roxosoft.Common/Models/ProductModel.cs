namespace Roxosoft.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ProductModel : BaseModel
    {
        [Key]
        [Required]
        public Guid Uid { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }
    }
}
