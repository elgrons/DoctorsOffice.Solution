using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace DocOffice.Models
{
  public class Patient
  {
    public int PatientId { get; set; }
    [Required(ErrorMessage = "The patient name field can't be empty!")]
    public string PatientName { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "You must add a doctor to a patient. Have you added a patient yet?")]
    public DateTime PatientBirthday { get; set; }
    public List<DoctorPatient> JoinEntities { get; }
  }
}