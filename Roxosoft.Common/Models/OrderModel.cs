namespace Roxosoft.Common.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class OrderModel : BaseModel
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Status { get; set; }

        public int TotalCount { get; set; }

        public decimal TotalPrice { get; set; }

        public string Description { get; set; }

        public IList<ProductsInOrders> ProductsInOrders { get; } = new List<ProductsInOrders>();
    }
}
