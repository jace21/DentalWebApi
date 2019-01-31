using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
  /// <prologue>
  ///
  /// <file name="ManageController.cs"/>
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
  /// the 'Manage' related operations.
  /// </summary>
  ///
  /// <remarks>
  /// N/A
  /// </remarks>
  [Authorize]
  public class ManageController : Controller
  {
    #region Private Members

    /// <summary>
    /// Application User Manager
    /// </summary>
    private ApplicationUserManager m_userManager;

    /// <summary>
    /// The Authentication Manager
    /// </summary>
    private IAuthenticationManager AuthenticationManager
    {
      get
      {
        return HttpContext.GetOwinContext().Authentication;
      }
    }

    #endregion

    #region Constructor

    /// <summary>
    /// Parameter-less constructor
    /// </summary>
    public ManageController()
    {
    }

    /// <summary>
    /// Constructor that initializes the user manager.
    /// </summary>
    /// 
    /// <param name="userManager">
    /// The Application's user manager
    /// </param>
    public ManageController(ApplicationUserManager userManager)
    {
      UserManager = userManager;
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets the Application User Manager. Set is not exposed.
    /// </summary>
    public ApplicationUserManager UserManager
    {
      get
      {
        return m_userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
      }
      private set
      {
        m_userManager = value;
      }
    }

    #endregion

    #region Get Operations

    /// <summary>
    /// Get Index View
    /// </summary>
    ///
    /// <remarks>
    /// GET: /Manage/Index
    /// </remarks>
    /// 
    /// <param name="message">
    /// The message displyed on the View.
    /// </param>
    /// 
    /// <returns>
    /// Returns the Index View.
    /// </returns>
    public async Task<ActionResult> Index(ManageMessageId? message)
    {
      ViewBag.StatusMessage =
          message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
          : message == ManageMessageId.Error ? "An error has occurred."
          : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
          : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
          : "";

      var model = new IndexViewModel
      {
        HasPassword = HasPassword(),
        PhoneNumber = await UserManager.GetPhoneNumberAsync(User.Identity.GetUserId()),
        BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(User.Identity.GetUserId())
      };
      return View(model);
    }

    /// <summary>
    /// Navigate to the Add Phone Numbere View.
    /// </summary>
    /// 
    /// <remarks>
    /// GET: /Manage/AddPhoneNumber
    /// </remarks>
    /// 
    /// <returns>
    /// Returns the AddPhoneNumber View.
    /// </returns>
    public ActionResult AddPhoneNumber()
    {
      return View();
    }

    /// <summary>
    /// Verify the Phone Number.
    /// </summary>
    /// 
    /// <remarks>
    /// GET: /Manage/VerifyPhoneNumber
    /// </remarks>
    /// 
    /// <param name="phoneNumber">
    /// The Phone Number that will be verified
    /// </param>
    /// 
    /// <returns>
    /// Returns the Manage View if no errors occur otherwise return back to the same view and
    /// display an error message.
    /// </returns>
    public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
    {
      var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), phoneNumber);

      ViewBag.Code = code;

      // NO SMS for now any pretend code works.
      return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
    }

    /// <summary>
    /// Remove the associated Phone Number from the account.
    /// </summary>
    /// 
    /// <remarks>
    /// GET: /Manage/RemovePhoneNumber
    /// </remarks>
    /// 
    /// <returns>
    /// Returns to the Manage View.
    /// </returns>
    public async Task<ActionResult> RemovePhoneNumber()
    {
      var result = await UserManager.SetPhoneNumberAsync(User.Identity.GetUserId(), null);

      if (!result.Succeeded)
      {
        return RedirectToAction("Index", new { Message = ManageMessageId.Error });
      }

      var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

      if (user != null)
      {
        await SignInAsync(user, isPersistent: false);
      }

      return RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess });
    }

    /// <summary>
    /// Navigate to the Send Password View.
    /// </summary>
    /// <remarks>
    /// GET: /Manage/ChangePassword
    /// </remarks>
    /// 
    /// <returns>
    /// Returns the ChangePassword View.
    /// </returns>
    public ActionResult ChangePassword()
    {
      return View();
    }

    #endregion

    #region Post Operations

    /// <summary>
    /// Add Phone Numbere
    /// </summary>
    /// 
    /// <remarks>
    /// POST: /Manage/AddPhoneNumber
    /// </remarks>
    /// 
    /// <param name="model">
    /// Contains the Phone Number.
    /// </param>
    /// 
    /// <returns>
    /// Redirects the View to the Verify Phone Number. If an error occurs
    /// redirect back to the same page and display error.
    /// </returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return View(model);
      }

      // Generate the token
      var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), model.Number);

      // SMS Service to send token but no sms service

      return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
    }

    /// <summary>
    /// Verify if the code is valid.
    /// </summary>
    /// 
    /// <remarks>
    /// POST: /Manage/VerifyPhoneNumber
    /// </remarks>
    /// 
    /// <param name="model">
    /// Contains the Phone Number and the Verification Code
    /// </param>
    /// 
    /// <returns>
    /// Returns to the Index View or returns to the same view if an error occurs.
    /// </returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return View(model);
      }
      var token = await UserManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId(), model.PhoneNumber);
      var result = await UserManager.ChangePhoneNumberAsync(User.Identity.GetUserId(), model.PhoneNumber, token);

      if (result.Succeeded)
      {
        var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

        if (user != null)
        {
          await SignInAsync(user, isPersistent: false);
        }
        return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
      }

      // Error occurred Add error state and return to the same view.
      ModelState.AddModelError(string.Empty, "Failed to verify phone");

      return View(model);
    }

    /// <summary>
    /// Changes the password associated with the user.
    /// </summary>
    /// 
    /// <remarks>
    /// POST: /Manage/ChangePassword
    /// </remarks>
    /// 
    /// <param name="model">
    /// Contains the new password
    /// </param>
    /// 
    /// <returns>
    /// If valid return to the Index page, otherwise return to the same page and display a warning.
    /// </returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return View(model);
      }
      var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);

      if (result.Succeeded)
      {
        var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

        if (user != null)
        {
          await SignInAsync(user, isPersistent: false);
        }

        return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
      }

      // Error occurred add error and return view.
      AddErrors(result);

      return View(model);
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Sign In Async.
    /// </summary>
    /// 
    /// <param name="user">
    /// The Application Use that will sign in.
    /// </param>
    /// 
    /// <param name="isPersistent">
    /// Determines if the log in is persistent (i.e. remember me)
    /// </param>
    /// 
    /// <returns>
    /// N/A
    /// </returns>
    private async Task SignInAsync(ApplicationUser user, bool isPersistent)
    {
      AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
      AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager, CookieAuthenticationDefaults.AuthenticationType));
    }

    /// <summary>
    /// Add Error Message to the ModelState
    /// </summary>
    /// 
    /// <param name="result">
    /// Results from identity operation.
    /// </param>
    private void AddErrors(IdentityResult result)
    {
      foreach (var error in result.Errors)
      {
        ModelState.AddModelError(string.Empty, error);
      }
    }

    /// <summary>
    /// Check if user has passowrd
    /// </summary>
    /// 
    /// <returns>
    /// Returns true if user has a password, false otherwise.
    /// </returns>
    private bool HasPassword()
    {
      var user = UserManager.FindById(User.Identity.GetUserId());
      if (user != null)
      {
        return user.PasswordHash != null;
      }
      return false;
    }

    #endregion
  }

  /// <summary>
  /// MessageId Enums
  /// </summary>
  public enum ManageMessageId
  {
    AddPhoneSuccess,
    ChangePasswordSuccess,
    RemovePhoneSuccess,
    Error
  }
}