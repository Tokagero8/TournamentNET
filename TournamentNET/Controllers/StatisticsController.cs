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
    public class StatisticsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Statistics
        public ActionResult Index()
        {
            var statistics = db.Statistics.Include(s => s.Matches).Include(s => s.Players);
            return View(statistics.ToList());
        }

        // GET: Statistics/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Statistics statistics = db.Statistics.Find(id);
            if (statistics == null)
            {
                return HttpNotFound();
            }
            return View(statistics);
        }

        // GET: Statistics/Create
        public ActionResult Create()
        {
            List<Matches> matches = db.Matches.ToList();
            List<Players> players = db.Players.ToList();
            List<ViewModelMatchTeams> matchTeamsList = new List<ViewModelMatchTeams>();
            List<ViewModelPlayer> name_Lastname = new List<ViewModelPlayer>();
            foreach (Matches m in matches)
            {
                matchTeamsList.Add(new ViewModelMatchTeams(m.match_id, m.Teams.name + " vs " + m.Teams1.name));
            }
            foreach (Players p in players)
            {
                name_Lastname.Add(new ViewModelPlayer(p.player_id, p.name + " " + p.last_name));
            }
            ViewBag.match_id = new SelectList(matchTeamsList, "match_id", "team1_and_team2");
            ViewBag.player_id = new SelectList(name_Lastname, "player_id", "name_lastname");
            return View();
        }

        // POST: Statistics/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "statistics_id,match_id,player_id,points,error,blocks,aces")] Statistics statistics)
        {
            if (ModelState.IsValid)
            {
                db.Statistics.Add(statistics);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.match_id = new SelectList(db.Matches, "match_id", "game_result", statistics.match_id);
            ViewBag.player_id = new SelectList(db.Players, "player_id", "name", statistics.player_id);
            return View(statistics);
        }

        // GET: Statistics/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Statistics statistics = db.Statistics.Find(id);
            if (statistics == null)
            {
                return HttpNotFound();
            }
            List<Matches> matches = db.Matches.ToList();
            List<Players> players = db.Players.ToList();
            List<ViewModelMatchTeams> matchTeamsList = new List<ViewModelMatchTeams>();
            List<ViewModelPlayer> name_Lastname = new List<ViewModelPlayer>();
            foreach (Matches m in matches)
            {
                matchTeamsList.Add(new ViewModelMatchTeams(m.match_id, m.Teams.name + " vs " + m.Teams1.name));
            }
            foreach (Players p in players)
            {
                name_Lastname.Add(new ViewModelPlayer(p.player_id, p.name + " " + p.last_name));
            }
            ViewBag.match_id = new SelectList(matchTeamsList, "match_id", "team1_and_team2", statistics.match_id);
            ViewBag.player_id = new SelectList(name_Lastname, "player_id", "name_lastname", statistics.player_id);
            return View(statistics);
        }

        // POST: Statistics/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "statistics_id,match_id,player_id,points,error,blocks,aces")] Statistics statistics)
        {
            if (ModelState.IsValid)
            {
                db.Entry(statistics).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.match_id = new SelectList(db.Matches, "match_id", "game_result", statistics.match_id);
            ViewBag.player_id = new SelectList(db.Players, "player_id", "name", statistics.player_id);
            return View(statistics);
        }

        // GET: Statistics/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Statistics statistics = db.Statistics.Find(id);
            if (statistics == null)
            {
                return HttpNotFound();
            }
            return View(statistics);
        }

        // POST: Statistics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Statistics statistics = db.Statistics.Find(id);
            db.Statistics.Remove(statistics);
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
