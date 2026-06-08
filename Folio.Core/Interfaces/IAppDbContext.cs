using Folio.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Folio.Core.Interfaces;

public interface IAppDbContext
{
    DbSet<FileRecord> Files { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
