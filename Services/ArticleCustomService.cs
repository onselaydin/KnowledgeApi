using KnowledgeApi.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace KnowledgeApi.Services
{
    public class ArticleCustomService
    {
        private readonly IMongoCollection<Article> _article;
        //private readonly IMongoCollection<ArtType> _articletype;
        public ArticleCustomService(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.MongoConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _article = database.GetCollection<Article>("articles");
           // _articletype = database.GetCollection<ArtType>("arttypes");
        }

        public async Task<List<Article>> GetTopTakeArticles(int TakeValue)
        {
            //var articles = await _article.AsQueryable().OrderByDescending(x => x.Id).Take(TakeValue).ToListAsync();
            //return await _article.Find(x => true).SortByDescending(x => x.Dates).Limit(limit).ToListAsync();
            var articles = await (from a in _article.AsQueryable()
                                  //join t in _articletype.AsQueryable() on a.ArticleType equals t.Id into j
                                  select new Article
                                  {
                                      Id = a.Id,
                                      Title = a.Title,
                                      Content = a.Content,
                                      Description = a.Description,
                                      Topics = a.Topics,
                                      Url = a.Url,
                                      ArticleType = a.ArttypeDetail.First().Title,
                                      Image = a.Image,
                                      Dates = a.Dates
                                  })
                            .OrderByDescending(o => o.Dates)
                            .Take(TakeValue)
                            .ToListAsync();

            // var articles = from a in _article.AsQueryable<Article>() select a;
            // var arttype = await _articletype.AsQueryable().OrderByDescending(x => x.Id).ToListAsync();
            return articles;

        }
        public async Task <List<Article>> FindArticle(string ArticleName)
        {
            var article = await (from a in _article.AsQueryable()
                                 .Where(x => x.Title.ToLower().Contains(ArticleName))
                                 select new Article
                                 {
                                     Id = a.Id,
                                     Title = a.Title,
                                     Content = a.Content,
                                     Description = a.Description,
                                     Topics = a.Topics,
                                     Url = a.Url,
                                     ArticleType = a.ArttypeDetail.First().Title,
                                     Image = a.Image,
                                     Dates = a.Dates
                                 }).ToListAsync();
            return article;
        }
        public async Task<Article> LastArticle()
        {
            var article = await (from a in _article.AsQueryable()
                                 select new Article
                                 {
                                     Id = a.Id,
                                     Title = a.Title,
                                     Content = a.Content,
                                     Description = a.Description,
                                     Topics = a.Topics,
                                     Url = a.Url,
                                     //ArticleType = a.ArttypeDetail.First().Title,
                                     ArttypeDetail= a.ArttypeDetail,
                                     Image = a.Image,
                                     Dates = a.Dates
                                 })
                                 .OrderByDescending(a=>a.Dates)
                                 .Take(1)
                                 .FirstOrDefaultAsync();
            return article;
        }

        public virtual async Task<Article> Create(Article model)
        {
            await _article.InsertOneAsync(model);
            return model;
        }

    }
}
