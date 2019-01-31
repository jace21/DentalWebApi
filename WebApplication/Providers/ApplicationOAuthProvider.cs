using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Providers
{
  /// <prologue>
  ///
  /// <file name="ApplicationOAuthProvider.cs"/>
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
  /// Provides OAuthentication.
  /// </summary>
  ///
  /// <remarks>
  /// N/A
  /// </remarks>
  public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
  {
    #region Private Members

    /// <summary>
    /// Public Client Identification
    /// </summary>
    private readonly string PublicClientId;

    #endregion

    #region Constructor

    /// <summary>
    /// Construct the Applications OAuthentication Provider
    /// </summary>
    /// 
    /// <param name="publicClientId">
    /// String for Public CLient Identifier
    /// </param>
    /// 
    /// <exception cref="ArgumentNullException">
    /// Throws Argument null exception when <paramref name="publicClientId"/> 
    /// is null.
    /// </exception>
    public ApplicationOAuthProvider(string publicClientId)
    {
      PublicClientId = publicClientId ?? throw new ArgumentNullException("publicClientId");
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Grant resource owner credentials
    /// </summary>
    /// 
    /// <param name="context">
    /// Context used to give resource owner credentials
    /// </param>
    /// 
    /// <returns>
    /// N/A
    /// </returns>
    public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
    {
      var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

      ApplicationUser user = await userManager.FindAsync(context.UserName, context.Password);

      if (user == null)
      {
        context.SetError("invalid_grant", "The user name or password is incorrect.");
        return;
      }

      ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager,
         OAuthDefaults.AuthenticationType);

      ClaimsIdentity cookiesIdentity = await user.GenerateUserIdentityAsync(userManager,
          CookieAuthenticationDefaults.AuthenticationType);

      AuthenticationProperties properties = CreateProperties(user.UserName);
      AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
      context.Validated(ticket);
      context.Request.Context.Authentication.SignIn(cookiesIdentity);
    }

    /// <summary>
    /// Set the Token End Points to the context.
    /// </summary>
    /// 
    /// <param name="context">
    /// Provides context for processing authentication.
    /// </param>
    /// 
    /// <returns>
    /// Return null results.
    /// </returns>
    public override Task TokenEndpoint(OAuthTokenEndpointContext context)
    {
      foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
      {
        context.AdditionalResponseParameters.Add(property.Key, property.Value);
      }

      return Task.FromResult<object>(null);
    }

    /// <summary>
    /// Validate user authentication.
    /// </summary>
    /// 
    /// <param name="context">
    /// Context used to validate client authentication
    /// </param>
    /// 
    /// <returns>
    /// Return null results.
    /// </returns>
    public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
    {
      // Resource owner password credentials does not provide a client ID.
      if (context.ClientId == null)
      {
        context.Validated();
      }

      return Task.FromResult<object>(null);
    }

    /// <summary>
    /// Validate the clients redirection URI.
    /// </summary>
    /// 
    /// <param name="context">
    /// Context used to validate client redirection uri
    /// </param>
    /// 
    /// <returns>
    /// Return null results.
    /// </returns>
    public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
    {
      if (context.ClientId == PublicClientId)
      {
        Uri expectedRootUri = new Uri(context.Request.Uri, "/");

        if (expectedRootUri.AbsoluteUri == context.RedirectUri)
        {
          context.Validated();
        }
      }

      return Task.FromResult<object>(null);
    }

    /// <summary>
    /// Create Authentication Properties
    /// </summary>
    /// 
    /// <param name="userName">
    /// The user name of the login that we are
    /// generating authentication properties for.
    /// </param>
    /// 
    /// <returns>
    /// Returns authentication properties
    /// </returns>
    public static AuthenticationProperties CreateProperties(string userName)
    {
      IDictionary<string, string> data = new Dictionary<string, string>
      {
        { "userName", userName }
      };

      return new AuthenticationProperties(data);
    }

    #endregion
  }
}