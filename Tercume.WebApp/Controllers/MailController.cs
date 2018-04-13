using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tercume.BusinessLayer;
using Tercume.BusinessLayer.Result;
using Tercume.Entities;
using Tercume.WebApp.Models;
using Tercume.WebApp.ViewModels;

namespace Tercume.WebApp.Controllers
{
    public class MailController : Controller
    {
        private TranslateManager translateManager = new TranslateManager();
        private MailManager mailManager = new MailManager();
        // GET: Mail
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MailBox()
        {
            var translate = translateManager.ListQueryable().Where(
                x => x.Translator.Id == CurrentSessionsTercuman.User.Id).Where(x=> x.Is_active == false && x.Is_finish==false).OrderByDescending(
                x => x.StartDate);

            return View(translate.ToList());
        }

        public ActionResult MailRead(int id)
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

            //var translate = translateManager.ListQueryable().Where(
            //   x => x.Id == id);


            //return View(translate.ToList());
        }
        public ActionResult ComposeUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ComposeUser(Mesaj msg)
        {
            msg.is_read = false;
            msg.Create_on = DateTime.Now;
            msg.sender_Email = CurrentSession.User.Email;
            msg.TercumeUser = CurrentSession.User;
            var errors = ModelState.Values.SelectMany(v => v.Errors);
              if (ModelState.IsValid)
            {
                
                mailManager.Insert(msg);
                return RedirectToAction("Index","Home");
            }

            return View(msg);
        }

        public ActionResult ComposeTercuman()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ComposeTercuman(Mesaj msg)
        {
            msg.is_read = false;
            msg.Create_on = DateTime.Now;

            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)

            {
                msg.Tercuman = CurrentSessionsTercuman.User;
                mailManager.Insert(msg);
                return RedirectToAction("IndexTercuman","Home");
            }

            return View(msg);
        }





        public ActionResult UserMailBox()
        {
            var mail = mailManager.ListQueryable().Where(
                x => x.receiver_Email==CurrentSession.User.Email && x.is_read == false).OrderByDescending(
                x => x.Create_on);

            return View(mail.ToList());
        }

        public ActionResult TercumanMailBox()
        {
            var mail = mailManager.ListQueryable().Where(
                x => x.receiver_Email == CurrentSessionsTercuman.User.Email && x.is_read == false).OrderByDescending(
                x => x.Create_on);

            return View(mail.ToList());
        }

        public ActionResult TercumanMailRead(int id)
        {
            BusinessLayerResult<Mesaj> res =
                mailManager.GetMail(id);

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

        public ActionResult UserMailRead(int id)
        {
            BusinessLayerResult<Mesaj> res =
                mailManager.GetMail(id);

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

        public ActionResult UserMailOkunmus(int id)
        {
            var mail = mailManager.ListQueryable().Where(
                 x => x.receiver_Email == CurrentSession.User.Email && x.is_read == true).OrderByDescending(
                 x => x.Create_on);

            return View(mail.ToList());
        }








        public ActionResult TranslatorMailBox()
        {
            var mail = mailManager.ListQueryable().Where(
                x => x.receiver_Email == CurrentSessionsTercuman.User.Email && x.is_read == false).OrderByDescending(
                x => x.Create_on);

            return View(mail.ToList());
        }

        public ActionResult TranslatorMailRead(int id)
        {
            BusinessLayerResult<Mesaj> res =
                mailManager.GetMail(id);

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

        public ActionResult TranslatorMailOkunmus(int id)
        {
            var mail = mailManager.ListQueryable().Where(
                 x => x.receiver_Email == CurrentSessionsTercuman.User.Email && x.is_read == true).OrderByDescending(
                 x => x.Create_on);

            return View(mail.ToList());
        }

        //[HttpGet]
        //public FileResult DownLoadFile(int id)
        //{
        //    BusinessLayerResult<Translate> res =
        //        translateManager.GetTranslate(id);

        //    return File(res.Result.Data, "application.pdf", res.Result.ContentType);

        //}

        [HttpGet]
        public FileResult DownloadFile(int id)
        {
            byte[] bytes;
            string fileName, contentType;

            BusinessLayerResult<Translate> res =
                translateManager.GetTranslate(id);



            bytes = res.Result.Data;
            fileName = res.Result.Title;
            contentType = res.Result.ContentType;


            if (res.Errors.Count > 0)
            {
                ErrorViewModel errorNotifyObj = new ErrorViewModel()
                {
                    Title = "Hata Oluştu",
                    Items = res.Errors
                };


            }

            return File(bytes, contentType, "Report.pdf");
        }





    }
}