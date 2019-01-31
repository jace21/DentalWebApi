using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
  /// <prologue>
  ///
  /// <file name="CalendarController.cs"/>
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
  /// the 'Calendar' related operations.
  /// </summary>
  ///
  /// <remarks>
  /// N/A
  /// </remarks>
  public class CalendarController : Controller
  {
    #region Private Members

    /// <summary>
    /// Database used to store events.
    /// </summary>
    private ProductionDatabaseEntities db = new ProductionDatabaseEntities();

    #endregion

    #region Get Actions

    /// <summary>
    /// Get Calendar events based on the Doctor's ID
    /// </summary>
    /// 
    /// <param name="Id">
    /// Doctor's GUID
    /// </param>
    /// 
    /// <returns>
    /// Returns JSON with all event's related to the Id.
    /// </returns>
    public ActionResult GetCalendarEvents(string Id)
    {
      var events = new List<EventModel>();

      // Get all the schedules
      var schedules = db.Schedules.Where(x => x.DoctorId.ToString() == Id);

      foreach (var schedule in schedules)
      {
        var e = new EventModel()
        {
          id = schedule.EventId.ToString(),
          patientId = schedule.PatientId.ToString(),
          patientName = schedule.Patient.Name,
          start = schedule.StartDate,
          end = schedule.EndDate,
          title = schedule.Subject,
          description = schedule.Description
        };
        events.Add(e);
      }
      return Json(events.ToArray(), JsonRequestBehavior.AllowGet);
    }

    #endregion

    #region Post Actions

    /// <summary>
    /// Remove events from the database.
    /// </summary>
    /// 
    /// <param name="id">
    /// Event ID of the event that is removed.
    /// </param>
    /// 
    /// <returns>
    /// Returns the status of the post operation.
    /// </returns>
    [HttpPost]
    public JsonResult DeleteEvent(string id)
    {
      var status = false;
      Guid guid = Guid.Parse(id);

      var eventToRemove = db.Schedules.Where(a => a.EventId == guid).FirstOrDefault();

      if (eventToRemove != null)
      {
        db.Schedules.Remove(eventToRemove);
        db.SaveChanges();
        status = true;
      }

      return new JsonResult { Data = new { status = status } };
    }

    /// <summary>
    /// Save event to the database.
    /// </summary>
    /// 
    /// <param name="id">
    /// Event Model with properties that will be either updated or
    /// saved to the database.
    /// </param>
    /// 
    /// <returns>
    /// Returns the status of the post operation.
    /// </returns>
    [HttpPost]
    public JsonResult SaveEvent(EventModel e)
    {
      var status = false;

      if (!string.IsNullOrWhiteSpace(e.id))
      {
        var guid = Guid.Parse(e.id);

        //Update the event.
        var eventVM = db.Schedules.Where(a => a.EventId == guid).FirstOrDefault();

        if (eventVM != null)
        {
          eventVM.StartDate = e.start;
          eventVM.EndDate = e.end;
          eventVM.Description = e.description;
          eventVM.Subject = e.title;
        }
      }
      else
      {
        // Create a new event.
        var patientGuid = Guid.Parse(e.patientId);
        var doctorGuid = Guid.Parse(e.doctorId);

        Schedule newSchedule = new Schedule()
        {
          Description = e.description,
          StartDate = e.start,
          EndDate = e.end,
          PatientId = patientGuid,
          EventId = Guid.NewGuid(),
          Subject = e.title,
          DoctorId = doctorGuid
        };

        db.Schedules.Add(newSchedule);
      }

      db.SaveChanges();
      status = true;

      return new JsonResult { Data = new { status = status } };
    }

    #endregion

    #region IDisposable

    /// <summary>
    /// Disposes of the Database connection.
    /// </summary>
    /// 
    /// <param name="disposing">
    /// Determines if dispose already occurred to prevent disposing twice.
    /// </param>
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        db.Dispose();
      }
      base.Dispose(disposing);
    }

    #endregion
  }
}