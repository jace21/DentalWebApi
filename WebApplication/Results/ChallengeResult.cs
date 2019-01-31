using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace WebApplication.Results
{
  /// <prologue>
  ///
  /// <file name="ChallengeResult.cs"/>
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
  /// Invokes authentications managers challenge async.
  /// </summary>
  ///
  /// <remarks>
  /// N/A
  /// </remarks>
  public class ChallengeResult : IHttpActionResult
  {
    #region Constructor

    /// <summary>
    /// Constuctor for Challenge Result
    /// </summary>
    /// 
    /// <param name="loginProvider">
    /// The provider for the login
    /// </param>
    /// 
    /// <param name="controller">
    /// The controller that will handle the http response
    /// </param>
    public ChallengeResult(string loginProvider, ApiController controller)
    {
      LoginProvider = loginProvider;
      Request = controller.Request;
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Login Provider
    /// </summary>
    public string LoginProvider { get; set; }

    /// <summary>
    /// HTTP Request Message
    /// </summary>
    public HttpRequestMessage Request { get; set; }

    #endregion

    #region Public Methods

    /// <summary>
    /// Challenge authentication to provider and send out http response.
    /// </summary>
    /// 
    /// <param name="cancellationToken">
    /// Cancellation token can throw interupt if cancel is called.
    /// </param>
    /// 
    /// <returns>
    /// Returns HTTP Response Message
    /// </returns>
    public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
    {
      Request.GetOwinContext().Authentication.Challenge(LoginProvider);

      HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
      response.RequestMessage = Request;
      return Task.FromResult(response);
    }

    #endregion
  }
}
