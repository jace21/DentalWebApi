using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
  /// <prologue>
  ///
  /// <file name="IndexViewModel.cs"/>
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
  /// Contains properties for user account that 
  /// user can change.
  /// </summary>
  ///
  /// <remarks>
  /// N/A
  /// </remarks>
  public class IndexViewModel
  {
    #region Public Properties

    /// <summary>
    /// Boolean that checks if password exist.
    /// </summary>
    public bool HasPassword { get; set; }

    /// <summary>
    /// The Phone number associated to the account.
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    /// Check if browser remembers
    /// </summary>
    public bool BrowserRemembered { get; set; }

    #endregion
  }

  /// <prologue>
  ///
  /// <file name="AddPhoneNumberViewModel.cs"/>
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
  /// Contains properties when user adds new
  /// phone number
  /// </summary>
  ///
  /// <remarks>
  /// N/A
  /// </remarks>
  public class AddPhoneNumberViewModel
  {
    #region Public Properties

    /// <summary>
    /// Phone number as a string.
    /// </summary>
    [Required]
    [Phone]
    [Display(Name = "Phone Number")]
    public string Number { get; set; }

    #endregion
  }

  /// <prologue>
  ///
  /// <file name="VerifyPhoneNumberViewModel.cs"/>
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
  /// Contains properties used to verify phone number
  /// </summary>
  ///
  /// <remarks>
  /// N/A
  /// </remarks>
  public class VerifyPhoneNumberViewModel
  {
    #region Public Properties

    /// <summary>
    /// Verification Code send to cell phone via SMS.
    /// </summary>
    [Required]
    [Display(Name = "Code")]
    public string Code { get; set; }

    /// <summary>
    /// Phone number as a string.
    /// </summary>
    [Required]
    [Phone]
    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; }

    #endregion
  }

  /// <prologue>
  ///
  /// <file name="ChangePasswordViewModel.cs"/>
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
  /// Contains properties used to change password
  /// for an existing account
  /// </summary>
  ///
  /// <remarks>
  /// N/A
  /// </remarks>
  public class ChangePasswordViewModel
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
    /// New password that will overwrite old password
    /// </summary>
    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "New password")]
    public string NewPassword { get; set; }

    /// <summary>
    /// Checked against new password to ensure password matches
    /// </summary>
    [DataType(DataType.Password)]
    [Display(Name = "Confirm new password")]
    [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }

    #endregion

  }
}