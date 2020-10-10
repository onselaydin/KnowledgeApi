using KnowledgeApi.Models;


namespace KnowledgeApi.Services
{
    public class MessageRepository : BaseMongoRepository<Article>
    {
        public MessageRepository(string mongoDBConnectionString, string dbName, string collectionName) : base(mongoDBConnectionString, dbName, collectionName)
        {
        }
    }
}
