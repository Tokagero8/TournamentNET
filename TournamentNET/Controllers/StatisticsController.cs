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
            ViewBag.match_id = new SelectList(db.Matches, "match_id", "game_result");
            ViewBag.player_id = new SelectList(db.Players, "player_id", "name");
            return View();
        }

        // POST: Statistics/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
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
            ViewBag.match_id = new SelectList(db.Matches, "match_id", "game_result", statistics.match_id);
            ViewBag.player_id = new SelectList(db.Players, "player_id", "name", statistics.player_id);
            return View(statistics);
        }

        // POST: Statistics/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
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
