using Intuit.Ipp.OAuth2PlatformClient;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Configuration;
using System.Web;

namespace QukBukSampleWeb.Controllers
{
    public class HomeController : Controller
    {
        //Instantiate OAuth2Client object with clientId, clientsecret, redirectUrl and environment
        public static OAuth2Client auth2Client;
        private string _AccessToken;
        private string _RefreshToken;

        public string Index()
        {
            return "hello First Time";
        }
        public string Welcome()
        {
            return "This is the Welcome action method...";
        }

        //Generate authorize url to get the OAuth2 code
        public ActionResult InitiateAuth()
        {
            var clientid = "ABTxW6lPn1V4YMmvANavfol94a5iuTBDTeg2649dUIAr7OPErt"; //ConfigurationManager.AppSettings["clientid"] as string;
            var clientsecret = "irYUpW7oCl7QosLhQjmk4u3pNjNR9jujN0XBLlBq"; // ConfigurationManager.AppSettings["clientsecret"] as string;
            var redirectUrl = "https://localhost:44300/home/callback"; //ConfigurationManager.AppSettings["redirectUrl"] as string; 
            var environment = "sandbox";//ConfigurationManager.AppSettings["appEnvironment"] as string; ;

            auth2Client ??= new OAuth2Client(clientid, clientsecret, redirectUrl, environment);

            List<OidcScopes> scopes = new List<OidcScopes>();
            scopes.Add(OidcScopes.Accounting);
            string authorizeUrl = auth2Client.GetAuthorizationURL(scopes);
            return Redirect(authorizeUrl);
        }

        public string Callback()
        {
            var value = HttpContext.Request.QueryString.Value;
            var result = HttpUtility.ParseQueryString(value);

            string code = result["code"] ?? "none";
            string realmId = result["realmId"] ?? "none";

            // Get OAuth2 Bearer token
            auth2Client.GetBearerTokenAsync(code).ContinueWith(async (response) =>
            {
                var tokenResult = await response;
                _AccessToken = tokenResult.AccessToken;
                _RefreshToken = tokenResult.RefreshToken;
                
            });

            //retrieve access_token and refresh_token
            return $"This is the Welcome action method...Access Token -{_AccessToken} Refresh Token - {_RefreshToken}";
        }
    }
}
