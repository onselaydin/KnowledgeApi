using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace KnowledgeApi.Models
{
    public class MongoBaseModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
