namespace redpen.abstractions.Results;

public class ChatOperationResult : OperationResult
{
    public TimeSpan TotalDuration { get; set; }
    public long TotalTokenUsage { get; set; }
}