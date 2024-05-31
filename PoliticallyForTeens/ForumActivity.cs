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
        private MenuFunctions menuFunctions;
        SQLiteConnection dbCommand;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            threadsList = new List<Threads>();


            menuFunctions = new MenuFunctions(this);

            dbCommand = new SQLiteConnection(Helper.Path());

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

            intent.PutExtra("threadId", thread1.id.ToString());
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