using Microsoft.AspNetCore.Mvc;
using DocOffice.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DocOffice.Controllers
{
  public class DoctorsController : Controller
  {
    private readonly DocOfficeContext _db;

    public DoctorsController(DocOfficeContext db)
    {
      _db = db;
    }
    public ActionResult Index()
    {
      return View(_db.Doctors.ToList());
    }

    public ActionResult Details(int id)
    {
      ViewBag.PatientId = new SelectList(_db.Patients, "PatientId", "PatientName");
      Doctor thisDoctor = _db.Doctors
          .Include(doctor => doctor.JoinEntities)
          .ThenInclude(join => join.Patient)
          .FirstOrDefault(doctor => doctor.DoctorId == id);
      return View(thisDoctor);
    }
    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Doctor doctor)
    {
      _db.Doctors.Add(doctor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddPatient(int id)
    {
      Doctor thisDoctor = _db.Doctors.FirstOrDefault(doctors => doctors.DoctorId == id);
      ViewBag.PatientId = new SelectList(_db.Patients, "PatientId", "PatientName");
      return View(thisDoctor);
    }

    [HttpPost]
    public ActionResult AddPatient(Doctor doctor, int patientId)
    {
      #nullable enable
      DoctorPatient? joinEntity = _db.DoctorPatients.FirstOrDefault(join => (join.PatientId == patientId && join.DoctorId == doctor.DoctorId));
      #nullable disable
      if (joinEntity == null && patientId != 0)
      {
        _db.DoctorPatients.Add(new DoctorPatient() { PatientId = patientId, DoctorId = doctor.DoctorId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = doctor.DoctorId });
    }

    public ActionResult Edit(int id)
    {
      Doctor thisDoctor = _db.Doctors.FirstOrDefault(doctors => doctors.DoctorId == id);
      return View(thisDoctor);
    }

    [HttpPost]
    public ActionResult Edit(Doctor doctor)
    {
      _db.Doctors.Update(doctor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Doctor thisDoctor = _db.Doctors.FirstOrDefault(doctors => doctors.DoctorId == id);
      return View(thisDoctor);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Doctor thisDoctor = _db.Doctors.FirstOrDefault(doctors => doctors.DoctorId == id);
      _db.Doctors.Remove(thisDoctor);
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
  }
}