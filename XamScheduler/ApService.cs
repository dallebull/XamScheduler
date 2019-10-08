
using Newtonsoft.Json;
using Org.Apache.Http.Client.Methods;
using Org.Apache.Http.Client;
using Syncfusion.SfCalendar.XForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Xamarin.Forms;
using XamScheduler.Model;

namespace XamScheduler
{
    class ApService
    {
        List<Appointment> appointments;
        public List<Appointment> FillAps(string Auth)
        {
            Appointment data = new Appointment();
            Auth = "bearer " + Auth;
            HttpWebRequest apiRequest =
            WebRequest.Create("https://timebooking.azurewebsites.net/api/timebooking/") as HttpWebRequest;
            apiRequest.ContentType = "application/json";
            apiRequest.Headers.Add("Authorization: ", Auth);


            string apiResponse = "";
            using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                apiResponse = reader.ReadToEnd();
            }

            var ApList = JsonConvert.DeserializeObject<List<Appointment>>(apiResponse);

            return ApList;
        }

       
    }
}
