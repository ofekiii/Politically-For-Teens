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
    class CommentsAdapter : BaseAdapter<Comments>
    {
        Context context;
        List<Comments> commentsList;

        SQLiteConnection dbCommand;

        public CommentsAdapter(Android.Content.Context context, System.Collections.Generic.List<Comments> commentsList)
        {
            this.context = context;
            this.commentsList = commentsList;
        }

        public List<Comments> GetList()
        {
            return this.commentsList;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override int Count
        {
            get { return this.commentsList.Count; }
        }

        public override Comments this[int position]
        {
            get { return this.commentsList[position]; }
        }



        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Android.Views.LayoutInflater layoutInflater = ((threadActivity)context).LayoutInflater;
            Android.Views.View view = layoutInflater.Inflate(Resource.Layout.CommentListViewLayout, parent, false);
            TextView commentorName = view.FindViewById<TextView>(Resource.Id.commenterName_TV);
            TextView commentDate_TV = view.FindViewById<TextView>(Resource.Id.commentDate_TV);
            TextView commentDescription = view.FindViewById<TextView>(Resource.Id.commentDescription);
            TextView commentId_TV = view.FindViewById<TextView>(Resource.Id.commentId_TV);
            RadioButton againstRB = view.FindViewById<RadioButton>(Resource.Id.againstRB);
            RadioButton proRB = view.FindViewById<RadioButton>(Resource.Id.proRB);
            ImageView thumbsUpImage = view.FindViewById<ImageView>(Resource.Id.thumbsUpImage);
            ImageView thumbsDownImage = view.FindViewById<ImageView>(Resource.Id.thumbsDownImage);

            dbCommand = new SQLiteConnection(Helper.Path());

            Comments temp = commentsList[position];

            if (temp != null)
            {
                var fName = dbCommand.Query<Users>("SELECT fName FROM Users WHERE email= '" + temp.userEmail + "'");
                var lName = dbCommand.Query<Users>("SELECT lName FROM Users WHERE email= '" + temp.userEmail + "'");

                commentorName.Text = "" + fName.ToString() + " " + lName.ToString();
                commentDescription.Text = temp.description.ToString();
                commentDate_TV.Text = System.DateTime.Now.ToString();
                commentId_TV.Text = temp.id.ToString();

                thumbsDownImage.Visibility = ViewStates.Invisible;
                thumbsUpImage.Visibility = ViewStates.Invisible;

                if (againstRB.Checked == true)
                {
                    thumbsDownImage.Visibility = ViewStates.Visible;
                    thumbsUpImage.Visibility = ViewStates.Invisible;
                }
                if(proRB.Checked == true)
                {
                    thumbsDownImage.Visibility = ViewStates.Invisible;
                    thumbsUpImage.Visibility = ViewStates.Visible;
                }
                


            }
            return view;
        }
    }
}