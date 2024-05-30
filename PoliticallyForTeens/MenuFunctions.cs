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
    public class MenuFunctions
    {
        private readonly Activity _activity;

        public MenuFunctions(Activity activity)
        {
            _activity = activity;
        }

        public bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menuHomePage:
                    OpenActivity(typeof(HomePageActivity));
                    return true;

                case Resource.Id.menuForumPage:
                    OpenActivity(typeof(ForumActivity));
                    return true;

                case Resource.Id.menuInformationPage:
                    // Add your code
                    return true;

                case Resource.Id.menuProfile:
                    OpenActivity(typeof(PersonalInfoActivity));
                    return true;

                case Resource.Id.menuLogout:
                    Logout();
                    return true;
            }

            return false;
        }

        public bool OnCreateOptionsMenu(IMenu menu)
        {
            _activity.MenuInflater.Inflate(Resource.Menu.mainMenu, menu);
            return true;
        }

        private void OpenActivity(System.Type activityType)
        {
            Intent intent = new Intent(_activity, activityType);
            _activity.StartActivity(intent);
        }

        private void Logout()
        {
            var editor = Helper.Sp().Edit();

            if (Helper.Sp().GetBoolean("rmbrmeCB", false))
            {
                editor.PutString("fname", "");
                editor.PutString("lname", "");
                editor.PutString("age", "");
                editor.PutInt("gender", 9);
                editor.PutString("picture", "");

                editor.Commit();
            }
            else
            {
                editor.PutString("email", "");
                editor.PutString("password", "");
                editor.PutString("fname", "");
                editor.PutString("lname", "");
                editor.PutString("age", "");
                editor.PutInt("gender", 9);
                editor.PutString("picture", "");

                editor.Commit();
            }

            OpenActivity(typeof(LoginActivity));
        }
    }
}