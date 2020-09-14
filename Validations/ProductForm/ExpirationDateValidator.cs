namespace ProductAPI.Data.Validations.Products
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using ProductAPI.Data.Interfaces;

    /// <summary>
    /// Attribute ExpirationDateValidator
    /// </summary>
    public class ExpirationDateValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var expireDate = (DateTime)value;
            var productValidator = (IProductFormValidator)validationContext.GetService(typeof(IProductFormValidator));
            
            if (productValidator == null)
            {
                return null;
            }

            return productValidator.IsValidExpirationDay(expireDate, out var message) ? null : new ValidationResult(message);
        }
    }
}
