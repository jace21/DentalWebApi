using System.Web.Mvc;

namespace WebApplication.Controllers
{
  /// <prologue>
  ///
  /// <file name="HomeController.cs"/>
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
  /// the 'Home' related operations.
  /// </summary>
  ///
  /// <remarks>
  /// N/A
  /// </remarks>
  public class HomeController : Controller
  {
    #region Get operations

    /// <summary>
    /// Get the Home Page and display it on the view.
    /// </summary>
    /// 
    /// <remarks>
    /// GET operation.
    /// </remarks>
    /// 
    /// <returns>
    /// Returns the index view of the Home page.
    /// </returns>
    public ActionResult Index()
    {
      ViewBag.Title = "Home Page";

      return View();
    }

    #endregion
  }
}
