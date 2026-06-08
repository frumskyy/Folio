using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Folio.Infrastructure.ExternalServices;

public class AzureBlobStorageService : IFileStorageService
{
    private readonly BlobContainerClient _container;

    public AzureBlobStorageService(BlobServiceClient blobServiceClient, string containerName)
    {
        _container = blobServiceClient.GetBlobContainerClient(containerName);
    }

    public async Task<string> UploadAsync(Stream fileStream, string fileName, string contentType)
    {
        var blobPath = $"{Guid.NewGuid()}/{fileName}";
        var blob = _container.GetBlobClient(blobPath);

        await blob.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = contentType });

        return blobPath;
    }

    public async Task<Stream> DownloadAsync(string blobPath)
    {
        var blob = _container.GetBlobClient(blobPath);
        var response = await blob.DownloadStreamingAsync();
        return response.Value.Content;
    }

    public async Task DeleteAsync(string blobPath)
    {
        var blob = _container.GetBlobClient(blobPath);
        await blob.DeleteIfExistsAsync();
    }
}
