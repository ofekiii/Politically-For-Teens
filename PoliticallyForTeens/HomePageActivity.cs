using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Support.V7.App;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace PoliticallyForTeens
{
    [Activity(Label = "HomePageActivity")]
    public class HomePageActivity : AppCompatActivity
    {
        SQLiteConnection dbcommand;
        TextView TwHello;
        TextView TwDate;
        Button btnToday;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.HomePageLayout);

            dbcommand = new SQLiteConnection(Helper.Path());
            TwHello = FindViewById<TextView>(Resource.Id.twHello);
            TwDate = FindViewById<TextView>(Resource.Id.twDate);
            btnToday = FindViewById<Button>(Resource.Id.btnToday);
            string userName = Helper.Sp().GetString("fname", null);
            TwHello.Text = "Hello " + userName;
            TwDate.Text = System.DateTime.Now.ToString();
            // Create your application here
        }
    }
}