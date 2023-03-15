using Microsoft.EntityFrameworkCore;

namespace DocOffice.Models
{
  public class DocOfficeContext : DbContext
  {
    public DbSet<ClassName> ClassName { get; set; }

    public DocOfficeContext(DbContextOptions options) : base(options) { }
  }
}