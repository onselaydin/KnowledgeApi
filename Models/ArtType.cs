using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace KnowledgeApi.Models
{
    public class ArtType: MongoBaseModel
    {
        [BsonElement("title")]
        [Required(ErrorMessage = "Başlık alanı boş geçilemez.")]
        public string Title { get; set; }

        [BsonElement("description")]
        [Required(ErrorMessage = "Açıklama alanı boş geçilemez.")]
        public string Description { get; set; }

        [BsonElement("dates")]
        public string Dates { get; set; }

    }
}
