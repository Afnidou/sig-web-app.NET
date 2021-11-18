using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class NRsController : Controller
    {
        private casaEntities db = new casaEntities();


        // GET: NRs
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View(db.NR.ToList());
        }

        // GET: NRs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NR nR = db.NR.Find(id);
            if (nR == null)
            {
                return HttpNotFound();
            }
            return View(nR);
        }

        // GET: NRs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NRs/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OBJECTID,Reference_,Prenom,Societe,Qualite,Nature_ter,Zonage,indice_Equ,Voirie,Autres,NR_Id,Etat,SHAPE_STAr,SHAPE_STLe,SHAPE")] NR nR)
        {
            if (ModelState.IsValid)
            {
                db.NR.Add(nR);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nR);
        }

        // GET: NRs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NR nR = db.NR.Find(id);
            if (nR == null)
            {
                return HttpNotFound();
            }
            return View(nR);
        }

        // POST: NRs/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OBJECTID,Reference_,Prenom,Societe,Qualite,Nature_ter,Zonage,indice_Equ,Voirie,Autres,NR_Id,Etat,SHAPE_STAr,SHAPE_STLe,SHAPE")] NR nR)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nR).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nR);
        }

        // GET: NRs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NR nR = db.NR.Find(id);
            if (nR == null)
            {
                return HttpNotFound();
            }
            return View(nR);
        }

        // POST: NRs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NR nR = db.NR.Find(id);
            db.NR.Remove(nR);
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
        [Authorize]
        public ActionResult carte()
        {
            return View();
        }

        public ActionResult zoom(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NR nR = db.NR.Find(id);
            if (nR == null)
            {
                return HttpNotFound();
            }
            return View(nR);
        }


        [Authorize(Roles="admin")]
        public ActionResult Chercher(String Etat, String NR_Id)
        {
            if (!String.IsNullOrEmpty(Etat) || (!String.IsNullOrEmpty(NR_Id)))
            {
                IEnumerable<NR> vls = db.NR.Where(v => v.Etat.Contains(Etat) && v.NR_Id.Contains(NR_Id));
                return View("Index", vls);
            }
            return View("Index", db.NR.ToList());
        }

        public ActionResult Stat()
        {
            int non = 0;
            int en = 0;
            int de = 0;
            foreach (NR n in db.NR)
            {
                if (n.Etat == "Non traite") { non++; ViewBag.non = non; }
                if (n.Etat == "En cours de traitement") { en++; ViewBag.en = en; }
                if (n.Etat == "Delivre") { de++; ViewBag.de = de; }
            }

            return View();
        }
    }
}