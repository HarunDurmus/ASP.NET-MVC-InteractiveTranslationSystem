using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tercume.BusinessLayer;

namespace Tercume.WebApp.Controllers
{
    public class FaturaController : Controller
    {

        private FaturaManager faturaManager = new FaturaManager();

        // GET: Fatura
        public ActionResult Index()
        {
            return View(faturaManager.List());
        }


    }
}