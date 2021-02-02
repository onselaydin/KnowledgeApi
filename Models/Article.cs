using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KnowledgeApi.Models
{
    public class Article : MongoBaseModel
    {
        [BsonElement("title")]
        //[Required(ErrorMessage = "Başlık alanı boş geçilemez.")]
        public string Title { get; set; }

        [BsonElement("content")]
        //[Required(ErrorMessage = "İçerik alanı boş geçilemez.")]
        public string Content { get; set; }

        [BsonElement("description")]
        //[Required(ErrorMessage = "Açıklama alanı boş geçilemez.")]
        public string Description { get; set; }

        [BsonElement("topics")]
        public string Topics { get; set; }

        [BsonElement("url")]
        //[Required(ErrorMessage = "Url alanı boş geçilemez.")]
        public string Url { get; set; }

        [BsonElement("articletype")]
        public string ArticleType { get; set; }

        [BsonElement("image")]
        //[Required(ErrorMessage = "Resim alanı boş geçilemez.")]
        public string Image{ get; set; }

        [BsonElement("dates")]
        public string Dates { get; set; }

        [BsonElement("arttypedetail")]
        public IList<ArtType> ArttypeDetail { get; set; }
    }
}
