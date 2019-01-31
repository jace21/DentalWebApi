using Microsoft.Owin.Security.OAuth;
using System.Web.Http;

namespace WebApplication
{
  /// <prologue>
  ///
  /// <file name="WebApiConfig.cs"/>
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
  /// Web Api Configurations
  /// </summary>
  ///
  /// <remarks>
  /// N/A
  /// </remarks>
  public static class WebApiConfig
  {
    #region Register

    /// <summary>
    /// Register http routes.
    /// </summary>
    /// 
    /// <param name="config">
    /// Http Configurations
    /// </param>
    public static void Register(HttpConfiguration config)
    {
      // Web API configuration and services
      // Configure Web API to use only bearer token authentication.
      config.SuppressDefaultHostAuthentication();
      config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

      // Web API routes
      config.MapHttpAttributeRoutes();

      config.Routes.MapHttpRoute(
          name: "DefaultApi",
          routeTemplate: "api/{controller}/{id}",
          defaults: new { id = RouteParameter.Optional }
      );
    }

    #endregion
  }
}
