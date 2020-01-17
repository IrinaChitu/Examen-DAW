using Examen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Examen.Models.Organizer;

namespace Examen.Controllers
{
    public class ContestController : Controller
    {
        private OrganizatorConcursDBContext db = new OrganizatorConcursDBContext();

        // GET: Contest
        public ActionResult Index()
        {
            if (TempData.ContainsKey("message"))
            {
                ViewBag.message = TempData["message"].ToString();
            }

            var contests = from contest in db.Contests.Include("Organizator")
                           select contest;
            ViewBag.Contests = contests;
            return View();
        }

        public ActionResult Show(int id)
        {
            Contest contest = db.Contests.Find(id);
            //ViewBag.Organizer = db.Organizers.Find(contest.IdOrganizator);
            return View(contest);

        }

        public ActionResult New()
        {
            Contest contest = new Contest();

            // preluam lista de categorii din metoda GetAllCategories()
            contest.Organizers = GetAllOrganizers();

            return View(contest);

        }

        [HttpPost]
        public ActionResult New(Contest contest)
        {
            contest.Organizers = GetAllOrganizers();
            try
            {
                if (ModelState.IsValid)
                {
                    if (contest.Data <= DateTime.Now)
                    {
                        TempData["eroare"] = "Oricat am vrea, nu ne putem intoarce in timp pt dvs.";
                        return View(contest);
                    }

                    db.Contests.Add(contest);
                    db.SaveChanges();
                    TempData["message"] = "Concursul a fost adaugat!";
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(contest);
                }
            }
            catch (Exception e)
            {
                return View(contest);
            }
        }

        public ActionResult Edit(int id)
        {
            Contest contest = db.Contests.Find(id);
            ViewBag.Contest = contest;
            contest.Organizers = GetAllOrganizers();

            return View(contest);
        }

        [HttpPut]
        public ActionResult Edit(int id, Contest requestContest)
        {
            requestContest.Organizers = GetAllOrganizers();

            try
            {
                if (ModelState.IsValid)
                {
                    if (requestContest.Data <= DateTime.Now)
                    {
                        TempData["eroare"] = "Oricat am vrea, nu ne putem intoarce in timp pt dvs.";
                        return View(requestContest);
                    }

                    Contest contest = db.Contests.Find(id);

                    if (TryUpdateModel(contest))
                    {
                        contest = requestContest;
                        db.SaveChanges();
                        TempData["message"] = "Concursul a fost modificat!";
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(requestContest);
                }

            }
            catch (Exception e)
            {
                return View(requestContest);
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Contest contest = db.Contests.Find(id);
            db.Contests.Remove(contest);
            db.SaveChanges();
            TempData["message"] = "Concursul a fost sters!";
            return RedirectToAction("Index");
        }

        [NonAction]
        public IEnumerable<SelectListItem> GetAllOrganizers()
        {
            var selectList = new List<SelectListItem>();

            var organizers = from organizer in db.Organizers
                         select organizer;

            foreach (var organizer in organizers)
            {
                selectList.Add(new SelectListItem
                {
                    Value = organizer.IdOrganizator.ToString(),
                    Text = organizer.Nume.ToString()
                });
            }

            return selectList;
        }


        public ActionResult Search()
        {
            return View();
        }

        public ActionResult CautaConcurs(DateTime data)
        {
            var listOfContests = new List<Contest>();

            var allContests = from concurs in db.Contests.Include("Organizator").OrderByDescending(c => c.NrParticipanti)
                                select concurs;

            foreach (var concurs in allContests)
            {
                if (concurs.Data >= data)
                {
                    listOfContests.Add(concurs);

                }
            }

            ViewBag.Contests = listOfContests;

            TempData["message"] = $"Concursuri care se desfasoara dupa data de {data}";
            return View("Index");
        }
    }
}