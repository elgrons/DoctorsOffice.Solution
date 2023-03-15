using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DocOffice.Models
{
  public class Doctor
  {
    public int DoctorId { get; set; }
    [Required(ErrorMessage = "The doctors's name can't be empty!")]
    public string DoctorName { get; set; }
    //[Range(1, int.MaxValue, ErrorMessage = "You must add a patient to a doctor. Have you added a doctor yet?")]
    public string DoctorTitle { get; set; }
    public List<DoctorPatient> JoinEntities { get; }
  }
}