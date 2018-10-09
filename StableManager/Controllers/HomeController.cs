using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StableManager.Models;

namespace StableManager.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Gallery()
        {
            ViewData["Message"] = "Your Gallery page.";

            return View();
        }

        [Route("Home/Services")]
        [Route("Home/Services/Overview")]
        public IActionResult ServicesOverview()
        {
            ViewData["Message"] = "Your Services-Overview page.";

            return View();
        }

        [Route("Home/Services/Coaching")]
        public IActionResult ServicesCoaching()
        {
            ViewData["Message"] = "Your Services-Coaching page.";

            return View();
        }

        [Route("Home/Services/Events")]
        public IActionResult ServicesEvents()
        {
            ViewData["Message"] = "Your Services-Events page.";

            return View();
        }

        [Route("Home/Services/Feeding")]
        public IActionResult ServicesFeeding()
        {
            ViewData["Message"] = "Your Services-Feeding page.";

            return View();
        }

        [Route("Home/Services/Boarding")]
        public IActionResult ServicesBoarding()
        {
            ViewData["Message"] = "Your Services-Boarding page.";

            return View();
        }

        [Route("Home/Services/Arena")]
        public IActionResult ServicesArena()
        {
            ViewData["Message"] = "Your Services-Arena page.";

            return View();
        }



        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
