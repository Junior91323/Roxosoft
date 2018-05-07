using System.ComponentModel.DataAnnotations;

namespace Roxosoft_TEST.Models.Product
{
    public class ProductRequestModel
    {
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Wrong price! Must be >= 1")]
        public decimal Price { get; set; }
    }
}
