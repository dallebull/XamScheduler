using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FriskaClient.Models
{
    public class Facit
    {
        public int ID { get; set; }

        public int Kontroll { get; set; }

        public string KontrollTag { get; set; }
        public int YearID { get; set; }
        public string QRURL { get; set; }
        public virtual ICollection<Year> Years { get; set; }


    }
}
