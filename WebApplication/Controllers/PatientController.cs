using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
  /// <prologue>
  ///
  /// <file name="PatientController.cs"/>
  ///
  /// <remarks>
  /// N/A
  /// </remarks>
  ///
  /// <author name="Jason Truong" date="01/27/19"/>
  /// 
  /// <history>
  ///   <entry date="01/27/19" author="Jason Truong" scr="" 
  ///          desc="Created"/> 
  /// </history>
  ///
  /// </prologue>
  /// 
  /// <summary>
  /// Controller for handling Get and Post operations for
  /// the 'Patient' related operations.
  /// </summary>
  ///
  /// <remarks>
  /// N/A
  /// </remarks>
  public class PatientController : Controller
  {
    #region Get Actions

    /// <summary>
    /// Get the Index Page and display it on the view.
    /// </summary>
    /// 
    /// <remarks>
    /// GET operation.
    /// </remarks>
    /// 
    /// <returns>
    /// Returns the index view of the Patient page.
    /// </returns>
    public ActionResult Index(string id)
    {
      return View("Index", new PatientModel(id));
    }

    /// <summary>
    /// Get all associated patients to the doctor ID.
    /// </summary>
    /// 
    /// <param name="id">
    /// Doctor's GUID.
    /// </param>
    /// 
    /// <returns>
    /// Returns the View.
    /// </returns>
    public ActionResult MyPatients(string id)
    {
      // List of patients
      IList<PatientModel> patients = new List<PatientModel>();

      using (ProductionDatabaseEntities entities = new ProductionDatabaseEntities())
      {
        Guid guid = Guid.Parse(id);
        Dentist doctor = entities.Dentists.Find(guid);

        foreach (var patient in doctor.Patients)
        {
          PatientModel newPatient = new PatientModel()
          {
            Address = patient.Address,
            Birthday = patient.Birthday.ToString("MMMM dd, yyyy"),
            City = patient.City,
            Name = patient.Name,
            StateCode = patient.StateCode,
            ZipCode = patient.ZipCode,
            DoctorId = patient.DoctorId.ToString()
          };

          // Format string for view only.
          newPatient.AddressString = string.Format("{0}, {1}, {2}, {3}", newPatient.Address,
                                                                         newPatient.City,
                                                                         newPatient.StateCode,
                                                                         newPatient.ZipCode);

          if (patient.Profile != null)
          {
            string str = Convert.ToBase64String(patient.Profile);
            str = @"data:image/png;base64," + str;
            newPatient.Profile = str;
          }

          patients.Add(newPatient);
        }
      }
      return View("MyPatients", new PatientListModel(id, patients));
    }

    #endregion

    #region Post Actions

    /// <summary>
    /// Add new patient to the database.
    /// </summary>
    /// 
    /// <param name="model">
    /// The patient model displayed on the view.
    /// </param>
    /// 
    /// <returns>
    /// Returns the View.
    /// </returns>
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public ActionResult RegisterPatient(PatientModel model)
    {
      if (ModelState.IsValid)
      {
        using (ProductionDatabaseEntities entities = new ProductionDatabaseEntities())
        {
          Patient patient = new Patient()
          {
            Address = model.Address,
            City = model.City,
            Id = Guid.NewGuid(),
            Name = model.Name,
            ZipCode = model.ZipCode,
            StateCode = model.StateCode
          };
          patient.Birthday = DateTime.Parse(model.Birthday);
          patient.DoctorId = Guid.Parse(model.DoctorId);
          entities.Patients.Add(patient);
          entities.SaveChanges();
        }
        return RedirectToAction("MyPatients", "Patient", new { id = model.DoctorId });
      }

      // Return to orginal page because it operation failed.
      return View("Index", model);
    }

    #endregion
  }
}