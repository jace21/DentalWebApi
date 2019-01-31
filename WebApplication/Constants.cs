namespace WebApplication
{
  /// <prologue>
  ///
  /// <file name="Constants.cs"/>
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
  /// Constants for default profile.
  /// </summary>
  ///
  /// <remarks>
  /// N/A
  /// </remarks>
  public class Constants
  {
    #region Default

    /// <summary>
    /// The default image string.
    /// </summary>
    public const string DefaultProfile =
      @"data:image/png;base64,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";

    #endregion
  }
}