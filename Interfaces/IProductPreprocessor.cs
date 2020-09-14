namespace ProductAPI.Interfaces
{
    using ProductAPI.Models;

    public interface IProductPreprocessor
    {
        /// <summary>
        /// Is product featured
        /// </summary>
        /// <param name="product"> product form from request </param>
        /// <returns> if correct true - otherwise false </returns>
        bool IsFeatured(ProductForm product);
    }
}
