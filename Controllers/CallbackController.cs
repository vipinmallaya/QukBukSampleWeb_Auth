using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace QukBukSampleWeb.Controllers
{
    public class CallbackController : Controller
    {
        private object _accessToken;
        private object _RefreshToken;

        // GET: CallbackControllercs
        public ActionResult Index()
        {
            var value = HttpContext.Request.QueryString.Value;
            var result = HttpUtility.ParseQueryString(value);

            string code = result["code"] ?? "none";
           // string realmId = result["realmId"] ?? "none";


            // Get OAuth2 Bearer token
            HomeController.auth2Client.GetBearerTokenAsync(code).ContinueWith(async (response) =>
               {
                   var tokenResult = await response;
                   _accessToken = tokenResult.AccessToken;
                   _RefreshToken = tokenResult.RefreshToken;
               });

            //retrieve access_token and refresh_token


            return View();
        }

        // GET: CallbackControllercs/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CallbackControllercs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CallbackControllercs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CallbackControllercs/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CallbackControllercs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CallbackControllercs/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CallbackControllercs/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
