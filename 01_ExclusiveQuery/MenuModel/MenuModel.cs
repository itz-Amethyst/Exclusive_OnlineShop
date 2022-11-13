using _01_ExclusiveQuery.Contracts.ArticleCategory;
using _01_ExclusiveQuery.Contracts.Order;
using _01_ExclusiveQuery.Contracts.ProductCategory;

namespace _01_ExclusiveQuery.MenuModel
{
    public class MenuModel
    {
        public List<ArticleCategoryQueryModel> ArticleCategories { get; set; }

        public List<ProductCategoryQueryModel> ProductCategories { get; set; }

        public List<CartQueryModel> CartQueryModel { get; set; }
        
    }
}
