using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication
{
  /// <prologue>
  ///
  /// <file name="ApplicationUserManager.cs"/>
  ///
  /// <remarks>
  /// N/A
  /// </remarks>
  ///
  /// 
  /// <author name="Jason Truong" date="01/27/19"/>
  /// 
  ///
  /// <history>
  ///   <entry date="01/27/19" author="Jason Truong" scr="" 
  ///          desc="Created"/> 
  /// </history>
  ///
  /// </prologue>
  /// <summary>
  /// Configure the application user manager used in this application.
  /// Used for user validation
  /// </summary>
  ///
  /// <remarks>
  /// N/A
  /// </remarks>
  public class ApplicationUserManager : UserManager<ApplicationUser>
  {
    #region Constructor

    /// <summary>
    /// Create Application User Manager
    /// </summary>
    /// 
    /// <param name="store">
    /// Storage of users
    /// </param>
    public ApplicationUserManager(IUserStore<ApplicationUser> store)
        : base(store)
    {
    }

    #endregion

    #region Public Static Methods

    /// <summary>
    /// Create Application User manager. Enforce Password validation.
    /// </summary>
    /// <param name="options">
    /// User Management options.
    /// </param>
    /// 
    /// <param name="context">
    /// Owin Environment
    /// </param>
    /// 
    /// <returns>
    /// Returns Application user manager.
    /// </returns>
    public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
    {
      var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));

      // Configure validation logic for usernames
      manager.UserValidator = new UserValidator<ApplicationUser>(manager)
      {
        AllowOnlyAlphanumericUserNames = false,
        RequireUniqueEmail = true
      };

      // Configure validation logic for passwords
      manager.PasswordValidator = new PasswordValidator
      {
        RequiredLength = 6,
        RequireNonLetterOrDigit = true,
        RequireDigit = true,
        RequireLowercase = true,
        RequireUppercase = true,
      };

      var dataProtectionProvider = options.DataProtectionProvider;

      if (dataProtectionProvider != null)
      {
        manager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
      }
      return manager;
    }

    #endregion
  }

  /// <prologue>
  ///
  /// <file name="ApplicationSignInManager.cs"/>
  ///
  /// <remarks>
  /// N/A
  /// </remarks>
  ///
  /// 
  /// <author name="Jason Truong" date="01/27/19"/>
  /// 
  ///
  /// <history>
  ///   <entry date="01/27/19" author="Jason Truong" scr="" 
  ///          desc="Created"/> 
  /// </history>
  ///
  /// </prologue>
  /// <summary>
  /// Configure the sign in manager
  /// </summary>
  ///
  /// <remarks>
  /// N/A
  /// </remarks>
  public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
  {
    #region Constructor

    /// <summary>
    /// Construct the Sign In Manager uses the application manager together with the authentication
    /// manager.
    /// </summary>
    /// 
    /// <param name="userManager">
    /// Application User Manager
    /// </param>
    /// 
    /// <param name="authenticationManager">
    /// The authentication Manager that uses cookies for authentication.
    /// </param>
    public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
       : base(userManager, authenticationManager)
    {
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Generate a User Identity with Cookie authentication.
    /// </summary>
    /// 
    /// <param name="user">
    /// User of the application
    /// </param>
    /// 
    /// <returns>
    /// Returns Claims Identity
    /// </returns>
    public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
    {
      return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager, CookieAuthenticationDefaults.AuthenticationType);
    }

    /// <summary>
    /// Create Application Sign In Manager
    /// </summary>
    /// 
    /// <param name="options">
    /// Options for Application Manager.
    /// </param>
    /// <param name="context">
    /// OWIN Context
    /// </param>
    /// 
    /// <returns>
    /// Returns Sign in application Manager
    /// </returns>
    public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
    {
      return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
    }

    #endregion
  }
}
