namespace ProductAPI.Options
{
    /// <summary>
    /// Options product validation and preprocessing
    /// </summary>
    public class ProductOptions
    {
        public uint ExpireDaysLimit { get; set; }

        public uint CategoriesLimit { get; set; }

        public double FeaturedRating { get; set; }
    }
}
