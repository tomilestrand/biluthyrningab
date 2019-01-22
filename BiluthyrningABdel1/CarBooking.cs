using System;
using System.Collections.Generic;
using System.Text;

namespace BiluthyrningABdel1
{
    class CarBooking
    {
        public int Id { get; set; }
        public string SSN { get; set; }
        public int CarType { get; set; }
        public string CarRegistrationNumber { get; set; }
        public DateTime StartTime { get; set; }
        public int NumberOfKmStart { get; set; }
    }
}
