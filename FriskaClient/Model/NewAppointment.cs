﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FriskaClient.Model
{
    class NewAppointment
    {

        public string name { get; set; }
        public DateTime startDateTime { get; set; }
        public DateTime endDateTime { get; set; }
    }
}