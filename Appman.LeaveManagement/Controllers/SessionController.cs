//using System.Web;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Owin.Security;
//using Microsoft.AspNetCore.Http;


//namespace Appman.LeaveManagement.Controllers
//{
//    public class SessionController : Controller
//    {
//        // [START login]
//        public void Login()
//        {
//            //Redirect to the Google OAuth 2.0 user consent screen
//            HttpContext.GetOwinContext().Authentication.Challenge(
//                new AuthenticationProperties { RedirectUri = "/" },
//                "Google"
//            );

//        }
//        // [END login]

//        // [START logout]
//        public ActionResult Logout()
//        {
//            Request.GetOwinContext().Authentication.SignOut();
//            return Redirect("/");
//        }
//        // [END logout]
//    }
//}