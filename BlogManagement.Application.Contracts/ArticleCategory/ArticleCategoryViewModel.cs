using Microsoft.AspNetCore.Http;

namespace BlogManagement.Application.Contracts.ArticleCategory
{
    public class ArticleCategoryViewModel
    {
        public string Name { get; set; }

        public IFormFile Picture { get; set; }

        public string Description { get; set; }

        public int ShowOrder { get; set; }

    }
}
