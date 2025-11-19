using TaskLists.Application.Common;
using TaskLists.Application.DTOs;
using TaskLists.Common;

namespace TaskLists.Application.Interfaces;

public interface ITaskListService
{
    Task<Result<PagedResult<TaskListDto>>> GetListsForUserAsync(
        string userId,
        Pager pager,
        CancellationToken ct);
}
