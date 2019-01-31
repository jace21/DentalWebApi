using System;

namespace WebApplication.Models
{
  /// <prologue>
  ///
  /// <file name="EventModel.cs"/>
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
  /// Contains Event Properties used for Full Calendar.
  /// </summary>
  ///
  /// <remarks>
  /// N/A
  /// </remarks>
  public class EventModel
  {
    #region Public Properties

    /// <summary>
    /// Event ID.
    /// </summary>
    public string id { get; set; }

    /// <summary>
    /// Event Start Time.
    /// </summary>
    public DateTime start { get; set; }

    /// <summary>
    /// Event End Time.
    /// </summary>
    public DateTime end { get; set; }

    /// <summary>
    /// Event Subject.
    /// </summary>
    public string title { get; set; }

    /// <summary>
    /// Doctor associated to event.
    /// </summary>
    public string doctorId { get; set; }

    /// <summary>
    /// Patient's name associated to event.
    /// </summary>
    public string patientName { get; set; }

    /// <summary>
    /// Patient's ID associated to event.
    /// </summary>
    public string patientId { get; set; }

    /// <summary>
    /// Event Description.
    /// </summary>
    public string description { get; set; }

    #endregion
  }
}