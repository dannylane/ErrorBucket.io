using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoveSeat;
using Newtonsoft.Json.Linq;

namespace ErrorBucket.io.Controllers
{
    public class LogController : Controller
    {
        //
        // GET: /Log/

        public LogController()
        {

        }
        public ActionResult Index(string errorMessage)
        {
            var client = new CouchClient("dannylane.iriscouch.com", 6984, "dannylane", "adminpass", false, AuthenticationType.Cookie);
            var udb = client.GetDatabase("testing");

            JObject jObject = new JObject();

            jObject.Add("DateTime", DateTime.UtcNow);
            jObject.Add("UserHostAddress", Request.UserHostAddress);
            jObject.Add("UserHostName", Request.UserHostName);
          //  jObject.Add("UserLanguages", Request.UserLanguages.ToString());
            
            foreach (var param in Request.QueryString.AllKeys)
            {
                jObject.Add(param, Request.QueryString[param]);
            }

            foreach (var param in Request.Headers.AllKeys)
            {
                jObject.Add(param, Request.Headers[param]);
            }

            var info = new LoveSeat.Document(jObject);
            
            udb.SaveDocument(info);
            return File("favicon.ico","image");
        }

        public LoveSeat.ViewResult List()
        {
            var client = new CouchClient("dannylane.iriscouch.com", 6984, "dannylane", "adminpass", false, AuthenticationType.Cookie);
            var udb = client.GetDatabase("testing");
            return udb.GetAllDocuments();
           // return new JsonResult {Data = db, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
        }
    }
}
