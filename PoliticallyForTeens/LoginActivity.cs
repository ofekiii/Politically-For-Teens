
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
        SQLiteConnection dbCommand = new SQLiteConnection(Helper.Path());

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.LoginLayout);


            if(!Helper.Initialize(this))
            {
                Toast.MakeText(this, "Failed to Initialize", ToastLength.Long).Show();
            }
        }




        public void OnClick(View v)
        {
            throw new System.NotImplementedException();
        }


    }
}