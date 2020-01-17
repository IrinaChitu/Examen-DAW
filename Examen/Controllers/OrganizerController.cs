using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Examen.Models.Organizer;

namespace Examen.Controllers
{
    public class OrganizerController : Controller
    {
        private OrganizatorConcursDBContext db = new OrganizatorConcursDBContext();

        // GET: Organizer
        public ActionResult Index()
        {
            if (TempData.ContainsKey("message"))
            {
                ViewBag.message = TempData["message"].ToString();
            }

            var organizers = from organizer in db.Organizers
                             orderby organizer.Nume
                           select organizer;
            ViewBag.Organizers = organizers;
            return View();
        }
    }
}