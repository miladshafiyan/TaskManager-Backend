using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TaskManager.Models
{
    public class task
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }
        public string username { get; set; } = null!;
        public string email { get; set; } = null!;
        public string text { get; set; } = null!;
        public int? status { get; set; }

    }
}
