using KnowledgeApi.Models;


namespace KnowledgeApi.Services
{
    public class ArtTypeRepository: BaseMongoRepository<ArtType>
    {
        public ArtTypeRepository(string mongoDBConnectionString, string dbName, string collectionName) : base(mongoDBConnectionString, dbName, collectionName)
        {

        }
    }
}
