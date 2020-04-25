using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace FriskaClient.iOS
{
    public class Application
    {
  
        static void Main(string[] args)
        {
            try
            {
                UIApplication.Main(args, null, "AppDelegate");
            }
            catch (Exception ex)
            {
               var myex = ex.ToString();
              
            } 
    
        }

    }
}
