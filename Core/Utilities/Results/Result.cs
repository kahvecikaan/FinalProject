namespace Core.Utilities.Results;

public class Result : IResult
{
    // why protected constructor? because we don't want to create an instance of Result class,
    // we want to create an instance of ErrorResult or SuccessResult class
    protected Result(bool success, string? message) : this(success) 
    {
        Message = message; // read-only properties can be set in the constructor
    }

    protected Result(bool success)
    {
        Success = success; // read-only properties can be set in the constructor
    }
    public bool Success { get; }
    public string? Message { get; }
}