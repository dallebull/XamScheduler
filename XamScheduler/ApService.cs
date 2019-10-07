
using Newtonsoft.Json;
using Syncfusion.SfCalendar.XForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Xamarin.Forms;
using XamScheduler.Model;

namespace XamScheduler
{
    class ApService
    {
        public List<Appointment> FillAps()
        {
            Appointment data = new Appointment();

            HttpWebRequest apiRequest =
            WebRequest.Create("https://timebookingapi.azurewebsites.net/api/timebooking") as HttpWebRequest;

            string apiResponse = "";
            using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                apiResponse = reader.ReadToEnd();
            }
            /*End*/

            var ApList = JsonConvert.DeserializeObject<List<Appointment>>(apiResponse);



            return ApList;
        }
    }
}
