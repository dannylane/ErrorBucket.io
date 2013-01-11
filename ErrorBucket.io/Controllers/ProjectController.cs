using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoveSeat;
using LoveSeat.Support;
using Newtonsoft.Json.Linq;

namespace ErrorBucket.Controllers
{
    public class ProjectController : Controller
    {
        //
        // GET: /Project/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(string projectName)
        {
            var client = new CouchClient("localhost", 5984, "dannylane", "adminpass", false, AuthenticationType.Cookie);
            var userresult = client.CreateUser(projectName, projectName);
            //var newUid = userresult.Value<string>("id");
            
            var u = client.GetUser(projectName);
            u["roles"] = new JArray(projectName + "reader_");
            var udb = client.GetDatabase("_users");
            udb.SaveDocument(u);

            var res = client.CreateDatabase(projectName);
            var db = client.GetDatabase(projectName);

            var security = db.getSecurityConfiguration();
            security.readers.roles.Add(projectName + "reader_");
            db.UpdateSecurityDocument(security);

            var readerClient = new CouchClient("localhost", 5984, projectName, projectName, false,
                                               AuthenticationType.Cookie);
            
           // var result = CouchBase.GetSession("localhost:5984", projectName, projectName);
            Response.Cookies.Add(new HttpCookie("auth", readerClient.GetSession().Value));
            return View();
        }
    }
}