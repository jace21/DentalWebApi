using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApplication.Models
{
  /// <prologue>
  ///
  /// <file name="ApplicationUser.cs"/>
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
  /// 
  /// <summary>
  /// Properties for the Application user. Add profile data for the user.
  /// </summary>
  ///
  /// <remarks>
  /// N/A
  /// </remarks>
  public class ApplicationUser : IdentityUser
  {
    #region Public Methods

    /// <summary>
    /// Generate User Identity.
    /// </summary>
    /// 
    /// <param name="manager">
    /// The user manager.
    /// </param>
    /// <param name="authenticationType">
    /// The authentication type which should be via cookies.
    /// </param>
    /// 
    /// <returns>
    /// Returns an user identity.
    /// </returns>
    public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
    {
      // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
      var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
      // Add custom user claims here
      return userIdentity;
    }

    #endregion
  }

  /// <prologue>
  ///
  /// <file name="ApplicationDbContext.cs"/>
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
  /// 
  /// <summary>
  /// Entity Framework database connection to database that contains user
  /// credentials such as hash passwords
  /// </summary>
  ///
  /// <remarks>
  /// N/A
  /// </remarks>
  public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
  {
    #region Constructor

    /// <summary>
    /// Construct that creates a connection to local created database.
    /// </summary>
    public ApplicationDbContext()
        : base("DefaultConnection", throwIfV1Schema: false)
    {
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Create Database Context
    /// </summary>
    /// 
    /// <returns>
    /// Returns Database context.
    /// </returns>
    public static ApplicationDbContext Create()
    {
      return new ApplicationDbContext();
    }

    #endregion
  }
}