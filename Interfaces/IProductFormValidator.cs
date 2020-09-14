namespace ProductAPI.Data.Interfaces
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Product form validator interface
    /// </summary>
    public interface IProductFormValidator
    {
        /// <summary>
        /// Check product date expiration 
        /// </summary>
        /// <param name="expirationDate"> expiration date</param>
        /// <param name="message"> error message </param>
        /// <returns> if correct true - otherwise false </returns>
        bool IsValidExpirationDay(DateTime? expirationDate, out string message);

        /// <summary>
        /// Check product categories ids 
        /// </summary>
        /// <param name="categoryIds"> categories ids </param>
        /// <param name="message"> error message </param>
        /// <returns> if correct true - otherwise false </returns>
        bool IsValidCategoriesIds(IList<int> categoryIds, out string message);
    }
}
