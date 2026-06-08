using Folio.Application.Interfaces;
using Folio.Core.Entities;
using Folio.Core.Interfaces;

namespace Folio.Application.Services;

public class UploadFileService : IUploadFileService
{
    private readonly IAppDbContext _db;
    private readonly IFileStorageService _storage;
    private readonly IMessageQueueService _queue;

    public UploadFileService(IAppDbContext db, IFileStorageService storage, IMessageQueueService queue)
    {
        _db = db;
        _storage = storage;
        _queue = queue;
    }

    public async Task<UploadResult> UploadAsync(Stream fileStream, string fileName, string contentType, long fileSizeBytes, CancellationToken cancellationToken = default)
    {
        var blobPath = await _storage.UploadAsync(fileStream, fileName, contentType);

        var record = new FileRecord
        {
            OriginalFileName = fileName,
            BlobPath = blobPath,
            ContentType = contentType,
            FileSizeBytes = fileSizeBytes,
            Status = ProcessingStatus.Uploaded
        };

        _db.Files.Add(record);
        await _db.SaveChangesAsync(cancellationToken);

        await _queue.PublishAsync(new ProcessingMessage
        {
            FileId = record.Id,
            BlobPath = blobPath,
            ContentType = contentType,
            OriginalFileName = fileName
        });

        record.Status = ProcessingStatus.Queued;
        await _db.SaveChangesAsync(cancellationToken);

        return new UploadResult(record.Id, record.OriginalFileName, record.Status.ToString());
    }
}
