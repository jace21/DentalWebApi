namespace WebApplication.Models
{
  /// <prologue>
  ///
  /// <file name="DoctorViewModel.cs"/>
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
  /// Doctor View Model holds values to bind to razor view.
  /// </summary>
  ///
  /// <remarks>
  /// N/A
  /// </remarks>
  public class DoctorViewModel
  {
    #region Properties

    /// <summary>
    /// Doctor GUID identification.
    /// </summary>
    public string Id { get; set; }

    #endregion

    #region Constructor

    /// <summary>
    /// Constructor initializes identification
    /// </summary>
    /// <param name="id">
    /// Doctor's GUID
    /// </param>
    public DoctorViewModel(string id)
    {
      Id = id;
    }

    #endregion
  }
}