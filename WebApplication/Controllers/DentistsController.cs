using System.Linq;
using System.Web.Http;
using WebApplication.Models;

namespace WebApplication.Controllers
{
  /// <prologue>
  ///
  /// <file name="DentistsController.cs"/>
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
  /// the 'Dentist' related operations.
  /// </summary>
  ///
  /// <remarks>
  /// N/A
  /// </remarks>
  public class DentistsController : ApiController
  {
    #region Private Members

    /// <summary>
    /// Entity Framework connection to database.
    /// </summary>
    private ProductionDatabaseEntities db = new ProductionDatabaseEntities();

    #endregion

    #region Get Operations

    /// <summary>
    /// Gets all dentists.
    /// </summary>
    /// 
    /// <remarks>
    /// api/Dentists
    /// </remarks>
    /// 
    /// <returns>
    /// Returns all dentists within database.
    /// </returns>
    public IQueryable<Dentist> GetDentists()
    {
      return db.Dentists;
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