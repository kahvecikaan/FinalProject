namespace Core.Utilities.Results;

public interface IResult
{
    bool Success { get; } // read-only
    string? Message { get; } // read-only
}