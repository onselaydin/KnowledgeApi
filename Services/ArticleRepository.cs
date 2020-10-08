using KnowledgeApi.Models;


namespace KnowledgeApi.Services
{
    public class ArticleRepository : BaseMongoRepository<Article>
    {
        public ArticleRepository(string mongoDBConnectionString, string dbName, string collectionName) : base(mongoDBConnectionString, dbName, collectionName)
        {
        }
    }
}
