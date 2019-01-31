using System.Web.Mvc;

namespace WebApplication
{
  /// <prologue>
  ///
  /// <file name="FilterConfig.cs"/>
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
  /// Configure and register global filters
  /// </summary>
  ///
  /// <remarks>
  /// N/A
  /// </remarks>
  public class FilterConfig
  {
    #region Register

    /// <summary>
    /// Register Global Filters
    /// </summary>
    /// 
    /// <param name="filters">
    /// Collection of Filters
    /// </param>
    public static void RegisterGlobalFilters(GlobalFilterCollection filters)
    {
      filters.Add(new HandleErrorAttribute());
    }

    #endregion
  }
}
