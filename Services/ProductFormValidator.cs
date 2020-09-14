namespace ProductAPI.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.EntityFrameworkCore.Internal;
    using Microsoft.Extensions.Options;

    using ProductAPI.Data.Factory;
    using ProductAPI.Data.Interfaces;
    using ProductAPI.Options;

    /// <inheritdoc cref="IProductFormValidator"/>
    public class ProductFormValidator : IProductFormValidator
    {
        /// <summary>
        /// Maximum amount of categories for product
        /// </summary>
        private uint _maxCatCount;

        /// <summary>
        /// Expire days limitation
        /// </summary>
        private uint _expireLimitDays;

        private IContextFactory _contextFactory;

        public ProductFormValidator(IOptions<ProductOptions> options, IContextFactory contextFactory)
        {
            this._maxCatCount = options.Value.CategoriesLimit;
            this._expireLimitDays = options.Value.ExpireDaysLimit;
            this._contextFactory = contextFactory;
        }

        /// <inheritdoc cref="IsValidCategoriesIds"/>
        public bool IsValidCategoriesIds(IList<int> categoryIds, out string message)
        {
            message = string.Empty;

            if (categoryIds == null || !EnumerableExtensions.Any(categoryIds))
            {
                message = "A product should has any category!";
            }
            else if (categoryIds.Count > this._maxCatCount)
            {
                message = $"A product shouldn't have more than {this._maxCatCount} category!";
            }
            else
            {
                using (var context = this._contextFactory.GetContext())
                {
                    var wrongIds = categoryIds.Where(id => context.Categories.FindAsync(id) == null);

                    if (!wrongIds.Any())
                    {
                        return true;
                    }

                    message = $"Categories have wrong ids: {string.Join(", ", categoryIds)}";
                }
            }

            return false;
        }


        /// <inheritdoc cref="IsValidExpirationDay"/>
        public bool IsValidExpirationDay(DateTime? expirationDate, out string message)
        {
            message = string.Empty;
            
            if (expirationDate == null || expirationDate < DateTime.Today.AddDays(this._expireLimitDays))
            {
                message = $"Product is too old! It should expire not less than {this._expireLimitDays} days since now";
                return false;
            }

            return true;
        }
    }
}
