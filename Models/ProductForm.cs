namespace ProductAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ProductAPI.Data.Validations.Products;
    using ProductAPI.Validations.ProductForm;

    /// <summary>
    /// Product form from request
    /// </summary>
    public class ProductForm
    {
        [Required]
        public string Name { get; set; }

        public bool Featured { get; set; }
        
        [ExpirationDateValidator]
        public DateTime? ExpirationDate { get; set; }

        public int ItemsInStock { get; set; } = 0;
        
        public DateTime? ReceiptDate { get; set; }

        public double Rating { get; set; }

        public int BrandId { get; set; }

        [CategoryProductsIdsValidator]
        public List<int> CategoryProductsIds { get; set; }
    }
}
