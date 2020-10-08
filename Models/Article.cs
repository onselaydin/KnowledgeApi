using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace KnowledgeApi.Models
{
    public class Article : MongoBaseModel
    {
        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("content")]
        public string Content { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("topics")]
        public string Topics { get; set; }

        [BsonElement("url")]
        public string Url { get; set; }

        [BsonElement("articletype")]
        public string ArticleType { get; set; }

        [BsonElement("image")]
        public string Image{ get; set; }

        [BsonElement("dates")]
        public string Dates { get; set; }

        [BsonElement("arttypedetail")]
        public IList<ArtType> ArttypeDetail { get; set; }
    }
}
