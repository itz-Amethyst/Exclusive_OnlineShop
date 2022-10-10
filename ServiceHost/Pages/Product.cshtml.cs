using _0_Framework.Application;
using _01_ExclusiveQuery.Contracts.Product;
using CommentManagement.Application.Contract.Comment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using ShopManagement.Application.Contracts.Comment;
using System.Net;

namespace ServiceHost.Pages
{
    public class ProductModel : PageModel
    {
        private readonly IProductQuery _productQuery;
        private readonly ICommentApplication _commentApplication;
        public ProductQueryModel Product;

        public ProductModel(IProductQuery productQuery, ICommentApplication commentApplication)
        {
            _productQuery = productQuery;
            _commentApplication = commentApplication;
        }

        public void OnGet(string id , bool created = false)
        {
            ViewData["Created"] = created;

            Product = _productQuery.GetProductDetails(id);
        }

        public IActionResult OnPost(AddComment command , string productSlug)
        {
            var response = Request.Form["g-recaptcha-response"];
            string secretKey = "6Lf-bm0iAAAAALfFjSkAOJkGmsT8LFS1jYmy6rw4";
            var client = new WebClient();
            var result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, response));
            var obj = JObject.Parse(result);
            var status = (bool)obj.SelectToken("success");
            var data =TempData["Created"] = false;

            if (status)
            {
                command.Type = CommentTypes.Product;
                _commentApplication.Add(command);
                return RedirectToPage("/Product", new { Id = productSlug , Created = true });
            }

            TempData["Message"] = "Google reCaptcha validation failed";

            return RedirectToPage("/Product");

        }
    }
}
