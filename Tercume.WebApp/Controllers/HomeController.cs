using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tercume.BusinessLayer;
using Tercume.BusinessLayer.Result;
using Tercume.DataAccessLayer;
using Tercume.Entities;
using Tercume.Entities.ValueObjects;
using Tercume.WebApp.Filter;
using Tercume.WebApp.Models;
using Tercume.WebApp.ViewModels;

namespace Tercume.WebApp.Controllers
{

    public class HomeController : Controller
    {

        DatabaseContext db = new DatabaseContext();
        TercumanManager tm = new TercumanManager();
        //private EvernoteUserManager evernoteUserManager = new EvernoteUserManager();
        private TercumanManager tercumanManager = new TercumanManager();
        private TercumeUserManager tercumeUserManager = new TercumeUserManager();
        private TranslateManager translateManager = new TranslateManager();
        private DilTercumenManager dilTercumenManager = new DilTercumenManager();
        private FaturaManager faturaManager = new FaturaManager();
        private DilManager dilManager = new DilManager();


        // GET: Home
        public ActionResult Index()
        {
            BusinessLayer.test t = new test();
            return View();

        }

        [HttpPost]
        public ActionResult Index(Translate model)
        {
            //ViewModel model2 = new DomainModel().FindSomething(model);
            ////IEnumerable<ViewModel> a = HelperFunction.dil(model);


            string kaynak_dil = model.KaynakDil;
            string hedef_dil = model.HedefDil;
            var tercuman = db.Tercumanlar.SqlQuery("select * "+
            "from[TercumeDb].[dbo].[Tercumanlar] as ter "+
            "where ter.Id in( "+


            "SELECT dt.Tercumanlar "+
            "FROM[TercumeDb].[dbo].[DilTercumen] as dt "+
            "where dt.Dil_isimler = (select id1.Id as kaynak from[TercumeDb].[dbo].[Diller] as id1 where Dil_isim =@kaynakdil ) " +
            "intersect "+
            "SELECT dt.Tercumanlar "+
            "FROM[TercumeDb].[dbo].[DilTercumen] as dt "+
            "where dt.Dil_isimler = (select id2.Id as hedef from[TercumeDb].[dbo].[Diller] as id2 where Dil_isim =@hedefdil))", new SqlParameter("@kaynakdil", kaynak_dil), new SqlParameter("@hedefdil", hedef_dil));
            


            return View("TercumanList", tercuman);

        }
        public ActionResult IndexUser()
        {
            return View();
        }
        
        public ActionResult IndexTercuman()
        {
            var translate = translateManager.ListQueryable().Where(
                x => x.Translator.Id == CurrentSessionsTercuman.User.Id).Where(x => x.Is_active == true);

            return View(translate.ToList());
        }
        
 
        public ActionResult ShowUserProfile()
        {
            BusinessLayerResult<TercumeUser> res =
                tercumeUserManager.GetUserById(CurrentSession.User.Id);

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
        public ActionResult ShowTercumanProfile()
        {
            BusinessLayerResult<Tercuman> res =
                tercumanManager.GetUserById(CurrentSessionsTercuman.User.Id);

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















        public ActionResult TercumanList()
        {
            return View();
        }
        

   
        //public ActionResult ShowUserProfile(int id)
        //{
        //    BusinessLayerResult<TercumeUser> res = tercumeUserManager.GetUserById(id);
        //    if (res.Errors.Count > 0)
        //    {
        //        ErrorViewModel errorNotifyObj = new ErrorViewModel()
        //        {
        //            Title = "Kullanıcı Bulunamadı",
        //            Items = res.Errors
        //        };

        //        return View("Error", errorNotifyObj);
        //    }
        //    return View(res.Result);
        //}
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult LoginUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginUser(LoginViewModelUser model)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<TercumeUser> res = tercumeUserManager.LoginUser(model);

                if (res.Errors.Count > 0)
                {

                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));

                    return View(model);
                }

                CurrentSession.Set<TercumeUser>("login", res.Result); // Session'a kullanıcı bilgi saklama..
                return RedirectToAction("Index");   // yönlendirme..
            }

