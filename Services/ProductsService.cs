namespace ProductAPI.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using ProductAPI.Data;
    using ProductAPI.Data.Factory;
    using ProductAPI.Data.Models;
    using ProductAPI.Interfaces;
    using ProductAPI.Models;

    public class ProductsService : IProductsService
    {
        private readonly IContextFactory _factory;
        
        public ProductsService(IContextFactory contextFactory)
        {
            _factory = contextFactory;
        }

        /// <inheritdoc cref="GetAllProductsData"/>
        public async Task<List<Product>> GetAllProductsData()
        {
            using (var context = _factory.GetContext())
            {
                return await Task.Run(() => context.Products.ToList());
            }
        }

        /// <inheritdoc cref="GetProductById"/>
        public async Task<Product> GetProductById(int id)
        {
            using (var context = this._factory.GetContext())
            {
                return await this.GetProductById(id, context);
            }
        }

        /// <summary>
        /// The get product by id.
        /// </summary>
        /// <param name="id">  product id  </param>
        /// <param name="context">  context </param>
        /// <returns> product or null </returns>
        private Task<Product> GetProductById(int id, MainDbContext context)
        {
            return context.Products.FindAsync(id);
        }

        /// <inheritdoc cref="DeleteProductById"/>
        public async Task DeleteProductById(int id)
        {
            using (var context = this._factory.GetContext())
            {
                var product = await this.GetProductById(id, context);
                if (product == null)
                {
                    return;
                }

                context.Products.Remove(product);
                await context.SaveChangesAsync();
            }
        }

        /// <inheritdoc cref="TryUpdateProduct"/>
        public async Task<bool> TryUpdateProduct(int id, ProductForm productForm)
        {
            using (var context = this._factory.GetContext())
            {
                var product = await this.GetProductById(id);

                if (product == null)
                {
                    return false;
                }

                product.Name = productForm.Name;
                product.Rating = productForm.Rating;
                product.BrandId = productForm.BrandId;
                product.ExpirationDate = productForm.ExpirationDate;
                product.ItemsInStock = productForm.ItemsInStock;
                product.ReceiptDate = productForm.ReceiptDate;
                product.Featured = productForm.Featured;

                context.Entry(product).State = EntityState.Modified; 
                await context.SaveChangesAsync();

                return true;
            }
        }

        /// <inheritdoc cref="AddProduct"/>
        public async Task AddProduct(Product product, IList<int> categoriesId)
        {
            using (var context = this._factory.GetContext())
            {
                await context.Products.AddAsync(product);
                foreach (var id in categoriesId)
                {
                    await context.CategoryProducts
                        .AddAsync(new CategoryProduct
                        {
                            ProductId = product.Id,
                            CategoryId = id
                        });
                }

                await context.SaveChangesAsync();
            }
        }
    }
}