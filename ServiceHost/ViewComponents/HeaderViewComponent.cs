using _01_ExclusiveQuery.Contracts.ArticleCategory;
using _01_ExclusiveQuery.Contracts.Order;
using _01_ExclusiveQuery.Contracts.ProductCategory;
using _01_ExclusiveQuery.MenuModel;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;

namespace ServiceHost.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly IProductCategoryQuery _productCategoryQuery;
        private readonly IArticleCategoryQuery _articleCategoryQuery;

        public HeaderViewComponent(IProductCategoryQuery productCategoryQuery, IArticleCategoryQuery articleCategoryQuery)
        {
            _productCategoryQuery = productCategoryQuery;
            _articleCategoryQuery = articleCategoryQuery;
        }

        public IViewComponentResult Invoke()
        {
            var result = new MenuModel
            {
                ArticleCategories = _articleCategoryQuery.GetArticleCategories(),
                ProductCategories = _productCategoryQuery.GetProductCategories()
            };

            if (Request.Cookies["cart-items"] != null)
            {
                var serializer = new JavaScriptSerializer();
                var value = Request.Cookies["cart-items"];
                result.CartQueryModel = serializer.Deserialize<List<CartQueryModel>>(value);
            }

            return View(result);
        }
    }
}
