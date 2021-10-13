using Intuit.Ipp.OAuth2PlatformClient;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Configuration;

namespace QukBukSampleWeb.Controllers
{
    public class HomeController : Controller
    {
        //Instantiate OAuth2Client object with clientId, clientsecret, redirectUrl and environment
        public static OAuth2Client auth2Client;
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
            var redirectUrl = "https://localhost:44300/callback"; //ConfigurationManager.AppSettings["redirectUrl"] as string; 
            var environment = "sandbox";//ConfigurationManager.AppSettings["appEnvironment"] as string; ;

            
            
            auth2Client ??= new OAuth2Client(clientid, clientsecret, redirectUrl, environment);

            List<OidcScopes> scopes = new List<OidcScopes>();
            scopes.Add(OidcScopes.Accounting);
            string authorizeUrl = auth2Client.GetAuthorizationURL(scopes);
            return Redirect(authorizeUrl);
        }

        public string Callback()
        {
            var value = HttpContext.Request.QueryString;
            return "This is the Welcome action method...";
        }
    }
}
