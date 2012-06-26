using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClixStackWeb.Controllers
{
    public class ViewController : Controller
    {
        //
        // Klaxster/View/AppName:AppKey

        public ActionResult Render(string PageData)
        {
            return View();
        }

    }
}
