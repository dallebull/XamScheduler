﻿using FriskaClient.Model;
using FriskaClient.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Input;
using Xamarin.Forms;

namespace FriskaClient
{
    public partial class AdminPage : ContentPage
    {
        public string furl = App.url + "api/AdminApi/";

        public int YearId { get; set; }

        public AdminPage()
        {
          

            InitializeComponent();

            ToolbarItem item = new ToolbarItem
            {
                Text = App.User,


            };

            this.ToolbarItems.Add(item);
            item.Clicked += OnUserDetails;
        }


 
        public async void OnKontrollClicked(object sender, EventArgs args)
        {

        
            await Navigation.PushAsync(new FacitPage());
        }
        
 
        public async void OnYearClicked(object sender, EventArgs args)
        {
      
            await Navigation.PushAsync(new YearPage());
        }  
       
        async void OnUserDetails(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserDetails());
        }  
        async void OnAllUsersClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AllUsers());
        }

    }
}