namespace Core.Utilities.Results;

public class Result : IResult
{
    public Result(bool success, string? message) : this(success)
    {
        Message = message; // read-only properties can be set in the constructor
    }
 
    public Result(bool success)
    {
        Success = success; // read-only properties can be set in the constructor
    }
    public bool Success { get; }
    public string? Message { get; }
}