namespace ProductAPI.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using ProductAPI.Data.Models;
    using ProductAPI.Interfaces;
    using ProductAPI.Models;

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductsService _productsService;
        
        private readonly IProductPreprocessor _productPreprocessor;


        public ProductController(IProductsService productsService, IProductPreprocessor productPreprocessor)
        {
            _productsService = productsService;
            _productPreprocessor = productPreprocessor;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var results = await _productsService.GetAllProductsData();
            return new JsonResult(results);
        }
        
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            var result = await _productsService.GetProductById(productId);
            if (result == null)
            {
                return this.NotFound();
            }

            return new JsonResult(result);
        }

        [HttpDelete("{productId}")]
        public async Task DeleteProductById(int productId)
        {
            await _productsService.DeleteProductById(productId);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductForm productForm)
        {
            var product = new Product(
                name: productForm.Name,
                rating: productForm.Rating,
                brandId: productForm.BrandId,
                expirationDate: productForm.ExpirationDate,
                itemsInStock: productForm.ItemsInStock,
                receiptDate: productForm.ReceiptDate,
                featured: this._productPreprocessor.IsFeatured(productForm));
                              
            await _productsService.AddProduct(product, productForm.CategoryProductsIds);

            return new JsonResult(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductForm productForm)
        {
            productForm.Featured = this._productPreprocessor.IsFeatured(productForm);
            var success = await _productsService.TryUpdateProduct(id, productForm);
            return success ? (IActionResult)this.NoContent() : this.NotFound(); 
        }
    }
}