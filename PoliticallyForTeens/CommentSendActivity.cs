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

namespace PoliticallyForTeens
{
    [Activity(Label = "CommentSendActivity")]
    public class CommentSendActivity : Activity
    {

        private MenuFunctions menuFunctions;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CommentSendLayout);

            // Create your application here


            menuFunctions = new MenuFunctions(this);

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