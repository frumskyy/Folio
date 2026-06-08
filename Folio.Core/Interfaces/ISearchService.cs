// Interfaces/ISearchService.cs
public interface ISearchService
{
    Task IndexDocumentAsync(Guid fileId, string fileName, string textContent);
    Task<IEnumerable<SearchResult>> SearchAsync(string query);
    Task DeleteDocumentAsync(Guid fileId);
}