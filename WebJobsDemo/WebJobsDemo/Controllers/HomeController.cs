﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebJobsDemo.Controllers
{
    using azureQueue;

    using WebJobsDemo.Models;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult SaveData(Source source)
        {
            var dbContext = new UsersContext();
            dbContext.Sources.Add(source);
            dbContext.SaveChanges();

            new QueueMgr().AddMessageToQueue(source.ToolName).Wait();
            return RedirectToAction("Index");
        }
    }
}
