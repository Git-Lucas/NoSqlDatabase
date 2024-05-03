using MongoDB.Bson.Serialization.Attributes;

namespace NoSqlDatabase.Models;

public abstract class BaseEntity
{
    [BsonId]
    public Guid Id { get; protected set; }
}
