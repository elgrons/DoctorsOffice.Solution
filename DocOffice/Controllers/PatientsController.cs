using Microsoft.AspNetCore.Mvc;
using DocOffice.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DocOffice.Controllers
{
  public class PatientsController : Controller
  {
        private readonly DocOfficeContext _db;

    public PatientsController(DocOfficeContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Patients.ToList());
    }

    public ActionResult Details(int id)
    {
      Patient thisPatient = _db.Patients
          .Include(patient => patient.JoinEntities)
          .ThenInclude(join => join.Doctor)
          .FirstOrDefault(patient => patient.PatientId == id);
      return View(thisPatient);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Patient patient)
    {
      _db.Patients.Add(patient);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddDoctor(int id)
    {
      Patient thisPatient = _db.Patients.FirstOrDefault(patients => patients.PatientId == id);
      ViewBag.DoctorId = new SelectList(_db.Doctors, "DoctorId", "DoctorName", "DoctorTitle");
      return View(thisPatient);
    }

    [HttpPost]
    public ActionResult AddDoctor(Patient patient, int doctorId)
    {
      #nullable enable
      DoctorPatient? joinEntity = _db.DoctorPatients.FirstOrDefault(join => (join.DoctorId == doctorId && join.PatientId == patient.PatientId));
      #nullable disable
      if (joinEntity == null && doctorId != 0)
      {
        _db.DoctorPatients.Add(new DoctorPatient() {
          DoctorId = doctorId, PatientId = patient.PatientId });
          _db.SaveChanges();
        }
        return RedirectToAction("Details", new { id = patient.PatientId});
      }

    public ActionResult Edit(int id)
    {
      Patient thisPatient = _db.Patients.FirstOrDefault(patients => patients.PatientId == id);
      return View(thisPatient);
    }

    [HttpPost]
    public ActionResult Edit(Patient patient)
    {
      _db.Patients.Update(patient);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Delete(int id)
    {
      Patient thisPatient = _db.Patients.FirstOrDefault(patients => patients.PatientId == id);
      return View(thisPatient);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Patient thisPatient = _db.Patients.FirstOrDefault(patients => patients.PatientId == id);
      _db.Patients.Remove(thisPatient);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

      [HttpPost]
      public ActionResult DeleteJoin(int joinId)
      {
        DoctorPatient joinEntry = _db.DoctorPatients.FirstOrDefault(entry => entry.DoctorPatientId == joinId);
        _db.DoctorPatients.Remove(joinEntry);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }

      // public static void OrderByBday()
      // {
      //   Patient[] patients = { new Patient { Birthdate= },
      //                 new Patient { Birthdate=4 },
      //                 new Patient {  Birthdate=1 } };

      //   IEnumerable<Patient> query = patients.OrderBy(patient => patient.Birthdate);

      //   foreach (Patient patient in query)
      //   {
      //       Console.WriteLine("{0} - {1}", patient.Birthdate);
      //   }
      // }
    }
  }