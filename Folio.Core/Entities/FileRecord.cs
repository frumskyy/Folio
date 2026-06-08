using Folio.Core.Common;

namespace Folio.Core.Entities;

public class FileRecord : BaseAuditableEntity
{
    public string OriginalFileName { get; set; } = string.Empty;
    public string BlobPath { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public long FileSizeBytes { get; set; }
    public ProcessingStatus Status { get; set; } = ProcessingStatus.Uploaded;
    public string? ExtractedText { get; set; }
    public DateTime? ProcessedAt { get; set; }
    public string? ErrorMessage { get; set; }
}