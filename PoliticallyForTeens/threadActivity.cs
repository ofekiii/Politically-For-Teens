using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PoliticallyForTeens
{
    [Activity(Label = "threadActivity")]
    public class threadActivity : Activity
    {
        TextView threadTitle_TV, fname_TV;
        EditText comment_ET;
        Button postCommentBtn;
        RadioButton againstRB, proRB;
        SQLiteConnection dbCommand;
        private MenuFunctions menuFunctions;
        string email;
        bool pro;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            email = Helper.Sp().GetString("email", "");
            string threadId = Intent.GetStringExtra("threadId");
            threadTitle_TV = FindViewById<TextView>(Resource.Id.threadTitle_TV);
            fname_TV = FindViewById<TextView>(Resource.Id.fname_TV);
            comment_ET = FindViewById<EditText>(Resource.Id.comment_ET);
            postCommentBtn = FindViewById<Button>(Resource.Id.postCommentBtn);
            againstRB = FindViewById<RadioButton>(Resource.Id.againstRB);
            proRB = FindViewById<RadioButton>(Resource.Id.proRB);


            menuFunctions = new MenuFunctions(this);

            dbCommand = new SQLiteConnection(Helper.Path());


            postCommentBtn.Click += PostCommentBtn_Click;
        }

        private void PostCommentBtn_Click(object sender, EventArgs e)
        {
            //Initiates a new session with the inputted information;
            if (againstRB.Checked == true)
            {
                pro = false;
            }
            if (proRB.Checked == true)
            {
                pro = true;
            }
            Comments comment = new Comments(comment_ET.Text, email, System.DateTime.Now.ToString(), pro);

            try
            {
                var change = dbCommand.Insert(comment);
                if (change > 0)
                {
                    Toast.MakeText(this, "Session Created", ToastLength.Long).Show();
                }
                else
                {
                    Toast.MakeText(this, "Action Unsuccessful", ToastLength.Long).Show();
                }

            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }
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