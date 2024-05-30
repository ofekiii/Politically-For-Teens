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
    class ThreadsAdapter:BaseAdapter<Threads>
    {
        Context context;
        List<Threads> threadsList;

        SQLiteConnection dbCommand;

        public ThreadsAdapter(Android.Content.Context context, System.Collections.Generic.List<Threads> sessionList)
        {
            this.context = context;
            this.threadsList = sessionList;
        }

        public List<Threads> GetList()
        {
            return this.threadsList;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override int Count
        {
            get { return this.threadsList.Count; }
        }

        public override Threads this[int position]
        {
            get { return this.threadsList[position]; }
        }


      
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Android.Views.LayoutInflater layoutInflater = ((ForumActivity)context).LayoutInflater;
            Android.Views.View view = layoutInflater.Inflate(Resource.Layout.threadListViewLayout, parent, false);
            TextView fNameListView = view.FindViewById<TextView>(Resource.Id.fNameListView);
            TextView threadListViewTitle = view.FindViewById<TextView>(Resource.Id.threadListViewTitle);

            dbCommand = new SQLiteConnection(Helper.Path());

            Threads temp = threadsList[position];

            if (temp != null)
            {
                var fName = dbCommand.Query<Users>("SELECT fName FROM Users WHERE email= '" + temp.userEmail + "'");
                var lName = dbCommand.Query<Users>("SELECT lName FROM Users WHERE email= '" + temp.userEmail + "'");
                var title = dbCommand.Query<Users>("SELECT title FROM Users WHERE email= '" + temp.userEmail + "'");

                fNameListView.Text = "" + fName + " " + lName;
                threadListViewTitle.Text = temp.title.ToString();
            }
            return view;
        }

    }
}