namespace ProductAPI.Services
{
    using Microsoft.Extensions.Options;

    using ProductAPI.Interfaces;
    using ProductAPI.Models;
    using ProductAPI.Options;

    public class ProductPreprocessor : IProductPreprocessor
    {
        /// <summary>
        /// Start of feature
        /// </summary>
        private readonly double _featuredRaiting;
        
        public ProductPreprocessor(IOptions<ProductOptions> options)
        {
            _featuredRaiting = options.Value.FeaturedRating;
        }

        /// <inheritdoc cref="IsFeatured"/>
        public bool IsFeatured(ProductForm product)
        {
            return product.Featured || product.Rating >= this._featuredRaiting;
        }
    }
}
