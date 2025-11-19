namespace TaskLists.Infrastructure.Options;

public class TaskListsDbOptions
{
    public const string SectionName = "TaskListsDb";

    public required string ConnectionString { get; set; } = null!;
    public required string DatabaseName { get; set; } = null!;
}
