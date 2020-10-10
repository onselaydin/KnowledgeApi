using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowledgeApi.Models
{
    public class Message: MongoBaseModel
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("subject")]
        public string Subject { get; set; }

        [BsonElement("message")]
        public string Msg { get; set; }

        [BsonElement("date")]
        public string Date { get; set; }

    }
}
