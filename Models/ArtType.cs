using MongoDB.Bson.Serialization.Attributes;

namespace KnowledgeApi.Models
{
    public class ArtType: MongoBaseModel
    {
        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("dates")]
        public string Dates { get; set; }

    }
}
