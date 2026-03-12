namespace EnemPrep.Application.Common;

public class Result<T>
{
    public bool Success { get; private set; }
    public T? Data { get; private set; }
    public string? ErrorMessage { get; private set; }

    private Result() { }

    public static Result<T> Ok(T data) => new() { Success = true, Data = data };

    public static Result<T> Fail(string errorMessage) => new() { Success = false, ErrorMessage = errorMessage };
}

public class Result
{
    public bool Success { get; private set; }
    public string? ErrorMessage { get; private set; }

    private Result() { }

    public static Result Ok() => new() { Success = true };

    public static Result Fail(string errorMessage) => new() { Success = false, ErrorMessage = errorMessage };
}
