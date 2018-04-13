using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tercume.BusinessLayer;
using Tercume.BusinessLayer.Result;
using Tercume.Entities;
using Tercume.WebApp.Filter;

namespace Tercume.WebApp.Controllers
{
    [Exc]
    public class TercumeUserController : Controller
    {
        private TercumeUserManager tercumeUserManager = new TercumeUserManager();


        public ActionResult Index()
        {
            return View(tercumeUserManager.List());
        }
        [Auth]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TercumeUser tercume_user = tercumeUserManager.Find(x => x.Id == id.Value);

            if (tercume_user == null)
            {
                return HttpNotFound();
            }

            return View(tercume_user);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TercumeUser user)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<TercumeUser> res = tercumeUserManager.Insert(user);

                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(user);
                }

                return RedirectToAction("Index");
            }

            return View(user);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TercumeUser user = tercumeUserManager.Find(x => x.Id == id.Value);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TercumeUser user)
        {

            if (ModelState.IsValid)
            {
                BusinessLayerResult<TercumeUser> res = tercumeUserManager.Update(user);

                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(user);
                }

                return RedirectToAction("Index");
            }
            return View(user);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TercumeUser user = tercumeUserManager.Find(x => x.Id == id.Value);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TercumeUser user = tercumeUserManager.Find(x => x.Id == id);
            tercumeUserManager.Delete(user);

            return RedirectToAction("Index");
        }
    }
}