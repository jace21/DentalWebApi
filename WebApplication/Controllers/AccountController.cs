using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebApplication.Models;

namespace WebApplication.Controllers
{
  /// <prologue>
  ///
  /// <file name="AccountController.cs"/>
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
  /// the 'Account' related operations.
  /// </summary>
  ///
  /// <remarks>
  /// N/A
  /// </remarks>
  [Authorize]
  [RoutePrefix("api/Account")]
  public class AccountController : ApiController
  {
    #region Private Members

    /// <summary>
    /// Login Provider
    /// </summary>
    private const string LocalLoginProvider = "Local";

    /// <summary>
    /// Application User Manager.
    /// </summary>
    private ApplicationUserManager m_userManager;

    /// <summary>
    /// Get Authentication Manager.
    /// </summary>
    private IAuthenticationManager Authentication
    {
      get { return Request.GetOwinContext().Authentication; }
    }

    #endregion

    #region Constructor

    /// <summary>
    /// Parameter-less constructor
    /// </summary>
    public AccountController()
    {
    }

    /// <summary>
    /// Initialize  Constructor with user manager and
    /// format for access token.
    /// </summary>
    /// 
    /// <param name="userManager">
    /// The user manager.
    /// </param>
    /// 
    /// <param name="accessTokenFormat">
    /// Access Token Format.
    /// </param>
    public AccountController(ApplicationUserManager userManager,
        ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
    {
      UserManager = userManager;
      AccessTokenFormat = accessTokenFormat;
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets the User Manager. Set is not exposed.
    /// </summary>
    public ApplicationUserManager UserManager
    {
      get
      {
        return m_userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
      }
      private set
      {
        m_userManager = value;
      }
    }

    /// <summary>
    /// Get the Access Token Format. Set is not exposed.
    /// </summary>
    public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

    #endregion

    #region Get Operations

    /// <summary>
    /// Get Error Results
    /// </summary>
    /// 
    /// <param name="result">
    /// Results of an identity operation.
    /// </param>
    /// 
    /// <returns>
    /// Returns error results.
    /// </returns>
    private IHttpActionResult GetErrorResult(IdentityResult result)
    {
      if (result == null)
      {
        return InternalServerError();
      }

      if (!result.Succeeded)
      {
        if (result.Errors != null)
        {
          foreach (string error in result.Errors)
          {
            ModelState.AddModelError(string.Empty, error);
          }
        }

        if (ModelState.IsValid)
        {
          // No ModelState errors are available to send, so just return an empty BadRequest.
          return BadRequest();
        }

        return BadRequest(ModelState);
      }

      return null;
    }

    #endregion

    #region Post Operations

    /// <summary>
    /// Sign out of currently logged in account.
    /// </summary>
    /// 
    /// <remarks>
    /// POST to api/Account/Logout
    /// </remarks>
    /// 
    /// <returns>
    /// Returns Ok result on success otherwise returns error results
    /// </returns>
    [Route("Logout")]
    public IHttpActionResult Logout()
    {
      Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
      return Ok();
    }

    /// <summary>
    /// Changes the current password to new password.
    /// </summary>
    /// 
    /// <param name="model">
    /// Change password model contains
    /// new password.
    /// </param>
    /// 
    /// <remarks>
    /// POST to api/Account/ChangePassword
    /// </remarks>
    /// 
    /// <returns>
    /// Returns Ok result on success otherwise returns error results
    /// </returns>
    [Route("ChangePassword")]
    public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
          model.NewPassword);

      if (!result.Succeeded)
      {
        return GetErrorResult(result);
      }

      return Ok();
    }

    /// <summary>
    /// Adds new user information to user manager.
    /// </summary>
    /// 
    /// <param name="model">
    /// Register Model contains user name infor for
    /// newly registered account.
    /// </param>
    /// 
    /// <remarks>
    /// POST to api/Account/Register
    /// </remarks>
    /// 
    /// <returns>
    /// Returns Ok result on success otherwise returns error results
    /// </returns>
    [AllowAnonymous]
    [Route("Register")]
    public async Task<IHttpActionResult> Register(RegisterBindingModel model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

      IdentityResult result = await UserManager.CreateAsync(user, model.Password);

      if (!result.Succeeded)
      {
        return GetErrorResult(result);
      }

      return Ok();
    }

    #endregion

    #region IDisposable

    /// <summary>
    /// Dispose of the user manager.
    /// </summary>
    /// 
    /// <param name="disposing">
    /// Used to determine if dispose already occurred to
    /// prevent multiple dispoing calls.
    /// </param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && m_userManager != null)
      {
        m_userManager.Dispose();
        m_userManager = null;
      }

      base.Dispose(disposing);
    }

    #endregion
  }
}
