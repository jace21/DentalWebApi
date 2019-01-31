using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
  /// <prologue>
  ///
  /// <file name="ForgotPasswordViewModel.cs"/>
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
  /// Contains properties for Forgot Password Model return by Account Controller.
  /// </summary>
  ///
  /// <remarks>
  /// N/A
  /// </remarks>
  public class ForgotPasswordViewModel
  {
    #region Public Properties

    /// <summary>
    /// Email string.
    /// </summary>
    [Required]
    [EmailAddress]
    [Display(Name = "Enter your Email")]
    public string Email { get; set; }

    #endregion
  }

  /// <prologue>
  ///
  /// <file name="RegisterViewModel.cs"/>
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
  /// Contains properties for Register Model return by Account Controller.
  /// </summary>
  ///
  /// <remarks>
  /// N/A
  /// </remarks>
  public class RegisterViewModel
  {
    #region Public Properties

    /// <summary>
    /// Email used as username for login.
    /// </summary>
    [Required]
    [EmailAddress]
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
  /// <file name="LoginViewModel.cs"/>
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
  /// Contains properties for Login Model return by Account Controller.
  /// </summary>
  ///
  /// <remarks>
  /// N/A
  /// </remarks>
  public class LoginViewModel
  {
    #region Public Properties

    /// <summary>
    /// Email as a user name.
    /// </summary>
    [Required]
    [Display(Name = "Email")]
    [EmailAddress]
    public string Email { get; set; }

    /// <summary>
    /// The password associated with the email.
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    /// <summary>
    /// Used to remember the login.
    /// </summary>
    [Display(Name = "Remember my login on this computer")]
    public bool RememberMe { get; set; }

    #endregion
  }
}
