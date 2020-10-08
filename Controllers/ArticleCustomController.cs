using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnowledgeApi.Models;
using KnowledgeApi.Services;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            var articles = await _articleService.GetTopTakeArticles(5);
            return Ok(articles);
        }

        [HttpPost("AddModel")]
        public virtual async Task<ActionResult> AddModel(Article model)
        {
            return Ok(await this._articleService.Create(model));
        }
        [HttpGet("FindArticle")]  ///{ArticleName}
        //[Route("FindArticle/{ArticleName}")]
        public virtual async Task<ActionResult> FindArticle(string ArticleName)
        {
            return Ok(await this._articleService.FindArticle(ArticleName));
        }

        [HttpGet("LastArticle")]
        public virtual async Task<ActionResult> LastArticle()
        {
            return Ok(await this._articleService.LastArticle());
        }
    }
}
