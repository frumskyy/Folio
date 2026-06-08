public class ProcessingMessage
{
    public Guid FileId { get; set; }
    public string BlobPath { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public string OriginalFileName { get; set; } = string.Empty;
}
