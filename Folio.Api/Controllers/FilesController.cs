using Folio.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Folio.Api.Controllers;

[ApiController]
[Route("api/files")]
public class FilesController : ControllerBase
{
    private static readonly string[] AllowedContentTypes =
    [
        "application/pdf",
        "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
        "text/plain"
    ];

    private readonly IUploadFileService _uploadService;

    public FilesController(IUploadFileService uploadService)
    {
        _uploadService = uploadService;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file, CancellationToken cancellationToken)
    {
        if (file.Length == 0)
            return BadRequest("File is empty.");

        if (!AllowedContentTypes.Contains(file.ContentType))
            return BadRequest("Unsupported file type. Allowed: PDF, DOCX, TXT.");

        var result = await _uploadService.UploadAsync(
            file.OpenReadStream(),
            file.FileName,
            file.ContentType,
            file.Length,
            cancellationToken);

        return CreatedAtAction(nameof(Upload), new { id = result.FileId }, result);
    }
}
