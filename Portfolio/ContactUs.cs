using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Portfolio
{
    public class ContactUs
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }  
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Message { get; set; }
        public DateTime SubmittedAt { get; set; }

    }
}
