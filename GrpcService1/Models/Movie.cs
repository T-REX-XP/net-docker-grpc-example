using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace GrpcService1.Models
{
    public class Movie
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Title { get; set; }
        public string Year { get; set; }
        public string Rated { get; set; }
        public DateTime Released { get; set; }
        public string Runtime { get; set; }
        public string Director { get; set; }
    }
}