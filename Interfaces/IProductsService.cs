namespace ProductAPI.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ProductAPI.Data.Models;
    using ProductAPI.Models;

    /// <summary>
    /// Product service
    /// </summary>
    public interface IProductsService
    {
        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns></returns>
        Task<List<Product>> GetAllProductsData();

        /// <summary>
        /// Get product by id
        /// </summary>
        /// <param name="id"> product id </param>
        /// <returns> product if there is product with id, else null </returns>
        Task<Product> GetProductById(int id);

        /// <summary>
        /// Delete product by id
        /// </summary>
        /// <param name="id"> product id </param>
        Task DeleteProductById(int id);

        /// <summary>
        /// Try update product by id
        /// </summary>
        /// <param name="id"> product id </param>
        /// <param name="productForm"></param>
        /// <returns> if success true - otherwise false </returns>
        Task<bool> TryUpdateProduct(int id, ProductForm productForm);

        /// <summary>
        /// Add product
        /// </summary>
        /// <param name="product"> product to add </param>
        /// <param name="categoriesId"> categories of product </param>
        Task AddProduct(Product product, IList<int> categoriesId);
    }
}