using System;
using System.Collections.Generic;
using System.Text;

namespace FriskaClient.Model
{
    public class KontrollSvar
    {
        public int ID { get; set; }

        public string UserID { get; set; }

        public int Kontroll { get; set; }

        public string KontrollTag { get; set; }

        public DateTime RegDate { get; set; }
        public bool Rattsvar { get; set; }

        public int YearID { get; set; }



    }
}
