using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DocOffice.Models
{
  public class Doctor
  {
    public int DoctorId { get; set; }
    public string DoctorName { get; set; }
    public string DoctorTitle { get; set; }
    public List<DoctorPatient> JoinEntities { get; }
  }
}