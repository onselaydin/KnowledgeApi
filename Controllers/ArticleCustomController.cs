using KnowledgeApi.Models;
using KnowledgeApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KnowledgeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleCustomController : Controller
    {

        private readonly ArticleCustomService _articleService;
        public ArticleCustomController(ArticleCustomService articleService)
        {
            _articleService = articleService;
        }

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            var articles = await _articleService.GetTopTakeArticles(5);
            return Ok(articles);
        }

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("AddModel")]
        public virtual async Task<ActionResult> AddModel(Article model)
        {
            return Ok(await this._articleService.Create(model));
        }

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("FindArticle")]  ///{ArticleName}
        //[Route("FindArticle/{ArticleName}")]
        public virtual async Task<ActionResult> FindArticle(string ArticleName)
        {
            return Ok(await this._articleService.FindArticle(ArticleName));
        }

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("LastArticle")]
        public virtual async Task<ActionResult> LastArticle()
        {
            return Ok(await this._articleService.LastArticle());
        }
    }
}
