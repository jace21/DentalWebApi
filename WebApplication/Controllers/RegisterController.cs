using System.Web.Mvc;

namespace WebApplication.Controllers
{
  /// <prologue>
  ///
  /// <file name="RegisterController.cs"/>
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
  /// the 'Register' related operations.
  /// </summary>
  ///
  /// <remarks>
  /// N/A
  /// </remarks>
  public class RegisterController : Controller
  {
    #region Get Actions

    /// <summary>
    /// Get the Register Page and display it on the view.
    /// </summary>
    /// 
    /// <remarks>
    /// GET operation.
    /// </remarks>
    /// 
    /// <returns>
    /// Returns the index view of the register page.
    /// </returns>
    [AllowAnonymous]
    public ActionResult Register()
    {
      return View();
    }

    #endregion
  }
}