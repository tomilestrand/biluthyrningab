using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiluthyrningABdel1;

namespace BiluthyrningMVC.Models
{
    public class CarRentalService
    {
        internal RentFormResponseVM MakeRentFormResponseVM(RentFormSubmitVM json)
        {
            string msg = "";
            Biluthyrning.RentCar(json.CarType, json.SSN, out msg);
            string[] msgs = msg.Split(' ');

            return new RentFormResponseVM { CarbookingId = int.Parse(msgs[10].Substring(0, 2)), RegNum = msgs[16], Status = "OK", CarType = json.CarType };
        }

        internal ReturnFormResponseVM MakeReturnFormResponseVM(ReturnFormSubmitVM json)
        {
            string msg = "";
            Biluthyrning.ReturnCar(json.CarbookingId, json.NewMilage, out msg);
            string[] msgs = msg.Split(' ');

            return new ReturnFormResponseVM { Status = "OK",TotalPrice=msgs[4].Substring(0,msgs[4].Length-2) };
        }
    }
}
