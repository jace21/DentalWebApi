using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
  /// <prologue>
  ///
  /// <file name="LoginController.cs"/>
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
  /// the 'Login' related operations.
  /// </summary>
  ///
  /// <remarks>
  /// N/A
  /// </remarks>
  public class LoginController : Controller
  {
    #region Private Members

    /// <summary>
    /// Application User Manager.
    /// </summary>
    private ApplicationUserManager m_userManager;

    /// <summary>
    /// Authentication Sign In Manager.
    /// </summary>
    private ApplicationSignInManager m_signInManager;

    /// <summary>
    /// Authentication Manager
    /// </summary>
    private IAuthenticationManager AuthenticationManager
    {
      get
      {
        return HttpContext.GetOwinContext().Authentication;
      }
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Get the user Manager. Set is not exposed.
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

    /// <summary>
    /// Get the Sign In Manager. Set is not exposed
    /// </summary>
    public ApplicationSignInManager SignInManager
    {
      get
      {
        return m_signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
      }
      private set { m_signInManager = value; }
    }

    #endregion

    #region Get Operations

    /// <summary>
    /// Navigate to Login Page.
    /// </summary>
    /// 
    /// <param name="returnUrl">
    /// URL of the Return page.
    /// </param>
    /// 
    /// <remarks>
    /// GET: /Login/Login
    /// </remarks>
    /// 
    /// <returns>
    /// Returns Login Page View.
    /// </returns>
    [AllowAnonymous]
    public ActionResult Login(string returnUrl)
    {
      ViewBag.ReturnUrl = returnUrl;

      return View();
    }

    /// <summary>
    /// Navigate to the Forgot Password page.
    /// </summary>
    /// 
    /// <remarks>
    /// GET: /Login/ForgotPassword
    /// </remarks>
    /// 
    /// <returns>
    /// Returns Forgot Password Page View.
    /// </returns>
    [AllowAnonymous]
    public ActionResult ForgotPassword()
    {
      return View();
    }

    /// <summary>
    /// Navigate to the Register page.
    /// </summary>
    /// 
    /// <remarks>
    /// GET: /Login/Register
    /// </remarks>
    /// 
    /// <returns>
    /// Returns Register Page View.
    /// </returns>
    [AllowAnonymous]
    public ActionResult Register()
    {
      return View();
    }

    /// <summary>
    /// Navigate to the Register page.
    /// </summary>
    /// 
    /// <remarks>
    /// GET: /Login/ForgotPasswordConfirmation
    /// </remarks>
    /// 
    /// <returns>
    /// Returns Register Page View.
    /// </returns>
    [AllowAnonymous]
    public ActionResult ForgotPasswordConfirmation()
    {
      ViewBag.Link = TempData["ViewBagLink"];

      return View();
    }

    #endregion

    #region Post Operations

    /// <summary>
    /// Signs the user in by verifying with sign in manager.
    /// </summary>
    /// 
    /// <remarks>
    /// POST: /Login/Login
    /// </remarks>
    /// 
    /// <returns>
    /// Returns to the Home Page on success.
    /// </returns>
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
    {
      if (!ModelState.IsValid)
      {
        return View(model);
      }

      var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);

      switch (result)
      {
        case SignInStatus.Success:
          return RedirectToLocal(returnUrl);
        case SignInStatus.RequiresVerification:
          return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
        case SignInStatus.Failure:
        default:
          ModelState.AddModelError(string.Empty, "Username or Password is incorrect.");
          return View(model);
      }
    }

    /// <summary>
    /// User gets an email confirmation with ability to reset password.
    /// </summary>
    /// 
    /// <remarks>
    /// POST: /Login/ForgotPassword
    /// </remarks>
    /// 
    /// <returns>
    /// Returns to the Confirmation Page.
    /// </returns>
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
    {
      if (ModelState.IsValid)
      {
        var user = await UserManager.FindByNameAsync(model.Email);

        if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
        {
          // Don't reveal that the user does not exist or is not confirmed
          return View("ForgotPasswordConfirmation");
        }

        string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);

        var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);

        await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");

        TempData["ViewBagLink"] = callbackUrl;

        return RedirectToAction("ForgotPasswordConfirmation", "Login");
      }

      // Forgot Password Failed
      return View(model);
    }

    /// <summary>
    /// Register new account.
    /// </summary>
    /// 
    /// <remarks>
    /// POST: /Login/Register
    /// </remarks>
    /// 
    /// <returns>
    /// Returns to the Login Page.
    /// </returns>
    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Register(RegisterViewModel model)
    {
      if (ModelState.IsValid)
      {
        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        var result = await UserManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
          return RedirectToAction("Login", "Login");
        }
        AddErrors(result);
      }

      // Registration failed.
      return View(model);
    }

    /// <summary>
    /// Signs off of the currently logged in account.
    /// </summary>
    /// 
    /// <remarks>
    /// POST: /Login/LogOff
    /// </remarks>
    /// 
    /// <returns>
    /// Returns the Home Page View.
    /// </returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult LogOff()
    {
      AuthenticationManager.SignOut();

      return RedirectToAction("Index", "Home");
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Add errors to the Model state.
    /// </summary>
    /// 
    /// <param name="result">
    /// Results of the identity operation.
    /// </param>
    private void AddErrors(IdentityResult result)
    {
      foreach (var error in result.Errors)
      {
        ModelState.AddModelError(string.Empty, error);
      }
    }

    /// <summary>
    /// Returns to the Home Page.
    /// </summary>
    /// 
    /// <param name="returnUrl">
    /// Link to the return URL
    /// </param>
    /// 
    /// <returns>
    /// Returns Home Page View.
    /// </returns>
    private ActionResult RedirectToLocal(string returnUrl)
    {
      if (Url.IsLocalUrl(returnUrl))
      {
        return Redirect(returnUrl);
      }
      return RedirectToAction("Index", "Home");
    }

    #endregion
  }
}