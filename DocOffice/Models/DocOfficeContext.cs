using Microsoft.EntityFrameworkCore;

namespace DocOffice.Models
{
  public class DocOfficeContext : DbContext
  {
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<DoctorPatient> DoctorPatients { get; set; }
    public DocOfficeContext(DbContextOptions options) : base(options) { }
  }
}