using KnowledgeApi.Models;
using KnowledgeApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController :  BaseMongoController<Article>
    {
        //test deneme
        public ArticleController(ArticleRepository articleRepository) : base(articleRepository)
        {
        }


    }
}
