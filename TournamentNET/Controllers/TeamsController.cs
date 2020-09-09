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
    public class TeamsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Teams
        public ActionResult Index()
        {
            var teams = db.Teams.Include(t => t.Tournaments);
            return View(teams.ToList());
        }

        // GET: Teams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teams teams = db.Teams.Find(id);
            List<Players> players = new List<Players>();
            players = teams.Players.ToList().FindAll(p => p.team_id == id).OrderBy(p => p.name).ToList();
            ViewModelTeamPlayers viewModelTeamPlayers = new ViewModelTeamPlayers();
            viewModelTeamPlayers.teams = teams;
            viewModelTeamPlayers.players = players;
            if (viewModelTeamPlayers == null)
            {
                return HttpNotFound();
            }
            return View(viewModelTeamPlayers);
        }

        // GET: Teams/Create
        public ActionResult Create()
        {
            ViewBag.tournament_id = new SelectList(db.Tournaments, "tournament_id", "name");
            return View();
        }

        // POST: Teams/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "team_id,tournament_id,name,ranking")] Teams teams)
        {
            if (ModelState.IsValid)
            {
                db.Teams.Add(teams);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.tournament_id = new SelectList(db.Tournaments, "tournament_id", "name", teams.tournament_id);
            return View(teams);
        }

        // GET: Teams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teams teams = db.Teams.Find(id);
            if (teams == null)
            {
                return HttpNotFound();
            }
            ViewBag.tournament_id = new SelectList(db.Tournaments, "tournament_id", "name", teams.tournament_id);
            return View(teams);
        }

        // POST: Teams/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "team_id,tournament_id,name,ranking")] Teams teams)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teams).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.tournament_id = new SelectList(db.Tournaments, "tournament_id", "name", teams.tournament_id);
            return View(teams);
        }

        // GET: Teams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teams teams = db.Teams.Find(id);
            if (teams == null)
            {
                return HttpNotFound();
            }
            return View(teams);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Teams teams = db.Teams.Find(id);
            db.Teams.Remove(teams);
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
