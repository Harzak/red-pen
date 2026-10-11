namespace redpen.abstractions.Options
{
    public class ChatClientOptions
    {
        public string Model { get; set; } = string.Empty;
        public Uri? Url { get; set; }
        public int TimeoutSeconds { get; set; } = 100;
    }
}
