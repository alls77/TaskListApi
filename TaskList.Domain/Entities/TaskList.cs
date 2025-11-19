using TaskLists.Common;

namespace TaskLists.Domain.Entities;

public class TaskList
{
    public string Id { get; private set; }
    public string Name { get; private set; }
    public string OwnerId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public HashSet<string> SharedWith { get; private set; }

    private TaskList(string name, string ownerId, IEnumerable<string>? sharedWith)
    {
        Name = name;
        OwnerId = ownerId;
        CreatedAt = DateTime.Now;
        SharedWith = [.. sharedWith ?? []];
        SharedWith.Remove(OwnerId);
    }

    private TaskList(string id, string name, string ownerId, DateTime createdAt, IEnumerable<string>? sharedWith)
    {
        Id = id;
        Name = name;
        OwnerId = ownerId;
        CreatedAt = createdAt;
        SharedWith = [.. sharedWith ?? []];
    }

    public static Result<TaskList> Create(string name, string ownerId, IEnumerable<string>? sharedWith = null)
    {
        if (string.IsNullOrWhiteSpace(ownerId))
            return Result<TaskList>.Failure("OwnerId is required");

        if (string.IsNullOrWhiteSpace(name))
            return Result<TaskList>.Failure("Name is required");

        if (name.Length < 1 || name.Length > 255)
            return Result<TaskList>.Failure("Name must be between 1 and 255 characters");

        var entity = new TaskList(
            name: name,
            ownerId: ownerId,
            sharedWith: sharedWith
        );

        return Result<TaskList>.Success(entity);
    }

    public static TaskList Rehydrate(string id, string name, string ownerId, DateTime createdAt, IEnumerable<string>? sharedWith)
        => new(id, name, ownerId, createdAt, sharedWith);

    public void ShareWith(string userId)
    {
        SharedWith.Add(userId);
    }

    public void UnshareWith(string userId)
    {
        SharedWith.Remove(userId);
    }
}
