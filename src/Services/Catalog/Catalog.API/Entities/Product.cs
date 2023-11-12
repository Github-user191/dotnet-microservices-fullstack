using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.API.Entities {
    public class Product {
        [BsonId] // MongoDB Annotations
        [BsonRepresentation(BsonType.ObjectId)] // Generates random 24 character ID in MongoDB
        [BsonElement("Id")]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Category")]
        public string Category { get; set; }

        [BsonElement("Summary")]
        public string Summary { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }

        [BsonElement("ImageFile")]
        public string ImageFile { get; set; }

        [BsonElement("Price")]
        public decimal Price { get; set; }

        public override string ToString() {
            return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Category)}: {Category}, {nameof(Summary)}: {Summary}, {nameof(Description)}: {Description}, {nameof(ImageFile)}: {ImageFile}, {nameof(Price)}: {Price}";
        }


    }
}
