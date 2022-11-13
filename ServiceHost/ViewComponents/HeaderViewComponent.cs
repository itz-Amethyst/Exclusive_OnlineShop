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

        public const string CookieName = "cart-items";

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
                var value = Request.Cookies[CookieName];
                try
                {
                    result.CartQueryModel = serializer.Deserialize<List<CartQueryModel>>(value);
                }
                catch (Exception e)
                {
                    HttpContext.Response.Cookies.Delete(CookieName);
                    Console.WriteLine(e);
                    return View(result);
                }
            }

            return View(result);
        }
    }
}
