using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace NoSqlDatabase.Models;

public interface IEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    Guid Id => Guid.NewGuid();
}
