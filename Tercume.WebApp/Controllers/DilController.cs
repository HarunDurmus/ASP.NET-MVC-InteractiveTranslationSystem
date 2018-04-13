using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tercume.BusinessLayer;
using Tercume.Entities;

namespace Tercume.WebApp.Controllers
{
    public class DilController : Controller
    {
        private DilManager dilManager = new DilManager();

        // GET: Dil
        public ActionResult Index()
        {
            return View(dilManager.List());
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Dil dil = dilManager.Find(x => x.Id == id.Value);

            if (dil == null)
            {
                return HttpNotFound();
            }

            return View(dil);
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Dil dil)
        {
           

            if (ModelState.IsValid)
            {
                dilManager.Insert(dil);
                
                return RedirectToAction("Index");
            }

            return View(dil);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Dil dil = dilManager.Find(x => x.Id == id.Value);

            if (dil == null)
            {
                return HttpNotFound();
            }
            return View(dil);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Dil dil)
        {
         
            if (ModelState.IsValid)
            {
                Dil cat = dilManager.Find(x => x.Id == dil.Id);
                cat.Dil_isim = dil.Dil_isim;
                
                dilManager.Update(cat);
                return RedirectToAction("Index");
            }
            return View(dil);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Dil dil = dilManager.Find(x => x.Id == id.Value);

            if (dil == null)
            {
                return HttpNotFound();
            }

            return View(dil);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dil category = dilManager.Find(x => x.Id == id);
            dilManager.Delete(category);

            return RedirectToAction("Index");
        }
    }
}