using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tercume.BusinessLayer;
using Tercume.BusinessLayer.Result;
using Tercume.DataAccessLayer;
using Tercume.Entities;
using Tercume.WebApp.Models;
using Tercume.WebApp.ViewModels;

namespace Tercume.WebApp.Controllers
{
    public class TercumanController : Controller
    {
        // GET: Tercuman

        #region Admin
        private TercumanManager tercumanManager = new TercumanManager();
        private DilManager dilManager = new DilManager();
        private DilTercumenManager diltercumenManager = new DilTercumenManager();

        public ActionResult Index()
        {
            return View(tercumanManager.List());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Tercuman tercuman = tercumanManager.Find(x => x.Id == id.Value);

            if (tercuman == null)
            {
                return HttpNotFound();
            }

            return View(tercuman);
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(Tercuman tercuman)
        {


            if (ModelState.IsValid)
            {
                tercuman.Create_on = DateTime.Now;
                BusinessLayerResult<Tercuman> res = tercumanManager.Insert(tercuman);

                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(tercuman);
                }

                return RedirectToAction("Index");
            }

            return View(tercuman);
        }

        public ActionResult Edit(int id)
        {
            BusinessLayerResult<Tercuman> res = tercumanManager.GetUserById(id);

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
        public ActionResult Edit(Tercuman tercuman)
        {

            if (ModelState.IsValid)
            {
                BusinessLayerResult<Tercuman> res = tercumanManager.Update(tercuman);

                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(tercuman);
                }

                return RedirectToAction("Index");
            }
            return View(tercuman);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Tercuman tercuman = tercumanManager.Find(x => x.Id == id.Value);

            if (tercuman == null)
            {
                return HttpNotFound();
            }

            return View(tercuman);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tercuman tercuman = tercumanManager.Find(x => x.Id == id);
            tercumanManager.Delete(tercuman);

            return RedirectToAction("Index");
        }

        #endregion




        DatabaseContext db = new DatabaseContext();
        public ActionResult DilEkle()
        {
            int ter_id = CurrentSessionsTercuman.User.Id;
            var dil = db.Diller.SqlQuery("select * from[TercumeDb].[dbo].Diller as d " +
                                            "WHERE d.Id not in( " +
                                            "select dt.Dil_isimler from[TercumeDb].[dbo].[DilTercumen] as dt " +
                                            "where dt.Tercumanlar = @ter_id)", new SqlParameter("@ter_id", ter_id));



            return View(dil);
            //return View(dilManager.List());
        }

        [HttpPost]
        public JsonResult SaveList(string ItemList)
        {
            DilTercumen dl = new DilTercumen();

            string[] arr = ItemList.Split(',');

            for (int i = 0; i < arr.Length; i++)
            {
                dl.Tercumanlar = CurrentSessionsTercuman.User.Id;
                dl.Dil_isimler = Convert.ToInt32(arr[i]);
                diltercumenManager.Insert(dl);
            }



            return Json("",JsonRequestBehavior.AllowGet);
           
        }





























       
    }
}