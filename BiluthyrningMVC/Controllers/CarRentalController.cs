using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiluthyrningABdel1;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BiluthyrningMVC.Controllers
{
    public class CarRentalController : Controller
    {
        [Route("/rentcar")]
        public IActionResult RentCar()
        {
            return Json("json");
        }

        [Route("/")]
        public IActionResult Index()
        {
            return Content("Hej");
        }
    }
}