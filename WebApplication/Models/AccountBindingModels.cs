using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
  /// <prologue>
  ///
  /// <file name="ChangePasswordBindingModel.cs"/>
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
  /// Contains properties for needed for changing password binded to
  /// Account Controller
  /// </summary>
  ///
  /// <remarks>
  /// N/A
  /// </remarks>
  public class ChangePasswordBindingModel
  {
    #region Public Properties

    /// <summary>
    /// Previously stored password.
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Current password")]
    public string OldPassword { get; set; }

    /// <summary>
    /// New Password for new account.
    /// </summary>
    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "New password")]
    public string NewPassword { get; set; }

    /// <summary>
    /// Compared to old password to ensure passwords match.
    /// </summary>
    [DataType(DataType.Password)]
    [Display(Name = "Confirm new password")]
    [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }

    #endregion
  }

  /// <prologue>
  ///
  /// <file name="RegisterBindingModel.cs"/>
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
  /// Contains properties for needed for registering new account binded to
  /// Account Controller
  /// </summary>
  ///
  /// <remarks>
  /// N/A
  /// </remarks>
  public class RegisterBindingModel
  {
    #region Public Properties

    /// <summary>
    /// Email used as username for login.
    /// </summary>
    [Required]
    [Display(Name = "Email")]
    public string Email { get; set; }

    /// <summary>
    /// New Password used to login with specified email.
    /// </summary>
    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    /// <summary>
    /// Used to confirm that the password matches.
    /// </summary>
    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }

    #endregion
  }

  /// <prologue>
  ///
  /// <file name="PatientModel.cs"/>
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
  /// Contains properties for patients associated on existing account.
  /// </summary>
  ///
  /// <remarks>
  /// N/A
  /// </remarks>
  public class PatientModel
  {
    #region Public Properties

    /// <summary>
    /// Date of Birth of the patient.
    /// </summary>
    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Birthday")]
    public string Birthday { get; set; }

    /// <summary>
    /// Patient's Name first and last.
    /// </summary>
    [Required]
    [Display(Name = "Name")]
    public string Name { get; set; }

    /// <summary>
    /// The Address where the patient resides.
    /// </summary>
    [Required]
    [Display(Name = "Address")]
    public string Address { get; set; }

    /// <summary>
    /// The City where the patient resides.
    /// </summary>
    [Required]
    [Display(Name = "City")]
    public string City { get; set; }

    /// <summary>
    /// The state where the patient resides.
    /// </summary>
    [Required]
    [StringLength(2, ErrorMessage = "The State Code must be Length of 2.", MinimumLength = 2)]
    [Display(Name = "State")]
    public string StateCode { get; set; }

    /// <summary>
    /// The ZipCode where the patient resides.
    /// </summary>
    [Required]
    [StringLength(6, ErrorMessage = "The Zip Code must be 5-6 characters long.", MinimumLength = 5)]
    [Display(Name = "Zip Code")]
    public string ZipCode { get; set; }

    /// <summary>
    /// The patients associated doctor.
    /// </summary>
    [Required]
    [Display(Name = "Doctor")]
    public string DoctorId { get; set; }

    /// <summary>
    /// The profile picture of the patient
    /// </summary>
    public string Profile { get; set; }

    /// <summary>
    /// The formatted address string which combines
    /// the address, city, state, and zipcode.
    /// </summary>
    public string AddressString { get; set; }

    #endregion

    #region Constructor

    /// <summary>
    /// Constructor that takes in doctor ID
    /// and initializes the associated doctor.
    /// </summary>
    /// 
    /// <param name="doctorId">
    /// GUID used to identify the doctor.
    /// </param>
    public PatientModel(string doctorId)
    {
      Profile = Constants.DefaultProfile;
      DoctorId = doctorId;
    }

    /// <summary>
    /// Parameterless constructor required for serialization/deserialization
    /// </summary>
    public PatientModel()
    {
      Profile = Constants.DefaultProfile;
    }

    #endregion
  }

  /// <prologue>
  ///
  /// <file name="PatientListModel.cs"/>
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
  /// Contains properties for list of Patients associated to a
  /// doctor.
  /// </summary>
  ///
  /// <remarks>
  /// N/A
  /// </remarks>
  public class PatientListModel
  {
    #region Public Properties

    /// <summary>
    /// Contains List of patients associated to the doctor.
    /// </summary>
    public IList<PatientModel> Patients { get; set; }

    /// <summary>
    /// Doctors Identification.
    /// </summary>
    [Required]
    public string DoctorId;

    #endregion

    #region Constructor

    /// <summary>
    /// Constructor for Patient List.
    /// </summary>
    /// 
    /// <param name="doctorId">
    /// Doctors GUID
    /// </param>
    /// 
    /// <param name="patients">
    /// List of patients associated to doctor's GUID
    /// </param>
    public PatientListModel(string doctorId, IList<PatientModel> patients)
    {
      DoctorId = doctorId;
      Patients = patients;
    }

    #endregion
  }

}
