using TaskLists.Common;

namespace TaskLists.Application.Common;

public class Pager
{
    public int Page { get; set; }
    public int PageSize { get; set; }

    private Pager(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }

    public static Result<Pager> Create(int page, int pageSize)
    {
        if (page < 1)
            return Result<Pager>.Failure("Page must be greater than 0");

        if (pageSize < 1 || pageSize > 100)
            return Result<Pager>.Failure("Page size must be between 1 and 100");

        return Result<Pager>.Success(new Pager(page, pageSize));
    }
}
