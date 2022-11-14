using _0_Framework.Application.Cookie;
using _01_ExclusiveQuery.Contracts.ArticleCategory;
using _01_ExclusiveQuery.Contracts.ProductCategory;
using _01_ExclusiveQuery.MenuModel;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly IProductCategoryQuery _productCategoryQuery;
        private readonly IArticleCategoryQuery _articleCategoryQuery;
        private readonly ISerializeCookie _serializeCookie;

        public const string CookieName = "cart-items";

        public HeaderViewComponent(IProductCategoryQuery productCategoryQuery, IArticleCategoryQuery articleCategoryQuery, ISerializeCookie serializeCookie)
        {
            _productCategoryQuery = productCategoryQuery;
            _articleCategoryQuery = articleCategoryQuery;
            _serializeCookie = serializeCookie;
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
                if (!_serializeCookie.CheckSerialize(result.CartQueryModel, HttpContext))
                {
                    //?Just in Case
                    _serializeCookie.DeleteCookie(HttpContext); ;
                    return View(result);
                }

                //if (result.CartQueryModel.Count <= 0)
                //{
                //    //CartMessage = "t";
                //    _serializeCookie.DeleteCookie(HttpContext);
                //    return View(result);
                //}
                result.CartQueryModel = _serializeCookie.Serialize(result.CartQueryModel, HttpContext);
                return View(result);
            }

            return View(result);
        }
    }
}
