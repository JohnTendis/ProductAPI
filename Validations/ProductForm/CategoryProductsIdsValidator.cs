namespace ProductAPI.Validations.ProductForm
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ProductAPI.Data.Interfaces;

    /// <summary>
    /// Attribute CategoryProductsIdsValidator
    /// </summary>
    public class CategoryProductsIdsValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var categoryIds = (IList<int>)value;
            var productValidator = (IProductFormValidator)validationContext.GetService(typeof(IProductFormValidator));
            
            if (productValidator == null)
            {
                return null;
            }

            return productValidator.IsValidCategoriesIds(categoryIds, out var message) ? null : new ValidationResult(message);
        }

    }
}
