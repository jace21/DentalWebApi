using System.Linq;
using System.Web.Http;
using WebApplication.Models;

namespace WebApplication.Controllers
{
  /// <prologue>
  ///
  /// <file name="PatientsController.cs"/>
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
  /// Controller for handling Get operation for 
  /// retrieving all patients from database.
  /// </summary>
  ///
  /// <remarks>
  /// N/A
  /// </remarks>
  public class PatientsController : ApiController
  {
    #region Private Members

    /// <summary>
    /// Entity Framework connection to database.
    /// </summary>
    private ProductionDatabaseEntities db = new ProductionDatabaseEntities();

    #endregion

    #region Get Operations

    /// <summary>
    /// Gets all patients.
    /// </summary>
    /// 
    /// <remarks>
    /// api/Patients
    /// </remarks>
    /// 
    /// <returns>
    /// Returns all patients within database.
    /// </returns>
    public IQueryable<Patient> GetPatients()
    {
      return db.Patients;
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