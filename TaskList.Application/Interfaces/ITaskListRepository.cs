using TaskLists.Application.Common;
using TaskLists.Application.DTOs;

namespace TaskLists.Application.Interfaces;

public interface ITaskListRepository
{
    /// <summary>
    /// Returns paged lists where userId is owner OR in sharedWith.
    /// </summary>
    Task<PagedResult<TaskListDto>> GetListsForUserAsync(
        string userId,
        Pager pager,
        Sort sorting,
        CancellationToken ct);
}