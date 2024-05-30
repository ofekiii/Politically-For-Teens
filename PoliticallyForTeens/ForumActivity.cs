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
    [Activity(Label = "ForumActivity")]
    public class ForumActivity : Activity, ListView.IOnItemClickListener
    {
        public static List<Threads> threadsList { get; set; }
        ThreadsAdapter threadsAdapter;
        ListView lv;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            threadsList = new List<Threads>();

            SQLiteConnection dbCommand = new SQLiteConnection(Helper.Path());

            lv.OnItemClickListener = this;
            try
            {
                threadsList = dbCommand.Query<Threads>("SELECT * FROM Threads");
                threadsAdapter = new ThreadsAdapter(this, threadsList);
                lv.Adapter = threadsAdapter;
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Long).Show();
            }

        }

        public void OnItemClick(AdapterView parent, View view, int position, long id)
        {
            Intent intent = new Intent(this, typeof(threadActivity));

            Threads thread1 = threadsList[position];

            intent.PutExtra("id", thread1.id.ToString());
            StartActivity(intent);



        }

    }
}