using Microsoft.AspNetCore.Mvc;
using TaskLists.Api.Common;
using TaskLists.Application.Common;
using TaskLists.Application.DTOs;
using TaskLists.Application.Interfaces;

namespace TaskLists.Api.Controllers;

[ApiController]
[Route("api/taskLists")]
public class TaskListApiController : ControllerBase
{
    private readonly ITaskListService _taskListService;

    public TaskListApiController(ITaskListService taskListService)
    {
        _taskListService = taskListService;
    }

    /// <summary>
    /// Get all task lists for a user (owned or shared) with pagination
    /// </summary>
    /// <param name="userId">Current user ID (from query)</param>
    /// <param name="page">Page number</param>
    /// <param name="pageSize">Items per page</param>
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<TaskListDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorStatus), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorStatus), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUserTaskLists(
        [FromQuery] string userId,
        [FromQuery] int page,
        [FromQuery] int pageSize,
        CancellationToken ct)
    {
        var pagerResult = Pager.Create(page, pageSize);

        if (pagerResult.IsFailure)
            return BadRequest(new ErrorStatus(400, pagerResult.Error!));

        var result = await _taskListService.GetListsForUserAsync(userId, pagerResult.Value!, ct);

        return result.IsSuccess
            ? Ok(result.Value)
            : StatusCode(500, new ErrorStatus(500, result.Error!));
    }
}
