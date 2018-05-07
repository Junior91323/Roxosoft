namespace Roxosoft_TEST.Models.Cart
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CartRequestModel
    {
        public Guid ProductUid { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Wrong count")]
        public int Count { get; set; }
    }
}
