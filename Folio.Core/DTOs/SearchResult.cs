// Models/SearchResult.cs
public class SearchResult
{
    public string FileId { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string Snippet { get; set; } = string.Empty;
    public float Score { get; set; }
}