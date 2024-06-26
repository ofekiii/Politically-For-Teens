﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PoliticallyForTeens
{
    [Activity(Label = "InformationPage")]
    public class InformationPage : Activity
    {
        Button Parties;
        Button RaL;
        Button Au;

        private MenuFunctions menuFunctions;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.InformationPage);

            Parties = FindViewById<Button>(Resource.Id.btnP);
            RaL = FindViewById<Button>(Resource.Id.btnRL);
            Au = FindViewById<Button>(Resource.Id.btnAu);

            Parties.Click += Parties_Click;
            RaL.Click += RaL_Click;
            Au.Click += Au_Click;

            // Create your application here

            menuFunctions = new MenuFunctions(this);

        }

        private void Au_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(AboutUsPageActivity));
            StartActivity(intent);
        }

        private void RaL_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(RightLeftInfoPageActivity));
            StartActivity(intent);
        }

        private void Parties_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(PartiesInformationPageActivity));
            StartActivity(intent);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            return menuFunctions.OnOptionsItemSelected(item);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            return menuFunctions.OnCreateOptionsMenu(menu);
        }

    }
}