using MongoDB.Bson;
using MongoDB.Driver;

namespace NoSqlDatabase.Data;

public class DatabaseContextMongoDb(IConfiguration configuration)
{
    private readonly MongoClient _client = new(configuration.GetConnectionString("DefaultConnection"));
    private Dictionary<string, List<string>>? _databasesAndCollections;

    public async Task<Dictionary<string, List<string>>> GetDatabasesAndCollectionsAsync()
    {
        if (_databasesAndCollections is not null)
        {
            return _databasesAndCollections;
        }

        _databasesAndCollections = [];

        IAsyncCursor<string> databasesResult = _client.ListDatabaseNames();

        await databasesResult.ForEachAsync(async databaseName =>
        {
            List<string> collectionNames = [];
            IMongoDatabase database = _client.GetDatabase(databaseName);
            IAsyncCursor<string> collectionNamesResult = database.ListCollectionNames();
            await collectionNamesResult.ForEachAsync(collectionName =>
            {
                collectionNames.Add(collectionName);
            });
            _databasesAndCollections.Add(databaseName, collectionNames);
        });

        return _databasesAndCollections;
    }

    public async Task<BsonDocument> GetDocumentAsync(string databaseName, string collectionName, int index = 0)
    {
        IMongoCollection<BsonDocument> collection = GetCollection(databaseName, collectionName);

        BsonDocument document = [];

        await collection
            .Find(doc => true)
            .Skip(index)
            .Limit(1)
            .ForEachAsync(doc => document = doc);

        return document;
    }

    public async Task<long> GetCollectionCountAsync(string databaseName, string collectionName)
    {
        IMongoCollection<BsonDocument> collection = GetCollection(databaseName, collectionName);
        return await collection.EstimatedDocumentCountAsync();
    }

    public async Task<UpdateResult> CreateOrUpdateFieldAsync(string databaseName, string collectionName, string id, string fieldName, string value)
    {
        IMongoCollection<BsonDocument> collection = GetCollection(databaseName, collectionName);
        UpdateDefinition<BsonDocument> update = Builders<BsonDocument>.Update.Set(fieldName, new BsonString(value));
        
        return await collection.UpdateOneAsync(CreateIdFilter(id), update);
    }

    public async Task<DeleteResult> DeleteDocumentAsync(string databaseName, string collectionName, string id)
    {
        IMongoCollection<BsonDocument> collection = GetCollection(databaseName, collectionName);

        return await collection.DeleteOneAsync(CreateIdFilter(id));
    }

    public async Task CreateDocumentAsync(string databaseName, string collectionName)
    {
        IMongoCollection<BsonDocument> collection = GetCollection(databaseName, collectionName);

        await collection.InsertOneAsync([]);
    }

    private IMongoCollection<BsonDocument> GetCollection(string databaseName, string collectionName)
    {
        IMongoDatabase db = _client.GetDatabase(databaseName);
        return db.GetCollection<BsonDocument>(collectionName);
    }

    private static BsonDocument CreateIdFilter(string id)
    {
        return new BsonDocument("_id", new BsonObjectId(new ObjectId(id)));
    }
}
