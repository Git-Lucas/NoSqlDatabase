using MongoDB.Driver;
using NoSqlDatabase.Models;

namespace NoSqlDatabase.Data;

public class DatabaseContextMongoDb
{
    private readonly MongoClient _client;
    private readonly IMongoDatabase _database;

    public DatabaseContextMongoDb(IConfiguration configuration)
    {
        _client = new(configuration["MongoDB:ConnectionString"]);
        _database = _client.GetDatabase(configuration["MongoDB:DatabaseName"]);
    }

    public IMongoCollection<T> GetCollection<T>() where T : BaseEntity
    {
        string collectionName = DatabaseUtils.GetNameCollection(typeof(T));

        return _database.GetCollection<T>(collectionName);
    }
}
