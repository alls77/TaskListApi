using TaskLists.Application.Common;
using TaskLists.Application.DTOs;
using TaskLists.Application.Interfaces;
using TaskLists.Common;
using TaskLists.Domain.Entities;

namespace TaskLists.Application.Services;

public class TaskListService : ITaskListService
{
    private readonly ITaskListRepository _repo;

    public TaskListService(ITaskListRepository repo)
    {
        _repo = repo;
    }

    public async Task<Result<PagedResult<TaskListDto>>> GetListsForUserAsync(
        string userId,
        Pager pager,
        CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(userId))
            return Result<PagedResult<TaskListDto>>.Failure("userId is required");

        var paged = await _repo.GetListsForUserAsync(userId, pager, new Sort(nameof(TaskList.CreatedAt), true), ct);
        return Result<PagedResult<TaskListDto>>.Success(paged);
    }
}