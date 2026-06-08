namespace Folio.Application.Interfaces;

public interface IUploadFileService
{
    Task<UploadResult> UploadAsync(Stream fileStream, string fileName, string contentType, long fileSizeBytes, CancellationToken cancellationToken = default);
}

public record UploadResult(Guid FileId, string FileName, string Status);
