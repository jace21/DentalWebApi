using System.Web.Mvc;

namespace WebApplication.Controllers
{
  /// <prologue>
  ///
  /// <file name="DoctorController.cs"/>
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
  /// the 'Doctor' related operations.
  /// </summary>
  ///
  /// <remarks>
  /// N/A
  /// </remarks>
  public class DoctorController : Controller
  {
    #region Get Actions

    /// <summary>
    /// Get the Doctor Page and display it on the view.
    /// </summary>
    /// 
    /// <remarks>
    /// GET operation.
    /// </remarks>
    /// 
    /// <returns>
    /// Returns the index view of the Doctor page.
    /// </returns>
    public ActionResult Index(string id)
    {
      return View("Index", new Models.DoctorViewModel(id));
    }

    #endregion
  }
}