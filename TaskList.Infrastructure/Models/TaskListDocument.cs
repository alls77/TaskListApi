using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TaskLists.Infrastructure.Models;

public class TaskListDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string OwnerId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public List<string>? SharedWith { get; set; } = [];
}