using TaskLists.Domain.Entities;
using TaskLists.Infrastructure.Models;

namespace TaskLists.Infrastructure.Mappers;

public static class TaskListMappers
{
    public static TaskList ToDomain(this TaskListDocument doc) =>
        TaskList.Rehydrate(
            id: doc.Id,
            name: doc.Name,
            ownerId: doc.OwnerId,
            createdAt: doc.CreatedAt,
            sharedWith: doc?.SharedWith
        );

    public static TaskListDocument ToDocument(this TaskList domain) =>
        new()
        {
            Id = domain.Id,
            Name = domain.Name,
            OwnerId = domain.OwnerId,
            CreatedAt = domain.CreatedAt,
            SharedWith = [.. domain.SharedWith]
        };
}
