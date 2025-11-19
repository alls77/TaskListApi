using MongoDB.Driver;
using TaskLists.Application.Common;
using TaskLists.Application.DTOs;
using TaskLists.Application.Interfaces;
using TaskLists.Infrastructure.Models;

namespace TaskLists.Infrastructure.Repositories;

public class TaskListRepository : ITaskListRepository
{
    private readonly MongoDbContext _context;

    public TaskListRepository(MongoDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<TaskListDto>> GetListsForUserAsync(
        string userId,
        Pager pager,
        Sort sorting,
        CancellationToken ct)
    {
        var filter = Builders<TaskListDocument>.Filter.Or(
            Builders<TaskListDocument>.Filter.Eq(x => x.OwnerId, userId),
            Builders<TaskListDocument>.Filter.AnyEq(x => x.SharedWith, userId)
        );

        var sort = sorting.SortDesc
            ? Builders<TaskListDocument>.Sort.Descending(sorting.SortBy)
            : Builders<TaskListDocument>.Sort.Ascending(sorting.SortBy);

        var projection = Builders<TaskListDocument>.Projection.Expression(x => new TaskListDto(x.Id, x.Name));

        var items = await _context.TaskLists
            .Find(filter)
            .Sort(sort)
            .Skip((pager.Page - 1) * pager.PageSize)
            .Limit(pager.PageSize)
            .Project(projection)
            .ToListAsync(ct);

        var total = await _context.TaskLists.CountDocumentsAsync(filter, cancellationToken: ct);

        return new PagedResult<TaskListDto>(items, pager.Page, pager.PageSize, (int)total);
    }
}
