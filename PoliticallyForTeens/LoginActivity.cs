
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using SQLite;
using Android.Views;
using Android.Content;

namespace PoliticallyForTeens
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class LoginActivity : AppCompatActivity, Android.Views.View.IOnClickListener
    {
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
        }


        public void OnClick(View v)
        {
            throw new System.NotImplementedException();
        }


    }
}