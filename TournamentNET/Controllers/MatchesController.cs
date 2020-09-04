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
    public class MatchesController : Controller
    {
        private Model1 db = new Model1();

        // GET: Matches
        public ActionResult Index()
        {
            var matches = db.Matches.Include(m => m.Teams).Include(m => m.Teams1).Include(m => m.Tournaments);
            return View(matches.ToList());
        }

        // GET: Matches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Matches matches = db.Matches.Find(id);
            if (matches == null)
            {
                return HttpNotFound();
            }
            return View(matches);
        }

        // GET: Matches/Create
        public ActionResult Create()
        {
            ViewBag.team1_id = new SelectList(db.Teams, "team_id", "name");
            ViewBag.team2_id = new SelectList(db.Teams, "team_id", "name");
            ViewBag.tournament_id = new SelectList(db.Tournaments, "tournament_id", "name");
            return View();
        }

        // POST: Matches/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "match_id,tournament_id,team1_id,team2_id,game_result,game_points,ladder_position")] Matches matches)
        {
            if (ModelState.IsValid)
            {
                db.Matches.Add(matches);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.team1_id = new SelectList(db.Teams, "team_id", "name", matches.team1_id);
            ViewBag.team2_id = new SelectList(db.Teams, "team_id", "name", matches.team2_id);
            ViewBag.tournament_id = new SelectList(db.Tournaments, "tournament_id", "name", matches.tournament_id);
            return View(matches);
        }

        // GET: Matches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Matches matches = db.Matches.Find(id);
            if (matches == null)
            {
                return HttpNotFound();
            }
            ViewBag.team1_id = new SelectList(db.Teams, "team_id", "name", matches.team1_id);
            ViewBag.team2_id = new SelectList(db.Teams, "team_id", "name", matches.team2_id);
            ViewBag.tournament_id = new SelectList(db.Tournaments, "tournament_id", "name", matches.tournament_id);
            return View(matches);
        }

        // POST: Matches/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "match_id,tournament_id,team1_id,team2_id,game_result,game_points,ladder_position")] Matches matches)
        {
            if (ModelState.IsValid)
            {
                db.Entry(matches).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.team1_id = new SelectList(db.Teams, "team_id", "name", matches.team1_id);
            ViewBag.team2_id = new SelectList(db.Teams, "team_id", "name", matches.team2_id);
            ViewBag.tournament_id = new SelectList(db.Tournaments, "tournament_id", "name", matches.tournament_id);
            return View(matches);
        }

        // GET: Matches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Matches matches = db.Matches.Find(id);
            if (matches == null)
            {
                return HttpNotFound();
            }
            return View(matches);
        }

        // POST: Matches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Matches matches = db.Matches.Find(id);
            db.Matches.Remove(matches);
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
