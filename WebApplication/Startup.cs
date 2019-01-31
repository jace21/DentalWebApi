using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(WebApplication.Startup))]

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
  /// Runs at startup and runs the configurations
  /// </summary>
  ///
  /// <remarks>
  /// N/A
  /// </remarks>
  public partial class Startup
  {
    #region Public Methods

    /// <summary>
    /// Runs the intitialization of the configurations
    /// </summary>
    /// 
    /// <param name="app">
    /// The web application
    /// </param>
    public void Configuration(IAppBuilder app)
    {
      ConfigureAuth(app);
    }

    #endregion
  }
}
