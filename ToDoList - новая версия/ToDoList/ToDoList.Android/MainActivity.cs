﻿using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Firebase;

namespace ToDoList.Droid
{
    [Activity(Label = "ToDoList", Icon = "@drawable/mainicon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
           
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
           
            base.OnCreate(savedInstanceState);
          
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            FirebaseApp.InitializeApp(Application.Context);
            Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
            
           
          
           
        }
    }
}