            return View(model);
        }

        public ActionResult LoginTercuman()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginTercuman(LoginViewModelTranslator model)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<Tercuman> res = tercumanManager.LoginTranslator(model);

                if (res.Errors.Count > 0)
                {

                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));

                    return View(model);
                }

                CurrentSessionsTercuman.Set<Tercuman>("login", res.Result); // Session'a kullanıcı bilgi saklama..
                return RedirectToAction("IndexTercuman");   // yönlendirme..
            }

            return View(model);
        }


        public ActionResult Register()
        {
            return View();
        }



        public ActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterUser(RegisterViewModelUser model)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<TercumeUser> res = tercumeUserManager.RegisterTercumeUser(model);

                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }
                OkViewModel notifyObj = new OkViewModel()
                {
                    Title = "Kayıt Başarılı",
                    RedirectingUrl = "/Home/Index"
                };

                notifyObj.Items.Add("Lütfen e-posta adresinize gönderdiğimiz aktivasyon link'ine tıklayarak hesabınızı aktive ediniz. Hesabınızı aktive etmeden not ekleyemez ve beğenme yapamazsınız.");

                return View("Ok", notifyObj);
            }
            
            return View(model);
        }

        public ActionResult RegisterTercuman()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterTercuman(RegisterViewModelTranslator model)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<Tercuman> res = tercumanManager.RegisterTercuman(model);
                DilTercumen dt = new DilTercumen();
                //burada ana dil ekle
                string ana_dil = model.AnaDil;
                BusinessLayerResult<Dil> dil = dilManager.GetLanguageById(ana_dil);
                dt.Dil_isimler = dil.Result.Id;
                dt.Tercumanlar = res.Result.Id;
                BusinessLayerResult<DilTercumen> res2 = dilTercumenManager.RegisterTercuman(dt);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }
                OkViewModel notifyObj = new OkViewModel()
                {
                    Title = "Kayıt Başarılı",
                    RedirectingUrl = "/Home/Login",
                };
                
                notifyObj.Items.Add("Lütfen e-posta adresinize gönderdiğimiz aktivasyon link'ine tıklayarak hesabınızı aktive ediniz. Hesabınızı aktive etmeden not ekleyemez ve beğenme yapamazsınız.");

                return View("Ok", notifyObj);
            }

            return View(model);
        }

        public ActionResult EditProfileTercuman()
        {
            BusinessLayerResult<Tercuman> res = tercumanManager.GetUserById(CurrentSessionsTercuman.User.Id);

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
        public ActionResult EditProfileTercuman(Tercuman model, HttpPostedFileBase ProfileImage)
        {
         
            if (ModelState.IsValid)
            {
                if (ProfileImage != null &&
                    (ProfileImage.ContentType == "image/jpeg" ||
                    ProfileImage.ContentType == "image/jpg" ||
                    ProfileImage.ContentType == "image/png"))
                {
                    string filename = $"user_{model.Id}.{ProfileImage.ContentType.Split('/')[1]}";

                    ProfileImage.SaveAs(Server.MapPath($"~/images/{filename}"));
                    model.ProfileImageFilename = filename;
                }

                BusinessLayerResult<Tercuman> res = tercumanManager.UpdateProfile(model);

                if (res.Errors.Count > 0)
                {
                    ErrorViewModelTercuman errorNotifyObj = new ErrorViewModelTercuman()
                    {
                        Items = res.Errors,
                        Title = "Profil Güncellenemedi.",
                        RedirectingUrl = "/Home/EditProfileTercuman"
                    };
                    
                    return View("ErrorTercuman", errorNotifyObj);
                }

                //Profil güncellendiği için session güncellendi.
                CurrentSessionsTercuman.Set<Tercuman>("login", res.Result);

                return RedirectToAction("ShowTercumanProfile");
            }

            return View(model);
        }







        public ActionResult EditProfileUser()
        {
            BusinessLayerResult<TercumeUser> res = tercumeUserManager.GetUserById(CurrentSession.User.Id);

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
        public ActionResult EditProfileUser(TercumeUser model, HttpPostedFileBase ProfileImage)
        {
          

            if (ModelState.IsValid)
            {
                if (ProfileImage != null &&
                    (ProfileImage.ContentType == "image/jpeg" ||
                    ProfileImage.ContentType == "image/jpg" ||
                    ProfileImage.ContentType == "image/png"))
                {
                    string filename = $"user_{model.Id}.{ProfileImage.ContentType.Split('/')[1]}";

                    ProfileImage.SaveAs(Server.MapPath($"~/images/{filename}"));
                    model.ProfileImageFilename = filename;
                }

                BusinessLayerResult<TercumeUser> res = tercumeUserManager.UpdateProfile(model);

                if (res.Errors.Count > 0)
                {
                    ErrorViewModel errorNotifyObj = new ErrorViewModel()
                    {
                        Items = res.Errors,
                        Title = "Profil Güncellenemedi.",
                        RedirectingUrl = "/Home/EditProfileUser"
                    };

                    return View("Error", errorNotifyObj);
                }

                //Profil güncellendiği için session güncellendi.
                CurrentSession.Set<TercumeUser>("login", res.Result);

                return RedirectToAction("ShowUserProfile");
            }

            return View(model);
        }











        public ActionResult DeleteProfileTercuman()
        {
            BusinessLayerResult<Tercuman> res =
                tercumanManager.RemoveUserById(CurrentSessionsTercuman.User.Id);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel errorNotifyObj = new ErrorViewModel()
                {
                    Items = res.Errors,
                    Title = "Profil Silinemedi.",
                    RedirectingUrl = "/Home/ShowProfile"
                };

                return View("Error", errorNotifyObj);
            }

            Session.Clear();

            return RedirectToAction("Index");
        }

        public ActionResult DeleteProfileUser()
        {
            BusinessLayerResult<TercumeUser> res =
                tercumeUserManager.RemoveUserById(CurrentSession.User.Id);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel errorNotifyObj = new ErrorViewModel()
                {
                    Items = res.Errors,
                    Title = "Profil Silinemedi.",
                    RedirectingUrl = "/Home/ShowProfile"
                };

                return View("Error", errorNotifyObj);
            }

            Session.Clear();

            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult LogoutUser()
        {
            CurrentSessionsTercuman.Clear();
            return RedirectToAction("Index");
        }
        public ActionResult LogoutTercuman()
        {
            CurrentSession.Clear();
            return RedirectToAction("Index");
        }


        public ActionResult AccessDenied()
        {
            return View();
        }


        public ActionResult HasError()
        {
            return View();
        }



        /*public ActionResult ByCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category cat = categoryManager.Find(x => x.Id == id.Value);

            if (cat == null)
            {
                return HttpNotFound();
                //return RedirectToAction("Index", "Home");
            }

            List<Note> notes = cat.Notes.Where(
                x => x.IsDraft == false).OrderByDescending(x => x.ModifiedOn).ToList()

            List<Note> notes = noteManager.ListQueryable().Where(
                x => x.IsDraft == false && x.CategoryId == id).OrderByDescending(
                x => x.ModifiedOn).ToList();

            return View("Index", notes);
        }*/

        /*public ActionResult MostLiked()
        {
            return View("Index", noteManager.ListQueryable().OrderByDescending(x => x.LikeCount).ToList());
        }*/

















        public ActionResult UserActivate(Guid id)
        {
            BusinessLayerResult<TercumeUser> res = tercumeUserManager.ActivateUser(id);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel errorNotifyObj = new ErrorViewModel()
                {
                    Title = "Geçersiz İşlem",
                    Items = res.Errors
                };

                return View("Error", errorNotifyObj);
            }

            OkViewModel okNotifyObj = new OkViewModel()
            {
                Title = "Hesap Aktifleştirildi",
                RedirectingUrl = "/Home/Login",
            };

            okNotifyObj.Items.Add("Hesabınız aktifleştirildi. Artık not paylaşabilir ve beğenme yapabilirsiniz.");

            return View("Ok", okNotifyObj);
        }


        public ActionResult TercumanActivate(Guid id)
        {
            BusinessLayerResult<Tercuman> res = tercumanManager.ActivateTercuman(id);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel errorNotifyObj = new ErrorViewModel()
                {
                    Title = "Geçersiz İşlem",
                    Items = res.Errors
                };

                return View("Error", errorNotifyObj);
            }

            OkViewModel okNotifyObj = new OkViewModel()
            {
                Title = "Hesap Aktifleştirildi",
                RedirectingUrl = "/Home/Login",
            };

            okNotifyObj.Items.Add("Hesabınız aktifleştirildi. Artık not paylaşabilir ve beğenme yapabilirsiniz.");

            return View("Ok", okNotifyObj);
        }

        public ActionResult TercumeCreate(int id)
        {
         

            if (CurrentSession.User == null)
            {
                ErrorViewModel errorNotifyObj = new ErrorViewModel()
                {
                    Title = "Lütfen Giriş yapınız",
                };

                return View("Error", errorNotifyObj);
            }


            ViewBag.Tercuman_id = id;
            BusinessLayerResult<DilTercumen> res =
                dilTercumenManager.GetDilTercumenByTranslatorId(id);
            Translate t = new Translate();
            t.StartDate = DateTime.Now;
            t.EndDate = DateTime.Now;


            //Tercuman ter = tercumanManager.Find(x=>x.Id == id);
            //Translate t = new Translate();
            //t.Translator = ter;

            ViewBag.Dil_idHedef = new SelectList(CacheHelper.GetLanguageFromCache(), "Id", "Dil_isim", t.HedefDil);
            ViewBag.Dil_idKaynak = new SelectList(CacheHelper.GetLanguageFromCache(), "Id", "Dil_isim", t.KaynakDil);
            return View(t);
        }

        [HttpPost]
        public ActionResult TercumeCreate(Translate translate, HttpPostedFileBase file)
        {
            //if (translate.Text.Length > 0 && translate.Text.Length <= 200)
            //{
            //    translate.Price = 20;
            //}
            //else if (translate.Text.Length > 200 && translate.Text.Length <= 400)
            //{
            //    translate.Price = 40;
            //}
            byte[] bytes;
            string contentType;
            using (BinaryReader br = new BinaryReader(file.InputStream))
            {
                bytes = br.ReadBytes(file.ContentLength);
                contentType = file.ContentType;
            }
            translate.Price = 30;
            translate.Data = bytes;
            translate.ContentType = contentType;
            translate.Is_active = false;
            translate.Is_finish = false;
            translate.StartDate = DateTime.Now;
            translate.EndDate = DateTime.Now;
            Tercuman ter = tercumanManager.Find(x => x.Id == translate.ter_id);
            translate.Translator = ter;

            //int id = ter.Ter.Id;
            //Tercuman tercuman = tercumanManager.Find(x => x.Id == id);
            //ter.Tercuman.Password = tercuman.Password;
            //ter.Tercuman.Email = tercuman.Email;
            //ter.Owner = CurrentSession.User;

            Fatura fat = new Fatura();
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)

            {

                translate.Owner = CurrentSession.User;
                translateManager.Insert(translate);
                fat.TercumeUser = CurrentSession.User;
                fat.Translator = translate.Translator;
                fat.Translate = translate;
                fat.UserName = CurrentSession.User.Name;
                fat.TranslatorName = translate.Translator.Name;
                fat.ToplamUcret = translate.Price;
                double u = translate.Price;
                fat.TercumanUcret = u * 0.3;
                faturaManager.Insert(fat);
                return RedirectToAction("Index");
            }

            //ViewBag.CategoryId = new SelectList(CacheHelper.GetCategoriesFromCache(), "Id", "Title", note.CategoryId);
            return View(translate);
        }


        //private Translate TranslateData = new Translate();
        //[HttpPost]
        //public void DataAl(HttpPostedFileBase postedFile)
        //{
        //    byte[] bytes;
        //    string contentType;
        //    using (BinaryReader br = new BinaryReader(postedFile.InputStream))
        //    {
        //        bytes = br.ReadBytes(postedFile.ContentLength);
        //        contentType = postedFile.ContentType;
        //    }
        //    TranslateData.Data = bytes;
        //    TranslateData.ContentType = contentType;
        //    //return RedirectToAction("TercumeCreate");
        //}




        //[HttpPost]
        //public FileResult DownloadFile(int translateId)
        //{
        //    byte[] bytes;
        //    string fileName, contentType;

        //    BusinessLayerResult<Translate> res =
        //        translateManager.GetTranslate(translateId);



        //    bytes = res.Result.Data;
        //    fileName = res.Result.Title;
        //    contentType = res.Result.ContentType;


        //    if (res.Errors.Count > 0)
        //    {
        //        ErrorViewModel errorNotifyObj = new ErrorViewModel()
        //        {
        //            Title = "Hata Oluştu",
        //            Items = res.Errors
        //        };


        //    }

        //    return File(bytes, contentType, fileName);
        //}


        [HttpGet]
        public FileResult DownLoadFile(int id)
        {


            BusinessLayerResult<Translate> res =
                translateManager.GetTranslate(id);



            return File(res.Result.Data, "application/pdf", res.Result.ContentType);

        }




    }
}