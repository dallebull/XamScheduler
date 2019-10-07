using System;
using System.Collections.Generic;
using System.Text;

namespace XamScheduler.Model
{
    class Appointment
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime startDateTime { get; set; }
        public DateTime endDateTime { get; set; }
    }
}
