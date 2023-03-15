using Microsoft.AspNetCore.Mvc;
using DocOffice.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DocOffice.Controllers
{
  public class HomeController : Controller
  {
    private readonly DocOfficeContext _db;
    
    public HomeController(DocOfficeContext db)
    {
      _db = db;
    }

    [HttpGet("/")]
    public ActionResult Index()
    {
      Doctor[] doctors = _db.Doctors.ToArray();
      Patient[] patients = _db.Patients.ToArray();
      Dictionary<string,object[]> model = new Dictionary<string, object[]>();
      model.Add("doctors", doctors);
      model.Add("patients", patients);
      return View(model);
    }
  }
}