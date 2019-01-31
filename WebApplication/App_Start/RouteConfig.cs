using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication
{
  /// <prologue>
  ///
  /// <file name="RouteConfig.cs"/>
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
  /// Route Configurations
  /// </summary>
  ///
  /// <remarks>
  /// N/A
  /// </remarks>
  public class RouteConfig
  {
    #region Register

    /// <summary>
    /// Register routes.
    /// </summary>
    /// 
    /// <param name="routes">
    /// Collection of routes
    /// </param>
    public static void RegisterRoutes(RouteCollection routes)
    {
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

      routes.MapRoute(
          name: "Default",
          url: "{controller}/{action}/{id}",
          defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
      );
    }

    #endregion
  }
}
