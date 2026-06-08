// Interfaces/IFileStorageService.cs
public interface IFileStorageService
{
    Task<string> UploadAsync(Stream fileStream, string fileName, string contentType); //returns Blob path
    Task<Stream> DownloadAsync(string blobPath);
    Task DeleteAsync(string blobPath);
}