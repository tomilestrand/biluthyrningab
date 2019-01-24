using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiluthyrningABdel1;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using BiluthyrningMVC.Models;

namespace BiluthyrningMVC.Controllers
{
    public class CarRentalController : Controller
    {
        CarRentalService service;

        public CarRentalController(CarRentalService service)
        {
            this.service = service;
        }

        [HttpPost]
        [Route("/rentcar")]
        public IActionResult RentCar([FromBody]RentFormSubmitVM json)
        {
            RentFormResponseVM response;
            if (Biluthyrning.ValidSSN(json.SSN))
            {
                response = service.MakeRentFormResponseVM(json);
            }
            else
            {
                response= new RentFormResponseVM { Status = "Invalid SSN" };
            }
            return Json(response);
        }

        [Route("/rentcar")]
        [HttpGet]
        public IActionResult RentCar()
        {
            return Ok();
        }

        [Route("/")]
        public IActionResult Index()
        {
            return Json(new Car { CarType = 1, NumOfKm = 10000, RegNum = "ABC123" });
        }

        [HttpPost]
        [Route("/returncar")]
        public IActionResult ReturnCar([FromBody]ReturnFormSubmitVM json)
        {
            ReturnFormResponseVM response = service.MakeReturnFormResponseVM(json);
           
            return Json(response);
        }
    }
}