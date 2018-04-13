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
using Tercume.WebApp.Models;
using Tercume.WebApp.ViewModels;

namespace Tercume.WebApp.Controllers
{
    public class TercumeController : Controller
    {
        // GET: Tercume
        private TercumeManager tercumeManager = new TercumeManager();
        private TercumeManager tercumeUserManager = new TercumeManager();
        private TercumanManager tercumanManager = new TercumanManager();
        private TranslateManager translateManager = new TranslateManager();
        private DilManager dilManager = new DilManager();


        #region Admin
        [Auth]
        public ActionResult Index()
        {
            return View(tercumeManager.List());
        }

        [Auth]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Translate tercume = tercumeManager.Find(x => x.Id == id.Value);

            if (tercume == null)
            {
                return HttpNotFound();
            }

            return View(tercume);
        }

        public ActionResult TercumeText(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Translate tercume = tercumeManager.Find(x => x.Id == id.Value);

            if (tercume == null)
            {
                return HttpNotFound();
            }

            return View(tercume);
        }

        [Auth]
        public ActionResult Create()
        {
            
            return View();
        }

        [Auth]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Translate translate)
        {
            if (ModelState.IsValid)
            {
                translate.Owner = CurrentSession.User;
                translateManager.Insert(translate);
                return RedirectToAction("Index");
            }
            return View(translate);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Translate user = tercumeManager.Find(x => x.Id == id.Value);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(Translate ter)
        {
            if (ModelState.IsValid)
            {
                Translate db_ter = tercumeManager.Find(x => x.Id == ter.Id);
                db_ter.EndDate = ter.EndDate;
                db_ter.StartDate = ter.StartDate;
                db_ter.Price = ter.Price;
                db_ter.Text = ter.Text;
                db_ter.TercumeText = ter.TercumeText;


                tercumeManager.Update(db_ter);

                return RedirectToAction("Index");
            }

            return View(ter);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Translate user = tercumeManager.Find(x => x.Id == id.Value);

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
            Translate user = tercumeManager.Find(x => x.Id == id);
            tercumeManager.Delete(user);

            return RedirectToAction("Index");
        } 
        #endregion





        #region User
        public ActionResult TercumeIstekleri()
        {

            var translate = translateManager.ListQueryable().Where(
                x => x.Owner.Id == CurrentSession.User.Id).Where(x => x.Is_active == false).Where(x => x.Is_finish == false);

            return View(translate.ToList());


        }
        public ActionResult DevamEdenTercumeler()
        {
            var translate = translateManager.ListQueryable().Where(
                x => x.Owner.Id == CurrentSession.User.Id).Where(x => x.Is_active == true).Where(x => x.Is_finish == false);

            return View(translate.ToList());

        }
        public ActionResult TercumeGecmisi()
        {
            var translate = translateManager.ListQueryable().Where(
                x => x.Owner.Id == CurrentSession.User.Id).Where(x => x.Is_active == false).Where(x => x.Is_finish == true);

            return View(translate.ToList());
        }
        public ActionResult TercumeGecmisiTercuman()
        {
            var translate = translateManager.ListQueryable().Where(
                x => x.Owner.Id == CurrentSessionsTercuman.User.Id).Where(x => x.Is_active == false).Where(x => x.Is_finish == true);

            return View(translate.ToList());
        }

        #endregion




        #region Tercuman
        
        public ActionResult TranslateKabul(int id)
        {
            BusinessLayerResult<Translate> res =
                translateManager.GetTranslate(id);

            res.Result.Is_active = true;
            translateManager.Update(res.Result);
            return RedirectToAction("IndexTercuman", "Home");   
        }

        
        public ActionResult TranslateReddet(int id)
        {
            if (ModelState.IsValid)
            {
                Translate db_translate = translateManager.Find(x => x.Id == id);
                db_translate.Translator = null;
                translateManager.Update(db_translate);
            }

            return RedirectToAction("IndexTercuman", "Home");
        }
        
        public ActionResult TranslateScreen(int id)
        {
            BusinessLayerResult<Translate> res =
                translateManager.GetTranslate(id);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel errorNotifyObj = new ErrorViewModel()
                {
                    Title = "Hata Oluştu",
                    Items = res.Errors
                };

                return View("Error", errorNotifyObj);
            }

            return View(res.Result);
        }
        [HttpPost]
        public ActionResult TranslateScreen(Translate translate)
        {
            if (ModelState.IsValid)
            {
                Translate db_translate = translateManager.Find(x => x.Id == translate.Id);


                db_translate.TercumeText = translate.TercumeText;

                translateManager.Update(db_translate);

                return RedirectToAction("IndexTercuman", "Home");
            }

            return View(translate);
        }
        public ActionResult TranslateFinish(int id)
        {
            if (ModelState.IsValid)
            {
                Translate db_translate = translateManager.Find(x => x.Id == id);
                db_translate.Is_finish = true;
                db_translate.Is_active = false;
                translateManager.Update(db_translate);
            }

            return RedirectToAction("IndexTercuman", "Home");
        } 
        #endregion




    }
}