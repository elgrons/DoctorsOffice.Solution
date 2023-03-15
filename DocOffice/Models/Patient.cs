using System.Collections.Generic;
using System;

namespace DocOffice.Models
{
  public class Patient
  {
    public int PatientId { get; set; }
    public string PatientName { get; set; }

    public DateTime PatientBirthday { get; set; }
    public List<DoctorPatient> JoinEntities { get; }
  }
}