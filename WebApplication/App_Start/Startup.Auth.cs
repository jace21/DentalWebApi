using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using WebApplication.Models;
using WebApplication.Providers;

namespace WebApplication
{
  /// <prologue>
  ///
  /// <file name="Startup.cs"/>
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
  /// Runs at startup and initializes configurations for authentication.
  /// </summary>
  ///
  /// <remarks>
  /// N/A
  /// </remarks>
  public partial class Startup
  {
    #region Public Properties

    /// <summary>
    /// Authentication Server Options
    /// </summary>
    public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

    /// <summary>
    /// Public Client Id
    /// </summary>
    public static string PublicClientId { get; private set; }

    #endregion

    #region Public Methods

    /// <summary>
    /// Configure authentication options using cookies for authentication.
    /// </summary>
    /// 
    /// <param name="app">
    /// The application
    /// </param>
    public void ConfigureAuth(IAppBuilder app)
    {
      // Configure the db context and user manager to use a single instance per request
      app.CreatePerOwinContext(ApplicationDbContext.Create);
      app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
      app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

      // Enable the application to use a cookie to store information for the signed in user
      // and to use a cookie to temporarily store information about a user logging in with a third party login provider
      app.UseCookieAuthentication(new CookieAuthenticationOptions());

      // Configure the application for OAuth based flow
      PublicClientId = "self";
      OAuthOptions = new OAuthAuthorizationServerOptions
      {
        TokenEndpointPath = new PathString("/Token"),
        Provider = new ApplicationOAuthProvider(PublicClientId),
        AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
        // In production mode set AllowInsecureHttp = false
        AllowInsecureHttp = true
      };

      // Enable the application to use bearer tokens to authenticate users
      app.UseOAuthBearerTokens(OAuthOptions);
    }

    #endregion
  }
}
