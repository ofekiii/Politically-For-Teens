using Android.App;
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
    [Activity(Label = "RightLeftInfoPageActivity")]
    public class RightLeftInfoPageActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RightLeftInfoPageLayout);

            // Create your application here
        }
    }
}