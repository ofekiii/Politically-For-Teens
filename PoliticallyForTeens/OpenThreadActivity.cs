using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
namespace PoliticallyForTeens
{
    [Activity(Label = "OpenThreadActivity")]
    public class OpenThreadActivity : Activity
    {
        EditText threadTitle;
        EditText threadDescription;
        Button threadPublish;
        SQLiteConnection dbCommand;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.OpenThreadLayout);
            threadTitle = FindViewById<EditText>(Resource.Id.threadTitle);
            threadDescription = FindViewById<EditText>(Resource.Id.threadDescription);
            threadPublish = FindViewById<Button>(Resource.Id.threadPublish);

            threadPublish.Click += ThreadPublish_Click;

            dbCommand = new SQLiteConnection(Helper.Path());
        }

        private void ThreadPublish_Click(object sender, EventArgs e)
        {
            bool valid = true;
            if(threadTitle.Text.Length<5)
            {
                valid = false;
                Toast.MakeText(this, "כותרת קצרה מדי", ToastLength.Long).Show();
            }
            else
            { 
            Threads thread = new Threads(threadDescription.Text, Helper.Sp().GetString("email", ""), System.DateTime.Now.ToString(), 0);

            try
            { 
                var change = dbCommand.Insert(thread);
                if (change > 0)
                {
                    Toast.MakeText(this, "האשכול פורסם בהצלחה", ToastLength.Long).Show();
                    Intent intent = new Intent(this, typeof(ForumActivity));
                    StartActivity(intent);
                    }
                else
                {
                    Toast.MakeText(this, "ישנה בעיה בפרסום האשכול", ToastLength.Long).Show();
                }
            }
            catch
            {
                    Toast.MakeText(this, "ישנה בעיה בפרסום האשכול", ToastLength.Long).Show();
                }
            }
        }

    }
}