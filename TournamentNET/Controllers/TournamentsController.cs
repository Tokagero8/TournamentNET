using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TournamentNET.Models;

namespace TournamentNET.Controllers
{
    public class TournamentsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Tournaments
        public ActionResult Index()
        {
            return View(db.Tournaments.ToList());
        }

        // GET: Tournaments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tournaments tournaments = db.Tournaments.Find(id); //Znajdowanie turniej z podanego id
            List<Teams> teams = new List<Teams>(); //tworzenie pustej list teamów
            teams = tournaments.Teams.ToList().FindAll(t => t.tournament_id == id).OrderBy(s => s.ranking).ToList(); //dodawanie do tej listy teamów, które są w turnieju i sortuje według rankingu
            List<Matches> matches = new List<Matches>(); // dodanie pustej listy meczów
            matches = tournaments.Matches.ToList(); ;
            ViewModelTourTeams tourTeams = new ViewModelTourTeams(); //tworzenie objektu, który ma w sobie i turniej i teamy
            tourTeams.tournaments = tournaments; //dodanie turnieju
            tourTeams.teams = teams; //dodanie teamów.
            tourTeams.matches = matches; // dodanie meczy
            if (tourTeams == null)
            {
                return HttpNotFound();
            }
            return View(tourTeams);
        }

        // GET: Tournaments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tournaments/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tournament_id,name")] Tournaments tournaments)
        {
            if (ModelState.IsValid)
            {
                db.Tournaments.Add(tournaments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tournaments);
        }

        // GET: Tournaments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tournaments tournaments = db.Tournaments.Find(id);
            if (tournaments == null)
            {
                return HttpNotFound();
            }
            return View(tournaments);
        }

        // POST: Tournaments/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tournament_id,name")] Tournaments tournaments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tournaments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tournaments);
        }

        // GET: Tournaments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tournaments tournaments = db.Tournaments.Find(id);
            if (tournaments == null)
            {
                return HttpNotFound();
            }
            return View(tournaments);
        }

        // POST: Tournaments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tournaments tournaments = db.Tournaments.Find(id);
            db.Tournaments.Remove(tournaments);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
