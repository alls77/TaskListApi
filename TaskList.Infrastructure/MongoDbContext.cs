using MongoDB.Driver;
using TaskLists.Infrastructure.Models;

namespace TaskLists.Infrastructure;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IMongoDatabase db)
    {
        _database = db;
    }

    public IMongoCollection<TaskListDocument> TaskLists =>
        _database.GetCollection<TaskListDocument>("tasklists");
}