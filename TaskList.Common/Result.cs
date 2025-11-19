namespace TaskLists.Common;

public class Result<T>
{
    public bool IsSuccess => Error is null;
    public bool IsFailure => !IsSuccess;
    public string? Error { get; } = null;
    public T? Value { get; }


    private Result(string error)
    {
        Error = error;
    }

    private Result(T value)
    {
        Value = value;
    }

    public static Result<T> Success(T value) => new(value);
    public static Result<T> Failure(string error) => new(error);
}
